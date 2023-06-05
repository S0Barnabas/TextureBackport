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
            btnPort = new Button();
            btnSearchOutput = new Button();
            label10 = new Label();
            tboxOutput = new TextBox();
            label4 = new Label();
            cbResourceMap = new ComboBox();
            tboxProgress = new TextBox();
            label5 = new Label();
            label6 = new Label();
            cbLogLevel = new ComboBox();
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
            // btnPort
            // 
            btnPort.Location = new Point(12, 456);
            btnPort.Name = "btnPort";
            btnPort.Size = new Size(341, 23);
            btnPort.TabIndex = 14;
            btnPort.Text = "Port to Beta 1.7.3";
            btnPort.UseVisualStyleBackColor = true;
            // 
            // btnSearchOutput
            // 
            btnSearchOutput.Location = new Point(318, 206);
            btnSearchOutput.Name = "btnSearchOutput";
            btnSearchOutput.Size = new Size(35, 23);
            btnSearchOutput.TabIndex = 25;
            btnSearchOutput.Text = "...";
            btnSearchOutput.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 188);
            label10.Name = "label10";
            label10.Size = new Size(95, 15);
            label10.TabIndex = 24;
            label10.Text = "Output directory";
            // 
            // tboxOutput
            // 
            tboxOutput.Location = new Point(12, 206);
            tboxOutput.Name = "tboxOutput";
            tboxOutput.Size = new Size(300, 23);
            tboxOutput.TabIndex = 23;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 129);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 27;
            label4.Text = "Resource map";
            // 
            // cbResourceMap
            // 
            cbResourceMap.DropDownStyle = ComboBoxStyle.DropDownList;
            cbResourceMap.FormattingEnabled = true;
            cbResourceMap.Location = new Point(12, 147);
            cbResourceMap.Name = "cbResourceMap";
            cbResourceMap.Size = new Size(299, 23);
            cbResourceMap.TabIndex = 26;
            // 
            // tboxProgress
            // 
            tboxProgress.Location = new Point(394, 27);
            tboxProgress.Multiline = true;
            tboxProgress.Name = "tboxProgress";
            tboxProgress.ReadOnly = true;
            tboxProgress.ScrollBars = ScrollBars.Vertical;
            tboxProgress.Size = new Size(484, 452);
            tboxProgress.TabIndex = 28;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(394, 9);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 29;
            label5.Text = "Progress log";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 251);
            label6.Name = "label6";
            label6.Size = new Size(54, 15);
            label6.TabIndex = 31;
            label6.Text = "Log level";
            // 
            // cbLogLevel
            // 
            cbLogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLogLevel.FormattingEnabled = true;
            cbLogLevel.Location = new Point(12, 269);
            cbLogLevel.Name = "cbLogLevel";
            cbLogLevel.Size = new Size(121, 23);
            cbLogLevel.TabIndex = 30;
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(890, 491);
            Controls.Add(label6);
            Controls.Add(cbLogLevel);
            Controls.Add(label5);
            Controls.Add(tboxProgress);
            Controls.Add(label4);
            Controls.Add(cbResourceMap);
            Controls.Add(btnSearchOutput);
            Controls.Add(label10);
            Controls.Add(tboxOutput);
            Controls.Add(btnPort);
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
        private Button btnPort;
        private Button btnSearchOutput;
        private Label label10;
        private TextBox tboxOutput;
        private Label label4;
        private ComboBox cbResourceMap;
        private TextBox tboxProgress;
        private Label label5;
        private Label label6;
        private ComboBox cbLogLevel;
    }
}