namespace ProgrammUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ui_progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ui_statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ui_textLog = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ui_updateButton = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ui_progressBar,
            this.ui_statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 391);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(467, 30);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ui_progressBar
            // 
            this.ui_progressBar.Name = "ui_progressBar";
            this.ui_progressBar.Size = new System.Drawing.Size(200, 24);
            // 
            // ui_statusLabel
            // 
            this.ui_statusLabel.Name = "ui_statusLabel";
            this.ui_statusLabel.Size = new System.Drawing.Size(42, 25);
            this.ui_statusLabel.Text = "Ready!";
            // 
            // ui_textLog
            // 
            this.ui_textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_textLog.Location = new System.Drawing.Point(0, 24);
            this.ui_textLog.Name = "ui_textLog";
            this.ui_textLog.Size = new System.Drawing.Size(467, 367);
            this.ui_textLog.TabIndex = 1;
            this.ui_textLog.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ui_updateButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(467, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ui_updateButton
            // 
            this.ui_updateButton.Name = "ui_updateButton";
            this.ui_updateButton.Size = new System.Drawing.Size(57, 20);
            this.ui_updateButton.Text = "Update";
            this.ui_updateButton.Click += new System.EventHandler(this.ui_updateButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 421);
            this.Controls.Add(this.ui_textLog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Updater";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar ui_progressBar;
        private System.Windows.Forms.ToolStripStatusLabel ui_statusLabel;
        private System.Windows.Forms.RichTextBox ui_textLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ui_updateButton;
    }
}

