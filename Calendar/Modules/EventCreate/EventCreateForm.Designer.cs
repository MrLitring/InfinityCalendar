namespace Calendare.Modules.EventCreate
{
    partial class EventCreateForm
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
            this.label_ID = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label_Count1 = new System.Windows.Forms.Label();
            this.label_Count2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_ID
            // 
            this.label_ID.Location = new System.Drawing.Point(12, 9);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(250, 30);
            this.label_ID.TabIndex = 0;
            this.label_ID.Text = "id : ";
            this.label_ID.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 78);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Название";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 165);
            this.textBox2.MaxLength = 100;
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(250, 129);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Описание";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 371);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(12, 337);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 28);
            this.button2.TabIndex = 7;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(171, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Уведомлять о наступлении?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label_Count1
            // 
            this.label_Count1.Location = new System.Drawing.Point(198, 101);
            this.label_Count1.Name = "label_Count1";
            this.label_Count1.Size = new System.Drawing.Size(64, 20);
            this.label_Count1.TabIndex = 11;
            this.label_Count1.Text = "0/X";
            this.label_Count1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_Count2
            // 
            this.label_Count2.Location = new System.Drawing.Point(198, 297);
            this.label_Count2.Name = "label_Count2";
            this.label_Count2.Size = new System.Drawing.Size(64, 20);
            this.label_Count2.TabIndex = 12;
            this.label_Count2.Text = "0/X";
            this.label_Count2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // EventCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 433);
            this.Controls.Add(this.label_Count2);
            this.Controls.Add(this.label_Count1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_ID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EventCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EventCreateForm";
            this.Load += new System.EventHandler(this.EventCreateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label_Count1;
        private System.Windows.Forms.Label label_Count2;
    }
}