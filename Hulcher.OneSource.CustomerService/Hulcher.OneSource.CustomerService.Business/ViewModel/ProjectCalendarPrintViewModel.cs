using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class ProjectCalendarPrintViewModel : IDisposable
    {
        #region [ Attributes ]
        IProjectCalendarPrintView _view;

        JobModel _jobModel;
        #endregion

        #region [ Constructor ]
        public ProjectCalendarPrintViewModel(IProjectCalendarPrintView view)
        {
            _view = view;
            _jobModel = new JobModel();

        }
        #endregion

        #region [ Methods ]

        public void CreateDynamicCalendar(IList<ProjectCalendarVO> listProjectCalendar, List<DateTime> calendarListRange)
        {
            if (listProjectCalendar.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<table class='ProjectCalendar' cellspacing='0' cellpadding='0'> <thead>");
                builder.Append("<tr>");
                for (int i = 0; i < calendarListRange.Count; i++)
                {
                    if (calendarListRange.Count - 1 == i)
                        builder.Append("<th> <div>");
                    else
                        builder.Append("<th class='BorderRight'> <div>");
                    builder.Append(calendarListRange[i].ToString("ddd MM/dd/yy"));
                    builder.Append("</div></th>");
                }
                builder.Append("</tr> </thead><tbody>");
                List<ProjectCalendarVO> divisionList = listProjectCalendar.Distinct(new Globals.ProjectCalendar.ProjectCalendarComparer()).ToList();
                for (int i = 0; i < divisionList.Count; i++)
                {
                    builder.Append("<tr>");

                    for (int j = 0; j < calendarListRange.Count; j++)
                    {

                        builder.Append("<td class='Division'>");
                        if (j == 0)
                            builder.Append(divisionList[i].DivisionName);
                        else
                            builder.Append("&nbsp;");
                        builder.Append("</td>");
                    }

                    builder.Append("</tr>");

                    for (int j = 0; j < divisionList[i].JobCalendarList.Count; j++)
                    {
                        bool writtenName = false;

                        builder.Append("<tr>");
                        for (int k = 0; k < calendarListRange.Count; k++)
                        {
                            JobCalendarVO jobCalendar = listProjectCalendar.FirstOrDefault(e => e.DivisionID == divisionList[i].DivisionID && e.CalendarDate == calendarListRange[k]).JobCalendarList.FirstOrDefault(job => job.JobID == divisionList[i].JobCalendarList[j].JobID);
                            if (jobCalendar.PaintDate)
                            {
                                builder.Append("<td class='PaintJob'");
                            }
                            else
                                builder.Append("<td");

                            builder.AppendFormat(">");

                            if (!writtenName && jobCalendar.PaintDate)
                            {
                                writtenName = true;
                                builder.Append("<a href=\"" + string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", jobCalendar.JobID) + "\"><div>&nbsp;<div style='position:absolute;'>" + jobCalendar.Job + " - " + jobCalendar.CustomerName + "</div></div></a>");
                            }
                            else
                                builder.Append("&nbsp;");

                            builder.Append("</td>");
                        }
                        builder.Append("</tr>");
                        for (int k = 0; k < divisionList[i].JobCalendarList[j].ResourceList.Count; k++)
                        {
                            bool writtenNameResource = false;
                            bool estimatedWorkStart = true;

                            builder.Append("<tr>");
                            for (int l = 0; l < calendarListRange.Count; l++)
                            {
                                ResourceVO resourceNextDay = null;
                                ResourceVO resource = listProjectCalendar.FirstOrDefault
                                                                (e =>
                                                                    e.DivisionID == divisionList[i].DivisionID &&
                                                                    e.CalendarDate == calendarListRange[l]
                                                                ).JobCalendarList.FirstOrDefault
                                                                                (job =>
                                                                                    job.JobID == divisionList[i].JobCalendarList[j].JobID
                                                                                 ).ResourceList.FirstOrDefault
                                                                                                (r =>
                                                                                                       (r.EquipmentID == divisionList[i].JobCalendarList[j].ResourceList[k].EquipmentID &&
                                                                                                       divisionList[i].JobCalendarList[j].ResourceList[k].EquipmentID.HasValue) ||
                                                                                                       (r.EmployeeID == divisionList[i].JobCalendarList[j].ResourceList[k].EmployeeID &&
                                                                                                       divisionList[i].JobCalendarList[j].ResourceList[k].EmployeeID.HasValue) ||
                                                                                                       (r.EquipmentTypeID == divisionList[i].JobCalendarList[j].ResourceList[k].EquipmentTypeID &&
                                                                                                       divisionList[i].JobCalendarList[j].ResourceList[k].EquipmentTypeID.HasValue)

                                                                                                       );

                                string sClass = string.Empty;

                                if (resource.Worked)
                                    sClass = "ResourceWorked";

                                if (resource.EstimatedWork)
                                {
                                    if (calendarListRange.Count != l + 1)
                                    {
                                        resourceNextDay = listProjectCalendar.FirstOrDefault
                                                                        (e =>
                                                                            e.DivisionID == divisionList[i].DivisionID &&
                                                                            e.CalendarDate == calendarListRange[l + 1]
                                                                        ).JobCalendarList.FirstOrDefault
                                                                                        (job =>
                                                                                            job.JobID == divisionList[i].JobCalendarList[j].JobID
                                                                                         ).ResourceList.FirstOrDefault
                                                                                                        (r =>
                                                                                                               ((r.EmployeeID == divisionList[i].JobCalendarList[j].ResourceList[k].EmployeeID &&
                                                                                                               divisionList[i].JobCalendarList[j].ResourceList[k].EmployeeID.HasValue) ||
                                                                                                               (r.EquipmentID == divisionList[i].JobCalendarList[j].ResourceList[k].EquipmentID &&
                                                                                                               divisionList[i].JobCalendarList[j].ResourceList[k].EquipmentID.HasValue)) &&
                                                                                                               r.EstimatedWork
                                                                                                        );
                                    }

                                    if (estimatedWorkStart)
                                    {
                                        sClass += " ResourceEstimatedStart";
                                        estimatedWorkStart = false;
                                    }
                                    else if (resource.EstimatedWork && !estimatedWorkStart && null != resourceNextDay)
                                        sClass += " ResourceEstimated";
                                    else if (resource.EstimatedWork && !estimatedWorkStart && null == resourceNextDay)
                                    {
                                        sClass += " ResourceEstimatedEnd";
                                        estimatedWorkStart = true;
                                    }
                                }

                                if (resource.Reserved)
                                    sClass += " Reserved";

                                builder.Append("<td" + (string.IsNullOrEmpty(sClass) ? ">" : " class='" + sClass.Trim() + "'><div>&nbsp;<div style='position:absolute;'>"));

                                if (!writtenNameResource && (resource.Worked || resource.EstimatedWork || resource.Reserved))
                                {
                                    writtenNameResource = true;
                                    if (resource.EmployeeID.HasValue)
                                        builder.Append(resource.EmployeeName);
                                    else if (resource.EquipmentID.HasValue)
                                        builder.Append(resource.EquipmentName);
                                    else if (resource.EquipmentTypeID.HasValue)
                                        builder.Append(resource.EquipmentTypeName);
                                }
                                else
                                    builder.Append("&nbsp;");

                                builder.Append("</div></div></td>");
                            }
                            builder.Append("</tr>");
                        }
                    }

                }

                builder.Append("</tbody> </table>");
                builder.Append("</br>");
                _view.AddCalendarSource(builder.ToString());
            }

        }

        public void DefaultDayProjectCalendar()
        {
            _view.StartDateValue = DateTime.Now.AddDays(-4);
            _view.EndDateValue = DateTime.Now;
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            if (null != _jobModel)
                _jobModel.Dispose();

            _jobModel = null;
        }

        #endregion
    }
}
