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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.numSec = new System.Windows.Forms.NumericUpDown();
            this.labelPerSec = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resultFail = new System.Windows.Forms.Label();
            this.resultTotal = new System.Windows.Forms.Label();
            this.resultSuccess = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Question = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Answer = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RandomAnswer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.numRepeat = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRandomTime = new System.Windows.Forms.CheckBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.numFrom = new System.Windows.Forms.NumericUpDown();
            this.numTo = new System.Windows.Forms.NumericUpDown();
            this.labelTo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSec)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numTo);
            this.groupBox1.Controls.Add(this.labelTo);
            this.groupBox1.Controls.Add(this.numFrom);
            this.groupBox1.Controls.Add(this.labelFrom);
            this.groupBox1.Controls.Add(this.cbRandomTime);
            this.groupBox1.Controls.Add(this.numRepeat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Controls.Add(this.numSec);
            this.groupBox1.Controls.Add(this.labelPerSec);
            this.groupBox1.Controls.Add(this.tbURL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 116);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(556, 58);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(556, 22);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 23);
            this.btnGetData.TabIndex = 5;
            this.btnGetData.Text = "Get data";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // numSec
            // 
            this.numSec.Location = new System.Drawing.Point(113, 60);
            this.numSec.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numSec.Name = "numSec";
            this.numSec.Size = new System.Drawing.Size(64, 20);
            this.numSec.TabIndex = 1;
            this.numSec.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelPerSec
            // 
            this.labelPerSec.AutoSize = true;
            this.labelPerSec.Location = new System.Drawing.Point(15, 62);
            this.labelPerSec.Name = "labelPerSec";
            this.labelPerSec.Size = new System.Drawing.Size(92, 13);
            this.labelPerSec.TabIndex = 0;
            this.labelPerSec.Text = "Once per second:";
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(53, 22);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(486, 20);
            this.tbURL.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resultFail);
            this.groupBox2.Controls.Add(this.resultTotal);
            this.groupBox2.Controls.Add(this.resultSuccess);
            this.groupBox2.Location = new System.Drawing.Point(677, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 51);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // resultFail
            // 
            this.resultFail.AutoSize = true;
            this.resultFail.Location = new System.Drawing.Point(117, 22);
            this.resultFail.Name = "resultFail";
            this.resultFail.Size = new System.Drawing.Size(35, 13);
            this.resultFail.TabIndex = 2;
            this.resultFail.Text = "Fail: 0";
            // 
            // resultTotal
            // 
            this.resultTotal.AutoSize = true;
            this.resultTotal.Location = new System.Drawing.Point(203, 22);
            this.resultTotal.Name = "resultTotal";
            this.resultTotal.Size = new System.Drawing.Size(43, 13);
            this.resultTotal.TabIndex = 1;
            this.resultTotal.Text = "Total: 0";
            this.resultTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // resultSuccess
            // 
            this.resultSuccess.AutoSize = true;
            this.resultSuccess.Location = new System.Drawing.Point(17, 22);
            this.resultSuccess.Name = "resultSuccess";
            this.resultSuccess.Size = new System.Drawing.Size(60, 13);
            this.resultSuccess.TabIndex = 0;
            this.resultSuccess.Text = "Success: 0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(12, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(920, 304);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Entry,
            this.Question,
            this.Answer,
            this.RandomAnswer});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(914, 285);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Location = new System.Drawing.Point(20, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(144, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(102, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnStart);
            this.groupBox4.Controls.Add(this.btnStop);
            this.groupBox4.Location = new System.Drawing.Point(677, 70);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(252, 59);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Control";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Entry
            // 
            this.Entry.HeaderText = "Entry";
            this.Entry.Name = "Entry";
            this.Entry.ReadOnly = true;
            // 
            // Question
            // 
            this.Question.HeaderText = "Question";
            this.Question.Name = "Question";
            this.Question.ReadOnly = true;
            // 
            // Answer
            // 
            this.Answer.HeaderText = "Answer";
            this.Answer.Name = "Answer";
            this.Answer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Answer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RandomAnswer
            // 
            this.RandomAnswer.HeaderText = "RandomAnswer";
            this.RandomAnswer.Name = "RandomAnswer";
            // 
            // numRepeat
            // 
            this.numRepeat.Location = new System.Drawing.Point(465, 60);
            this.numRepeat.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numRepeat.Name = "numRepeat";
            this.numRepeat.Size = new System.Drawing.Size(74, 20);
            this.numRepeat.TabIndex = 9;
            this.numRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Repeat (times):";
            // 
            // cbRandomTime
            // 
            this.cbRandomTime.AutoSize = true;
            this.cbRandomTime.Location = new System.Drawing.Point(18, 87);
            this.cbRandomTime.Name = "cbRandomTime";
            this.cbRandomTime.Size = new System.Drawing.Size(88, 17);
            this.cbRandomTime.TabIndex = 10;
            this.cbRandomTime.Text = "Random time";
            this.cbRandomTime.UseVisualStyleBackColor = true;
            this.cbRandomTime.CheckedChanged += new System.EventHandler(this.cbRandomTime_CheckedChanged);
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(15, 62);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(36, 13);
            this.labelFrom.TabIndex = 11;
            this.labelFrom.Text = "From: ";
            this.labelFrom.Visible = false;
            // 
            // numFrom
            // 
            this.numFrom.Location = new System.Drawing.Point(57, 60);
            this.numFrom.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numFrom.Name = "numFrom";
            this.numFrom.Size = new System.Drawing.Size(64, 20);
            this.numFrom.TabIndex = 12;
            this.numFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFrom.Visible = false;
            // 
            // numTo
            // 
            this.numTo.Location = new System.Drawing.Point(166, 60);
            this.numTo.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numTo.Name = "numTo";
            this.numTo.Size = new System.Drawing.Size(64, 20);
            this.numTo.TabIndex = 14;
            this.numTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTo.Visible = false;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(134, 62);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(26, 13);
            this.labelTo.TabIndex = 13;
            this.labelTo.Text = "To: ";
            this.labelTo.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 450);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(960, 489);
            this.MinimumSize = new System.Drawing.Size(960, 489);
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSec)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRepeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown numSec;
        private System.Windows.Forms.Label labelPerSec;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label resultFail;
        private System.Windows.Forms.Label resultTotal;
        private System.Windows.Forms.Label resultSuccess;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Question;
        private System.Windows.Forms.DataGridViewComboBoxColumn Answer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RandomAnswer;
        private System.Windows.Forms.NumericUpDown numRepeat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTo;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.NumericUpDown numFrom;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.CheckBox cbRandomTime;
    }
}

