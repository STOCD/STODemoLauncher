namespace StoDemoLauncher.Filters
{
    partial class RemoveSectionsFileControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.messagesGroupBox = new System.Windows.Forms.GroupBox();
            this.selectNoneButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.sectionsListBox = new System.Windows.Forms.ListBox();
            this.filterListTextBox = new System.Windows.Forms.TextBox();
            this.filterListLabel = new System.Windows.Forms.Label();
            this.messagesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // messagesGroupBox
            // 
            this.messagesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesGroupBox.Controls.Add(this.filterListLabel);
            this.messagesGroupBox.Controls.Add(this.filterListTextBox);
            this.messagesGroupBox.Controls.Add(this.selectNoneButton);
            this.messagesGroupBox.Controls.Add(this.label1);
            this.messagesGroupBox.Controls.Add(this.selectAllButton);
            this.messagesGroupBox.Controls.Add(this.sectionsListBox);
            this.messagesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.messagesGroupBox.Name = "messagesGroupBox";
            this.messagesGroupBox.Size = new System.Drawing.Size(328, 240);
            this.messagesGroupBox.TabIndex = 4;
            this.messagesGroupBox.TabStop = false;
            this.messagesGroupBox.Text = "Search Results";
            // 
            // selectNoneButton
            // 
            this.selectNoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectNoneButton.Location = new System.Drawing.Point(166, 211);
            this.selectNoneButton.Name = "selectNoneButton";
            this.selectNoneButton.Size = new System.Drawing.Size(75, 23);
            this.selectNoneButton.TabIndex = 1;
            this.selectNoneButton.Text = "Select None";
            this.selectNoneButton.UseVisualStyleBackColor = true;
            this.selectNoneButton.Click += new System.EventHandler(this.selectNone_Event);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select the elements to be removed from the demo file.";
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllButton.Location = new System.Drawing.Point(247, 211);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 2;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAll_Event);
            // 
            // sectionsListBox
            // 
            this.sectionsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sectionsListBox.FormattingEnabled = true;
            this.sectionsListBox.IntegralHeight = false;
            this.sectionsListBox.Location = new System.Drawing.Point(7, 36);
            this.sectionsListBox.Name = "sectionsListBox";
            this.sectionsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.sectionsListBox.Size = new System.Drawing.Size(315, 169);
            this.sectionsListBox.TabIndex = 0;
            this.sectionsListBox.SelectedIndexChanged += new System.EventHandler(this.notificationsListBox_SelectedIndexChanged);
            // 
            // filterListTextBox
            // 
            this.filterListTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterListTextBox.Location = new System.Drawing.Point(59, 213);
            this.filterListTextBox.Name = "filterListTextBox";
            this.filterListTextBox.Size = new System.Drawing.Size(101, 20);
            this.filterListTextBox.TabIndex = 3;
            this.filterListTextBox.TextChanged += new System.EventHandler(this.filterListTextBox_TextChanged);
            // 
            // filterListLabel
            // 
            this.filterListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filterListLabel.AutoSize = true;
            this.filterListLabel.Location = new System.Drawing.Point(6, 216);
            this.filterListLabel.Name = "filterListLabel";
            this.filterListLabel.Size = new System.Drawing.Size(47, 13);
            this.filterListLabel.TabIndex = 4;
            this.filterListLabel.Text = "Filter list:";
            // 
            // RemoveSectionsFileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.messagesGroupBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RemoveSectionsFileControl";
            this.Size = new System.Drawing.Size(328, 240);
            this.Load += new System.EventHandler(this.RemoveMessagesFileControl_Load);
            this.messagesGroupBox.ResumeLayout(false);
            this.messagesGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox messagesGroupBox;
        private System.Windows.Forms.Button selectNoneButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.ListBox sectionsListBox;
        private System.Windows.Forms.Label filterListLabel;
        private System.Windows.Forms.TextBox filterListTextBox;
    }
}
