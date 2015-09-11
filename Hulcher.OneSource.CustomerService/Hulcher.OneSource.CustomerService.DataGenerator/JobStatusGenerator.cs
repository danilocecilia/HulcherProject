using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Schema.Tools.DataGenerator;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Sql;


namespace Hulcher.OneSource.CustomerService.DataGenerator
{
    /// <summary>
    /// Class that will generate controlled data for Job Status
    /// </summary>
    [DatabaseSchemaProviderCompatibility(typeof(SqlDatabaseSchemaProvider))]
    public class JobStatusGenerator : Generator
    {
        #region Attributes

        private Random _random;
        private List<string> _jobStatus;
        private string _jobStatusSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Generates a new random object
        /// </summary>
        private Random Random
        {
            get
            {
                if (_random == null)
                    _random = new Random(base.Seed);
                return _random;
            }
        }

        /// <summary>
        /// Output Property to return the generated value
        /// </summary>
        [Output(Description = "Generates values for Job Status", Name = "JobStatus")]
        public string JobStatus
        {
            get
            {
                return this._jobStatusSelected;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method called when a new value needs to be generated
        /// </summary>
        protected override void OnGenerateNextValues()
        {
            _jobStatus = new List<string> { "Bid", "Active", "Closed" };
            _jobStatusSelected = string.Format("{0}",GetRandomJobStatus());
        }

        /// <summary>
        /// Pick a random job status from the list
        /// </summary>
        /// <returns>selected random job status from list</returns>
        private string GetRandomJobStatus()
        {
            int randomIndex = Random.Next(0, _jobStatus.Count);
            return _jobStatus[randomIndex];
        }

        #endregion
    }
}
