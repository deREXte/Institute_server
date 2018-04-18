namespace ClientWF
{
    partial class GenerateUsersResult
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
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.button_SaveInFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(26, 23);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(274, 158);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // button_SaveInFile
            // 
            this.button_SaveInFile.Location = new System.Drawing.Point(100, 264);
            this.button_SaveInFile.Name = "button_SaveInFile";
            this.button_SaveInFile.Size = new System.Drawing.Size(110, 23);
            this.button_SaveInFile.TabIndex = 1;
            this.button_SaveInFile.Text = "Сохранить в файл";
            this.button_SaveInFile.UseVisualStyleBackColor = true;
            this.button_SaveInFile.Click += new System.EventHandler(this.button_SaveInFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(225, 264);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // GenerateUsersResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 317);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_SaveInFile);
            this.Controls.Add(this.richTextBox);
            this.Name = "GenerateUsersResult";
            this.Text = "GenerateUsersResult";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button button_SaveInFile;
        private System.Windows.Forms.Button button2;
    }
}