namespace AFGForm
{
    partial class QuestionForm
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Answer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cbIgnore = new System.Windows.Forms.CheckBox();
            this.cbRandom = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbQuestion = new System.Windows.Forms.TextBox();
            this.cbAnswer = new System.Windows.Forms.ComboBox();
            this.contextComboBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolContextComboBox = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolContextComboBox = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEntry = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbOtherOption = new System.Windows.Forms.TextBox();
            this.lbOtherOption = new System.Windows.Forms.Label();
            this.tbInputType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contextDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolContextDataGrid = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextComboBox.SuspendLayout();
            this.contextDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Answer,
            this.Select});
            this.dataGridView1.ContextMenuStrip = this.contextDataGrid;
            this.dataGridView1.Location = new System.Drawing.Point(86, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(471, 34);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Answer
            // 
            this.Answer.HeaderText = "Answer";
            this.Answer.Name = "Answer";
            this.Answer.ReadOnly = true;
            this.Answer.Width = 67;
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Width = 43;
            // 
            // cbIgnore
            // 
            this.cbIgnore.AutoSize = true;
            this.cbIgnore.Enabled = false;
            this.cbIgnore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbIgnore.Location = new System.Drawing.Point(598, 15);
            this.cbIgnore.Name = "cbIgnore";
            this.cbIgnore.Size = new System.Drawing.Size(115, 17);
            this.cbIgnore.TabIndex = 22;
            this.cbIgnore.Text = "Ignore other option";
            this.cbIgnore.UseVisualStyleBackColor = true;
            // 
            // cbRandom
            // 
            this.cbRandom.AutoSize = true;
            this.cbRandom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbRandom.Location = new System.Drawing.Point(480, 15);
            this.cbRandom.Name = "cbRandom";
            this.cbRandom.Size = new System.Drawing.Size(103, 17);
            this.cbRandom.TabIndex = 15;
            this.cbRandom.Text = "Random answer";
            this.cbRandom.UseVisualStyleBackColor = true;
            this.cbRandom.CheckedChanged += new System.EventHandler(this.cbRandom_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(24, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Answer: ";
            // 
            // tbQuestion
            // 
            this.tbQuestion.Location = new System.Drawing.Point(86, 50);
            this.tbQuestion.Name = "tbQuestion";
            this.tbQuestion.ReadOnly = true;
            this.tbQuestion.Size = new System.Drawing.Size(627, 20);
            this.tbQuestion.TabIndex = 21;
            // 
            // cbAnswer
            // 
            this.cbAnswer.ContextMenuStrip = this.contextComboBox;
            this.cbAnswer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnswer.FormattingEnabled = true;
            this.cbAnswer.Location = new System.Drawing.Point(86, 85);
            this.cbAnswer.Name = "cbAnswer";
            this.cbAnswer.Size = new System.Drawing.Size(627, 21);
            this.cbAnswer.TabIndex = 11;
            // 
            // contextComboBox
            // 
            this.contextComboBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolContextComboBox,
            this.deleteToolContextComboBox});
            this.contextComboBox.Name = "contextComboBox";
            this.contextComboBox.Size = new System.Drawing.Size(108, 48);
            // 
            // addToolContextComboBox
            // 
            this.addToolContextComboBox.Name = "addToolContextComboBox";
            this.addToolContextComboBox.Size = new System.Drawing.Size(180, 22);
            this.addToolContextComboBox.Text = "Add";
            this.addToolContextComboBox.Click += new System.EventHandler(this.addToolContextComboBox_Click);
            // 
            // deleteToolContextComboBox
            // 
            this.deleteToolContextComboBox.Name = "deleteToolContextComboBox";
            this.deleteToolContextComboBox.Size = new System.Drawing.Size(107, 22);
            this.deleteToolContextComboBox.Text = "Delete";
            this.deleteToolContextComboBox.Click += new System.EventHandler(this.deleteToolContextComboBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(25, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Question: ";
            // 
            // tbEntry
            // 
            this.tbEntry.Location = new System.Drawing.Point(158, 12);
            this.tbEntry.Name = "tbEntry";
            this.tbEntry.ReadOnly = true;
            this.tbEntry.Size = new System.Drawing.Size(106, 20);
            this.tbEntry.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(115, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Entry: ";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(54, 10);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(47, 20);
            this.tbID.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(24, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "ID: ";
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(557, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(638, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbOtherOption
            // 
            this.tbOtherOption.Location = new System.Drawing.Point(577, 100);
            this.tbOtherOption.Multiline = true;
            this.tbOtherOption.Name = "tbOtherOption";
            this.tbOtherOption.ReadOnly = true;
            this.tbOtherOption.Size = new System.Drawing.Size(136, 20);
            this.tbOtherOption.TabIndex = 24;
            this.tbOtherOption.Visible = false;
            // 
            // lbOtherOption
            // 
            this.lbOtherOption.AutoSize = true;
            this.lbOtherOption.Location = new System.Drawing.Point(574, 84);
            this.lbOtherOption.Name = "lbOtherOption";
            this.lbOtherOption.Size = new System.Drawing.Size(71, 13);
            this.lbOtherOption.TabIndex = 25;
            this.lbOtherOption.Text = "Other option: ";
            this.lbOtherOption.Visible = false;
            // 
            // tbInputType
            // 
            this.tbInputType.Location = new System.Drawing.Point(345, 12);
            this.tbInputType.Name = "tbInputType";
            this.tbInputType.ReadOnly = true;
            this.tbInputType.Size = new System.Drawing.Size(117, 20);
            this.tbInputType.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(281, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Input type: ";
            // 
            // contextDataGrid
            // 
            this.contextDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolContextDataGrid});
            this.contextDataGrid.Name = "contextDataGrid";
            this.contextDataGrid.Size = new System.Drawing.Size(97, 26);
            // 
            // addToolContextDataGrid
            // 
            this.addToolContextDataGrid.Name = "addToolContextDataGrid";
            this.addToolContextDataGrid.Size = new System.Drawing.Size(180, 22);
            this.addToolContextDataGrid.Text = "Add";
            this.addToolContextDataGrid.Click += new System.EventHandler(this.addToolContextDataGrid_Click);
            // 
            // QuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 189);
            this.ControlBox = false;
            this.Controls.Add(this.tbInputType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbOtherOption);
            this.Controls.Add(this.tbOtherOption);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbIgnore);
            this.Controls.Add(this.cbRandom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbQuestion);
            this.Controls.Add(this.cbAnswer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbEntry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(757, 199);
            this.Name = "QuestionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuestionForm";
            this.Load += new System.EventHandler(this.QuestionForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QuestionForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextComboBox.ResumeLayout(false);
            this.contextDataGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbIgnore;
        private System.Windows.Forms.CheckBox cbRandom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbQuestion;
        private System.Windows.Forms.ComboBox cbAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEntry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Answer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.TextBox tbOtherOption;
        private System.Windows.Forms.Label lbOtherOption;
        private System.Windows.Forms.TextBox tbInputType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextComboBox;
        private System.Windows.Forms.ToolStripMenuItem addToolContextComboBox;
        private System.Windows.Forms.ToolStripMenuItem deleteToolContextComboBox;
        private System.Windows.Forms.ContextMenuStrip contextDataGrid;
        private System.Windows.Forms.ToolStripMenuItem addToolContextDataGrid;
    }
}