using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.DataContext
{
	public class EFUnitOfWork : IUnitOfWork
	{
		public ObjectContext Context { get; set; }

		public EFUnitOfWork()
		{
			Context = new CustomerServiceModelContainer();
		}

		public void Save()
		{
			Context.SaveChanges();
		}
		
		public bool LazyLoadingEnabled
		{
			get { return Context.ContextOptions.LazyLoadingEnabled; }
			set { Context.ContextOptions.LazyLoadingEnabled = value;}
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.ContextOptions.ProxyCreationEnabled; }
			set { Context.ContextOptions.ProxyCreationEnabled = value; }
		}
		
		public string ConnectionString
		{
			get { return Context.Connection.ConnectionString; }
			set { Context.Connection.ConnectionString = value; }
		}

		public IObjectSet<T> CreateObjectSet<T>() where T : class
        {
            return this.Context.CreateObjectSet<T>();
        }

		public bool TryGetObjectStateEntry(object entity, out ObjectStateEntry stateEntry)
		{
			if (null != this.Context)
				return this.Context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry);
			else
			{
				stateEntry = null;
				return false;
			}
		}

		public void ChangeObjectState(object entity, System.Data.EntityState entityState)
        {
            if (null != this.Context)
            {
                this.Context.ObjectStateManager.ChangeObjectState(entity, entityState);
            }
        }

		public void Dispose()
		{
			this.Context.Dispose();
		}
	}
}