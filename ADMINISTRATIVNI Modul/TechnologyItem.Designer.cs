namespace ADMINISTRATIVNI_Modul
{
    partial class TechnologyItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Tech = new System.Windows.Forms.Label();
            this.tb_Tech = new System.Windows.Forms.TextBox();
            this.tb_Skill = new System.Windows.Forms.TextBox();
            this.lbl_Skill = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Tech
            // 
            this.lbl_Tech.AutoSize = true;
            this.lbl_Tech.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Tech.Location = new System.Drawing.Point(4, 7);
            this.lbl_Tech.Name = "lbl_Tech";
            this.lbl_Tech.Size = new System.Drawing.Size(93, 16);
            this.lbl_Tech.TabIndex = 0;
            this.lbl_Tech.Text = "Technology:";
            // 
            // tb_Tech
            // 
            this.tb_Tech.Location = new System.Drawing.Point(103, 4);
            this.tb_Tech.Name = "tb_Tech";
            this.tb_Tech.Size = new System.Drawing.Size(224, 22);
            this.tb_Tech.TabIndex = 1;
            // 
            // tb_Skill
            // 
            this.tb_Skill.Location = new System.Drawing.Point(103, 32);
            this.tb_Skill.Name = "tb_Skill";
            this.tb_Skill.Size = new System.Drawing.Size(224, 22);
            this.tb_Skill.TabIndex = 3;
            // 
            // lbl_Skill
            // 
            this.lbl_Skill.AutoSize = true;
            this.lbl_Skill.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Skill.Location = new System.Drawing.Point(4, 35);
            this.lbl_Skill.Name = "lbl_Skill";
            this.lbl_Skill.Size = new System.Drawing.Size(79, 16);
            this.lbl_Skill.TabIndex = 2;
            this.lbl_Skill.Text = "Skill level:";
            // 
            // TechnologyItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tb_Skill);
            this.Controls.Add(this.lbl_Skill);
            this.Controls.Add(this.tb_Tech);
            this.Controls.Add(this.lbl_Tech);
            this.Name = "TechnologyItem";
            this.Size = new System.Drawing.Size(333, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Tech;
        public System.Windows.Forms.TextBox tb_Tech;
        public System.Windows.Forms.TextBox tb_Skill;
        private System.Windows.Forms.Label lbl_Skill;
    }
}
