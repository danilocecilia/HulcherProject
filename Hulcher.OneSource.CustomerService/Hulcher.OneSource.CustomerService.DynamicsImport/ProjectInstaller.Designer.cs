namespace Hulcher.OneSource.CustomerService.DynamicsImport
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
            this.DynamicsImportProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DynamicsImportInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DynamicsImportProcessInstaller
            // 
            this.DynamicsImportProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.DynamicsImportProcessInstaller.Password = null;
            this.DynamicsImportProcessInstaller.Username = null;
            // 
            // DynamicsImportInstaller
            // 
            this.DynamicsImportInstaller.ServiceName = "DynamicsImportService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DynamicsImportProcessInstaller,
            this.DynamicsImportInstaller});

		}

		#endregion

        private System.ServiceProcess.ServiceProcessInstaller DynamicsImportProcessInstaller;
        private System.ServiceProcess.ServiceInstaller DynamicsImportInstaller;
	}
}