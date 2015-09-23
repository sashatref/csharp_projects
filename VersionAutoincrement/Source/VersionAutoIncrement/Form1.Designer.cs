namespace VersionAutoIncrement
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ui_saveHFile = new System.Windows.Forms.Button();
            this.ui_okButton = new System.Windows.Forms.Button();
            this.ui_propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.74627F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.25373F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.ui_saveHFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ui_okButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 464);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 53);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // ui_saveHFile
            // 
            this.ui_saveHFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_saveHFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ui_saveHFile.Location = new System.Drawing.Point(3, 3);
            this.ui_saveHFile.Name = "ui_saveHFile";
            this.ui_saveHFile.Size = new System.Drawing.Size(231, 47);
            this.ui_saveHFile.TabIndex = 4;
            this.ui_saveHFile.Text = "Save buildNumber.h";
            this.ui_saveHFile.UseVisualStyleBackColor = true;
            this.ui_saveHFile.Click += new System.EventHandler(this.ui_saveHFile_Click);
            // 
            // ui_okButton
            // 
            this.ui_okButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ui_okButton.Location = new System.Drawing.Point(240, 3);
            this.ui_okButton.Name = "ui_okButton";
            this.ui_okButton.Size = new System.Drawing.Size(225, 47);
            this.ui_okButton.TabIndex = 0;
            this.ui_okButton.Text = "Save to rc";
            this.ui_okButton.UseVisualStyleBackColor = true;
            this.ui_okButton.Click += new System.EventHandler(this.ui_okButton_Click);
            // 
            // ui_propertyGrid
            // 
            this.ui_propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.ui_propertyGrid.Name = "ui_propertyGrid";
            this.ui_propertyGrid.Size = new System.Drawing.Size(468, 464);
            this.ui_propertyGrid.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 517);
            this.Controls.Add(this.ui_propertyGrid);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "RC file editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ui_okButton;
        private System.Windows.Forms.PropertyGrid ui_propertyGrid;
        private System.Windows.Forms.Button ui_saveHFile;
    }
}

