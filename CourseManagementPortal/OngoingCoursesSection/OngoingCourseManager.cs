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

            SqlCommand sqlCommand = new SqlCommand("Select oc.Id as No, oc.Name, c.Name as CourseName, t.Name as TeacherName, t.Surname as TeacherSurname, oc.PlannedStartDate, oc.PlannedEndDate, oc.StartDate, oc.EndDate from tblOngoingCourses oc " +
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

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblOngoingCourses(Name, CourseId, TeacherId, PlannedStartDate, PlannedEndDate) values(@name, @courseId, @teacherId, @plannedStartDate, @plannedEndDate)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", ongoingCourses.Name);
            sqlCommand.Parameters.AddWithValue("@courseId", ongoingCourses.CourseId);
            sqlCommand.Parameters.AddWithValue("@teacherId", ongoingCourses.TeacherId);
            sqlCommand.Parameters.AddWithValue("@plannedStartDate", ongoingCourses.PlannedStartDate);
            sqlCommand.Parameters.AddWithValue("@plannedEndDate", ongoingCourses.PlannedEndDate);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Update(OngoingCourses ongoingCourses)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Update tblOngoingCourses set StartDate = @startDate, EndDate = @endDate where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@startDate", ongoingCourses.StartDate);
            sqlCommand.Parameters.AddWithValue("@endDate", ongoingCourses.EndDate);
            sqlCommand.Parameters.AddWithValue("@id", ongoingCourses.Id);

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
