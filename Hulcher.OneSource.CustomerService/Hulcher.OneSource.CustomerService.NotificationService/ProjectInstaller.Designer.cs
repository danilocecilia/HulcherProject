namespace Hulcher.OneSource.CustomerService.NotificationService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NotificationProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.NotificationServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // NotificationProcessInstaller
            // 
            this.NotificationProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.NotificationProcessInstaller.Password = null;
            this.NotificationProcessInstaller.Username = null;
            // 
            // NotificationServiceInstaller
            // 
            this.NotificationServiceInstaller.ServiceName = "WatchService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NotificationProcessInstaller,
            this.NotificationServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller NotificationProcessInstaller;
        private System.ServiceProcess.ServiceInstaller NotificationServiceInstaller;
    }
}