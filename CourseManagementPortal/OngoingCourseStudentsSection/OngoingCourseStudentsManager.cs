using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.OngoingCourseStudentsSection
{
    public class OngoingCourseStudentsManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllOngoingCourseStudents()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("select ocs.Id as No, oc.Name, ocs.LessonName, s.Name as StudentName, s.Surname as StudentSurname, ocs.IsInLesson, ocs.LessonDate, ocs.Note from tblOngoingCourseStudents ocs " +
                "join tblOngoingCourses oc on ocs.OngoingCourseId = oc.Id " +
                "join tblStudent s on ocs.StudentId = s.Id", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(OngoingCourseStudents ongoingCourseStudents)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblOngoingCourseStudents(OngoingCourseId, LessonName, StudentId, IsInLesson, LessonDate, Note) values(@ongoingCourseId, @lessonName, @studentId, @isInLesson, @lessonDate, @note)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ongoingCourseId", ongoingCourseStudents.OngoingCourseId);
            sqlCommand.Parameters.AddWithValue("@lessonName", ongoingCourseStudents.LessonName);
            sqlCommand.Parameters.AddWithValue("@studentId", ongoingCourseStudents.StudentId);
            sqlCommand.Parameters.AddWithValue("@isInLesson", ongoingCourseStudents.IsInLesson);
            sqlCommand.Parameters.AddWithValue("@lessonDate", ongoingCourseStudents.LessonDate);
            sqlCommand.Parameters.AddWithValue("@note", ongoingCourseStudents.Note);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblOngoingCourseStudents where Id = @id", _sqlConnection);
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
