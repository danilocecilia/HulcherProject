using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class ProjectCalendarVO
    {
        public ProjectCalendarVO()
        {
            JobCalendarList = new List<JobCalendarVO>();
        }

        public string DivisionName { get; set; }

        public DateTime CalendarDate { get; set; }

        public List<JobCalendarVO> JobCalendarList { get; set; }

        public int DivisionID { get; set; }
    }

    [Serializable]
    public class JobCalendarVO
    {
        public JobCalendarVO()
        {
            ResourceList = new List<ResourceVO>();
        }

        public string Job { get; set; }

        public List<ResourceVO> ResourceList { get; set; }

        public bool PaintDate { get; set; }

        public int JobID { get; set; }

        public string CustomerName { get; set; }

        public string DivisionName { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }

        public object StatusName { get; set; }

        public object ActionName { get; set; }
    }

    [Serializable]
    public class ResourceVO
    {
        public string EmployeeName { get; set; }

        public string EquipmentName { get; set; }

        public string EquipmentTypeName { get; set; }

        public bool EstimatedWork { get; set; }

        public bool Reserved { get; set; }

        public bool Worked { get; set; }

        public int? EmployeeID { get; set; }

        public int? EquipmentID { get; set; }

        public int? EquipmentTypeID { get; set; }

        public int ResourceColor { get; set;}
    }
}
