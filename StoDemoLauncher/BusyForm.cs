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
    public partial class BusyForm : Form
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static BusyForm instance = null;

        private static Point location = new Point(100, 100);

        /// <summary>
        /// The threat for this form
        /// </summary>
        private static Thread thread = null;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private BusyForm()
        {
            InitializeComponent();
            //this.progressBar.MarqueeAnimationSpeed = 30;
        }

        /// <summary>
        /// Launches SplashScreen
        /// </summary>
        static private void ShowForm()
        {
            instance = new BusyForm();
            instance.Location = BusyForm.location;
            instance.ShowDialog();
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
        static public void ShowBusyScreen(Form owner)
        {
            // Make sure it is only launched once.
            if (instance != null)
                return;
            BusyForm.location = owner.Location;
            BusyForm.location.X += owner.Size.Width / 2 - 100;
            BusyForm.location.Y += owner.Size.Height / 2 - 35;
            thread = new Thread(new ThreadStart(BusyForm.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        /// <summary>
        /// makes the Spash screen disappear
        /// </summary>
        static public void HideBusyScreen()
        {
            if (instance != null)
            {
                // There might occur a racing condition, when we rapidly open and close a form.
                // So we may need to wait for the form to initialize and cloase it then again
                bool closed = false;
                while (!closed)
                {
                    try
                    {
                        instance.Invoke(new MethodInvoker(delegate { CloseForm(); }));
                        closed = true;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
