using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.CourseSection
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
    }
}
