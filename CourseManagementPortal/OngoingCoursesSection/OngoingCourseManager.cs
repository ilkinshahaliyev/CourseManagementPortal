using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.OngoingCoursesSection
{
    public class OngoingCourseManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllOngoingCourses()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select oc.Id, c.Name as CourseName, t.Name as TeacherName, t.Surname as TeacherSurname, oc.StartDate, oc.EndDate from tblOngoingCourses oc " +
                "join tblCourse c on oc.CourseId = c.Id " +
                "join tblTeacher t on oc.TeacherId = t.Id", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(OngoingCourses ongoingCourses)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblOngoingCourses(CourseId, TeacherId, StartDate, EndDate) values(@courseId, @teacherId, @startDate, @endDate)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@courseId", ongoingCourses.CourseId);
            sqlCommand.Parameters.AddWithValue("@teacherId", ongoingCourses.TeacherId);
            sqlCommand.Parameters.AddWithValue("@startDate", ongoingCourses.StartDate);
            sqlCommand.Parameters.AddWithValue("@endDate", ongoingCourses.EndDate);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblOngoingCourses where Id = @id", _sqlConnection);
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
