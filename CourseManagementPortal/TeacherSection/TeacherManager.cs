using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.TeacherSection
{
    public class TeacherManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllTeachers()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from tblTeacher", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            sqlDataReader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(Teacher teacher)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblTeacher(Name, Surname, DateOfBirth, Profession) values(@name, @surname, @dateOfBirth, @profession)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", teacher.Name);
            sqlCommand.Parameters.AddWithValue("@surname", teacher.Surname);
            sqlCommand.Parameters.AddWithValue("@dateOfBirth", teacher.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@profession", teacher.Profession);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Update(Teacher teacher)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Update tblTeacher set Name = @name, Surname = @surname, DateOfBirth = @dateOfBirth, Profession = @profession where Id = @id", _sqlConnection);

            sqlCommand.Parameters.AddWithValue("@name", teacher.Name);
            sqlCommand.Parameters.AddWithValue("@surname", teacher.Surname);
            sqlCommand.Parameters.AddWithValue("@dateOfBirth", teacher.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@profession", teacher.Profession);
            sqlCommand.Parameters.AddWithValue("@id", teacher.Id);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblTeacher where Id = @id", _sqlConnection);

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
