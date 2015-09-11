using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using AZROLESLib;

namespace Hulcher.OneSource.CustomerService.Core.Security
{
    #region [ Structs ]

    /// <summary>
    /// Struct to return AZOperation information
    /// </summary>
    public struct AZOperation
    {
        public int ID;
        public string Name;
        public bool Result;
    }

    #endregion

    public class AZManager : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Authorization Store class
        /// </summary>
        private AzAuthorizationStore _azStore;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class constructor.
        /// </summary>
        public AZManager()
        {
        }

        #endregion

        #region [ Methods ]

        public AZOperation[] CheckAccessByName(string userName, string domain, string[] operationNames)
        {
            return CheckAccess(userName, domain, ShowOperations(operationNames));
        }

        public  virtual AZOperation[] CheckAccessById(string userName, string domain, Globals.Security.Operations[] operationIds)
        {
            AZOperation[] azOperationList = new AZOperation[operationIds.Length];
            for (int i = 0; i < operationIds.Length; i++)
                azOperationList[i] = new AZOperation() { ID = (int)operationIds[i] };

            return CheckAccess(userName, domain, azOperationList);
        }

        public AZOperation[] CheckAccess(string _userName, string _domain, AZOperation[] _operations)
        {
            if (null == _azStore)
            {
                _azStore = new AzAuthorizationStore();
                _azStore.Initialize(0, Globals.Configuration.AzManagerStoreConnectionString, null);
            }

            IAzApplication azApp = _azStore.OpenApplication("CustomerService");
            try
            {
                IAzClientContext context = azApp.InitializeClientContextFromName(_userName, _domain, null);

                object[] operationIds = new object[_operations.Length];

                for (int i = 0; i < _operations.Length; i++)
                {
                    operationIds[i] = _operations[i].ID;
                }

                object[] result = (object[])context.AccessCheck("Auditstring",
                      new object[1], operationIds, null, null, null, null, null);

                for (int j = 0; j < result.Length; j++)
                {
                    if (int.Parse(result[j].ToString()) != 0)
                    {
                        _operations[j].Result = false;
                    }
                    else
                    {
                        _operations[j].Result = true;
                    }
                }

                return _operations;
            }
            catch
            {
                throw;
            }
        }

        public AZOperation[] ShowOperations(string[] _OperationNames)
        {
            if (null == _azStore)
            {
                _azStore = new AzAuthorizationStore();
                _azStore.Initialize(0, Globals.Configuration.AzManagerStoreConnectionString, null);
            }

            IAzApplication azApp = _azStore.OpenApplication("CustomerService");

            List<AZOperation> _Operations = new List<AZOperation>();

            foreach (string opName in _OperationNames)
            {
                IAzOperation op = azApp.OpenOperation(opName, null);
                AZOperation operationAux = new AZOperation();

                operationAux.ID = op.OperationID;
                operationAux.Name = op.Name;

                _Operations.Add(operationAux);
            }

            return _Operations.ToArray();
        }

        #endregion

        #region [ IDisposable implementation ]

        public void Dispose()
        {
            if (null != _azStore)
                _azStore = null;
        }

        #endregion
    }
}
