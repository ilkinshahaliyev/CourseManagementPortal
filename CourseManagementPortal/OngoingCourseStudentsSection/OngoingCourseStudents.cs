using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.OngoingCourseStudentsSection
{
    public class OngoingCourseStudents
    {
        public int Id { get; set; }
        public int OngoingCourseId { get; set; }
        public string? LessonName { get; set; }
        public int StudentId { get; set; }
        public string? IsInLesson { get; set; }
        public DateTime LessonDate { get; set; }
        public string? Note { get; set; }
    }
}
