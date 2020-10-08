namespace StoDemoLauncher
{
    partial class RenderDemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenderDemoForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.screenShotGroupBox = new System.Windows.Forms.GroupBox();
            this.screenshotTypeLabel = new System.Windows.Forms.Label();
            this.screenshotTypeComboBox = new System.Windows.Forms.ComboBox();
            this.screenshotPrefixTextBox = new System.Windows.Forms.TextBox();
            this.screenshotPrefixLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.useGameResolutionCheckBox = new System.Windows.Forms.CheckBox();
            this.resolutionPresetsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.doubleResolutionCheckBox = new System.Windows.Forms.CheckBox();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.fovNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.shadowMapSizeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.shadowsCheckBox = new System.Windows.Forms.CheckBox();
            this.bloomQualityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.visScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.visScaleLabel = new System.Windows.Forms.Label();
            this.reduceMipMapsCheckBox = new System.Windows.Forms.CheckBox();
            this.reduceTexturesCheckBox = new System.Windows.Forms.CheckBox();
            this.highQualityDofCheckBox = new System.Windows.Forms.CheckBox();
            this.PostProcessingCheckBox = new System.Windows.Forms.CheckBox();
            this.chatBubblesCheckBox = new System.Windows.Forms.CheckBox();
            this.floatingTextCheckBox = new System.Windows.Forms.CheckBox();
            this.userInteraceCheckBox = new System.Windows.Forms.CheckBox();
            this.fpsCounterCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.warningsCheckBox = new System.Windows.Forms.CheckBox();
            this.hudCheckBox = new System.Windows.Forms.CheckBox();
            this.screenShotGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bloomQualityNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visScaleNumericUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(362, 239);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.ok_Event);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(443, 239);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancel_Event);
            // 
            // screenShotGroupBox
            // 
            this.screenShotGroupBox.Controls.Add(this.screenshotTypeLabel);
            this.screenShotGroupBox.Controls.Add(this.screenshotTypeComboBox);
            this.screenShotGroupBox.Controls.Add(this.screenshotPrefixTextBox);
            this.screenShotGroupBox.Controls.Add(this.screenshotPrefixLabel);
            this.screenShotGroupBox.Location = new System.Drawing.Point(12, 12);
            this.screenShotGroupBox.Name = "screenShotGroupBox";
            this.screenShotGroupBox.Size = new System.Drawing.Size(250, 72);
            this.screenShotGroupBox.TabIndex = 0;
            this.screenShotGroupBox.TabStop = false;
            this.screenShotGroupBox.Text = "Image Sequence";
            // 
            // screenshotTypeLabel
            // 
            this.screenshotTypeLabel.AutoSize = true;
            this.screenshotTypeLabel.Location = new System.Drawing.Point(6, 48);
            this.screenshotTypeLabel.Name = "screenshotTypeLabel";
            this.screenshotTypeLabel.Size = new System.Drawing.Size(58, 13);
            this.screenshotTypeLabel.TabIndex = 3;
            this.screenshotTypeLabel.Text = "File format:";
            // 
            // screenshotTypeComboBox
            // 
            this.screenshotTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenshotTypeComboBox.FormattingEnabled = true;
            this.screenshotTypeComboBox.Items.AddRange(new object[] {
            "JPEG (*.jpg)",
            "TGA (*.tga)"});
            this.screenshotTypeComboBox.Location = new System.Drawing.Point(81, 45);
            this.screenshotTypeComboBox.Name = "screenshotTypeComboBox";
            this.screenshotTypeComboBox.Size = new System.Drawing.Size(163, 21);
            this.screenshotTypeComboBox.TabIndex = 1;
            // 
            // screenshotPrefixTextBox
            // 
            this.screenshotPrefixTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenshotPrefixTextBox.Location = new System.Drawing.Point(81, 19);
            this.screenshotPrefixTextBox.Name = "screenshotPrefixTextBox";
            this.screenshotPrefixTextBox.Size = new System.Drawing.Size(163, 20);
            this.screenshotPrefixTextBox.TabIndex = 0;
            this.screenshotPrefixTextBox.TextChanged += new System.EventHandler(this.screenshotPrefixTextBox_TextChanged);
            // 
            // screenshotPrefixLabel
            // 
            this.screenshotPrefixLabel.AutoSize = true;
            this.screenshotPrefixLabel.Location = new System.Drawing.Point(6, 22);
            this.screenshotPrefixLabel.Name = "screenshotPrefixLabel";
            this.screenshotPrefixLabel.Size = new System.Drawing.Size(36, 13);
            this.screenshotPrefixLabel.TabIndex = 0;
            this.screenshotPrefixLabel.Text = "Prefix:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.useGameResolutionCheckBox);
            this.groupBox1.Controls.Add(this.resolutionPresetsComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.doubleResolutionCheckBox);
            this.groupBox1.Controls.Add(this.heightNumericUpDown);
            this.groupBox1.Controls.Add(this.widthNumericUpDown);
            this.groupBox1.Controls.Add(this.heightLabel);
            this.groupBox1.Controls.Add(this.widthLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 143);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resolution";
            // 
            // useGameResolutionCheckBox
            // 
            this.useGameResolutionCheckBox.AutoSize = true;
            this.useGameResolutionCheckBox.Location = new System.Drawing.Point(6, 19);
            this.useGameResolutionCheckBox.Name = "useGameResolutionCheckBox";
            this.useGameResolutionCheckBox.Size = new System.Drawing.Size(158, 17);
            this.useGameResolutionCheckBox.TabIndex = 15;
            this.useGameResolutionCheckBox.Text = "Use current game resolution";
            this.useGameResolutionCheckBox.UseVisualStyleBackColor = true;
            this.useGameResolutionCheckBox.CheckedChanged += new System.EventHandler(this.useGameResolutionCheckBox_CheckedChanged);
            // 
            // resolutionPresetsComboBox
            // 
            this.resolutionPresetsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resolutionPresetsComboBox.FormattingEnabled = true;
            this.resolutionPresetsComboBox.Items.AddRange(new object[] {
            "240p (4:3 320 x 240)",
            "360p (16:9 640 x 360)",
            "480p (4:3 640 x 480)",
            "720p (16:9 1280 x 720)",
            "1080p (16:9 1920 x 1080)",
            "Custom size"});
            this.resolutionPresetsComboBox.Location = new System.Drawing.Point(81, 42);
            this.resolutionPresetsComboBox.Name = "resolutionPresetsComboBox";
            this.resolutionPresetsComboBox.Size = new System.Drawing.Size(163, 21);
            this.resolutionPresetsComboBox.TabIndex = 0;
            this.resolutionPresetsComboBox.SelectedIndexChanged += new System.EventHandler(this.resolutionPresetsComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Presets:";
            // 
            // doubleResolutionCheckBox
            // 
            this.doubleResolutionCheckBox.AutoSize = true;
            this.doubleResolutionCheckBox.Checked = true;
            this.doubleResolutionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.doubleResolutionCheckBox.Location = new System.Drawing.Point(6, 122);
            this.doubleResolutionCheckBox.Name = "doubleResolutionCheckBox";
            this.doubleResolutionCheckBox.Size = new System.Drawing.Size(184, 17);
            this.doubleResolutionCheckBox.TabIndex = 11;
            this.doubleResolutionCheckBox.Text = "Supersampling (double resolution)";
            this.doubleResolutionCheckBox.UseVisualStyleBackColor = true;
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heightNumericUpDown.Location = new System.Drawing.Point(81, 96);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            5120,
            0,
            0,
            0});
            this.heightNumericUpDown.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(163, 20);
            this.heightNumericUpDown.TabIndex = 2;
            this.heightNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.heightNumericUpDown.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.heightNumericUpDown.ValueChanged += new System.EventHandler(this.resolutionChanged_Event);
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widthNumericUpDown.Location = new System.Drawing.Point(81, 70);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            5120,
            0,
            0,
            0});
            this.widthNumericUpDown.Minimum = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(163, 20);
            this.widthNumericUpDown.TabIndex = 1;
            this.widthNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            1280,
            0,
            0,
            0});
            this.widthNumericUpDown.ValueChanged += new System.EventHandler(this.resolutionChanged_Event);
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(6, 98);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 1;
            this.heightLabel.Text = "Height:";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(6, 72);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(38, 13);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width:";
            // 
            // fovNumericUpDown
            // 
            this.fovNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fovNumericUpDown.Location = new System.Drawing.Point(81, 237);
            this.fovNumericUpDown.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this.fovNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fovNumericUpDown.Name = "fovNumericUpDown";
            this.fovNumericUpDown.Size = new System.Drawing.Size(163, 20);
            this.fovNumericUpDown.TabIndex = 14;
            this.fovNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.fovNumericUpDown.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.fovNumericUpDown.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Field of view:";
            this.label2.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.shadowMapSizeComboBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.shadowsCheckBox);
            this.groupBox2.Controls.Add(this.bloomQualityNumericUpDown);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.visScaleNumericUpDown);
            this.groupBox2.Controls.Add(this.visScaleLabel);
            this.groupBox2.Controls.Add(this.reduceMipMapsCheckBox);
            this.groupBox2.Controls.Add(this.reduceTexturesCheckBox);
            this.groupBox2.Controls.Add(this.highQualityDofCheckBox);
            this.groupBox2.Controls.Add(this.PostProcessingCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(268, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 163);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quality";
            // 
            // shadowMapSizeComboBox
            // 
            this.shadowMapSizeComboBox.FormattingEnabled = true;
            this.shadowMapSizeComboBox.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048"});
            this.shadowMapSizeComboBox.Location = new System.Drawing.Point(128, 84);
            this.shadowMapSizeComboBox.Name = "shadowMapSizeComboBox";
            this.shadowMapSizeComboBox.Size = new System.Drawing.Size(116, 21);
            this.shadowMapSizeComboBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Shadow map size:";
            // 
            // shadowsCheckBox
            // 
            this.shadowsCheckBox.AutoSize = true;
            this.shadowsCheckBox.Checked = true;
            this.shadowsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shadowsCheckBox.Location = new System.Drawing.Point(6, 67);
            this.shadowsCheckBox.Name = "shadowsCheckBox";
            this.shadowsCheckBox.Size = new System.Drawing.Size(70, 17);
            this.shadowsCheckBox.TabIndex = 13;
            this.shadowsCheckBox.Text = "Shadows";
            this.shadowsCheckBox.UseVisualStyleBackColor = true;
            // 
            // bloomQualityNumericUpDown
            // 
            this.bloomQualityNumericUpDown.Location = new System.Drawing.Point(128, 111);
            this.bloomQualityNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bloomQualityNumericUpDown.Name = "bloomQualityNumericUpDown";
            this.bloomQualityNumericUpDown.Size = new System.Drawing.Size(116, 20);
            this.bloomQualityNumericUpDown.TabIndex = 12;
            this.bloomQualityNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bloomQualityNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Bloom quality:";
            // 
            // visScaleNumericUpDown
            // 
            this.visScaleNumericUpDown.Location = new System.Drawing.Point(128, 137);
            this.visScaleNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.visScaleNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.visScaleNumericUpDown.Name = "visScaleNumericUpDown";
            this.visScaleNumericUpDown.Size = new System.Drawing.Size(116, 20);
            this.visScaleNumericUpDown.TabIndex = 10;
            this.visScaleNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.visScaleNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // visScaleLabel
            // 
            this.visScaleLabel.AutoSize = true;
            this.visScaleLabel.Location = new System.Drawing.Point(6, 139);
            this.visScaleLabel.Name = "visScaleLabel";
            this.visScaleLabel.Size = new System.Drawing.Size(76, 13);
            this.visScaleLabel.TabIndex = 9;
            this.visScaleLabel.Text = "Level of detail:";
            // 
            // reduceMipMapsCheckBox
            // 
            this.reduceMipMapsCheckBox.AutoSize = true;
            this.reduceMipMapsCheckBox.Location = new System.Drawing.Point(128, 21);
            this.reduceMipMapsCheckBox.Name = "reduceMipMapsCheckBox";
            this.reduceMipMapsCheckBox.Size = new System.Drawing.Size(114, 17);
            this.reduceMipMapsCheckBox.TabIndex = 6;
            this.reduceMipMapsCheckBox.Text = "Reduce MIP maps";
            this.reduceMipMapsCheckBox.UseVisualStyleBackColor = true;
            // 
            // reduceTexturesCheckBox
            // 
            this.reduceTexturesCheckBox.AutoSize = true;
            this.reduceTexturesCheckBox.Location = new System.Drawing.Point(128, 44);
            this.reduceTexturesCheckBox.Name = "reduceTexturesCheckBox";
            this.reduceTexturesCheckBox.Size = new System.Drawing.Size(104, 17);
            this.reduceTexturesCheckBox.TabIndex = 3;
            this.reduceTexturesCheckBox.Text = "Reduce textures";
            this.reduceTexturesCheckBox.UseVisualStyleBackColor = true;
            // 
            // highQualityDofCheckBox
            // 
            this.highQualityDofCheckBox.AutoSize = true;
            this.highQualityDofCheckBox.Checked = true;
            this.highQualityDofCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.highQualityDofCheckBox.Location = new System.Drawing.Point(6, 44);
            this.highQualityDofCheckBox.Name = "highQualityDofCheckBox";
            this.highQualityDofCheckBox.Size = new System.Drawing.Size(104, 17);
            this.highQualityDofCheckBox.TabIndex = 1;
            this.highQualityDofCheckBox.Text = "High quality DoF";
            this.highQualityDofCheckBox.UseVisualStyleBackColor = true;
            // 
            // PostProcessingCheckBox
            // 
            this.PostProcessingCheckBox.AutoSize = true;
            this.PostProcessingCheckBox.Checked = true;
            this.PostProcessingCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PostProcessingCheckBox.Location = new System.Drawing.Point(6, 21);
            this.PostProcessingCheckBox.Name = "PostProcessingCheckBox";
            this.PostProcessingCheckBox.Size = new System.Drawing.Size(101, 17);
            this.PostProcessingCheckBox.TabIndex = 0;
            this.PostProcessingCheckBox.Text = "Post processing";
            this.PostProcessingCheckBox.UseVisualStyleBackColor = true;
            // 
            // chatBubblesCheckBox
            // 
            this.chatBubblesCheckBox.AutoSize = true;
            this.chatBubblesCheckBox.Location = new System.Drawing.Point(110, 249);
            this.chatBubblesCheckBox.Name = "chatBubblesCheckBox";
            this.chatBubblesCheckBox.Size = new System.Drawing.Size(88, 17);
            this.chatBubblesCheckBox.TabIndex = 13;
            this.chatBubblesCheckBox.Text = "Chat bubbles";
            this.chatBubblesCheckBox.UseVisualStyleBackColor = true;
            this.chatBubblesCheckBox.Visible = false;
            // 
            // floatingTextCheckBox
            // 
            this.floatingTextCheckBox.AutoSize = true;
            this.floatingTextCheckBox.Location = new System.Drawing.Point(232, 249);
            this.floatingTextCheckBox.Name = "floatingTextCheckBox";
            this.floatingTextCheckBox.Size = new System.Drawing.Size(83, 17);
            this.floatingTextCheckBox.TabIndex = 14;
            this.floatingTextCheckBox.Text = "Floating text";
            this.floatingTextCheckBox.UseVisualStyleBackColor = true;
            this.floatingTextCheckBox.Visible = false;
            // 
            // userInteraceCheckBox
            // 
            this.userInteraceCheckBox.AutoSize = true;
            this.userInteraceCheckBox.Location = new System.Drawing.Point(232, 226);
            this.userInteraceCheckBox.Name = "userInteraceCheckBox";
            this.userInteraceCheckBox.Size = new System.Drawing.Size(92, 17);
            this.userInteraceCheckBox.TabIndex = 15;
            this.userInteraceCheckBox.Text = "User interface";
            this.userInteraceCheckBox.UseVisualStyleBackColor = true;
            this.userInteraceCheckBox.Visible = false;
            // 
            // fpsCounterCheckBox
            // 
            this.fpsCounterCheckBox.AutoSize = true;
            this.fpsCounterCheckBox.Location = new System.Drawing.Point(6, 19);
            this.fpsCounterCheckBox.Name = "fpsCounterCheckBox";
            this.fpsCounterCheckBox.Size = new System.Drawing.Size(85, 17);
            this.fpsCounterCheckBox.TabIndex = 16;
            this.fpsCounterCheckBox.Text = "FPS counter";
            this.fpsCounterCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.warningsCheckBox);
            this.groupBox3.Controls.Add(this.fpsCounterCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(268, 181);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 52);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Overlays";
            // 
            // warningsCheckBox
            // 
            this.warningsCheckBox.AutoSize = true;
            this.warningsCheckBox.Location = new System.Drawing.Point(128, 19);
            this.warningsCheckBox.Name = "warningsCheckBox";
            this.warningsCheckBox.Size = new System.Drawing.Size(71, 17);
            this.warningsCheckBox.TabIndex = 17;
            this.warningsCheckBox.Text = "Warnings";
            this.warningsCheckBox.UseVisualStyleBackColor = true;
            // 
            // hudCheckBox
            // 
            this.hudCheckBox.AutoSize = true;
            this.hudCheckBox.Location = new System.Drawing.Point(110, 226);
            this.hudCheckBox.Name = "hudCheckBox";
            this.hudCheckBox.Size = new System.Drawing.Size(107, 17);
            this.hudCheckBox.TabIndex = 18;
            this.hudCheckBox.Text = "Heads up display";
            this.hudCheckBox.UseVisualStyleBackColor = true;
            this.hudCheckBox.Visible = false;
            // 
            // RenderDemoForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(530, 274);
            this.Controls.Add(this.hudCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chatBubblesCheckBox);
            this.Controls.Add(this.fovNumericUpDown);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.floatingTextCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userInteraceCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.screenShotGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenderDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Render Demo";
            this.screenShotGroupBox.ResumeLayout(false);
            this.screenShotGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bloomQualityNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visScaleNumericUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox screenShotGroupBox;
        private System.Windows.Forms.Label screenshotTypeLabel;
        private System.Windows.Forms.ComboBox screenshotTypeComboBox;
        private System.Windows.Forms.TextBox screenshotPrefixTextBox;
        private System.Windows.Forms.Label screenshotPrefixLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown widthNumericUpDown;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.ComboBox resolutionPresetsComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown heightNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox PostProcessingCheckBox;
        private System.Windows.Forms.CheckBox reduceTexturesCheckBox;
        private System.Windows.Forms.CheckBox highQualityDofCheckBox;
        private System.Windows.Forms.CheckBox reduceMipMapsCheckBox;
        private System.Windows.Forms.NumericUpDown visScaleNumericUpDown;
        private System.Windows.Forms.Label visScaleLabel;
        private System.Windows.Forms.NumericUpDown bloomQualityNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox doubleResolutionCheckBox;
        private System.Windows.Forms.CheckBox chatBubblesCheckBox;
        private System.Windows.Forms.CheckBox floatingTextCheckBox;
        private System.Windows.Forms.CheckBox userInteraceCheckBox;
        private System.Windows.Forms.CheckBox fpsCounterCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown fovNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox warningsCheckBox;
        private System.Windows.Forms.ComboBox shadowMapSizeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox shadowsCheckBox;
        private System.Windows.Forms.CheckBox hudCheckBox;
        private System.Windows.Forms.CheckBox useGameResolutionCheckBox;
    }
}