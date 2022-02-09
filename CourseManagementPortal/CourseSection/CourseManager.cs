using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CourseManagementPortal.CourseSection
{
    public class CourseManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        public DataTable GetAllCourses()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from tblCourse", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            sqlDataReader.Close();
            ConnectionCloseControl();

            return dataTable;
        }

        public void Add(Course course)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Insert into tblCourse(Name, Duration, Price, CreationTime) values(@name, @duration, @price, @creationTime)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", course.Name);
            sqlCommand.Parameters.AddWithValue("@duration", course.Duration);
            sqlCommand.Parameters.AddWithValue("@price", course.Price);
            sqlCommand.Parameters.AddWithValue("@creationTime", course.CreationTime);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Update(Course course)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Update tblCourse set Name = @name, Duration = @duration, Price = @price, ModificationTime = @modificationTime where Id = @id", _sqlConnection);

            sqlCommand.Parameters.AddWithValue("@name", course.Name);
            sqlCommand.Parameters.AddWithValue("@duration", course.Duration);
            sqlCommand.Parameters.AddWithValue("@price", course.Price);
            sqlCommand.Parameters.AddWithValue("@modificationTime", course.ModificationTime);
            sqlCommand.Parameters.AddWithValue("@id", course.Id);

            sqlCommand.ExecuteNonQuery();

            ConnectionCloseControl();
        }

        public void Delete(int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand(@"Delete from tblCourse where Id = @id", _sqlConnection);

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
