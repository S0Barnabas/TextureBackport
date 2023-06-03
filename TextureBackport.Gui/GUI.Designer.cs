namespace TextureBackport.Gui
{
    partial class GUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tboxSourceFile = new TextBox();
            label1 = new Label();
            btnSearchSourceFile = new Button();
            cbVersion = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            cbResolution = new ComboBox();
            chDefaultTemplates = new CheckBox();
            btnSearchTerrain = new Button();
            label4 = new Label();
            tboxTerrain = new TextBox();
            btnSearchItem = new Button();
            label5 = new Label();
            tboxItem = new TextBox();
            btnPort = new Button();
            pbarTerrain = new ProgressBar();
            label6 = new Label();
            label7 = new Label();
            pbarItems = new ProgressBar();
            label8 = new Label();
            pbarMobs = new ProgressBar();
            label9 = new Label();
            pbarArmor = new ProgressBar();
            btnSearchOutput = new Button();
            label10 = new Label();
            tboxOutput = new TextBox();
            SuspendLayout();
            // 
            // tboxSourceFile
            // 
            tboxSourceFile.Location = new Point(12, 27);
            tboxSourceFile.Name = "tboxSourceFile";
            tboxSourceFile.Size = new Size(300, 23);
            tboxSourceFile.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 1;
            label1.Text = "Source texture pack";
            // 
            // btnSearchSourceFile
            // 
            btnSearchSourceFile.Location = new Point(318, 27);
            btnSearchSourceFile.Name = "btnSearchSourceFile";
            btnSearchSourceFile.Size = new Size(35, 23);
            btnSearchSourceFile.TabIndex = 2;
            btnSearchSourceFile.Text = "...";
            btnSearchSourceFile.UseVisualStyleBackColor = true;
            // 
            // cbVersion
            // 
            cbVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVersion.FormattingEnabled = true;
            cbVersion.Location = new Point(12, 85);
            cbVersion.Name = "cbVersion";
            cbVersion.Size = new Size(121, 23);
            cbVersion.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 67);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 4;
            label2.Text = "Version";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(190, 67);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 6;
            label3.Text = "Resolution";
            // 
            // cbResolution
            // 
            cbResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            cbResolution.FormattingEnabled = true;
            cbResolution.Location = new Point(190, 85);
            cbResolution.Name = "cbResolution";
            cbResolution.Size = new Size(121, 23);
            cbResolution.TabIndex = 5;
            // 
            // chDefaultTemplates
            // 
            chDefaultTemplates.AutoSize = true;
            chDefaultTemplates.Location = new Point(13, 217);
            chDefaultTemplates.Name = "chDefaultTemplates";
            chDefaultTemplates.Size = new Size(142, 19);
            chDefaultTemplates.TabIndex = 7;
            chDefaultTemplates.Text = "Use built-in templates";
            chDefaultTemplates.UseVisualStyleBackColor = true;
            // 
            // btnSearchTerrain
            // 
            btnSearchTerrain.Location = new Point(318, 280);
            btnSearchTerrain.Name = "btnSearchTerrain";
            btnSearchTerrain.Size = new Size(35, 23);
            btnSearchTerrain.TabIndex = 10;
            btnSearchTerrain.Text = "...";
            btnSearchTerrain.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 262);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 9;
            label4.Text = "Terrain atlas";
            // 
            // tboxTerrain
            // 
            tboxTerrain.Location = new Point(12, 280);
            tboxTerrain.Name = "tboxTerrain";
            tboxTerrain.Size = new Size(300, 23);
            tboxTerrain.TabIndex = 8;
            // 
            // btnSearchItem
            // 
            btnSearchItem.Location = new Point(319, 341);
            btnSearchItem.Name = "btnSearchItem";
            btnSearchItem.Size = new Size(35, 23);
            btnSearchItem.TabIndex = 13;
            btnSearchItem.Text = "...";
            btnSearchItem.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 323);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 12;
            label5.Text = "Item atlas";
            // 
            // tboxItem
            // 
            tboxItem.Location = new Point(13, 341);
            tboxItem.Name = "tboxItem";
            tboxItem.Size = new Size(300, 23);
            tboxItem.TabIndex = 11;
            // 
            // btnPort
            // 
            btnPort.Location = new Point(11, 542);
            btnPort.Name = "btnPort";
            btnPort.Size = new Size(344, 23);
            btnPort.TabIndex = 14;
            btnPort.Text = "Port to Beta 1.7.3";
            btnPort.UseVisualStyleBackColor = true;
            // 
            // pbarTerrain
            // 
            pbarTerrain.Location = new Point(13, 412);
            pbarTerrain.Name = "pbarTerrain";
            pbarTerrain.Size = new Size(342, 10);
            pbarTerrain.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 394);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 16;
            label6.Text = "Terrain";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(13, 431);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 18;
            label7.Text = "Items";
            // 
            // pbarItems
            // 
            pbarItems.Location = new Point(13, 449);
            pbarItems.Name = "pbarItems";
            pbarItems.Size = new Size(342, 10);
            pbarItems.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 463);
            label8.Name = "label8";
            label8.Size = new Size(37, 15);
            label8.TabIndex = 20;
            label8.Text = "Mobs";
            // 
            // pbarMobs
            // 
            pbarMobs.Location = new Point(13, 481);
            pbarMobs.Name = "pbarMobs";
            pbarMobs.Size = new Size(342, 10);
            pbarMobs.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 499);
            label9.Name = "label9";
            label9.Size = new Size(41, 15);
            label9.TabIndex = 22;
            label9.Text = "Armor";
            // 
            // pbarArmor
            // 
            pbarArmor.Location = new Point(13, 517);
            pbarArmor.Name = "pbarArmor";
            pbarArmor.Size = new Size(342, 10);
            pbarArmor.TabIndex = 21;
            // 
            // btnSearchOutput
            // 
            btnSearchOutput.Location = new Point(318, 146);
            btnSearchOutput.Name = "btnSearchOutput";
            btnSearchOutput.Size = new Size(35, 23);
            btnSearchOutput.TabIndex = 25;
            btnSearchOutput.Text = "...";
            btnSearchOutput.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 128);
            label10.Name = "label10";
            label10.Size = new Size(95, 15);
            label10.TabIndex = 24;
            label10.Text = "Output directory";
            // 
            // tboxOutput
            // 
            tboxOutput.Location = new Point(12, 146);
            tboxOutput.Name = "tboxOutput";
            tboxOutput.Size = new Size(300, 23);
            tboxOutput.TabIndex = 23;
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 578);
            Controls.Add(btnSearchOutput);
            Controls.Add(label10);
            Controls.Add(tboxOutput);
            Controls.Add(label9);
            Controls.Add(pbarArmor);
            Controls.Add(label8);
            Controls.Add(pbarMobs);
            Controls.Add(label7);
            Controls.Add(pbarItems);
            Controls.Add(label6);
            Controls.Add(pbarTerrain);
            Controls.Add(btnPort);
            Controls.Add(btnSearchItem);
            Controls.Add(label5);
            Controls.Add(tboxItem);
            Controls.Add(btnSearchTerrain);
            Controls.Add(label4);
            Controls.Add(tboxTerrain);
            Controls.Add(chDefaultTemplates);
            Controls.Add(label3);
            Controls.Add(cbResolution);
            Controls.Add(label2);
            Controls.Add(cbVersion);
            Controls.Add(btnSearchSourceFile);
            Controls.Add(label1);
            Controls.Add(tboxSourceFile);
            Name = "GUI";
            Text = "Texture Backport GUI";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private TextBox tboxSourceFile;
        private Label label1;
        private Button btnSearchSourceFile;
        private ComboBox cbVersion;
        private Label label2;
        private Label label3;
        private ComboBox cbResolution;
        private CheckBox chDefaultTemplates;
        private Button btnSearchTerrain;
        private Label label4;
        private TextBox tboxTerrain;
        private Button btnSearchItem;
        private Label label5;
        private TextBox tboxItem;
        private Button btnPort;
        private ProgressBar pbarTerrain;
        private Label label6;
        private Label label7;
        private ProgressBar pbarItems;
        private Label label8;
        private ProgressBar pbarMobs;
        private Label label9;
        private ProgressBar pbarArmor;
        private Button btnSearchOutput;
        private Label label10;
        private TextBox tboxOutput;
    }
}