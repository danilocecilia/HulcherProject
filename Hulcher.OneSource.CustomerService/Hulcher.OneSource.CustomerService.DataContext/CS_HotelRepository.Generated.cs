using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_HotelRepository
	{
		private IRepository<CS_Hotel> _repository {get;set;}
		public IRepository<CS_Hotel> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_HotelRepository(IRepository<CS_Hotel> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Hotel Add(CS_Hotel entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Hotel> AddList(IList<CS_Hotel> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Hotel Update(CS_Hotel entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Hotel> UpdateList(IList<CS_Hotel> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Hotel> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Hotel> ListAll(System.Linq.Expressions.Expression<Func<CS_Hotel, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Hotel> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Hotel, bool>> where, System.Linq.Expressions.Expression<Func<CS_Hotel, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Hotel Get(System.Linq.Expressions.Expression<Func<CS_Hotel, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}