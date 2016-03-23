namespace LRRMapExpander_Formed
{
    partial class Form1
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
            this.button_expand = new System.Windows.Forms.Button();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.textBox_y = new System.Windows.Forms.TextBox();
            this.label_x = new System.Windows.Forms.Label();
            this.label_y = new System.Windows.Forms.Label();
            this.textBox_output = new System.Windows.Forms.TextBox();
            this.checkBox_match = new System.Windows.Forms.CheckBox();
            this.label_match = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_expand
            // 
            this.button_expand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_expand.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_expand.Location = new System.Drawing.Point(262, 12);
            this.button_expand.Name = "button_expand";
            this.button_expand.Size = new System.Drawing.Size(104, 48);
            this.button_expand.TabIndex = 0;
            this.button_expand.Text = "EXPAND!";
            this.button_expand.UseVisualStyleBackColor = true;
            this.button_expand.Click += new System.EventHandler(this.Expand);
            // 
            // textBox_x
            // 
            this.textBox_x.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_x.Location = new System.Drawing.Point(145, 14);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(100, 20);
            this.textBox_x.TabIndex = 1;
            this.textBox_x.Text = "0";
            // 
            // textBox_y
            // 
            this.textBox_y.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_y.Location = new System.Drawing.Point(145, 40);
            this.textBox_y.Name = "textBox_y";
            this.textBox_y.Size = new System.Drawing.Size(100, 20);
            this.textBox_y.TabIndex = 2;
            this.textBox_y.Text = "0";
            // 
            // label_x
            // 
            this.label_x.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_x.AutoSize = true;
            this.label_x.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_x.Location = new System.Drawing.Point(13, 17);
            this.label_x.Name = "label_x";
            this.label_x.Size = new System.Drawing.Size(130, 13);
            this.label_x.TabIndex = 3;
            this.label_x.Text = "Horizontal Expansion:";
            // 
            // label_y
            // 
            this.label_y.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_y.AutoSize = true;
            this.label_y.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_y.Location = new System.Drawing.Point(27, 43);
            this.label_y.Name = "label_y";
            this.label_y.Size = new System.Drawing.Size(116, 13);
            this.label_y.TabIndex = 4;
            this.label_y.Text = "Vertical Expansion:";
            // 
            // textBox_output
            // 
            this.textBox_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_output.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_output.Location = new System.Drawing.Point(12, 97);
            this.textBox_output.Multiline = true;
            this.textBox_output.Name = "textBox_output";
            this.textBox_output.ReadOnly = true;
            this.textBox_output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_output.Size = new System.Drawing.Size(360, 296);
            this.textBox_output.TabIndex = 5;
            // 
            // checkBox_match
            // 
            this.checkBox_match.AutoSize = true;
            this.checkBox_match.Location = new System.Drawing.Point(145, 69);
            this.checkBox_match.Name = "checkBox_match";
            this.checkBox_match.Size = new System.Drawing.Size(15, 14);
            this.checkBox_match.TabIndex = 6;
            this.checkBox_match.UseVisualStyleBackColor = true;
            // 
            // label_match
            // 
            this.label_match.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_match.AutoSize = true;
            this.label_match.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_match.Location = new System.Drawing.Point(56, 69);
            this.label_match.Name = "label_match";
            this.label_match.Size = new System.Drawing.Size(87, 13);
            this.label_match.TabIndex = 7;
            this.label_match.Text = "Height Match:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 412);
            this.Controls.Add(this.label_match);
            this.Controls.Add(this.checkBox_match);
            this.Controls.Add(this.textBox_output);
            this.Controls.Add(this.label_y);
            this.Controls.Add(this.label_x);
            this.Controls.Add(this.textBox_y);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.button_expand);
            this.MinimumSize = new System.Drawing.Size(330, 160);
            this.Name = "Form1";
            this.Text = "LEGO Rock Raiders Map Expander";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_expand;
        private System.Windows.Forms.TextBox textBox_x;
        private System.Windows.Forms.TextBox textBox_y;
        private System.Windows.Forms.Label label_x;
        private System.Windows.Forms.Label label_y;
        private System.Windows.Forms.TextBox textBox_output;
        private System.Windows.Forms.CheckBox checkBox_match;
        private System.Windows.Forms.Label label_match;
    }
}

