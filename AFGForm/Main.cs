﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Threading;

namespace AFGForm
{
    public partial class Main : Form
    {
        private static Random random = new Random();
        private static readonly HttpClient client = new HttpClient();
        private Dictionary<long, Dictionary<string, List<string>>> dataURL;
        private int success = 0, fail = 0;
        private Thread thread = null;

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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;

            if (dataGridView1.CurrentCell.ColumnIndex == 4)
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
            
            bool randomAnswer = Convert.ToBoolean(dataGridView1.Rows[index].Cells[4].Value.ToString());
            
            QuestionForm fm = new QuestionForm(id, entry, question, currentAnswer, this.dataURL, randomAnswer);
            fm.ShowDialog();

            if (fm.isSuccess)
            {
                dataGridView1.Rows[index].Cells[0].Value = fm.getID();
                dataGridView1.Rows[index].Cells[1].Value = fm.getEntry();
                dataGridView1.Rows[index].Cells[2].Value = fm.getQuestion();
                dataGridView1.Rows[index].Cells[3].Value = fm.getCurrentAnswer();
                dataGridView1.Rows[index].Cells[4].Value = fm.getRandomAnswer();
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
            this.reloadResult();
        }

        private async void btnGetData_Click(object sender, EventArgs e)
        {
            if (tbURL.Text.Contains("/viewform"))
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
                        btnStart.Enabled = true;
                        btnRandomAll.Enabled = true;
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
            return;
        }

        private void loadDataGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 1;
            foreach (var s in this.dataURL)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = i;
                row.Cells[1].Value = s.Key;

                foreach (var ss in s.Value)
                {
                    row.Cells[2].Value = ss.Key;
                    foreach(string sss in ss.Value)
                    {
                        row.Cells[3].Value = sss;
                        break;
                    }
                    break;
                }

                row.Cells[4].Value = false;
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

            lbRUNNING.Text = "STOPPED";
            lbRUNNING.ForeColor = System.Drawing.Color.Red;
        }

        public string randomString(int length)
        {
            const string chars = " qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async void process()
        {
            this.buttonWhileRunning();

            this.success = 0;
            this.fail = 0;
            this.reloadResult();

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

                    string answer = "";
                    bool isRandomStr = false;

                    if (isRandom)
                    {
                        var q = this.dataURL[id][question];
                        if (q.Count > 0)
                        {
                            int rand = random.Next(0, q.Count);
                            answer = q[rand];

                            if (string.IsNullOrEmpty(answer))
                            {
                                answer = this.randomString(random.Next(5, 30));
                                isRandomStr = true;
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

                    if (isRandomStr)
                    {
                        values.Add(new KeyValuePair<string, string>("entry." + id.ToString() + ".other_option_response", answer));
                        values.Add(new KeyValuePair<string, string>("entry." + id.ToString(), "__other_option__"));
                    }
                    else
                    {
                        values.Add(new KeyValuePair<string, string>("entry." + id.ToString(), answer));
                    }
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

            MessageBox.Show("Done all!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.buttonWhileStopped();
            this.thread = null;
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

        private void numRepeat_TextChanged(object sender, EventArgs e)
        {
            this.reloadResult();
        }
    }
}
