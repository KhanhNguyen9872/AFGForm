using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Threading;
using System.Security.Principal;

namespace AFGForm
{
    public partial class Main : Form
    {
        private static Random random = new Random();
        private static readonly HttpClient client = new HttpClient();
        private Dictionary<long, Dictionary<string, List<string>>> dataURL;
        private int success = 0, fail = 0;
        private Thread thread = null;
        private bool autoEmail = false;

        public Main()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            numFrom.TextChanged += new EventHandler(numFrom_TextChanged);
            numTo.TextChanged += new EventHandler(numTo_TextChanged);
            numRepeat.TextChanged += new EventHandler(numRepeat_TextChanged);
            
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36");
            this.dataURL = new Dictionary<long, Dictionary<string, List<string>>>();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.tbURL.Text = "https://docs.google.com/forms/d/e/1FAIpQLSeBshz5tDbwSB1I7cM_sk8SMjXsWo5qh3O7hB7SgIncqwRTKQ/viewform?usp=sf_link";
            this.reloadResult();
            this.buttonWhileStopped();
            btnStart.Enabled = false;
            btnRandomAll.Enabled = false;
            btnIgnoreOtherAll.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;

            bool randomAnswer = Convert.ToBoolean(dataGridView1.Rows[index].Cells[4].Value.ToString());
            bool ignoreOther = Convert.ToBoolean(dataGridView1.Rows[index].Cells[5].Value.ToString());

            if (!randomAnswer)
            {
                dataGridView1.Rows[index].Cells[5].Value = false;
                ignoreOther = false;
            }

            if ((dataGridView1.CurrentCell.ColumnIndex == 4) || (dataGridView1.CurrentCell.ColumnIndex == 5))
            {
                
                return;
            }

            int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
            long entry = Convert.ToInt64(dataGridView1.Rows[index].Cells[1].Value.ToString());
            string question = dataGridView1.Rows[index].Cells[2].Value.ToString();
            var curAnswer = dataGridView1.Rows[index].Cells[3].Value;
            string currentAnswer;
            if (curAnswer == null)
            {
                currentAnswer = null;
            } else
            {
                currentAnswer = curAnswer.ToString();
            }

            QuestionForm fm = new QuestionForm(id, entry, question, currentAnswer, this.dataURL, randomAnswer, ignoreOther);
            fm.ShowDialog();

            if (fm.isSuccess)
            {
                dataGridView1.Rows[index].Cells[0].Value = fm.getID();
                dataGridView1.Rows[index].Cells[1].Value = fm.getEntry();
                dataGridView1.Rows[index].Cells[2].Value = fm.getQuestion();
                dataGridView1.Rows[index].Cells[3].Value = fm.getCurrentAnswer();
                dataGridView1.Rows[index].Cells[4].Value = fm.getRandomAnswer();
                dataGridView1.Rows[index].Cells[5].Value = fm.getCbIgnore();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tbURL.Text = "";
            numRepeat.Value = 1;
            numSec.Value = 1;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            tbURL.ReadOnly = false;
            this.autoEmail = false;
            this.reloadResult();
        }

