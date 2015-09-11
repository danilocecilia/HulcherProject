namespace Hulcher.OneSource.CustomerService.DPIService
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
            this.DPIProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DPIServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DPIProcessInstaller
            // 
            this.DPIProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.DPIProcessInstaller.Password = null;
            this.DPIProcessInstaller.Username = null;
            // 
            // DPIServiceInstaller
            // 
            this.DPIServiceInstaller.ServiceName = "DPIService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DPIProcessInstaller,
            this.DPIServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DPIProcessInstaller;
        private System.ServiceProcess.ServiceInstaller DPIServiceInstaller;
    }
}