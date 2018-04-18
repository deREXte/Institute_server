namespace ClientWF
{
    partial class ConnectForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox_Login = new System.Windows.Forms.TextBox();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Button_Connect = new System.Windows.Forms.Button();
            this.TB_Connection = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextBox_Login
            // 
            this.TextBox_Login.Location = new System.Drawing.Point(211, 44);
            this.TextBox_Login.Name = "TextBox_Login";
            this.TextBox_Login.Size = new System.Drawing.Size(170, 20);
            this.TextBox_Login.TabIndex = 0;
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Location = new System.Drawing.Point(211, 70);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.Size = new System.Drawing.Size(170, 20);
            this.TextBox_Password.TabIndex = 1;
            // 
            // Button_Connect
            // 
            this.Button_Connect.Location = new System.Drawing.Point(306, 113);
            this.Button_Connect.Name = "Button_Connect";
            this.Button_Connect.Size = new System.Drawing.Size(75, 23);
            this.Button_Connect.TabIndex = 2;
            this.Button_Connect.Text = "button1";
            this.Button_Connect.UseVisualStyleBackColor = true;
            this.Button_Connect.Click += new System.EventHandler(this.Button_Connect_Click);
            // 
            // TB_Connection
            // 
            this.TB_Connection.Location = new System.Drawing.Point(32, 44);
            this.TB_Connection.Name = "TB_Connection";
            this.TB_Connection.Size = new System.Drawing.Size(100, 20);
            this.TB_Connection.TabIndex = 3;
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 191);
            this.Controls.Add(this.TB_Connection);
            this.Controls.Add(this.Button_Connect);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.TextBox_Login);
            this.Name = "ConnectForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_Login;
        private System.Windows.Forms.TextBox TextBox_Password;
        private System.Windows.Forms.Button Button_Connect;
        private System.Windows.Forms.TextBox TB_Connection;
    }
}