        private async void btnGetData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbURL.Text))
            {
                MessageBox.Show("URL is empty!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!tbURL.Text.Contains("https://"))
            {
                tbURL.Text = "https://" + tbURL.Text;
            }

            if (tbURL.Text.Contains("http://"))
            {
                tbURL.Text = tbURL.Text.Replace("http://", "https://");
            }

            LoadingForm fm = new LoadingForm();
            fm.Show();

            if (tbURL.Text.Contains("https://forms.gle/"))
            {
                HttpResponseMessage response;
                try
                {
                    response = await client.GetAsync(tbURL.Text);
                    string url = response.RequestMessage.RequestUri.AbsoluteUri.ToString();
                    if (string.IsNullOrEmpty(url))
                    {
                        MessageBox.Show("Cannot get form URL!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fm.Dispose();
                        return;
                    }

                    tbURL.Text = url;
                } catch (Exception ex)
                {
                    MessageBox.Show("Network error!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fm.Dispose();
                    return;
                }
                
            }

            if ((tbURL.Text.Contains("https://docs.google.com/forms/d/e/")) && (tbURL.Text.Contains("/viewform")))
            {
                tbURL.Text = tbURL.Text.Split('?')[0];

                var response = await client.GetStringAsync(tbURL.Text);

                string pattern = @"var FB_PUBLIC_LOAD_DATA_ = (.*?);<\/script>";
                Match match = Regex.Match(response, pattern);

                if (match.Success)
                {
                    string responseText = match.Groups[1].Value;

                    var jsonDocument = JsonDocument.Parse(responseText);
                    var rootElement = jsonDocument.RootElement;

                    var resultList = new List<string>();
                    this.loadData(rootElement, resultList);
                    this.loadDataGrid();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        if (response.Contains("<input type=\"email\" class=\""))
                        {
                            this.autoEmail = true;
                        } else
                        {
                            this.autoEmail = false;
                        }
                        btnStart.Enabled = true;
                        btnRandomAll.Enabled = true;
                        btnIgnoreOtherAll.Enabled = true;
                        tbURL.ReadOnly = true;
                    } else
                    {
                        btnStart.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Cannot get data from this URL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Wrong URL (a URL must have /viewform)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fm.Dispose();
            return;
        }

        private void loadDataGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 1;
            foreach (var s in this.dataURL)
            {
                bool isEnableIgnore = false;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = i;
                row.Cells[1].Value = s.Key;

                foreach (var ss in s.Value)
                {
                    row.Cells[2].Value = ss.Key;
                    row.Cells[3].Value = null;
                    foreach(string sss in ss.Value)
                    {
                        if (row.Cells[3].Value == null)
                        {
                            row.Cells[3].Value = sss;
                        }

                        if ((string.IsNullOrEmpty(sss)) || sss.Equals(" "))
                        {
                            isEnableIgnore = true;
                            break;
                        }
                    }
                    break;
                }

                row.Cells[4].Value = false;
                row.Cells[5].Value = false;

                if (!isEnableIgnore)
                {
                    row.Cells[5].ReadOnly = true;
                }
                dataGridView1.Rows.Add(row);
                i = i + 1;
            }

            if  (this.dataURL.Count > 0)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            } else
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void loadData(JsonElement element, List<string> resultList)
        {
            this.dataURL.Clear();
            var i = element.EnumerateArray();
            int count = 0;
            foreach (var item in i)
            {
                if (count == 1)
                {
                    i = item.EnumerateArray();
                    break;
                }
                count = count + 1;
            }

            count = 0;
            foreach (var item in i)
            {
                if (count == 1)
                {
                    i = item.EnumerateArray();
                    break;
                }
                count = count + 1;
            }

            foreach (var item in i)
            {
                var ques = item.EnumerateArray();
                int c = 0;
                long id = 0;
                string question = "";
                List<string> answer = null;
                foreach(var q in ques)
                {
                    if (c == 1)
                    {
                        question = q.GetString();
                    }

                    if (c == 4)
                    {
                        if (q.ValueKind == JsonValueKind.Null)
                        {
                            break;
                        }
                        var answ = q.EnumerateArray();
                        foreach(var ans in answ)
                        {
                            int countAnswer = 0;
                            foreach(var an in ans.EnumerateArray())
                            {
                                if (countAnswer == 0)
                                {
                                    id = an.GetInt64();
                                }

                                if (countAnswer == 1)
                                {
                                    answer = new List<string>();
                                    if (an.ValueKind == JsonValueKind.Null)
                                    {
                                        break;
                                    }

                                    foreach (var an2 in an.EnumerateArray())
                                    {
                                        foreach(var an3 in an2.EnumerateArray())
                                        {
                                            answer.Add(an3.ToString());
                                            break;
                                        }
                                    }
                                    break;
                                }

                                countAnswer += 1;
                            }
                        }

                        this.dataURL[id] = new Dictionary<string, List<string>>();
                        this.dataURL[id].Add(question, answer);
                        id = 0;
                        question = "";
                        answer = null;
                    }

                    c += 1;
                }
            }
        }

        private void buttonWhileRunning()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            cbRandomTime.Enabled = false;
            numFrom.Enabled = false;
            numTo.Enabled = false;
            numSec.Enabled = false;
            numRepeat.Enabled = false;
            btnReset.Enabled = false;
            btnGetData.Enabled = false;
            tbURL.ReadOnly = true;
            dataGridView1.Enabled = false;
            btnRandomAll.Enabled = false;
            btnIgnoreOtherAll.Enabled = false;

            lbRUNNING.Text = "RUNNING";
            lbRUNNING.ForeColor = System.Drawing.Color.Green;
        }

        private void buttonWhileStopped()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            cbRandomTime.Enabled = true;
            numFrom.Enabled = true;
            numTo.Enabled = true;
            numSec.Enabled = true;
            numRepeat.Enabled = true;
            btnReset.Enabled = true;
            btnGetData.Enabled = true;
            tbURL.ReadOnly = false;
            dataGridView1.Enabled = true;
            btnRandomAll.Enabled = true;
            btnIgnoreOtherAll.Enabled = true;

            lbRUNNING.Text = "STOPPED";
            lbRUNNING.ForeColor = System.Drawing.Color.Red;
        }

        public string randomString(int length)
        {
            return this.randomString(length, false);
        }

        public string randomString(int length, bool ignoreSpace)
        {
            string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            if (!ignoreSpace)
            {
                chars = chars + " ";
            }
            
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async void process()
        {
            this.buttonWhileRunning();

            this.success = 0;
            this.fail = 0;
            this.reloadResult();
            bool ignoreBadRequest = false;
            
            string url = tbURL.Text.Replace("viewform", "formResponse");

            for (int i = 0; i < this.numRepeat.Value; i++)
            {
                if (this.thread == null)
                {
                    return;
                }

                var values = new List<KeyValuePair<string, string>>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    long id = Convert.ToInt64(row.Cells[1].Value.ToString());
                    string question = row.Cells[2].Value.ToString();
                    var data = row.Cells[3].Value;
                    bool isRandom = Convert.ToBoolean(row.Cells[4].Value);
                    bool isIgnore = Convert.ToBoolean(row.Cells[5].Value);
                    bool isIgnoreReadOnly = row.Cells[5].ReadOnly;

                    string answer = "";
                    bool isRandomStr = false;

                    var q = this.dataURL[id][question];
                    if (isRandom)
                    {
                        if (q.Count > 0)
                        {
                            if (isIgnore)
                            {
                                do
                                {
                                    int rand = random.Next(0, q.Count);
                                    answer = q[rand];
                                } while (string.IsNullOrEmpty(answer));
                            } else
                            {
                                int rand = random.Next(0, q.Count);
                                answer = q[rand];

                                if (string.IsNullOrEmpty(answer))
                                {
                                    answer = this.randomString(random.Next(5, 30));
                                    isRandomStr = true;
                                }
                            }
                        }
                        else
                        {
                            answer = this.randomString(random.Next(5, 30));
                        }
                    }
                    else
                    {
                        if (data == null)
                        {
                            answer = "";
                        }
                        else
                        {
                            answer = data.ToString();
                        }
                    }

                    if (!isRandomStr)
                    {
                        if (notIn(q, answer))
                        {
                            isRandomStr = true;
                        }
                    }

                    if ((isRandomStr) && (!isIgnoreReadOnly))
                    {
                        values.Add(new KeyValuePair<string, string>("entry." + id.ToString() + ".other_option_response", answer));
                        values.Add(new KeyValuePair<string, string>("entry." + id.ToString(), "__other_option__"));
                    }
                    else
                    {
                        values.Add(new KeyValuePair<string, string>("entry." + id.ToString(), answer));
                    }
                    values.Add(new KeyValuePair<string, string>("entry." + id.ToString() + "_sentinel", ""));
                }

                if (this.autoEmail)
                {
                    values.Add(new KeyValuePair<string, string>("emailAddress", this.randomString(16, true) + "@gmail.com"));
                }

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    this.success += 1;
                }
                else
                {
                    this.fail += 1;

                    if ((response.StatusCode == System.Net.HttpStatusCode.BadRequest) && (!ignoreBadRequest))
                    {
                        var p = MessageBox.Show("Bad request at " + i.ToString() + "\nMaybe wrong info when submit\nPress Abort -> Stop process\nPress Retry -> Continue process\nPress Ignore -> Ignore all bad request popup", "BAD REQUEST", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
                        if (p == DialogResult.Abort)
                        {
                            this.reloadResult();
                            this.buttonWhileStopped();
                            this.thread = null;
                            return;
                        }

                        if (p == DialogResult.Ignore)
                        {
                            ignoreBadRequest = true;
                        }

                        if (p == DialogResult.Retry)
                        {

                        }
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        this.reloadResult();
                        this.buttonWhileStopped();
                        this.thread = null;
                        MessageBox.Show("This form requires login!", "UNAUTHORIZED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                this.reloadResult();

                if (cbRandomTime.Checked)
                {
                    int num = random.Next((int)numFrom.Value, ((int)numTo.Value) + 1);

                    this.sleep((int)num);
                } else
                {
                    this.sleep((int)numSec.Value);
                }
            }

            this.buttonWhileStopped();
            this.thread = null;
            MessageBox.Show("Completed all jobs!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool notIn(List<string> q, string answer)
        {
            bool check = true;
            foreach(string s in q)
            {
                if (answer.Equals(s))
                {
                    check = false;
                    break;
                }
            }

            return check;
        }

        private void sleep(int seconds)
        {
            for(int sec = seconds; sec >= 0; sec--)
            {
                this.reloadResult(sec);
                Thread.Sleep(1000);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if ((row.Cells[3].Value == null) || (string.IsNullOrEmpty(row.Cells[3].Value.ToString())))
                {
                    if (!Convert.ToBoolean(row.Cells[4].Value.ToString()))
                    {
                        if (MessageBox.Show("Found empty answer from [ID: " + row.Cells[0].Value.ToString() + "]\nDo you want to continue?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            break;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            this.thread = new Thread(this.process);
            this.thread.IsBackground = true;
            this.thread.Start();
        }

        private void reloadResult()
        {
            this.reloadResult(0);
        }

        private void reloadResult(int sec)
        {
            this.lbStatus.Text = "|     Total: " + this.numRepeat.Value.ToString() + "     |     Success: " + this.success.ToString() + "     |     Failed:  " + this.fail.ToString() + "     |     Waiting: " + sec.ToString() + " s     |";
        }

        private void cbRandomTime_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRandomTime.Checked)
            {
                labelPerSec.Visible = false;
                numSec.Visible = false;

                labelFrom.Visible = true;
                numFrom.Visible = true;
                labelTo.Visible = true;
                numTo.Visible = true;
            } else
            {
                labelPerSec.Visible = true;
                numSec.Visible = true;

                labelFrom.Visible = false;
                numFrom.Visible = false;
                labelTo.Visible = false;
                numTo.Visible = false;
            }
        }

        private void btnRandomAll_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[4].Value = true;
                }
            }
        }

        private void numFrom_TextChanged(object sender, EventArgs e)
        {
            this.numTo_TextChanged(sender, e);
        }

        private void numTo_TextChanged(object sender, EventArgs e)
        {
            if (numFrom.Value > numTo.Value)
            {
                numTo.Value = numFrom.Value + 1;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.thread == null)
            {
                return;
            }

            this.thread.Abort();
            this.thread = null;
            this.buttonWhileStopped();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "EXIT PROGRAM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                return;
            }
            e.Cancel = true;
        }

        private void btnIgnoreOtherAll_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[5].ReadOnly)
                    {
                        continue;
                    }
                    row.Cells[5].Value = true;
                }
            }
        }

        private void numRepeat_TextChanged(object sender, EventArgs e)
        {
            this.reloadResult();
        }
    }
}
