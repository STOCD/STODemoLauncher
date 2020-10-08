using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Model;

namespace StoDemoLauncher
{
    /// <summary>
    /// Creates, reads, and restores demo backups
    /// </summary>
    public class BackupManager
    {
        private GameClient gameClient;

        public BackupManager(GameClient gameClient)
        {
            this.gameClient = gameClient;
        }

        /// <summary>
        /// Backs up a demo file
        /// </summary>
        /// <param name="demo">DemoFileInfo of the demo to backup</param>
        /// <returns>true if the backup was written, false otherwise</returns>
        public bool BackupDemo(DemoInfo demo)
        {
            // backup file by default
            DialogResult overwriteConfirmed = DialogResult.Yes;

            if (System.IO.File.Exists(this.GetDemoBackupPath(demo)))
            {
                overwriteConfirmed = MessageBox.Show(
                    "There already exists a backup of this demo file. You can\n" +
                    "replace the existing backup with the current version of\n" +
                    "this demo file. Make sure, the current version is working\n" +
                    "properly and is not corrupted. This action cannot be\n" +
                    "undone.\n\n" +
                    "Do you want to replace the existing backup?",
                    "Confirm File Replace",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
            }
            if (overwriteConfirmed.Equals(DialogResult.Yes))
            {
                if (!System.IO.Directory.Exists(this.GetDemosBackupPath(demo.Server))) System.IO.Directory.CreateDirectory(this.GetDemosBackupPath(demo.Server));
                System.IO.File.Copy(this.gameClient.GetDemoPath(demo), this.GetDemoBackupPath(demo), true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Restores demo from a backup
        /// </summary>
        /// <param name="demo"></param>
        /// <returns>true if the demo was replaced with the backup, false
        /// otherwise</returns>
        public bool RestoreBackup(DemoInfo demo)
        {
            // restore file by default
            DialogResult overwriteConfirmed = DialogResult.Yes;

            if (System.IO.File.Exists(this.GetDemoBackupPath(demo)))
            {
                overwriteConfirmed = MessageBox.Show(
                    "You are about to replace this demo file with a backup.\n" +
                    "This action cannot be undone.\n\n" +
                    "Do you want to replace the existing file with the backup?",
                    "Confirm File Replace",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
            }
            if (overwriteConfirmed.Equals(DialogResult.Yes))
            {
                System.IO.File.Copy(this.GetDemoBackupPath(demo), this.gameClient.GetDemoPath(demo), true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the path to the demos backup folder
        /// </summary>
        /// <param name="server">The installation for which to backup</param>
        /// <returns>The path to the demos backup folder</returns>
        public string GetDemosBackupPath(GameServer server)
        {
            return this.gameClient.GetDemosPath(server) + "\\StoDemoLauncherBackup";
        }

        /// <summary>
        /// Returns the filename for the backup of a demo file
        /// </summary>
        /// <param name="demo">DemoFileInfo of the demo to backup</param>
        /// <returns>The absolute path the demo backup</returns>
        public string GetDemoBackupPath(DemoInfo demo)
        {
            return this.GetDemosBackupPath(demo.Server) + "\\" + demo.FileName;
        }

        /// <summary>
        /// Chekcs if a given demo file has a backup
        /// </summary>
        /// <param name="demo">DemoFileInfo for the demo to check</param>
        /// <returns>true if the demo file has a backup, false otherwise</returns>
        public bool hasBackup(DemoInfo demo)
        {
            return System.IO.File.Exists(this.GetDemoBackupPath(demo));
        }
    }
}
