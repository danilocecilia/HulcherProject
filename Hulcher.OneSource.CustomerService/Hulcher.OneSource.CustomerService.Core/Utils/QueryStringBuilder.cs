using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.Utils
{
    public class QueryStringBuilder
    {
        private StringBuilder queryStringBuilder;

        public QueryStringBuilder()
        {
            queryStringBuilder = new StringBuilder();
        }

        /// <summary>
        /// Append parameter/value to querystring
        /// </summary>
        /// <param name="parameterName">parameter name</param>
        /// <param name="value">value of the parameter</param>
        public void AppendQueryString(string parameterName, string value)
        {
            AppendSpecialChar();
            if (!string.IsNullOrEmpty(value))
            {
                queryStringBuilder.Append(parameterName);
                queryStringBuilder.Append("=");
                queryStringBuilder.Append(value);
            }
        }

        public override string ToString()
        {
            return queryStringBuilder.ToString();
        }

        /// <summary>
        /// Add the special character (?) if it's the first parameter. Otherwise (&)
        /// </summary>
        private void AppendSpecialChar()
        {
            if (string.IsNullOrEmpty(queryStringBuilder.ToString()))
            {
                queryStringBuilder.Append("?");
            }
            else
            {
                queryStringBuilder.Append("&");
            }
        }
    }
}
