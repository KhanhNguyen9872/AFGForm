using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AFGForm
{
    public partial class QuestionForm : Form
    {
        public bool isSuccess = false;
        private Dictionary<long, Dictionary<string, List<string>>> data;
        private int id;
        private long entry;
        private string type;
        private string question;
        private string currentAnswer;
        private bool randomAnswer;
        private bool ignoreOther;
        private bool d_IgnoreOther = false; 

        public QuestionForm(int id, long entry, string type, string question, string currentAnswer, Dictionary<long, Dictionary<string, List<string>>> data, bool randomAnswer, bool ignoreOther)
        {
            InitializeComponent();
            this.id = id;
            this.entry = entry;
            this.question = question;
            this.currentAnswer = currentAnswer;
            this.data = data;
            this.randomAnswer = randomAnswer;
            this.ignoreOther = ignoreOther;
            this.type = type;
        }

        public void disableIgnoreOther(bool a)
        {
            if (a)
            {
                cbIgnore.Enabled = false;
                this.d_IgnoreOther = true;
            } else
            {
                if (cbRandom.Checked)
                {
                    cbIgnore.Enabled = true;
                }
                this.d_IgnoreOther = false;
            }
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            this.Text = "ID: " + this.id.ToString() + "  |  Entry: " + this.entry.ToString(); 
            tbID.Text = this.id.ToString();
            tbEntry.Text = this.entry.ToString();
            tbQuestion.Text = this.question;
            cbRandom.Checked = this.randomAnswer;
            cbIgnore.Checked = this.ignoreOther;

            if (type.Equals("Multi select"))
            {
                this.Height = this.Height + 90;
                System.Drawing.Point p;

                p = button1.Location;
                p.Y = p.Y + 120;
                button1.Location = p;

                p = button2.Location;
                p.Y = p.Y + 120;
                button2.Location = p;

                System.Drawing.Size s;
                s = dataGridView1.Size;
                s.Height = s.Height + 110;
                dataGridView1.Size = s;

                s = tbOtherOption.Size;
                s.Height = s.Height + 110;
                tbOtherOption.Size = s;

                lbOtherOption.Visible = true;
                tbOtherOption.Visible = true;
                cbAnswer.Visible = false;
                dataGridView1.Visible = true;

                dataGridView1.Rows.Clear();

                DataGridViewRow row;
                foreach (string ss in this.data[this.entry][this.question])
                {
                    row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[1].Value = false;

                    if ((string.IsNullOrEmpty(ss)) || ss.Equals(" "))
                    {
                        tbOtherOption.ReadOnly = false;
                        row.Cells[0].Value = "Other option";

                        string[] lst = this.currentAnswer.Split((char)0);
                        string c = lst[lst.Length - 1];

                        foreach (string sss in this.data[this.entry][this.question])
                        {
                            if (c.Equals(sss))
                            {
                                c = null;
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(c))
                        {
                            tbOtherOption.Text = c;
                            row.Cells[1].Value = true;
                        }

                        dataGridView1.Rows.Add(row);
                        continue;
                    }

                    row.Cells[0].Value = ss;
                    foreach(string c in this.currentAnswer.Split((char)0))
                    {
                        if (ss.Equals(c))
                        {
                            row.Cells[1].Value = true;
                            break;
                        }
                    }
                    dataGridView1.Rows.Add(row);
                }
            } else
            {
                this.Height = this.Height - 30;

                cbAnswer.Items.Clear();

                foreach (string s in this.data[this.entry][this.question])
                {
                    if ((string.IsNullOrEmpty(s)) || s.Equals(" "))
                    {
                        cbAnswer.DropDownStyle = ComboBoxStyle.DropDown;
                        continue;
                    }

                    cbAnswer.Items.Add(s);
                }

                if (cbAnswer.Items.Count > 0)
                {
                    int index = -1;
                    int i = 0;
                    foreach (string s in cbAnswer.Items)
                    {
                        if (s.Equals(this.currentAnswer))
                        {
                            index = i;
                            break;
                        }
                        i = i + 1;
                    }

                    cbAnswer.SelectedIndex = index;
                }
                else
                {
                    cbAnswer.DropDownStyle = ComboBoxStyle.DropDown;
                    cbAnswer.Text = this.currentAnswer;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.isSuccess = true;
            this.Close();
        }

        public int getID()
        {
            return Convert.ToInt32(tbID.Text);
        }

        public long getEntry()
        {
            return Convert.ToInt64(tbEntry.Text);
        }

        public string getQuestion()
        {
            return tbQuestion.Text;
        }

        public string getCurrentAnswer()
        {
            if (this.type.Equals("Multi select"))
            {
                string fullTxt = "";
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    string txt = row.Cells[0].Value.ToString();
                    bool isSelected = Convert.ToBoolean(row.Cells[1].Value);

                    if (isSelected)
                    {
                        if (!string.IsNullOrEmpty(fullTxt))
                        {
                            fullTxt = fullTxt + (char)0;
                        }

                        if ((txt.Equals("Other option")) && (row == dataGridView1.Rows[dataGridView1.Rows.Count - 1]))
                        {
                            txt = tbOtherOption.Text;
                        }

                        fullTxt = fullTxt + txt;
                    }
                }

                return fullTxt;
            }
            return cbAnswer.Text;
        }

        public bool getRandomAnswer()
        {
            return cbRandom.Checked;
        }

        public bool getCbIgnore()
        {
            return cbIgnore.Checked;
        }

        private void cbRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRandom.Checked)
            {
                cbAnswer.Enabled = false;
                dataGridView1.Enabled = false;
                tbOtherOption.Enabled = false;
                if (!this.d_IgnoreOther)
                {
                    cbIgnore.Enabled = true;
                }
            } else
            {
                cbAnswer.Enabled = true;
                dataGridView1.Enabled = true;
                tbOtherOption.Enabled = true;
                if (!this.d_IgnoreOther)
                {
                    cbIgnore.Enabled = false;
                    cbIgnore.Checked = false;
                }
            }
        }

        private void cbAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.QuestionForm_KeyPress(sender, e);
        }

        private void QuestionForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.button2_Click(sender, e);
            }

            if (e.KeyChar == (char)27)
            {
                this.button1_Click(sender, e);
            }
        }
    }
}
