using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Test.UtilTests
{
    [TestClass]
    public class WebUtilTest
    {
        [TestMethod]
        public void ShouldNotBuildQueryStringParameterIfANullableObjectIsPassed()
        {
            //Arrange
            Mock<IDashboardView> mockDashboardView = new Mock<IDashboardView>();
            mockDashboardView.SetupProperty(e => e.BeginDateCallLogViewFilter, new DateTime(2011,4,15));
            mockDashboardView.SetupProperty(e => e.EndDateCallLogViewFilter, DateTime.MinValue);
            mockDashboardView.SetupProperty(e => e.JobStatusCallLogViewFilter, 1);
            mockDashboardView.SetupProperty(e => e.DivisionValueCallLogViewFilter,null);
            WebUtil util = new WebUtil();

            //Act
            string value = util.BuildQueryStringToPrintCallLogViewInDashBoardView(mockDashboardView.Object);

            //Assert
            Assert.AreEqual("?StartDateFilter=04/15/2011&JobStatusFilter=1", value);
        }

        [TestMethod]
        public void ShouldBuildEntireQueryStringForCallLogViewIfAllParametersArePassed()
        {
            //Arrange
            Mock<IDashboardView> mockDashboardView = new Mock<IDashboardView>();
            mockDashboardView.SetupProperty(e => e.DashBoardViewType, Globals.Dashboard.ViewType.JobSummaryView);
            mockDashboardView.SetupProperty(e => e.BeginDateCallLogViewFilter, new DateTime(2011, 4, 15));
            mockDashboardView.SetupProperty(e => e.EndDateCallLogViewFilter, new DateTime(2011,4,25));
            mockDashboardView.SetupProperty(e => e.JobStatusCallLogViewFilter, 1);
            mockDashboardView.SetupProperty(e => e.DivisionValueCallLogViewFilter, 2);
            mockDashboardView.SetupProperty(e => e.CallTypeCallLogViewFilter, 3);
            mockDashboardView.SetupProperty(e => e.ModifiedByCallLogViewFilter, "druziska");
            mockDashboardView.SetupProperty(e => e.OrderBy, new string[] { "orderby", "ASC" });
            mockDashboardView.SetupProperty(e => e.ShiftTransferLogCallLogViewFilter, true);
            mockDashboardView.SetupProperty(e => e.GeneralLogCallLogViewFilter, true);
            WebUtil util = new WebUtil();

            //Act
            string value = util.BuildQueryStringToPrintCallLogViewInDashBoardView(mockDashboardView.Object);

            //Assert
            Assert.AreEqual("?ViewPoint=2&StartDateFilter=04/15/2011&EndDateFilter=04/25/2011&JobStatusFilter=1&DivisionFilter=2&CallTypeFilter=3&ModifiedByFilter=druziska&OrderBy=orderby ASC&ShiftTransferLogFilter=True&GeneralLogFilter=True", value);
        }

        [TestMethod]
        public void ShouldBuildClientScriptStringForDashboardToPrintPageWithQueryString()
        {
            //Arrange
            Mock<IDashboardView> mockDashboardView = new Mock<IDashboardView>();
            mockDashboardView.SetupProperty(e => e.DashBoardViewType, Globals.Dashboard.ViewType.JobSummaryView);
            WebUtil util = new WebUtil();
            string queryStringValue = util.BuildQueryStringToPrintCallLogViewInDashBoardView(mockDashboardView.Object);

            //Act
            string script = util.BuildNewWindowClientScript("/DashboardPrint.aspx", queryStringValue,"",800,600,true,true,true);

            //Assert
            Assert.AreEqual("javascript: var newWindow = window.open('/DashboardPrint.aspx?ViewPoint=2','','width=800,height=600,scrollbars=1,resizable=yes');return false;", script);
        }

        [TestMethod]
        public void ShouldBuildEntireQueryStringForJobSummaryViewIfAllParametersArePassed()
        {
            //Arrange
            Mock<IDashboardView> mockDashboardView = new Mock<IDashboardView>();
            mockDashboardView.SetupProperty(e => e.DashBoardViewType, Globals.Dashboard.ViewType.JobSummaryView);
            mockDashboardView.SetupProperty(e => e.JobStatusFilterValue, 1);
            mockDashboardView.SetupProperty(e => e.JobNumberFilterValue, 2);
            mockDashboardView.SetupProperty(e => e.DivisionFilterValue, 3);
            mockDashboardView.SetupProperty(e => e.CustomerFilterValue, 4);
            mockDashboardView.SetupProperty(e => e.DateFilterTypeValue, Globals.Dashboard.DateFilterType.InitialCallDate);
            mockDashboardView.SetupProperty(e => e.BeginDateJobSummaryValue, new DateTime(2011, 04, 15));
            mockDashboardView.SetupProperty(e => e.EndDateJobSummaryValue, new DateTime(2011, 04, 25));
            mockDashboardView.SetupProperty(e => e.OrderBy, new string[] { "orderby", "ASC" });
            WebUtil util = new WebUtil();

            //Act
            string value = util.BuildQueryStringToPrintJobSummaryInDashBoardView(mockDashboardView.Object);

            //Assert
            Assert.AreEqual("?ViewPoint=2&JobStatusFilter=1&JobNumberFilter=2&DivisionFilter=3&CustomerFilter=4&DateTypeFilter=1&StartDateFilter=04/15/2011&EndDateFilter=04/25/2011&OrderBy=orderby ASC", value);
        }

        [TestMethod]
        public void ShouldBuildQueryStringForQuickSearch()
        {
            //Arrange
            Mock<IDefaultPageView> mockDefaultPageView = new Mock<IDefaultPageView>();
            mockDefaultPageView.SetupProperty(e => e.QuickSearchJobId, 1);
            WebUtil util = new WebUtil();

            //Act
            string queryString = util.BuildQueryStringToQuickSearch(mockDefaultPageView.Object);

            //Assert
            Assert.AreEqual("?JobId=1", queryString);
        }

        [TestMethod]
        public void ShouldBuildScriptForQuickSearch()
        {
            //Arrange
            string queryString = "?JobId=1";
            WebUtil util = new WebUtil();

            //Act
            string script = util.BuildNewWindowClientScript("/JobRecord.aspx", queryString, string.Empty, 870, 600, true, true, false);

            //Assert
            Assert.AreEqual("javascript: var newWindow = window.open('/JobRecord.aspx?JobId=1','','width=870,height=600,scrollbars=1,resizable=yes');", script);
        }

        [TestMethod]
        public void ShouldBuildQueryStringForQuickReference()
        {            
            //Arrange
            WebUtil util = new WebUtil();

            //Act
            string queryString = util.BuildQueryStringForQuickReference(new Dictionary<string,string>{{"JobId","8"}} );

            //Assert
            Assert.AreEqual("?JobId=8",queryString);
        }

        [TestMethod]
        public void ShouldBuildScriptForQuickReferenceWhenPassingQueryString()
        {
            //Arrange
            WebUtil util = new WebUtil();
            string queryString = "?JobId=8";

            //Act
            string script = util.BuildNewWindowClientScript("/JobRecord.aspx", queryString,"",870,600,true,true,false);

            //Assert
            Assert.AreEqual("javascript: var newWindow = window.open('/JobRecord.aspx?JobId=8','','width=870,height=600,scrollbars=1,resizable=yes');", script);
        }
    }
}
