
namespace 监考分配
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileButton = new System.Windows.Forms.Button();
            this.cal = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.openTeachingOfficeFile = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.limitGroupBox = new System.Windows.Forms.GroupBox();
            this.addLimitButton = new System.Windows.Forms.Button();
            this.limitGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.button = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.limitGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.limitGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(594, 607);
            this.dataGridView1.TabIndex = 1;
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(116, 24);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(114, 35);
            this.openFileButton.TabIndex = 3;
            this.openFileButton.Text = "读入考试信息";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // cal
            // 
            this.cal.Location = new System.Drawing.Point(271, 24);
            this.cal.Name = "cal";
            this.cal.Size = new System.Drawing.Size(147, 35);
            this.cal.TabIndex = 4;
            this.cal.Text = "产生监考分配方案";
            this.cal.UseVisualStyleBackColor = true;
            this.cal.Click += new System.EventHandler(this.cal_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(457, 24);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(58, 35);
            this.save.TabIndex = 5;
            this.save.Text = "保存";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // openTeachingOfficeFile
            // 
            this.openTeachingOfficeFile.Location = new System.Drawing.Point(180, 24);
            this.openTeachingOfficeFile.Name = "openTeachingOfficeFile";
            this.openTeachingOfficeFile.Size = new System.Drawing.Size(143, 35);
            this.openTeachingOfficeFile.TabIndex = 6;
            this.openTeachingOfficeFile.Text = "读入教研室信息";
            this.openTeachingOfficeFile.UseVisualStyleBackColor = true;
            this.openTeachingOfficeFile.Click += new System.EventHandler(this.openTeachingOfficeFile_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 65);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 27;
            this.dataGridView2.Size = new System.Drawing.Size(502, 285);
            this.dataGridView2.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.openFileButton);
            this.groupBox1.Controls.Add(this.cal);
            this.groupBox1.Controls.Add(this.save);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(532, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 678);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "考试信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.openTeachingOfficeFile);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 356);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "教研室信息";
            // 
            // limitGroupBox
            // 
            this.limitGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.limitGroupBox.Controls.Add(this.addLimitButton);
            this.limitGroupBox.Controls.Add(this.limitGridView);
            this.limitGroupBox.Location = new System.Drawing.Point(12, 374);
            this.limitGroupBox.Name = "limitGroupBox";
            this.limitGroupBox.Size = new System.Drawing.Size(514, 310);
            this.limitGroupBox.TabIndex = 10;
            this.limitGroupBox.TabStop = false;
            this.limitGroupBox.Text = "限制信息";
            // 
            // addLimitButton
            // 
            this.addLimitButton.Location = new System.Drawing.Point(427, 24);
            this.addLimitButton.Name = "addLimitButton";
            this.addLimitButton.Size = new System.Drawing.Size(81, 35);
            this.addLimitButton.TabIndex = 1;
            this.addLimitButton.Text = "添加";
            this.addLimitButton.UseVisualStyleBackColor = true;
            this.addLimitButton.Click += new System.EventHandler(this.addLimitButton_Click);
            // 
            // limitGridView
            // 
            this.limitGridView.AllowUserToAddRows = false;
            this.limitGridView.AllowUserToDeleteRows = false;
            this.limitGridView.AllowUserToResizeRows = false;
            this.limitGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.limitGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.limitGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.date,
            this.time,
            this.button});
            this.limitGridView.Location = new System.Drawing.Point(6, 66);
            this.limitGridView.Name = "limitGridView";
            this.limitGridView.RowHeadersVisible = false;
            this.limitGridView.RowHeadersWidth = 51;
            this.limitGridView.RowTemplate.Height = 27;
            this.limitGridView.Size = new System.Drawing.Size(502, 238);
            this.limitGridView.TabIndex = 0;
            this.limitGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.limitGridView_CellContentClick);
            // 
            // name
            // 
            this.name.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.name.HeaderText = "教研室名称";
            this.name.MaxDropDownItems = 20;
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.name.Width = 125;
            // 
            // date
            // 
            this.date.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.date.HeaderText = "日期";
            this.date.MaxDropDownItems = 20;
            this.date.MinimumWidth = 6;
            this.date.Name = "date";
            this.date.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.date.Width = 125;
            // 
            // time
            // 
            this.time.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.time.DropDownWidth = 5;
            this.time.HeaderText = "时间";
            this.time.Items.AddRange(new object[] {
            "上午",
            "下午",
            "晚上"});
            this.time.MaxDropDownItems = 3;
            this.time.MinimumWidth = 6;
            this.time.Name = "time";
            this.time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.time.Width = 125;
            // 
            // button
            // 
            this.button.HeaderText = "";
            this.button.MinimumWidth = 6;
            this.button.Name = "button";
            this.button.Text = "删除";
            this.button.UseColumnTextForButtonValue = true;
            this.button.Width = 125;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 702);
            this.Controls.Add(this.limitGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.limitGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.limitGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button cal;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button openTeachingOfficeFile;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox limitGroupBox;
        private System.Windows.Forms.Button addLimitButton;
        private System.Windows.Forms.DataGridView limitGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn name;
        private System.Windows.Forms.DataGridViewComboBoxColumn date;
        private System.Windows.Forms.DataGridViewComboBoxColumn time;
        private System.Windows.Forms.DataGridViewButtonColumn button;
    }
}

