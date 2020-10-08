namespace StoDemoLauncher
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clearMruListButton = new System.Windows.Forms.Button();
            this.mruCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.keepMruListCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.demosListViewGroupBox = new System.Windows.Forms.GroupBox();
            this.confirmDeleteCheckBox = new System.Windows.Forms.CheckBox();
            this.demosListViewEnterComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.demosListViewDoubleClickComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.demosListViewBoxLabel = new System.Windows.Forms.Label();
            this.generalTabLabel = new System.Windows.Forms.Label();
            this.autoUpdateGroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.autoUpdateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.autoUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.autoUpdateBoxLabel = new System.Windows.Forms.Label();
            this.advancedTabPage = new System.Windows.Forms.TabPage();
            this.advancedTabLabel = new System.Windows.Forms.Label();
            this.gameClientGroupBox = new System.Windows.Forms.GroupBox();
            this.gameClientBoxLabel = new System.Windows.Forms.Label();
            this.tribbleBrowseButton = new System.Windows.Forms.Button();
            this.holodeckPathBrowseButton = new System.Windows.Forms.Button();
            this.stoInstallationPathBrowseButton = new System.Windows.Forms.Button();
            this.tribblePathTextBox = new System.Windows.Forms.TextBox();
            this.holodeckPathTextBox = new System.Windows.Forms.TextBox();
            this.stoInstallationPathTextBox = new System.Windows.Forms.TextBox();
            this.stoInstallationPathLabel = new System.Windows.Forms.Label();
            this.tribbleInstallationPathLabel = new System.Windows.Forms.Label();
            this.holodeckPathLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.defaultsButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.redshirtBrowseButton = new System.Windows.Forms.Button();
            this.redshirtPathTextBox = new System.Windows.Forms.TextBox();
            this.redshirtInstallationPathLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mruCountNumericUpDown)).BeginInit();
            this.demosListViewGroupBox.SuspendLayout();
            this.autoUpdateGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoUpdateNumericUpDown)).BeginInit();
            this.advancedTabPage.SuspendLayout();
            this.gameClientGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.generalTabPage);
            this.tabControl.Controls.Add(this.advancedTabPage);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(530, 270);
            this.tabControl.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.groupBox1);
            this.generalTabPage.Controls.Add(this.demosListViewGroupBox);
            this.generalTabPage.Controls.Add(this.generalTabLabel);
            this.generalTabPage.Controls.Add(this.autoUpdateGroupBox);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalTabPage.Size = new System.Drawing.Size(522, 244);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clearMruListButton);
            this.groupBox1.Controls.Add(this.mruCountNumericUpDown);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.keepMruListCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(264, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 124);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recently Used File List";
            // 
            // clearMruListButton
            // 
            this.clearMruListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearMruListButton.Location = new System.Drawing.Point(171, 95);
            this.clearMruListButton.Name = "clearMruListButton";
            this.clearMruListButton.Size = new System.Drawing.Size(75, 23);
            this.clearMruListButton.TabIndex = 15;
            this.clearMruListButton.Text = "Clear List";
            this.clearMruListButton.UseVisualStyleBackColor = true;
            this.clearMruListButton.Click += new System.EventHandler(this.clearMruListButton_Click);
            // 
            // mruCountNumericUpDown
            // 
            this.mruCountNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mruCountNumericUpDown.Location = new System.Drawing.Point(188, 68);
            this.mruCountNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mruCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mruCountNumericUpDown.Name = "mruCountNumericUpDown";
            this.mruCountNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.mruCountNumericUpDown.TabIndex = 14;
            this.mruCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mruCountNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Show this number of recent files:";
            // 
            // keepMruListCheckBox
            // 
            this.keepMruListCheckBox.AutoSize = true;
            this.keepMruListCheckBox.Checked = true;
            this.keepMruListCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepMruListCheckBox.Location = new System.Drawing.Point(6, 45);
            this.keepMruListCheckBox.Name = "keepMruListCheckBox";
            this.keepMruListCheckBox.Size = new System.Drawing.Size(199, 17);
            this.keepMruListCheckBox.TabIndex = 12;
            this.keepMruListCheckBox.Text = "Keep a list of most recently used files";
            this.keepMruListCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "The demo launcher can maintain a list of most recently used demo files in the Fil" +
    "e menu.";
            // 
            // demosListViewGroupBox
            // 
            this.demosListViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.demosListViewGroupBox.Controls.Add(this.confirmDeleteCheckBox);
            this.demosListViewGroupBox.Controls.Add(this.demosListViewEnterComboBox);
            this.demosListViewGroupBox.Controls.Add(this.label3);
            this.demosListViewGroupBox.Controls.Add(this.demosListViewDoubleClickComboBox);
            this.demosListViewGroupBox.Controls.Add(this.label2);
            this.demosListViewGroupBox.Controls.Add(this.demosListViewBoxLabel);
            this.demosListViewGroupBox.Location = new System.Drawing.Point(6, 19);
            this.demosListViewGroupBox.Name = "demosListViewGroupBox";
            this.demosListViewGroupBox.Size = new System.Drawing.Size(252, 219);
            this.demosListViewGroupBox.TabIndex = 12;
            this.demosListViewGroupBox.TabStop = false;
            this.demosListViewGroupBox.Text = "Demos List View";
            // 
            // confirmDeleteCheckBox
            // 
            this.confirmDeleteCheckBox.AutoSize = true;
            this.confirmDeleteCheckBox.Checked = true;
            this.confirmDeleteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.confirmDeleteCheckBox.Location = new System.Drawing.Point(6, 150);
            this.confirmDeleteCheckBox.Name = "confirmDeleteCheckBox";
            this.confirmDeleteCheckBox.Size = new System.Drawing.Size(180, 17);
            this.confirmDeleteCheckBox.TabIndex = 15;
            this.confirmDeleteCheckBox.Text = "Display delete confimation dialog";
            this.confirmDeleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // demosListViewEnterComboBox
            // 
            this.demosListViewEnterComboBox.FormattingEnabled = true;
            this.demosListViewEnterComboBox.Items.AddRange(new object[] {
            "(None)",
            "Play Demo",
            "Edit Demo",
            "Render Demo",
            "Record Demo Audio",
            "Open Folder in File Browser"});
            this.demosListViewEnterComboBox.Location = new System.Drawing.Point(6, 123);
            this.demosListViewEnterComboBox.Name = "demosListViewEnterComboBox";
            this.demosListViewEnterComboBox.Size = new System.Drawing.Size(240, 21);
            this.demosListViewEnterComboBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Action on pressing \"Enter\" key:";
            // 
            // demosListViewDoubleClickComboBox
            // 
            this.demosListViewDoubleClickComboBox.FormattingEnabled = true;
            this.demosListViewDoubleClickComboBox.Items.AddRange(new object[] {
            "(None)",
            "Play Demo",
            "Edit Demo",
            "Render Demo",
            "Record Demo Audio",
            "Open Folder in File Browser"});
            this.demosListViewDoubleClickComboBox.Location = new System.Drawing.Point(6, 77);
            this.demosListViewDoubleClickComboBox.Name = "demosListViewDoubleClickComboBox";
            this.demosListViewDoubleClickComboBox.Size = new System.Drawing.Size(240, 21);
            this.demosListViewDoubleClickComboBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Action on mouse double-click:";
            // 
            // demosListViewBoxLabel
            // 
            this.demosListViewBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.demosListViewBoxLabel.Location = new System.Drawing.Point(6, 16);
            this.demosListViewBoxLabel.Name = "demosListViewBoxLabel";
            this.demosListViewBoxLabel.Size = new System.Drawing.Size(240, 39);
            this.demosListViewBoxLabel.TabIndex = 10;
            this.demosListViewBoxLabel.Text = "The demos list view is the primary display area of the demo launcher, where you c" +
    "an browse and manage demo files.";
            // 
            // generalTabLabel
            // 
            this.generalTabLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generalTabLabel.Location = new System.Drawing.Point(6, 3);
            this.generalTabLabel.Name = "generalTabLabel";
            this.generalTabLabel.Size = new System.Drawing.Size(510, 13);
            this.generalTabLabel.TabIndex = 11;
            this.generalTabLabel.Text = "You can configure the default behavior of the demo launcher on this page.";
            // 
            // autoUpdateGroupBox
            // 
            this.autoUpdateGroupBox.Controls.Add(this.label5);
            this.autoUpdateGroupBox.Controls.Add(this.autoUpdateNumericUpDown);
            this.autoUpdateGroupBox.Controls.Add(this.label4);
            this.autoUpdateGroupBox.Controls.Add(this.autoUpdateCheckBox);
            this.autoUpdateGroupBox.Controls.Add(this.autoUpdateBoxLabel);
            this.autoUpdateGroupBox.Location = new System.Drawing.Point(264, 19);
            this.autoUpdateGroupBox.Name = "autoUpdateGroupBox";
            this.autoUpdateGroupBox.Size = new System.Drawing.Size(252, 89);
            this.autoUpdateGroupBox.TabIndex = 0;
            this.autoUpdateGroupBox.TabStop = false;
            this.autoUpdateGroupBox.Text = "Auto-Update";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "hours.";
            // 
            // autoUpdateNumericUpDown
            // 
            this.autoUpdateNumericUpDown.Location = new System.Drawing.Point(82, 63);
            this.autoUpdateNumericUpDown.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.autoUpdateNumericUpDown.Name = "autoUpdateNumericUpDown";
            this.autoUpdateNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.autoUpdateNumericUpDown.TabIndex = 3;
            this.autoUpdateNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.autoUpdateNumericUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Search every";
            // 
            // autoUpdateCheckBox
            // 
            this.autoUpdateCheckBox.AutoSize = true;
            this.autoUpdateCheckBox.Checked = true;
            this.autoUpdateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoUpdateCheckBox.Location = new System.Drawing.Point(6, 45);
            this.autoUpdateCheckBox.Name = "autoUpdateCheckBox";
            this.autoUpdateCheckBox.Size = new System.Drawing.Size(179, 17);
            this.autoUpdateCheckBox.TabIndex = 1;
            this.autoUpdateCheckBox.Text = "Automatically search for updates";
            this.autoUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoUpdateBoxLabel
            // 
            this.autoUpdateBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoUpdateBoxLabel.Location = new System.Drawing.Point(6, 16);
            this.autoUpdateBoxLabel.Name = "autoUpdateBoxLabel";
            this.autoUpdateBoxLabel.Size = new System.Drawing.Size(240, 26);
            this.autoUpdateBoxLabel.TabIndex = 0;
            this.autoUpdateBoxLabel.Text = "The demo launcher can automatically search for updates on the internet.";
            // 
            // advancedTabPage
            // 
            this.advancedTabPage.Controls.Add(this.advancedTabLabel);
            this.advancedTabPage.Controls.Add(this.gameClientGroupBox);
            this.advancedTabPage.Location = new System.Drawing.Point(4, 22);
            this.advancedTabPage.Name = "advancedTabPage";
            this.advancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.advancedTabPage.Size = new System.Drawing.Size(522, 244);
            this.advancedTabPage.TabIndex = 1;
            this.advancedTabPage.Text = "Advanced";
            this.advancedTabPage.UseVisualStyleBackColor = true;
            // 
            // advancedTabLabel
            // 
            this.advancedTabLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedTabLabel.Location = new System.Drawing.Point(6, 3);
            this.advancedTabLabel.Name = "advancedTabLabel";
            this.advancedTabLabel.Size = new System.Drawing.Size(510, 39);
            this.advancedTabLabel.TabIndex = 10;
            this.advancedTabLabel.Text = resources.GetString("advancedTabLabel.Text");
            // 
            // gameClientGroupBox
            // 
            this.gameClientGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameClientGroupBox.Controls.Add(this.redshirtBrowseButton);
            this.gameClientGroupBox.Controls.Add(this.redshirtPathTextBox);
            this.gameClientGroupBox.Controls.Add(this.redshirtInstallationPathLabel);
            this.gameClientGroupBox.Controls.Add(this.gameClientBoxLabel);
            this.gameClientGroupBox.Controls.Add(this.tribbleBrowseButton);
            this.gameClientGroupBox.Controls.Add(this.holodeckPathBrowseButton);
            this.gameClientGroupBox.Controls.Add(this.stoInstallationPathBrowseButton);
            this.gameClientGroupBox.Controls.Add(this.tribblePathTextBox);
            this.gameClientGroupBox.Controls.Add(this.holodeckPathTextBox);
            this.gameClientGroupBox.Controls.Add(this.stoInstallationPathTextBox);
            this.gameClientGroupBox.Controls.Add(this.stoInstallationPathLabel);
            this.gameClientGroupBox.Controls.Add(this.tribbleInstallationPathLabel);
            this.gameClientGroupBox.Controls.Add(this.holodeckPathLabel);
            this.gameClientGroupBox.Location = new System.Drawing.Point(6, 46);
            this.gameClientGroupBox.Name = "gameClientGroupBox";
            this.gameClientGroupBox.Size = new System.Drawing.Size(510, 163);
            this.gameClientGroupBox.TabIndex = 1;
            this.gameClientGroupBox.TabStop = false;
            this.gameClientGroupBox.Text = "Game Client";
            // 
            // gameClientBoxLabel
            // 
            this.gameClientBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameClientBoxLabel.Location = new System.Drawing.Point(6, 16);
            this.gameClientBoxLabel.Name = "gameClientBoxLabel";
            this.gameClientBoxLabel.Size = new System.Drawing.Size(498, 26);
            this.gameClientBoxLabel.TabIndex = 9;
            this.gameClientBoxLabel.Text = "In order to function properly, the demo launcher relies on the executable files o" +
    "f Star Trek Online. You can manually set the paths to the game\'s installation fo" +
    "lders.";
            // 
            // tribbleBrowseButton
            // 
            this.tribbleBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tribbleBrowseButton.Location = new System.Drawing.Point(429, 105);
            this.tribbleBrowseButton.Name = "tribbleBrowseButton";
            this.tribbleBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.tribbleBrowseButton.TabIndex = 8;
            this.tribbleBrowseButton.Text = "Browse...";
            this.tribbleBrowseButton.UseVisualStyleBackColor = true;
            this.tribbleBrowseButton.Click += new System.EventHandler(this.browseTribblePathBrowse_Event);
            // 
            // holodeckPathBrowseButton
            // 
            this.holodeckPathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.holodeckPathBrowseButton.Location = new System.Drawing.Point(429, 76);
            this.holodeckPathBrowseButton.Name = "holodeckPathBrowseButton";
            this.holodeckPathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.holodeckPathBrowseButton.TabIndex = 7;
            this.holodeckPathBrowseButton.Text = "Browse...";
            this.holodeckPathBrowseButton.UseVisualStyleBackColor = true;
            this.holodeckPathBrowseButton.Click += new System.EventHandler(this.browseHolodeckPathBrowse_Event);
            // 
            // stoInstallationPathBrowseButton
            // 
            this.stoInstallationPathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stoInstallationPathBrowseButton.Location = new System.Drawing.Point(429, 47);
            this.stoInstallationPathBrowseButton.Name = "stoInstallationPathBrowseButton";
            this.stoInstallationPathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.stoInstallationPathBrowseButton.TabIndex = 6;
            this.stoInstallationPathBrowseButton.Text = "Browse...";
            this.stoInstallationPathBrowseButton.UseVisualStyleBackColor = true;
            this.stoInstallationPathBrowseButton.Click += new System.EventHandler(this.browseStoInstallationPathBrowse_Event);
            // 
            // tribblePathTextBox
            // 
            this.tribblePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tribblePathTextBox.Location = new System.Drawing.Point(149, 107);
            this.tribblePathTextBox.Name = "tribblePathTextBox";
            this.tribblePathTextBox.Size = new System.Drawing.Size(274, 20);
            this.tribblePathTextBox.TabIndex = 5;
            // 
            // holodeckPathTextBox
            // 
            this.holodeckPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.holodeckPathTextBox.Location = new System.Drawing.Point(149, 78);
            this.holodeckPathTextBox.Name = "holodeckPathTextBox";
            this.holodeckPathTextBox.Size = new System.Drawing.Size(274, 20);
            this.holodeckPathTextBox.TabIndex = 4;
            // 
            // stoInstallationPathTextBox
            // 
            this.stoInstallationPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stoInstallationPathTextBox.Location = new System.Drawing.Point(149, 49);
            this.stoInstallationPathTextBox.Name = "stoInstallationPathTextBox";
            this.stoInstallationPathTextBox.Size = new System.Drawing.Size(274, 20);
            this.stoInstallationPathTextBox.TabIndex = 3;
            // 
            // stoInstallationPathLabel
            // 
            this.stoInstallationPathLabel.AutoSize = true;
            this.stoInstallationPathLabel.Location = new System.Drawing.Point(6, 52);
            this.stoInstallationPathLabel.Name = "stoInstallationPathLabel";
            this.stoInstallationPathLabel.Size = new System.Drawing.Size(111, 13);
            this.stoInstallationPathLabel.TabIndex = 2;
            this.stoInstallationPathLabel.Text = "Game launcher folder:";
            // 
            // tribbleInstallationPathLabel
            // 
            this.tribbleInstallationPathLabel.AutoSize = true;
            this.tribbleInstallationPathLabel.Location = new System.Drawing.Point(6, 110);
            this.tribbleInstallationPathLabel.Name = "tribbleInstallationPathLabel";
            this.tribbleInstallationPathLabel.Size = new System.Drawing.Size(123, 13);
            this.tribbleInstallationPathLabel.TabIndex = 1;
            this.tribbleInstallationPathLabel.Text = "Tribble installation folder:";
            // 
            // holodeckPathLabel
            // 
            this.holodeckPathLabel.AutoSize = true;
            this.holodeckPathLabel.Location = new System.Drawing.Point(6, 81);
            this.holodeckPathLabel.Name = "holodeckPathLabel";
            this.holodeckPathLabel.Size = new System.Drawing.Size(137, 13);
            this.holodeckPathLabel.TabIndex = 0;
            this.holodeckPathLabel.Text = "Holodeck installation folder:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(386, 288);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.ok_Event);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(467, 288);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // defaultsButton
            // 
            this.defaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.defaultsButton.Location = new System.Drawing.Point(12, 288);
            this.defaultsButton.Name = "defaultsButton";
            this.defaultsButton.Size = new System.Drawing.Size(75, 23);
            this.defaultsButton.TabIndex = 3;
            this.defaultsButton.Text = "Defaults";
            this.defaultsButton.UseVisualStyleBackColor = true;
            this.defaultsButton.Click += new System.EventHandler(this.defaults_Event);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // redshirtBrowseButton
            // 
            this.redshirtBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.redshirtBrowseButton.Location = new System.Drawing.Point(429, 134);
            this.redshirtBrowseButton.Name = "redshirtBrowseButton";
            this.redshirtBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.redshirtBrowseButton.TabIndex = 12;
            this.redshirtBrowseButton.Text = "Browse...";
            this.redshirtBrowseButton.UseVisualStyleBackColor = true;
            this.redshirtBrowseButton.Click += new System.EventHandler(this.browseRedshirtPathBrowse_Event);
            // 
            // redshirtPathTextBox
            // 
            this.redshirtPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.redshirtPathTextBox.Location = new System.Drawing.Point(149, 136);
            this.redshirtPathTextBox.Name = "redshirtPathTextBox";
            this.redshirtPathTextBox.Size = new System.Drawing.Size(274, 20);
            this.redshirtPathTextBox.TabIndex = 11;
            // 
            // redshirtInstallationPathLabel
            // 
            this.redshirtInstallationPathLabel.AutoSize = true;
            this.redshirtInstallationPathLabel.Location = new System.Drawing.Point(6, 139);
            this.redshirtInstallationPathLabel.Name = "redshirtInstallationPathLabel";
            this.redshirtInstallationPathLabel.Size = new System.Drawing.Size(130, 13);
            this.redshirtInstallationPathLabel.TabIndex = 10;
            this.redshirtInstallationPathLabel.Text = "Redshirt installation folder:";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(554, 323);
            this.Controls.Add(this.defaultsButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Load_Event);
            this.tabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mruCountNumericUpDown)).EndInit();
            this.demosListViewGroupBox.ResumeLayout(false);
            this.demosListViewGroupBox.PerformLayout();
            this.autoUpdateGroupBox.ResumeLayout(false);
            this.autoUpdateGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoUpdateNumericUpDown)).EndInit();
            this.advancedTabPage.ResumeLayout(false);
            this.gameClientGroupBox.ResumeLayout(false);
            this.gameClientGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button defaultsButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabPage advancedTabPage;
        private System.Windows.Forms.Label advancedTabLabel;
        private System.Windows.Forms.GroupBox gameClientGroupBox;
        private System.Windows.Forms.Label gameClientBoxLabel;
        private System.Windows.Forms.Button tribbleBrowseButton;
        private System.Windows.Forms.Button holodeckPathBrowseButton;
        private System.Windows.Forms.Button stoInstallationPathBrowseButton;
        private System.Windows.Forms.TextBox tribblePathTextBox;
        private System.Windows.Forms.TextBox holodeckPathTextBox;
        private System.Windows.Forms.TextBox stoInstallationPathTextBox;
        private System.Windows.Forms.Label stoInstallationPathLabel;
        private System.Windows.Forms.Label tribbleInstallationPathLabel;
        private System.Windows.Forms.Label holodeckPathLabel;
        private System.Windows.Forms.GroupBox autoUpdateGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown autoUpdateNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox autoUpdateCheckBox;
        private System.Windows.Forms.Label autoUpdateBoxLabel;
        private System.Windows.Forms.Label generalTabLabel;
        private System.Windows.Forms.GroupBox demosListViewGroupBox;
        private System.Windows.Forms.ComboBox demosListViewDoubleClickComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label demosListViewBoxLabel;
        private System.Windows.Forms.ComboBox demosListViewEnterComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox keepMruListCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearMruListButton;
        private System.Windows.Forms.NumericUpDown mruCountNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox confirmDeleteCheckBox;
        private System.Windows.Forms.Button redshirtBrowseButton;
        private System.Windows.Forms.TextBox redshirtPathTextBox;
        private System.Windows.Forms.Label redshirtInstallationPathLabel;
    }
}