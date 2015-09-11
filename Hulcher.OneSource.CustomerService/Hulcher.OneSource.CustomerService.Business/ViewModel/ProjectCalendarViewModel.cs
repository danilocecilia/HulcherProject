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
    public class ProjectCalendarViewModel : IDisposable
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the Page View
        /// </summary>
        private IBaseProjectCalendarView _view;

        /// <summary>
        /// Instance of the Job Model
        /// </summary>
        private JobModel _model;
        #endregion

        #region [ Constructor ]
        /// <summary>
        /// Project Calendar Constructor
        /// </summary>
        /// <param name="view">Interface View</param>
        public ProjectCalendarViewModel(IBaseProjectCalendarView view)
        {
            _view = view;
            _model = new JobModel();
        }
        #endregion

        #region [ Methods ]

        public List<ProjectCalendarVO> ReturnVO()
        {
            List<ResourceVO> resourceList1 = new List<ResourceVO>();
            List<ResourceVO> resourceList2 = new List<ResourceVO>();
            List<JobCalendarVO> jobCalendarList = new List<JobCalendarVO>();
            List<ProjectCalendarVO> projectCalendarList = new List<ProjectCalendarVO>();

            ResourceVO res1 = new ResourceVO()
            {
                EquipmentID = 1,
                EquipmentName = "Truck1",
                EstimatedWork = true,
                Worked = true
            };

            ResourceVO res2 = new ResourceVO()
            {
                EmployeeID = 1,
                EmployeeName = "John Doe",
                EstimatedWork = false,
                Worked = true
            };

            resourceList1.Add(res1);
            resourceList1.Add(res2);

            JobCalendarVO jobVO = new JobCalendarVO()
            {
                Job = "Job1",
                JobID = 1,
                PaintDate = true,
                ResourceList = resourceList1
            };

            jobCalendarList.Add(jobVO);

            ProjectCalendarVO vo = new ProjectCalendarVO()
            {
                CalendarDate = new DateTime(2011, 12, 01),
                DivisionID = 1,
                DivisionName = "Div1",
                JobCalendarList = jobCalendarList
            };

            ProjectCalendarVO vo2 = new ProjectCalendarVO()
            {
                CalendarDate = new DateTime(2011, 12, 02),
                DivisionID = 1,
                DivisionName = "Div1",
                JobCalendarList = jobCalendarList
            };

            projectCalendarList.Add(vo);
            projectCalendarList.Add(vo2);

            return projectCalendarList;
        }

        public void CreateDynamicCalendar(IList<ProjectCalendarVO> listProjectCalendar)
        {
            if (listProjectCalendar.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<table class='ProjectCalendar' cellspacing='0' cellpadding='0'> <thead>");
                builder.Append("<tr>");
                for (int i = 0; i < _view.CalendarDateRange.Count; i++)
                {
                    if (_view.CalendarDateRange.Count - 1 == i)
                        builder.Append("<th> <div>");
                    else
                        builder.Append("<th class='BorderRight'> <div>");
                    builder.Append(_view.CalendarDateRange[i].ToString("ddd MM/dd/yy"));
                    builder.Append("</div></th>");
                }
                builder.Append("</tr> </thead><tbody>");
                List<ProjectCalendarVO> divisionList = listProjectCalendar.Distinct(new Globals.ProjectCalendar.ProjectCalendarComparer()).ToList();
                for (int i = 0; i < divisionList.Count; i++)
                {
                    builder.Append("<tr>");

                    for (int j = 0; j < _view.CalendarDateRange.Count; j++)
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
                        for (int k = 0; k < _view.CalendarDateRange.Count; k++)
                        {
                            JobCalendarVO jobCalendar = listProjectCalendar.FirstOrDefault(e => e.DivisionID == divisionList[i].DivisionID && e.CalendarDate == _view.CalendarDateRange[k]).JobCalendarList.FirstOrDefault(job => job.JobID == divisionList[i].JobCalendarList[j].JobID);
                            if (jobCalendar.PaintDate)
                            {
                                builder.Append("<td class='PaintJob'");
                            }
                            else
                                builder.Append("<td");

                            if (jobCalendar.PaintDate)
                            {
                                builder.AppendFormat(" onmouseenter=\"javascript: ShowToolTip(document.getElementById('ProjectToolTip{0}'));\"", jobCalendar.JobID);
                                builder.AppendFormat(" onmouseleave=\"javascript: document.getElementById('ProjectToolTip{0}').style.display = 'none';\" ", jobCalendar.JobID);
                            }

                            builder.AppendFormat(">");

                            if (!writtenName && jobCalendar.PaintDate)
                            {
                                writtenName = true;
                                builder.Append("<a href=\"" + string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", jobCalendar.JobID) + "\"><div>&nbsp;<div style='position:absolute;'>" + jobCalendar.Job + " - " + jobCalendar.CustomerName + "</div></div></a>");

                                builder.Append(string.Format(@"<DIV style='background-color:white; display:none; position:fixed; width:250; z-index: 999;' id='ProjectToolTip{0}'>
	                                <DIV class='divUpperFields'>
	                                    <DIV class='label'><SPAN>Job #:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{1}</SPAN></DIV>
	                                    <DIV class='label'><SPAN>Company Name:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{2}</SPAN></DIV>
	                                </DIV>
	                                <DIV class='divUpperFields'>
	                                    <DIV class='label'><SPAN>Division:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{3}</SPAN></DIV>
	                                    <DIV class='label'><SPAN>City:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{4}</SPAN></DIV>
	                                    <DIV class='label'><SPAN>State:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{5}</SPAN></DIV>
                                        <DIV class='label'><SPAN>Job Satus:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{6}</SPAN></DIV>
                                        <DIV class='label'><SPAN>Job Action:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{7}</SPAN></DIV>
	                                </DIV>
                                </DIV>", jobCalendar.JobID, jobCalendar.Job, jobCalendar.CustomerName, jobCalendar.DivisionName, jobCalendar.CityName, jobCalendar.StateName, jobCalendar.StatusName, jobCalendar.ActionName));
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
                            for (int l = 0; l < _view.CalendarDateRange.Count; l++)
                            {
                                ResourceVO resourceNextDay = null;
                                ResourceVO resource = listProjectCalendar.FirstOrDefault
                                                                (e =>
                                                                    e.DivisionID == divisionList[i].DivisionID &&
                                                                    e.CalendarDate == _view.CalendarDateRange[l]
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
                                    if (_view.CalendarDateRange.Count != l + 1)
                                    {
                                        resourceNextDay = listProjectCalendar.FirstOrDefault
                                                                        (e =>
                                                                            e.DivisionID == divisionList[i].DivisionID &&
                                                                            e.CalendarDate == _view.CalendarDateRange[l + 1]
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
                                    
                                    if (resource.EstimatedWork && !estimatedWorkStart && null == resourceNextDay)
                                    {
                                        sClass += " ResourceEstimatedEnd";
                                        estimatedWorkStart = true;
                                    }
                                }

                                if (resource.Reserved)
                                    sClass += " Reserved";

                                Globals.ProjectCalendar.ResourceType rType = (Globals.ProjectCalendar.ResourceType)resource.ResourceColor;

                                
                                switch (rType)
                                {
                                    case Globals.ProjectCalendar.ResourceType.ReservedEmployee:
                                        if(resource.EmployeeID.HasValue)
                                            sClass += " Resource Employee";
                                        break;
                                    case Globals.ProjectCalendar.ResourceType.ReservedEquipment:
                                        if(resource.EquipmentTypeID.HasValue)
                                            sClass += " Resource Equipment";
                                        break;
                                    case Globals.ProjectCalendar.ResourceType.AddEmployee:
                                        if(resource.EmployeeID.HasValue)
                                            sClass += " Resource AddEmployee";
                                        break;
                                    case Globals.ProjectCalendar.ResourceType.AddEquipment:
                                        if(resource.EquipmentID.HasValue)
                                            sClass += " Resource AddEquipment";
                                        break;
                                    default:
                                        break;
                                }



                                builder.Append("<td");
                                if (resource.EmployeeID.HasValue)
                                {
                                    builder.AppendFormat(" onmouseenter=\"javascript: ShowToolTip(document.getElementById('ProjectToolTipResource{0}'));\"", resource.EmployeeID.Value);
                                    builder.AppendFormat(" onmouseleave=\"javascript: document.getElementById('ProjectToolTipResource{0}').style.display = 'none';\" ", resource.EmployeeID.Value);
                                }
                                else if (resource.EquipmentID.HasValue)
                                {
                                    builder.AppendFormat(" onmouseenter=\"javascript: ShowToolTip(document.getElementById('ProjectToolTipResource{0}'));\"", resource.EquipmentID.Value);
                                    builder.AppendFormat(" onmouseleave=\"javascript: document.getElementById('ProjectToolTipResource{0}').style.display = 'none';\" ", resource.EquipmentID.Value);
                                }
                                else if (resource.EquipmentTypeID.HasValue)
                                {
                                    builder.AppendFormat(" onmouseenter=\"javascript: ShowToolTip(document.getElementById('ProjectToolTipResource{0}'));\"", resource.EquipmentTypeID.Value);
                                    builder.AppendFormat(" onmouseleave=\"javascript: document.getElementById('ProjectToolTipResource{0}').style.display = 'none';\" ", resource.EquipmentTypeID.Value);
                                }
                                if (!string.IsNullOrEmpty(sClass))
                                {
                                    builder.Append(" class='" + sClass.Trim() + "'><div>&nbsp;<div style='position:absolute;'>");

                                }
                                else
                                {
                                    builder.Append(">");
                                }                                

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

                                builder.Append("</div></div>");

                                if (resource.EmployeeID.HasValue)
                                {
                                    builder.Append(string.Format(@"<DIV style='background-color:white; display:none; position:fixed; width:250; z-index: 999;' id='ProjectToolTipResource{0}'>
	                                <DIV class='divUpperFields'>
	                                    <DIV class='label'><SPAN>Resource:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{1}</SPAN></DIV>
                                        </DIV>
                                    </DIV>", resource.EmployeeID.Value, resource.EmployeeName));
                                }
                                else if (resource.EquipmentID.HasValue)
                                {
                                    builder.Append(string.Format(@"<DIV style='background-color:white; display:none; position:fixed; width:250; z-index: 999;' id='ProjectToolTipResource{0}'>
	                                <DIV class='divUpperFields'>
	                                    <DIV class='label'><SPAN>Resource:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{1}</SPAN></DIV>
                                        </DIV>
                                    </DIV>", resource.EquipmentID.Value, resource.EquipmentName));
                                }
                                else if (resource.EquipmentTypeID.HasValue)
                                {
                                    builder.Append(string.Format(@"<DIV style='background-color:white; display:none; position:fixed; width:250; z-index: 999;' id='ProjectToolTipResource{0}'>
	                                <DIV class='divUpperFields'>
	                                    <DIV class='label'><SPAN>Resource:</SPAN></DIV>
	                                    <DIV class='text'><SPAN>{1}</SPAN></DIV>
                                        </DIV>
                                    </DIV>", resource.EquipmentTypeID.Value, resource.EquipmentTypeName));
                                }

                                builder.Append("</td>");
                            }
                            builder.Append("</tr>");
                        }
                    }

                }

                builder.Append("</tbody> </table>");
                _view.CalendarSource = builder.ToString();
            }

        }

        public void DefaultDayProjectCalendar()
        {
            _view.StartDateValue = DateTime.Now.AddDays(-6);
            _view.EndDateValue = DateTime.Now;
        }
        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            if (null != _model)
                _model.Dispose();

            _model = null;
        }

        #endregion
    }
}
