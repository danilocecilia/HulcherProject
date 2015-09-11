using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.EntityExtensions
{
    public interface IInitialAdvisePerson
    {
        string Description {get;}

        string Person { get;  }

        string Note { get;  }

        string ContactInfo { get;  }

        int ID { get; }
    }
}
