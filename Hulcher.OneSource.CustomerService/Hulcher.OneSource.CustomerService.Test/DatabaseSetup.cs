using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Configuration;
using System.IO;

namespace Hulcher.OneSource.CustomerService.Test
{
    [TestClass]
    public class DatabaseSetup
    {
        [AssemblyInitialize()]
        public static void IntializeAssembly(TestContext ctx)
        {
            CheckDbProPathsAndAdjust();
        }

        public static void CheckDbProPathsAndAdjust()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection("DatabaseUnitTesting");

            if (section == null) return;

            DatabaseUnitTestingSection dbUnitTestConfig = section as DatabaseUnitTestingSection;

            if (dbUnitTestConfig == null) return;

            string dbProjectFile = dbUnitTestConfig.DatabaseDeployment.DatabaseProjectFileName;
            string dataGenPlanFile = dbUnitTestConfig.DataGeneration.DataGenerationFileName;

            bool adjustedDbProjectFile = CheckPathAndAdjustForTeamBuild("DatabaseProjectFileName", ref dbProjectFile);
            bool adjustedDbGenerationPlanFile = CheckPathAndAdjustForTeamBuild("DataGenerationFileName", ref dataGenPlanFile);

            if (adjustedDbGenerationPlanFile == true)
            {
                dbUnitTestConfig.DataGeneration.DataGenerationFileName = dataGenPlanFile;
            }

            if (adjustedDbProjectFile == true)
            {
                dbUnitTestConfig.DatabaseDeployment.DatabaseProjectFileName = dbProjectFile;
            }

            if (adjustedDbGenerationPlanFile == true || adjustedDbProjectFile == true)
            {
                Console.WriteLine("Saving changes to app.config for DBPro.");
                config.Save();
            }
        }

        /// <summary>
        /// Adjusts the file path for team build file/folder structure
        /// </summary>
        /// <param name="fileDescription"></param>
        /// <param name="path"></param>
        /// <returns>True if the path value was changed.</returns>
        public static bool CheckPathAndAdjustForTeamBuild(string fileDescription, ref string path)
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (File.Exists(path) == false && path.StartsWith("..\\..\\..\\") == true)
                {
                    Console.WriteLine("{0} was not found.  Attempting to correct.", fileDescription);

                    // If we're running in a Team Build then the path is a little different
                    path = path.Replace("..\\..\\..\\", "..\\..\\..\\Sources\\Hulcher.OneSource.CustomerService\\");

                    Console.WriteLine("Trying {0} at {1}.", fileDescription, path);

                    if (File.Exists(path) == true)
                    {
                        // change where the db project is loaded from
                        Console.WriteLine("Using corrected {0} at {1}.",
                          fileDescription,
                          path);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Could not find {0} at corrected path {1}.",
                          fileDescription,
                          path);
                        return false;
                    }
                }
                else
                {
                    // it exists at the default location
                    // probably because we're running in Visual Studio
                    return false;
                }
            }
            else
            {
                // This kind of file is not configured 
                // probably no data generation plan or no auto-deploy db at start of unit tests
                return false;
            }
        }

    }
}
