
namespace kljmhnbgfvcd
{
    partial class Archiver
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
            this.Archive_button = new System.Windows.Forms.Button();
            this.Unarchive_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // Archive_button
            // 
            this.Archive_button.Location = new System.Drawing.Point(82, 68);
            this.Archive_button.Name = "Archive_button";
            this.Archive_button.Size = new System.Drawing.Size(124, 37);
            this.Archive_button.TabIndex = 0;
            this.Archive_button.Text = "Архивировать";
            this.Archive_button.UseVisualStyleBackColor = true;
            this.Archive_button.Click += new System.EventHandler(this.Archive_button_Click);
            // 
            // Unarchive_button
            // 
            this.Unarchive_button.Location = new System.Drawing.Point(82, 136);
            this.Unarchive_button.Name = "Unarchive_button";
            this.Unarchive_button.Size = new System.Drawing.Size(124, 37);
            this.Unarchive_button.TabIndex = 1;
            this.Unarchive_button.Text = "Разархивировать";
            this.Unarchive_button.UseVisualStyleBackColor = true;
            this.Unarchive_button.Click += new System.EventHandler(this.Unarchive_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Archiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 273);
            this.Controls.Add(this.Unarchive_button);
            this.Controls.Add(this.Archive_button);
            this.Name = "Archiver";
            this.Text = "Архиватор ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Archive_button;
        private System.Windows.Forms.Button Unarchive_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

