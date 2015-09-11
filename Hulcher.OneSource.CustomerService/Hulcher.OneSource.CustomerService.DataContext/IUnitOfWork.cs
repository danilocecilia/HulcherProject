using System;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.DataContext
{
	public interface IUnitOfWork : IDisposable
	{
		ObjectContext Context { get; set; }
		void Save();
		bool LazyLoadingEnabled { get; set; }
		bool ProxyCreationEnabled { get; set; }
		string ConnectionString { get; set; }
		System.Data.Objects.IObjectSet<T> CreateObjectSet<T>() where T : class;	
		bool TryGetObjectStateEntry(object entity, out ObjectStateEntry stateEntry);
        void ChangeObjectState(object entity, System.Data.EntityState entityState);
	}
}