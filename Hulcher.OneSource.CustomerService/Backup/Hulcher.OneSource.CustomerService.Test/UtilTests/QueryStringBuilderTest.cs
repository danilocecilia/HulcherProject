using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Test.UtilTests
{
    [TestClass]
    public class QueryStringBuilderTest
    {
        [TestMethod]
        public void ShouldBuildQueryStringCorrectlyWhenPassingParameterAsFirstParameter()
        {
            //Arrange
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            DateTime dateTime = new DateTime(2011, 04, 15);

            //Act
            queryBuilder.AppendQueryString("StartDateFilter", dateTime.ToString("MM/dd/yyyy"));

            //Assert
            Assert.AreEqual("?StartDateFilter=04/15/2011", queryBuilder.ToString());
        }

        [TestMethod]
        public void ShouldBuildQueryStringCorrectlyWhenPassingParameterAsSecondParameter()
        {
            //Arrange
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            DateTime dateTimeFirst = new DateTime(2011, 04, 15);
            DateTime dateTimeEnd = new DateTime(2011, 04, 25);
            //Act
            queryBuilder.AppendQueryString("StartDateFilter", dateTimeFirst.ToString("MM/dd/yyyy"));
            queryBuilder.AppendQueryString("EndDateFilter", dateTimeEnd.ToString("MM/dd/yyyy"));

            //Assert
            Assert.AreEqual("?StartDateFilter=04/15/2011&EndDateFilter=04/25/2011", queryBuilder.ToString());
        }
    }
}
