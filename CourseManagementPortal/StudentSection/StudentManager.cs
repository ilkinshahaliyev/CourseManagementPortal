using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortal.StudentSection
{
    public class StudentManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllStudents()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from tblStudent", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            sqlDataReader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(Student student)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblStudent(Name, Surname, DateOfBirth, CreationTime) values(@name, @surname, @dateOfBirth, @creationTime)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", student.Name);
            sqlCommand.Parameters.AddWithValue("@surname", student.Surname);
            sqlCommand.Parameters.AddWithValue("@dateOfBirth", student.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@creationTime", student.CreationTime);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Update(Student student)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Update tblStudent set Name = @name, Surname = @surname, DateOfBirth = @dateOfBirth, ModificationTime = @modificationTime where Id = @id", _sqlConnection);

            sqlCommand.Parameters.AddWithValue("@name", student.Name);
            sqlCommand.Parameters.AddWithValue("@surname", student.Surname);
            sqlCommand.Parameters.AddWithValue("@dateOfBirth", student.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@modificationTime", student.ModificationTime);
            sqlCommand.Parameters.AddWithValue("@id", student.Id);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblStudent where Id = @id", _sqlConnection);

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
