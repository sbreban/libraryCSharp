namespace client
{
    partial class LoginWindow
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
            this.userText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passText = new System.Windows.Forms.TextBox();
            this.loginBut = new System.Windows.Forms.Button();
            this.clearBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User";
            // 
            // userText
            // 
            this.userText.Location = new System.Drawing.Point(88, 6);
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(98, 20);
            this.userText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // passText
            // 
            this.passText.Location = new System.Drawing.Point(88, 30);
            this.passText.Name = "passText";
            this.passText.PasswordChar = '*';
            this.passText.Size = new System.Drawing.Size(98, 20);
            this.passText.TabIndex = 3;
            // 
            // loginBut
            // 
            this.loginBut.Location = new System.Drawing.Point(15, 64);
            this.loginBut.Name = "loginBut";
            this.loginBut.Size = new System.Drawing.Size(67, 27);
            this.loginBut.TabIndex = 4;
            this.loginBut.Text = "Login";
            this.loginBut.UseVisualStyleBackColor = true;
            this.loginBut.Click += new System.EventHandler(this.loginBut_Click);
            // 
            // clearBut
            // 
            this.clearBut.Location = new System.Drawing.Point(119, 64);
            this.clearBut.Name = "clearBut";
            this.clearBut.Size = new System.Drawing.Size(67, 27);
            this.clearBut.TabIndex = 5;
            this.clearBut.Text = "Clear";
            this.clearBut.UseVisualStyleBackColor = true;
            this.clearBut.Click += new System.EventHandler(this.clearBut_Click);
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 96);
            this.Controls.Add(this.clearBut);
            this.Controls.Add(this.loginBut);
            this.Controls.Add(this.passText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userText);
            this.Controls.Add(this.label1);
            this.Name = "LoginWindow";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passText;
        private System.Windows.Forms.Button loginBut;
        private System.Windows.Forms.Button clearBut;
    }
}

