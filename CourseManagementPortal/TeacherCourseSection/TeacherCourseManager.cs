using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.TeacherCourseSection
{
    public class TeacherCourseManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllTeacherCourses()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("select tc.Id as No, t.Name as TeacherName, t.Surname as TeacherSurname, c.Name as CourseName from tblTeacherCourse tc " +
                "join tblTeacher t on tc.TeacherId = t.Id " +
                "join tblCourse c on tc.CourseId = c.Id", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(TeacherCourse teacherCourse)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblTeacherCourse(TeacherId, CourseId) values(@teacherId, @courseId)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@teacherId", teacherCourse.TeacherId);
            sqlCommand.Parameters.AddWithValue("@courseId", teacherCourse.CourseId);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblTeacherCourse where Id = @id", _sqlConnection);
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
