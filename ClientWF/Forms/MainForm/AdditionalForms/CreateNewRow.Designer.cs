﻿namespace ClientWF.AddtionalForms
{
    partial class CreateNewRow
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
            this.dataGridView_AddRow = new System.Windows.Forms.DataGridView();
            this.button_AddRow = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AddRow)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_AddRow
            // 
            this.dataGridView_AddRow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AddRow.Location = new System.Drawing.Point(16, 10);
            this.dataGridView_AddRow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView_AddRow.Name = "dataGridView_AddRow";
            this.dataGridView_AddRow.RowTemplate.Height = 24;
            this.dataGridView_AddRow.Size = new System.Drawing.Size(356, 105);
            this.dataGridView_AddRow.TabIndex = 0;
            // 
            // button_AddRow
            // 
            this.button_AddRow.Location = new System.Drawing.Point(197, 139);
            this.button_AddRow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_AddRow.Name = "button_AddRow";
            this.button_AddRow.Size = new System.Drawing.Size(104, 19);
            this.button_AddRow.TabIndex = 1;
            this.button_AddRow.Text = "Добавить строку";
            this.button_AddRow.UseVisualStyleBackColor = true;
            this.button_AddRow.Click += new System.EventHandler(this.button_AddRow_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(316, 139);
            this.button_Exit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(56, 19);
            this.button_Exit.TabIndex = 2;
            this.button_Exit.Text = "Закрыть";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // CreateNewRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 193);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_AddRow);
            this.Controls.Add(this.dataGridView_AddRow);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "CreateNewRow";
            this.Text = "CreateNewRow";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AddRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_AddRow;
        private System.Windows.Forms.Button button_AddRow;
        private System.Windows.Forms.Button button_Exit;
    }
}