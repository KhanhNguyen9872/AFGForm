namespace AFGForm
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numTo = new System.Windows.Forms.NumericUpDown();
            this.labelTo = new System.Windows.Forms.Label();
            this.numFrom = new System.Windows.Forms.NumericUpDown();
            this.labelFrom = new System.Windows.Forms.Label();
            this.cbRandomTime = new System.Windows.Forms.CheckBox();
            this.numRepeat = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.numSec = new System.Windows.Forms.NumericUpDown();
            this.labelPerSec = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.btnRandomAll = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbRUNNING = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Question = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Answer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RandomAnswer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IgnoreOther = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnIgnoreOtherAll = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSec)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.numTo);
            this.groupBox1.Controls.Add(this.labelTo);
            this.groupBox1.Controls.Add(this.numFrom);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.labelFrom);
            this.groupBox1.Controls.Add(this.cbRandomTime);
            this.groupBox1.Controls.Add(this.numRepeat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Controls.Add(this.numSec);
            this.groupBox1.Controls.Add(this.labelPerSec);
            this.groupBox1.Controls.Add(this.tbURL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // numTo
            // 
            resources.ApplyResources(this.numTo, "numTo");
            this.numTo.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numTo.Name = "numTo";
            this.numTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelTo
            // 
            resources.ApplyResources(this.labelTo, "labelTo");
            this.labelTo.Name = "labelTo";
            // 
            // numFrom
            // 
            resources.ApplyResources(this.numFrom, "numFrom");
            this.numFrom.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numFrom.Name = "numFrom";
            // 
            // labelFrom
            // 
            resources.ApplyResources(this.labelFrom, "labelFrom");
            this.labelFrom.Name = "labelFrom";
            // 
            // cbRandomTime
            // 
            resources.ApplyResources(this.cbRandomTime, "cbRandomTime");
            this.cbRandomTime.Name = "cbRandomTime";
            this.cbRandomTime.UseVisualStyleBackColor = true;
            this.cbRandomTime.CheckedChanged += new System.EventHandler(this.cbRandomTime_CheckedChanged);
            // 
            // numRepeat
            // 
            resources.ApplyResources(this.numRepeat, "numRepeat");
            this.numRepeat.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRepeat.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRepeat.Name = "numRepeat";
            this.numRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnGetData
            // 
            resources.ApplyResources(this.btnGetData, "btnGetData");
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // numSec
            // 
            resources.ApplyResources(this.numSec, "numSec");
            this.numSec.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numSec.Name = "numSec";
            // 
            // labelPerSec
            // 
            resources.ApplyResources(this.labelPerSec, "labelPerSec");
            this.labelPerSec.Name = "labelPerSec";
            // 
            // tbURL
            // 
            resources.ApplyResources(this.tbURL, "tbURL");
            this.tbURL.Name = "tbURL";
            // 
            // btnRandomAll
            // 
            resources.ApplyResources(this.btnRandomAll, "btnRandomAll");
            this.btnRandomAll.Name = "btnRandomAll";
            this.btnRandomAll.UseVisualStyleBackColor = true;
            this.btnRandomAll.Click += new System.EventHandler(this.btnRandomAll_Click);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Entry,
            this.Question,
            this.Answer,
            this.RandomAnswer,
            this.IgnoreOther});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.btnIgnoreOtherAll);
            this.groupBox4.Controls.Add(this.btnRandomAll);
            this.groupBox4.Controls.Add(this.btnStart);
            this.groupBox4.Controls.Add(this.btnStop);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbRUNNING);
            this.panel1.Name = "panel1";
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.Name = "lbStatus";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lbRUNNING
            // 
            resources.ApplyResources(this.lbRUNNING, "lbRUNNING");
            this.lbRUNNING.ForeColor = System.Drawing.Color.Red;
            this.lbRUNNING.Name = "lbRUNNING";
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Entry
            // 
            resources.ApplyResources(this.Entry, "Entry");
            this.Entry.Name = "Entry";
            this.Entry.ReadOnly = true;
            // 
            // Question
            // 
            resources.ApplyResources(this.Question, "Question");
            this.Question.Name = "Question";
            this.Question.ReadOnly = true;
            // 
            // Answer
            // 
            resources.ApplyResources(this.Answer, "Answer");
            this.Answer.Name = "Answer";
            this.Answer.ReadOnly = true;
            // 
            // RandomAnswer
            // 
            resources.ApplyResources(this.RandomAnswer, "RandomAnswer");
            this.RandomAnswer.Name = "RandomAnswer";
            // 
            // IgnoreOther
            // 
            resources.ApplyResources(this.IgnoreOther, "IgnoreOther");
            this.IgnoreOther.Name = "IgnoreOther";
            this.IgnoreOther.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IgnoreOther.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnIgnoreOtherAll
            // 
            resources.ApplyResources(this.btnIgnoreOtherAll, "btnIgnoreOtherAll");
            this.btnIgnoreOtherAll.Name = "btnIgnoreOtherAll";
            this.btnIgnoreOtherAll.UseVisualStyleBackColor = true;
            this.btnIgnoreOtherAll.Click += new System.EventHandler(this.btnIgnoreOtherAll_Click);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSec)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown numSec;
        private System.Windows.Forms.Label labelPerSec;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.NumericUpDown numRepeat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTo;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.NumericUpDown numFrom;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.CheckBox cbRandomTime;
        private System.Windows.Forms.Button btnRandomAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbRUNNING;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Question;
        private System.Windows.Forms.DataGridViewTextBoxColumn Answer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RandomAnswer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IgnoreOther;
        private System.Windows.Forms.Button btnIgnoreOtherAll;
    }
}

