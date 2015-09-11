using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Data;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class CallLogDaoTest
    {
        //private const int CONSULTING_ID = 1;
        //private DateTime FILTER_DATE = new DateTime(4543, 7, 16, 1, 40, 29);
        //private const string USER = "ÇôjOI";
        //private const string TYPE = "ÇôjOI";

        //private CS_CallLog _foundCallLog;
        //private IList<CS_CallLog> _foundCallLogList;

        //[TestInitialize]
        //public void Initialize()
        //{
        //    //DatabaseTestClass.TestService.GenerateData();
        //}

        //[TestMethod]
        //public void TestGetSpecificJobCallLogs()
        //{
        //    _foundCallLogList = CallLogDao.Singleton.ListJobCallLogs(1);
        //    for (int i = 0; i < _foundCallLogList.Count; i++)
        //    {
        //        Assert.AreEqual(CONSULTING_ID, _foundCallLogList[i].JobID);
        //    }
        //}

        //[TestMethod]
        //public void TestGetFilteredJobCallLogsByDate()
        //{
        //    _foundCallLogList = CallLogDao.Singleton.ListFilteredJobCallLogs(Core.Globals.JobRecord.FilterType.Date, FILTER_DATE.Date.ToString(), 1);
        //    for (int i = 0; i < _foundCallLogList.Count; i++)
        //    {
        //        Assert.AreEqual(FILTER_DATE.Date, _foundCallLogList[i].CallDate.Date);
        //    }
        //}

        //[TestMethod]
        //public void TestGetFilteredJobCallLogsByTime()
        //{
        //    _foundCallLogList = CallLogDao.Singleton.ListFilteredJobCallLogs(Core.Globals.JobRecord.FilterType.Time, FILTER_DATE.TimeOfDay.ToString(), 1);
        //    for (int i = 0; i < _foundCallLogList.Count; i++)
        //    {
        //        Assert.AreEqual(FILTER_DATE.Hour, _foundCallLogList[i].CallDate.Hour);
        //        Assert.AreEqual(FILTER_DATE.Minute, _foundCallLogList[i].CallDate.Minute);
        //    }
        //}

        //[TestMethod]
        //public void TestGetFilteredCallLogsByType()
        //{
        //    _foundCallLogList = CallLogDao.Singleton.ListFilteredJobCallLogs(Core.Globals.JobRecord.FilterType.Type, TYPE, 4);
        //    for (int i = 0; i < _foundCallLogList.Count; i++)
        //    {
        //        Assert.AreEqual(TYPE, _foundCallLogList[i].CS_CallType.CS_PrimaryCallType.Type);
        //    }
        //}

        //[TestMethod]
        //public void TestGetFilteredCallLogsByUser()
        //{
        //    _foundCallLogList = CallLogDao.Singleton.ListFilteredJobCallLogs(Core.Globals.JobRecord.FilterType.User, USER, 4);
        //    for (int i = 0; i < _foundCallLogList.Count; i++)
        //    {
        //        Assert.AreEqual(USER, _foundCallLogList[i].CreatedBy);
        //    }
        //}
    }
}
