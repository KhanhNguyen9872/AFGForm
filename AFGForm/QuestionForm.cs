using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AFGForm
{
    public partial class QuestionForm : Form
    {
        public bool isSuccess = false;
        private Dictionary<long, Dictionary<string, List<string>>> data;
        private List<string> removedAnswer = new List<string>();
        private int id;
        private long entry;
        private string type;
        private string question;
        private string currentAnswer;
        private bool randomAnswer;
        private bool ignoreOther;
        private bool d_IgnoreOther = false;
        private bool isClear = false;
        private bool isCheckboxGrid = false; // Track if current grid is checkbox type
        private CheckBox cbRandomAsRadio = null; // Checkbox for "Random as radio box" option

        private bool randomAsRadioState = false; // Store state for "Random as radio box"

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

        public void setRandomAsRadioState(bool state)
        {
            this.randomAsRadioState = state;
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

        private void loadData()
        {
            if (type.Equals("Multi select"))
            {
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
                    foreach (string c in this.currentAnswer.Split((char)0))
                    {
                        if (ss.Equals(c))
                        {
                            row.Cells[1].Value = true;
                            break;
                        }
                    }
                    dataGridView1.Rows.Add(row);
                }
            } else if (type.Equals("Multi choice/checkbox grid"))
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                
                // Ensure DataGridView is visible
                dataGridView1.Visible = true;
                dataGridView1.Enabled = true;
                
                // Parse grid data structure
                List<string> gridData = null;
                long actualEntry = this.entry;
                
                // Try to find grid data - first try with current entry
                if (this.data.ContainsKey(this.entry) && this.data[this.entry].ContainsKey(this.question))
                {
                    gridData = this.data[this.entry][this.question];
                }
                
                // If not found or invalid, search in all entries for this question
                if (gridData == null || gridData.Count == 0 || !gridData[0].Equals("7"))
                {
                    foreach (var entryData in this.data)
                    {
                        if (entryData.Value.ContainsKey(this.question))
                        {
                            List<string> testData = entryData.Value[this.question];
                            if (testData.Count > 0 && testData[0].Equals("7"))
                            {
                                gridData = testData;
                                actualEntry = entryData.Key;
                                break;
                            }
                        }
                    }
                }
                
                if (gridData == null || gridData.Count == 0 || !gridData[0].Equals("7"))
                {
                    // Show empty grid if no data
                    DataGridViewTextBoxColumn rowLabelCol = new DataGridViewTextBoxColumn();
                    rowLabelCol.Name = "RowLabel";
                    rowLabelCol.HeaderText = "Row";
                    rowLabelCol.ReadOnly = true;
                    rowLabelCol.Width = 300;
                    dataGridView1.Columns.Add(rowLabelCol);
                    return;
                }
                
                if (gridData.Count == 0 || !gridData[0].Equals("7"))
                {
                    // Show empty grid if invalid data
                    DataGridViewTextBoxColumn rowLabelCol = new DataGridViewTextBoxColumn();
                    rowLabelCol.Name = "RowLabel";
                    rowLabelCol.HeaderText = "Row";
                    rowLabelCol.ReadOnly = true;
                    rowLabelCol.Width = 300;
                    dataGridView1.Columns.Add(rowLabelCol);
                    return;
                }
                
                // Get columns (options)
                List<string> columns = new List<string>();
                int gridRowsIndex = -1;
                int gridType = 0; // 0 = Radio Grid, 1 = Checkbox Grid
                
                for (int i = 1; i < gridData.Count; i++)
                {
                    if (gridData[i].Equals("GRID_ROWS"))
                    {
                        gridRowsIndex = i;
                        // Check next element for grid type
                        if (i + 1 < gridData.Count && gridData[i + 1].StartsWith("GRID_TYPE:"))
                        {
                            string gridTypeStr = gridData[i + 1].Replace("GRID_TYPE:", "");
                            try
                            {
                                gridType = Convert.ToInt32(gridTypeStr);
                            }
                            catch
                            {
                                gridType = 0; // Default to radio
                            }
                            gridRowsIndex = i + 1; // Skip GRID_TYPE marker
                        }
                        break;
                    }
                    // Skip empty strings but add non-empty values
                    if (!string.IsNullOrEmpty(gridData[i]))
                    {
                        columns.Add(gridData[i]);
                    }
                }
                
                if (gridRowsIndex == -1)
                {
                    // If no GRID_ROWS marker found, assume all data after first element are columns
                    // This handles case where data structure might be different
                    if (columns.Count == 0)
                    {
                        // Show empty grid if no columns
                        DataGridViewTextBoxColumn rowLabelCol = new DataGridViewTextBoxColumn();
                        rowLabelCol.Name = "RowLabel";
                        rowLabelCol.HeaderText = "Row";
                        rowLabelCol.ReadOnly = true;
                        rowLabelCol.Width = 300;
                        dataGridView1.Columns.Add(rowLabelCol);
                        return;
                    }
                    // Use all remaining data as rows if no marker
                    gridRowsIndex = gridData.Count;
                }
                
                if (columns.Count == 0)
                {
                    // Show empty grid if no columns
                    DataGridViewTextBoxColumn rowLabelCol = new DataGridViewTextBoxColumn();
                    rowLabelCol.Name = "RowLabel";
                    rowLabelCol.HeaderText = "Row";
                    rowLabelCol.ReadOnly = true;
                    rowLabelCol.Width = 300;
                    dataGridView1.Columns.Add(rowLabelCol);
                    return;
                }
                
                // Set AutoSizeColumnsMode for responsive layout
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                // Create columns: Row label + options
                DataGridViewTextBoxColumn rowLabelCol2 = new DataGridViewTextBoxColumn();
                rowLabelCol2.Name = "RowLabel";
                rowLabelCol2.HeaderText = "Row";
                rowLabelCol2.ReadOnly = true;
                rowLabelCol2.Width = 150; // Smaller width since we truncate text
                rowLabelCol2.MinimumWidth = 100;
                rowLabelCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                rowLabelCol2.FillWeight = 20; // 20% of available width (reduced since text is truncated)
                dataGridView1.Columns.Add(rowLabelCol2);
                
                // Calculate fill weight for option columns (70% divided by number of columns)
                float optionFillWeight = 70.0f / columns.Count;
                
                // Store grid type in form for later use
                this.isCheckboxGrid = (gridType == 1);
                
                for (int i = 0; i < columns.Count; i++)
                {
                    string colHeader = columns[i];
                    // Truncate column header if too long (max 5 chars + "...")
                    if (colHeader.Length > 5)
                    {
                        colHeader = colHeader.Substring(0, 5) + "...";
                    }
                    
                    // Use CheckBox column for both checkbox and radio grid
                    DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                    col.Name = "Option" + i.ToString();
                    col.HeaderText = colHeader;
                    col.Width = 100;
                    col.MinimumWidth = 80;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.FillWeight = optionFillWeight;
                    col.Tag = columns[i]; // Store actual column value in Tag
                    dataGridView1.Columns.Add(col);
                }
                
                // Parse current answer: format differs for radio vs checkbox
                // Radio: "rowEntryId:option" separated by (char)0
                // Checkbox: "rowEntryId:option1,option2,option3" separated by (char)0
                Dictionary<long, List<string>> currentSelections = new Dictionary<long, List<string>>();
                if (!string.IsNullOrEmpty(this.currentAnswer))
                {
                    string[] answers = this.currentAnswer.Split((char)0);
                    foreach (string ans in answers)
                    {
                        if (ans.Contains(":"))
                        {
                            string[] parts = ans.Split(':');
                            if (parts.Length == 2)
                            {
                                try
                                {
                                    long rowEntryId = Convert.ToInt64(parts[0]);
                                    if (isCheckboxGrid)
                                    {
                                        // Multiple options separated by comma
                                        List<string> options = new List<string>(parts[1].Split(','));
                                        currentSelections[rowEntryId] = options;
                                    }
                                    else
                                    {
                                        // Single option
                                        List<string> options = new List<string>();
                                        options.Add(parts[1]);
                                        currentSelections[rowEntryId] = options;
                                    }
                                }
                                catch
                                {
                                    // Skip invalid entry
                                }
                            }
                        }
                    }
                }
                
                // Add rows
                int rowsAdded = 0;
                for (int i = gridRowsIndex + 1; i < gridData.Count; i += 2)
                {
                    if (i + 1 >= gridData.Count)
                    {
                        break;
                    }
                    
                    try
                    {
                        string entryIdStr = gridData[i];
                        string rowLabel = gridData[i + 1];
                        
                        if (string.IsNullOrEmpty(entryIdStr) || string.IsNullOrEmpty(rowLabel))
                        {
                            continue;
                        }
                        
                        long rowEntryId = Convert.ToInt64(entryIdStr);
                        
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1);
                        
                        // Truncate row label if too long (max 55 chars + "...")
                        string displayLabel = rowLabel;
                        if (displayLabel.Length > 55)
                        {
                            displayLabel = displayLabel.Substring(0, 55) + "...";
                        }
                        row.Cells[0].Value = displayLabel;
                        row.Cells[0].ToolTipText = rowLabel; // Show full text on hover
                        row.Tag = rowEntryId; // Store row entry ID in Tag
                        
                        // For both checkbox and radio grid: set all to false (unchecked) by default
                        for (int j = 1; j < dataGridView1.Columns.Count; j++)
                        {
                            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[j];
                            if (cell != null)
                            {
                                cell.Value = false;
                            }
                        }
                        
                        // Then set checked options if exists
                        if (currentSelections.ContainsKey(rowEntryId))
                        {
                            List<string> selectedOptions = currentSelections[rowEntryId];
                            for (int j = 1; j < dataGridView1.Columns.Count; j++)
                            {
                                DataGridViewCheckBoxColumn col = (DataGridViewCheckBoxColumn)dataGridView1.Columns[j];
                                string actualColumnValue = col.Tag != null ? col.Tag.ToString() : "";
                                
                                // Check if this column is in selected options
                                if (selectedOptions.Contains(actualColumnValue))
                                {
                                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[j];
                                    if (cell != null)
                                    {
                                        cell.Value = true;
                                    }
                                }
                            }
                        }
                        
                        dataGridView1.Rows.Add(row);
                        rowsAdded++;
                    }
                    catch (Exception)
                    {
                        // Skip invalid row data
                        continue;
                    }
                }
                
                // Add event handler for radio grid behavior (only one checkbox per row)
                if (!isCheckboxGrid)
                {
                    dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged_RadioGrid;
                    dataGridView1.CellValueChanged += dataGridView1_CellValueChanged_RadioGrid;
                }
                
                // Add "Random as radio box" checkbox if random is enabled and it's checkbox grid
                if (cbRandom.Checked && this.isCheckboxGrid)
                {
                    if (cbRandomAsRadio == null)
                    {
                        cbRandomAsRadio = new CheckBox();
                        cbRandomAsRadio.Text = "Random as radio box";
                        cbRandomAsRadio.AutoSize = true;
                        // Position to the right of cbIgnore checkbox
                        cbRandomAsRadio.Location = new System.Drawing.Point(cbIgnore.Location.X + cbIgnore.Width + 20, cbIgnore.Location.Y);
                        cbRandomAsRadio.Enabled = true;
                        cbRandomAsRadio.Checked = this.randomAsRadioState; // Load saved state
                        this.Controls.Add(cbRandomAsRadio);
                    }
                    else
                    {
                        cbRandomAsRadio.Checked = this.randomAsRadioState; // Load saved state
                    }
                    cbRandomAsRadio.Visible = true;
                }
                
                // If no rows were added, add at least one empty row to show the grid
                if (rowsAdded == 0 && columns.Count > 0)
                {
                    DataGridViewRow emptyRow = new DataGridViewRow();
                    emptyRow.CreateCells(dataGridView1);
                    emptyRow.Cells[0].Value = "No rows found";
                    dataGridView1.Rows.Add(emptyRow);
                }
                
                // Refresh DataGridView to ensure it displays
                dataGridView1.Refresh();
                dataGridView1.Update();
                
                tbOtherOption.Visible = false;
                lbOtherOption.Visible = false;
            } else if (type.Equals("Date"))
            {

            } else if (type.Equals("Time")) {

            } else
            {
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

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            this.Text = "ID: " + this.id.ToString() + "  |  Entry: " + this.entry.ToString(); 
            tbID.Text = this.id.ToString();
            tbEntry.Text = this.entry.ToString();
            tbInputType.Text = this.type;
            tbQuestion.Text = this.question;
            cbRandom.Checked = this.randomAnswer;
            cbIgnore.Checked = this.ignoreOther;

            if (type.Equals("Multi select"))
            {
                this.Height = this.Height + 90;
                System.Drawing.Point p;

                p = clearBtn.Location;
                p.Y = p.Y + 120;
                clearBtn.Location = p;

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
                dataGridView1.Visible = true;
            } else if (type.Equals("Multi choice/checkbox grid"))
            {
                // Enable form resizing
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                
                // Hide other controls
                cbAnswer.Visible = false;
                tbOtherOption.Visible = false;
                lbOtherOption.Visible = false;
                
                // Calculate grid size based on data - will be recalculated in loadData
                // But set initial size here
                int rowCount = 5; // Default, will be updated
                int colCount = 5; // Default, will be updated
                
                if (this.data.ContainsKey(this.entry) && this.data[this.entry].ContainsKey(this.question))
                {
                    List<string> gridData = this.data[this.entry][this.question];
                    if (gridData.Count > 0 && gridData[0].Equals("7"))
                    {
                        int gridRowsIndex = -1;
                        for (int i = 1; i < gridData.Count; i++)
                        {
                            if (gridData[i].Equals("GRID_ROWS"))
                            {
                                gridRowsIndex = i;
                                break;
                            }
                            colCount++;
                        }
                        
                        if (gridRowsIndex > 0)
                        {
                            rowCount = (gridData.Count - gridRowsIndex - 1) / 2;
                        }
                    }
                }
                
                // Set minimum form size based on grid
                int minWidth = Math.Max(800, (colCount + 1) * 120 + 400);
                int minHeight = Math.Max(400, rowCount * 30 + 250);
                this.MinimumSize = new System.Drawing.Size(minWidth, minHeight);
                
                // Set initial form size
                int initialWidth = Math.Min(1200, Math.Max(800, (colCount + 1) * 120 + 400));
                int initialHeight = Math.Max(400, rowCount * 30 + 250);
                this.Size = new System.Drawing.Size(initialWidth, initialHeight);
                
                // Set DataGridView to fill available space
                dataGridView1.Location = new System.Drawing.Point(86, 86);
                dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                
                // Calculate initial size with margins
                int gridWidth = this.ClientSize.Width - 180; // Left margin + right margin
                int gridHeight = this.ClientSize.Height - 200; // Top margin + bottom margin (for buttons)
                dataGridView1.Size = new System.Drawing.Size(Math.Max(600, gridWidth), Math.Max(150, gridHeight));
                
                // Set buttons to anchor bottom right
                clearBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                
                // Position buttons at bottom
                int buttonY = this.ClientSize.Height - 40;
                clearBtn.Location = new System.Drawing.Point(this.ClientSize.Width - 250, buttonY);
                button1.Location = new System.Drawing.Point(this.ClientSize.Width - 170, buttonY);
                button2.Location = new System.Drawing.Point(this.ClientSize.Width - 80, buttonY);
                
                // Make sure DataGridView is visible and on top
                dataGridView1.Visible = true;
                dataGridView1.Enabled = true;
                dataGridView1.BringToFront();
                dataGridView1.Show();
                dataGridView1.Refresh();
                
                // Ensure it's not hidden behind other controls
                this.Controls.SetChildIndex(dataGridView1, 0);
                
                // Add resize handler to update grid size
                this.Resize += QuestionForm_Resize;
            }
            else if (type.Equals("Date"))
            {
                if (!string.IsNullOrEmpty(this.currentAnswer))
                {
                    datePicker.Value = new DateTime(
                        Convert.ToInt32(this.currentAnswer.Split('-')[0]),
                        Convert.ToInt32(this.currentAnswer.Split('-')[1]),
                        Convert.ToInt32(this.currentAnswer.Split('-')[2])
                    );
                } else
                {
                    datePicker.Value = DateTime.Now;
                }

                datePicker.Visible = true;
                this.Height = this.Height - 30;
            } else if (type.Equals("Time"))
            {
                if (!string.IsNullOrEmpty(this.currentAnswer))
                {
                    timePicker.Value = new DateTime(
                        1975,
                        01,
                        01,
                        Convert.ToInt32(this.currentAnswer.Split(':')[0]),
                        Convert.ToInt32(this.currentAnswer.Split(':')[1]),
                        0
                    );
                } else
                {
                    datePicker.Value = DateTime.Now;
                }

                timePicker.Visible = true;
                this.Height = this.Height - 30;
            } else
            {
                this.Height = this.Height - 30;
                cbAnswer.Visible = true;
            }
            this.loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(string s in this.removedAnswer)
            {
                this.data[this.entry][this.question].Remove(s);
            }
            if (this.type.Equals("Date"))
            {
                this.currentAnswer = datePicker.Value.ToString("yyyy-MM-dd");
            }
            else if (this.type.Equals("Time"))
            {
                this.currentAnswer = timePicker.Value.ToString("HH:mm");
            }
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
            if (this.isClear)
            {
                return "";
            }

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
            } else if (this.type.Equals("Multi choice/checkbox grid"))
            {
                string fullTxt = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Tag == null) continue;
                    
                    long rowEntryId = Convert.ToInt64(row.Tag);
                    List<string> selectedOptions = new List<string>();
                    
                    // For both checkbox and radio grid: collect checked options
                    for (int i = 1; i < row.Cells.Count; i++)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[i];
                        if (cell.Value != null && Convert.ToBoolean(cell.Value))
                        {
                            // Get actual column value from Tag
                            DataGridViewCheckBoxColumn col = (DataGridViewCheckBoxColumn)dataGridView1.Columns[i];
                            if (col.Tag != null)
                            {
                                selectedOptions.Add(col.Tag.ToString());
                            }
                        }
                    }
                    
                    if (selectedOptions.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(fullTxt))
                        {
                            fullTxt = fullTxt + (char)0;
                        }
                        // For checkbox: join multiple options with comma
                        // For radio: single option (but format is same)
                        string optionsStr = string.Join(",", selectedOptions);
                        fullTxt = fullTxt + rowEntryId.ToString() + ":" + optionsStr;
                    }
                }

                return fullTxt;
            }
            else if (this.type.Equals("Date") || this.type.Equals("Time"))
            {
                return this.currentAnswer;
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

        public bool getRandomAsRadio()
        {
            if (cbRandomAsRadio != null)
            {
                return cbRandomAsRadio.Checked;
            }
            return false;
        }

        private void cbRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRandom.Checked)
            {
                cbAnswer.Enabled = false;
                dataGridView1.Enabled = false;
                tbOtherOption.Enabled = false;
                timePicker.Enabled = false;
                datePicker.Enabled = false;
                if (!this.d_IgnoreOther)
                {
                    cbIgnore.Enabled = true;
                }
                
                // Show "Random as radio box" checkbox for checkbox grid
                if (this.type.Equals("Multi choice/checkbox grid") && this.isCheckboxGrid)
                {
                    if (cbRandomAsRadio == null)
                    {
                        cbRandomAsRadio = new CheckBox();
                        cbRandomAsRadio.Text = "Random as radio box";
                        cbRandomAsRadio.AutoSize = true;
                        // Position to the right of cbIgnore checkbox
                        cbRandomAsRadio.Location = new System.Drawing.Point(cbIgnore.Location.X + cbIgnore.Width + 20, cbIgnore.Location.Y);
                        cbRandomAsRadio.Enabled = true;
                        cbRandomAsRadio.Checked = this.randomAsRadioState; // Load saved state
                        this.Controls.Add(cbRandomAsRadio);
                    }
                    else
                    {
                        cbRandomAsRadio.Checked = this.randomAsRadioState; // Load saved state
                    }
                    cbRandomAsRadio.Visible = true;
                }
            } else
            {
                cbAnswer.Enabled = true;
                dataGridView1.Enabled = true;
                tbOtherOption.Enabled = true;
                timePicker.Enabled = true;
                datePicker.Enabled = true;
                if (!this.d_IgnoreOther)
                {
                    cbIgnore.Enabled = false;
                    cbIgnore.Checked = false;
                }
                
                // Hide "Random as radio box" checkbox
                if (cbRandomAsRadio != null)
                {
                    cbRandomAsRadio.Visible = false;
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

        private bool deleteAnswer(string answer)
        {
            bool check = false;

            List<DataGridViewRow> listRow = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (answer.Equals(row.Cells[0].Value.ToString()))
                {
                    check = true;
                    continue;
                }

                listRow.Add(row);
            }

            if (check)
            {
                dataGridView1.Rows.Clear();
                foreach (DataGridViewRow row in listRow)
                {
                    dataGridView1.Rows.Add(row);
                }
            }

            return check;
        }

        private void dataGridView1_CurrentCellDirtyStateChanged_RadioGrid(object sender, EventArgs e)
        {
            // Only handle for radio grid (not checkbox grid)
            if (!this.type.Equals("Multi choice/checkbox grid") || this.isCheckboxGrid)
            {
                return;
            }
            
            // Commit the edit to trigger CellValueChanged
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged_RadioGrid(object sender, DataGridViewCellEventArgs e)
        {
            // Only handle for radio grid (not checkbox grid)
            if (!this.type.Equals("Multi choice/checkbox grid") || this.isCheckboxGrid)
            {
                return;
            }
            
            // Only handle checkbox columns (column index > 0)
            if (e.ColumnIndex <= 0 || e.RowIndex < 0)
            {
                return;
            }
            
            DataGridViewCheckBoxCell changedCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (changedCell.Value != null && Convert.ToBoolean(changedCell.Value))
            {
                // If this checkbox was checked, uncheck all other checkboxes in the same row
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    if (i != e.ColumnIndex)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[i];
                        if (cell != null)
                        {
                            cell.Value = false;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // For grid type, handle checkbox clicks for radio behavior
            if (this.type.Equals("Multi choice/checkbox grid") && e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                // Allow checkbox clicks to be processed
                return;
            }
            
            // Only handle click on first column (Row column) for delete functionality
            // Ignore clicks on other columns (option columns)
            if (e.ColumnIndex != 0)
            {
                return;
            }
            
            // For Multi select type, check if clicking on checkbox column
            if (this.type.Equals("Multi select") && e.ColumnIndex == 1)
            {
                return;
            }
            
            int index = e.RowIndex;
            if (index < 0 || index >= dataGridView1.Rows.Count)
            {
                return;
            }

            string answer = dataGridView1.Rows[index].Cells[0].Value.ToString();

            // For grid type, allow delete but only when clicking on Row column
            if (this.type.Equals("Multi choice/checkbox grid"))
            {
                if (MessageBox.Show("Do you want to delete row [" + answer + "]?", "DELETE ROW", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(index);
                }
                return;
            }

            if ((index == dataGridView1.Rows.Count - 1) && (!d_IgnoreOther) && (answer.Equals("Other option")))
            {
                return;
            }

            if (MessageBox.Show("Do you want to delete [" + answer + "]?", "DELETE ANSWER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (deleteAnswer(answer))
                {
                    this.removedAnswer.Add(answer);
                } else 
                {
                    MessageBox.Show("Failed when delete answer", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteToolContextComboBox_Click(object sender, EventArgs e)
        {
            string answer = cbAnswer.Text;
            if (string.IsNullOrEmpty(answer))
            {
                return;
            }

            if (MessageBox.Show("Do you want to delete [" + answer + "]?", "DELETE ANSWER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (cbAnswer.Items.Count > 1)
                {
                    cbAnswer.Items.Remove(answer);
                    cbAnswer.SelectedIndex = 0;
                    this.removedAnswer.Add(answer);
                }
                else
                {
                    MessageBox.Show("Cannot remove last item", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void addToolContextComboBox_Click(object sender, EventArgs e)
        {
            if (this.d_IgnoreOther)
            {
                MessageBox.Show("Can't add item!\n [Other option] not found in question OR inputType is not [Short answer, Paragraph]", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 
        }

        private void addToolContextDataGrid_Click(object sender, EventArgs e)
        {
            if (this.d_IgnoreOther)
            {
                MessageBox.Show("Can't add item when [Other option] not found in question!", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            this.isClear = true;
            this.isSuccess = true;
            this.Close();
        }
        
        private void QuestionForm_Resize(object sender, EventArgs e)
        {
            if (type.Equals("Multi choice/checkbox grid"))
            {
                // Update DataGridView size when form is resized
                if (dataGridView1.Visible)
                {
                    int marginLeft = 86;
                    int marginRight = 86;
                    int marginTop = 86;
                    int marginBottom = 60; // Space for buttons
                    
                    int gridWidth = this.ClientSize.Width - marginLeft - marginRight;
                    int gridHeight = this.ClientSize.Height - marginTop - marginBottom;
                    
                    dataGridView1.Location = new System.Drawing.Point(marginLeft, marginTop);
                    dataGridView1.Size = new System.Drawing.Size(Math.Max(600, gridWidth), Math.Max(150, gridHeight));
                    
                    // Update button positions - anchor to bottom right
                    int buttonY = this.ClientSize.Height - 40;
                    clearBtn.Location = new System.Drawing.Point(this.ClientSize.Width - 250, buttonY);
                    button1.Location = new System.Drawing.Point(this.ClientSize.Width - 170, buttonY);
                    button2.Location = new System.Drawing.Point(this.ClientSize.Width - 80, buttonY);
                }
            }
        }
    }
}
