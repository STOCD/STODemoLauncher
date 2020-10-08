using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace StoDemoLauncher
{
    public partial class SplashScreen : Form
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static SplashScreen instance = null;

        /// <summary>
        /// The threat for this form
        /// </summary>
        private static Thread thread = null;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private SplashScreen()
        {
            InitializeComponent();
            this.versionLabel.Text += Application.ProductVersion;
        }

        /// <summary>
        /// Launches SplashScreen
        /// </summary>
        static private void ShowForm()
        {
            instance = new SplashScreen();
            Application.Run(instance);
        }
        
        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        static private void CloseForm() 
        {
            instance.Close();
            instance = null;
        }

        /// <summary>
        /// makes the Splash screen visible
        /// </summary>
        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (instance != null)
                return;
            thread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static public void CloseSplashScreen()
        {
            if (instance != null) instance.Invoke(new MethodInvoker(delegate { CloseForm(); }));
        }
    }
}
