namespace Enjoying.Logging.WindowsApp
{
    partial class LoggingExampleForm
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
            this.btnLogError = new System.Windows.Forms.Button();
            this.btnExample1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogError
            // 
            this.btnLogError.Location = new System.Drawing.Point(38, 35);
            this.btnLogError.Name = "btnLogError";
            this.btnLogError.Size = new System.Drawing.Size(75, 23);
            this.btnLogError.TabIndex = 1;
            this.btnLogError.Text = "Log Error";
            this.btnLogError.UseVisualStyleBackColor = true;
            this.btnLogError.Click += new System.EventHandler(this.btnLogError_Click);
            // 
            // btnExample1
            // 
            this.btnExample1.Location = new System.Drawing.Point(590, 35);
            this.btnExample1.Name = "btnExample1";
            this.btnExample1.Size = new System.Drawing.Size(75, 23);
            this.btnExample1.TabIndex = 2;
            this.btnExample1.Text = "RunToClick";
            this.btnExample1.UseVisualStyleBackColor = true;
            this.btnExample1.Click += new System.EventHandler(this.btnExample1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(590, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Exception";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(590, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Exception Read Key";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(590, 145);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Null Chain Exception";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // LoggingExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 256);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExample1);
            this.Controls.Add(this.btnLogError);
            this.Name = "LoggingExampleForm";
            this.Text = "Logging Example";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogError;
        private System.Windows.Forms.Button btnExample1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

