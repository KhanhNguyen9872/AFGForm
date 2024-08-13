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
        private string question;
        private string currentAnswer;
        private bool randomAnswer;

        public QuestionForm(int id, long entry, string question, string currentAnswer, Dictionary<long, Dictionary<string, List<string>>> data, bool randomAnswer)
        {
            InitializeComponent();
            this.id = id;
            this.entry = entry;
            this.question = question;
            this.currentAnswer = currentAnswer;
            this.data = data;
            this.randomAnswer = randomAnswer;
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            this.Text = "ID: " + this.id.ToString() + "  |  Entry: " + this.entry.ToString(); 
            tbID.Text = this.id.ToString();
            tbEntry.Text = this.entry.ToString();
            tbQuestion.Text = this.question;
            cbRandom.Checked = this.randomAnswer;

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

            if (cbAnswer.Items.Count > 0) {
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
            } else
            {
                cbAnswer.DropDownStyle = ComboBoxStyle.DropDown;
                cbAnswer.Text = this.currentAnswer;
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
            return cbAnswer.Text;
        }

        public bool getRandomAnswer()
        {
            return cbRandom.Checked;
        }

        private void cbRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRandom.Checked)
            {
                cbAnswer.Enabled = false;
            } else
            {
                cbAnswer.Enabled = true;
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
