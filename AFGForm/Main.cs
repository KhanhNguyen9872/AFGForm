using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AFGForm
{
    public partial class Main : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private Dictionary<long, Dictionary<string, List<string>>> dataURL;
        private int success = 0, fail = 0;

        public Main()
        {
            InitializeComponent();
            numRepeat.TextChanged += new EventHandler(numRepeat_TextChanged);
            
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36");
            this.dataURL = new Dictionary<long, Dictionary<string, List<string>>>();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.tbURL.Text = "https://docs.google.com/forms/d/e/1FAIpQLSeBshz5tDbwSB1I7cM_sk8SMjXsWo5qh3O7hB7SgIncqwRTKQ/viewform?usp=sf_link";
            this.reloadResult();
            this.buttonWhileStopped();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tbURL.Text = "";
            numRepeat.Value = 1;
            numSec.Value = 1;
            this.reloadResult();
        }

        private async void btnGetData_Click(object sender, EventArgs e)
        {
            if (tbURL.Text.Contains("/viewform"))
            {
                tbURL.Text = tbURL.Text.Split('?')[0];

                var response = await client.GetStringAsync(tbURL.Text);

                /*
                string pattern = @"<title>(.*?)<\/title>";
                Match match = Regex.Match(response, pattern, RegexOptions.IgnoreCase);
                string titleURL = "";

                if (match.Success)
                {
                    titleURL = match.Groups[1].Value;
                }
                else
                {
                    MessageBox.Show("Cannot get title from this URL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                */

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
            int i = 1;
            foreach (var s in this.dataURL)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = i;
                row.Cells[1].Value = s.Key;

                DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                foreach (var ss in s.Value)
                {
                    row.Cells[2].Value = ss.Key;
                    foreach(string sss in ss.Value)
                    {
                        comboBoxCell.Items.Add(sss);
                    }
                    break;
                }
                if (comboBoxCell.Items.Count > 0)
                {
                    comboBoxCell.Value = comboBoxCell.Items[0];
                }
                row.Cells[3] = comboBoxCell;
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
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            this.buttonWhileRunning();

            this.success = 0;
            this.fail = 0;
            this.reloadResult();

            var values = new List<KeyValuePair<string, string>>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var id = row.Cells[1].Value;
                var data = row.Cells[3].Value;
                values.Add(new KeyValuePair<string, string>("entry." + id.ToString(), data.ToString()));
            }

            string url = tbURL.Text.Replace("viewform", "formResponse");

            for (int i = 0; i < this.numRepeat.Value; i++)
            {
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
            }

            MessageBox.Show("Done all!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.buttonWhileStopped();
        }

        private void reloadResult()
        {
            this.resultSuccess.Text = "Success: " + this.success.ToString();
            this.resultFail.Text = "Fail: " + this.fail.ToString();
            this.resultTotal.Text = "Total: " + this.numRepeat.Value.ToString();
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

        private void numRepeat_TextChanged(object sender, EventArgs e)
        {
            this.reloadResult();
        }
    }
}
