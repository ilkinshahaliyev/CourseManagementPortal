using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.OngoingCoursesSection
{
    public class OngoingCourses
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
