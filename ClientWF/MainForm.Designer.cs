namespace ClientWF
{
    partial class MainForm
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
            this.DataGridView_MainView = new System.Windows.Forms.DataGridView();
            this.ButtonGetTableList = new System.Windows.Forms.Button();
            this.ListBox_Tables = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.администраторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сгенерироватьЛогиныИПаролиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_MainView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView_MainView
            // 
            this.DataGridView_MainView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_MainView.Location = new System.Drawing.Point(225, 39);
            this.DataGridView_MainView.Name = "DataGridView_MainView";
            this.DataGridView_MainView.Size = new System.Drawing.Size(655, 311);
            this.DataGridView_MainView.TabIndex = 0;
            // 
            // ButtonGetTableList
            // 
            this.ButtonGetTableList.Location = new System.Drawing.Point(28, 322);
            this.ButtonGetTableList.Name = "ButtonGetTableList";
            this.ButtonGetTableList.Size = new System.Drawing.Size(120, 28);
            this.ButtonGetTableList.TabIndex = 3;
            this.ButtonGetTableList.Text = "Получить список таблиц";
            this.ButtonGetTableList.UseVisualStyleBackColor = true;
            this.ButtonGetTableList.Click += new System.EventHandler(this.ButtonGetTableList_Click);
            // 
            // ListBox_Tables
            // 
            this.ListBox_Tables.FormattingEnabled = true;
            this.ListBox_Tables.Location = new System.Drawing.Point(28, 39);
            this.ListBox_Tables.Name = "ListBox_Tables";
            this.ListBox_Tables.Size = new System.Drawing.Size(120, 238);
            this.ListBox_Tables.TabIndex = 4;
            this.ListBox_Tables.DoubleClick += new System.EventHandler(this.ListBox_Tables_DoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.администраторToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1159, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // администраторToolStripMenuItem
            // 
            this.администраторToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сгенерироватьЛогиныИПаролиToolStripMenuItem});
            this.администраторToolStripMenuItem.Name = "администраторToolStripMenuItem";
            this.администраторToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.администраторToolStripMenuItem.Text = "Администратор";
            // 
            // сгенерироватьЛогиныИПаролиToolStripMenuItem
            // 
            this.сгенерироватьЛогиныИПаролиToolStripMenuItem.Name = "сгенерироватьЛогиныИПаролиToolStripMenuItem";
            this.сгенерироватьЛогиныИПаролиToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.сгенерироватьЛогиныИПаролиToolStripMenuItem.Text = "Сгенерировать логины и пароли";
            this.сгенерироватьЛогиныИПаролиToolStripMenuItem.Click += new System.EventHandler(this.GenUserDataToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 516);
            this.Controls.Add(this.ListBox_Tables);
            this.Controls.Add(this.ButtonGetTableList);
            this.Controls.Add(this.DataGridView_MainView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "B";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_MainView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_MainView;
        private System.Windows.Forms.Button ButtonGetTableList;
        private System.Windows.Forms.ListBox ListBox_Tables;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem администраторToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сгенерироватьЛогиныИПаролиToolStripMenuItem;
    }
}