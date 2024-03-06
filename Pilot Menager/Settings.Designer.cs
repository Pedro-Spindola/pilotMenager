namespace Pilot_Menager
{
    partial class TelaSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaSettings));
            comboBoxSelectCor = new ComboBox();
            panel4 = new Panel();
            label1 = new Label();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxSelectCor
            // 
            comboBoxSelectCor.Anchor = AnchorStyles.Left;
            comboBoxSelectCor.DropDownHeight = 150;
            comboBoxSelectCor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSelectCor.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxSelectCor.FormattingEnabled = true;
            comboBoxSelectCor.IntegralHeight = false;
            comboBoxSelectCor.ItemHeight = 17;
            comboBoxSelectCor.Items.AddRange(new object[] { "Preto", "Branco", "Automatico" });
            comboBoxSelectCor.Location = new Point(267, 173);
            comboBoxSelectCor.Margin = new Padding(0);
            comboBoxSelectCor.Name = "comboBoxSelectCor";
            comboBoxSelectCor.Size = new Size(280, 25);
            comboBoxSelectCor.TabIndex = 1;
            comboBoxSelectCor.SelectedIndexChanged += comboBoxSelectCor_SelectedIndexChanged;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(230, 240, 240);
            panel4.Controls.Add(label1);
            panel4.Location = new Point(20, 350);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(760, 50);
            panel4.TabIndex = 14;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(760, 50);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // TelaSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(80, 80, 80);
            ClientSize = new Size(800, 420);
            ControlBox = false;
            Controls.Add(panel4);
            Controls.Add(comboBoxSelectCor);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TelaSettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TelaSettings";
            Load += TelaSettings_Load;
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBoxSelectCor;
        private Panel panel4;
        private Label label1;
    }
}