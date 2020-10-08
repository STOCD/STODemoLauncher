using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StoDemoLauncher
{
    public partial class RenameDemoForm : Form
    {
        /// <summary>
        /// The text entered by the user in the text box
        /// </summary>
        public string UserText
        {
            get
            {
                return this.textBox.Text;
            }
            set
            {
                this.textBox.Text = value;
            }
        }

        /// <summary>
        /// Creates a new RenameDemoForm
        /// </summary>
        /// <param name="initialUserText"></param>
        public RenameDemoForm(string initialUserText)
        {
            InitializeComponent();
            this.UserText = initialUserText;
            this.textBox.SelectAll();
        }

        /// <summary>
        /// Callback for "TextChanged" event from textBox
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event Arguments</param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
               ((TextBox)sender).Undo();
               MessageBox.Show(
                    this,
                    "You may not use the following charactes in a file name:\n" +
                    "\\ / : * ? \" < > |",
                    "Invalid Characters",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
