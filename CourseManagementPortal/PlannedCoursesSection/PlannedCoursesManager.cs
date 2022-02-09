using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.PlannedCoursesSection
{
    public class PlannedCoursesManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllPlannedCourses()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("select pc.Id, c.Name as CourseName, t.Name as TeacherName, t.Surname as TeacherSurname, pc.PlannedStartDate, pc.PlannedEndDate from tblPlannedCourses pc " +
                "join tblCourse c on pc.CourseId = c.Id " +
                "join tblTeacher t on pc.TeacherId = t.Id", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(PlannedCourses plannedCourses)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblPlannedCourses(CourseId, TeacherId, PlannedStartDate, PlannedEndDate) values(@courseId, @teacherId, @plannedStartDate, @plannedEndDate)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@courseId", plannedCourses.CourseId);
            sqlCommand.Parameters.AddWithValue("@teacherId", plannedCourses.TeacherId);
            sqlCommand.Parameters.AddWithValue("@plannedStartDate", plannedCourses.PlannedStartDate);
            sqlCommand.Parameters.AddWithValue("@plannedEndDate", plannedCourses.PlannedEndDate);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblPlannedCourses where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        private void ConnectionOpenControl()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }

        private void ConnectionCloseControl()
        {
            if (_sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
        }
    }
}
