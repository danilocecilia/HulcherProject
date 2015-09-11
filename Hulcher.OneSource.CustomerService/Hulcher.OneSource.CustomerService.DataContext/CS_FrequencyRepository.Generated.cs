using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FrequencyRepository
	{
		private IRepository<CS_Frequency> _repository {get;set;}
		public IRepository<CS_Frequency> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FrequencyRepository(IRepository<CS_Frequency> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Frequency Add(CS_Frequency entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Frequency> AddList(IList<CS_Frequency> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Frequency Update(CS_Frequency entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Frequency> UpdateList(IList<CS_Frequency> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Frequency> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Frequency> ListAll(System.Linq.Expressions.Expression<Func<CS_Frequency, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Frequency> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Frequency, bool>> where, System.Linq.Expressions.Expression<Func<CS_Frequency, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Frequency Get(System.Linq.Expressions.Expression<Func<CS_Frequency, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}