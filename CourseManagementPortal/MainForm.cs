using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseManagementPortal.CourseSection;
using CourseManagementPortal.StudentSection;
using CourseManagementPortal.TeacherSection;
using CourseManagementPortal.TeacherCourseSection;
using CourseManagementPortal.OngoingCoursesSection;
using CourseManagementPortal.OngoingCourseStudentsSection;

namespace CourseManagementPortal
{
    public partial class frmMain : Form
    {
        SqlConnection _sqlConnection = new SqlConnection(@"Server = .\SQLEXPRESS; Database = DBCourseManagementPortal; Trusted_Connection = True; TrustServerCertificate = True;");

        CourseManager _courseManager = new CourseManager();

        StudentManager _studentManager = new StudentManager();

        TeacherManager _teacherManager = new TeacherManager();

        TeacherCourseManager _teacherCourseManager = new TeacherCourseManager();

        OngoingCourseManager _ongoingCourseManager = new OngoingCourseManager();

        OngoingCourseStudentsManager _ongoingCourseStudentsManager = new OngoingCourseStudentsManager();

        public frmMain()
        {
            InitializeComponent();
        }

        private void LoadCourses()
        {
            dgvCourse.DataSource = _courseManager.GetAllCourses();
        }

        private void LoadStudents()
        {
            dgvStudent.DataSource = _studentManager.GetAllStudents();
        }

        private void LoadTeachers()
        {
            dgvTeacher.DataSource = _teacherManager.GetAllTeachers();
        }

        private void LoadTeacherCourses()
        {
            dgvTeacherCourse.DataSource = _teacherCourseManager.GetAllTeacherCourses();
        }

        private void LoadOngoingCourses()
        {
            dgvOngoingCourses.DataSource = _ongoingCourseManager.GetAllOngoingCourses();
        }

        private void LoadOngoingCourseStudents()
        {
            dgvOngoingCourseStudents.DataSource = _ongoingCourseStudentsManager.GetAllOngoingCourseStudents();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to Course management portal.", "Course management portal", MessageBoxButtons.OK);

            LoadCourses();

            LoadStudents();

            CellClickCourse();

            LoadTeachers();

            LoadTeacherCourses();

            LoadOngoingCourses();

            LoadOngoingCourseStudents();
        }

        private void btnCourseCreate_Click(object sender, EventArgs e)
        {
            Course course = new()
            {
                Name = tbxCourseNameCreate.Text,
                Duration = Convert.ToInt32(nudCourseDurationCreate.Value),
                Price = Convert.ToInt32(nudCoursePriceCreate.Value),
                CreationTime = DateTime.Now
            };

            if (string.IsNullOrEmpty(tbxCourseNameCreate.Text) || nudCourseDurationCreate.Value <= 0 || nudCoursePriceCreate.Value <= 0)
            {
                MessageBox.Show("No course were added! Course name can't be empty and duration and " +
                    "price must be greater than 0", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _courseManager.Add(course);

                MessageBox.Show("Course added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadCourses();

            tbxCourseNameCreate.Text = string.Empty;
            nudCourseDurationCreate.Value = 0;
            nudCoursePriceCreate.Value = 0;
        }

        private void btnCourseUpdate_Click(object sender, EventArgs e)
        {
            Course course = new()
            {
                Id = Convert.ToInt32(dgvCourse.CurrentRow.Cells[0].Value),
                Name = tbxCourseNameUpdate.Text,
                Duration = Convert.ToInt32(nudCourseDurationUpdate.Value),
                Price = Convert.ToInt32(nudCoursePriceUpdate.Value),
                ModificationTime = DateTime.Now
            };

            _courseManager.Update(course);

            LoadCourses();

            MessageBox.Show("Course updated succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCourseDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvCourse.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"The course id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _courseManager.Delete(id);

                LoadCourses();

                MessageBox.Show($"The course id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No course were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCourse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CellClickCourse();
        }

        private void CellClickCourse()
        {
            try
            {
                tbxCourseNameUpdate.Text = dgvCourse.CurrentRow.Cells[1].Value.ToString();
                nudCourseDurationUpdate.Value = Convert.ToInt32(dgvCourse.CurrentRow.Cells[2].Value);
                nudCoursePriceUpdate.Value = Convert.ToInt32(dgvCourse.CurrentRow.Cells[3].Value);
            }
            catch
            {
                MessageBox.Show("Error : Do not click to an empty cell!", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStudentCreate_Click(object sender, EventArgs e)
        {
            Student student = new()
            {
                Name = tbxStudentNameCreate.Text,
                Surname = tbxStudentSurnameCreate.Text,
                DateOfBirth = dtpStudentDateOfBirthCreate.Value,
                CreationTime = DateTime.Now
            };

            if (string.IsNullOrEmpty(tbxStudentNameCreate.Text) || string.IsNullOrEmpty(tbxStudentSurnameCreate.Text))
            {
                MessageBox.Show("No student were added! Student name and surname can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _studentManager.Add(student);

                MessageBox.Show("Student added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadStudents();

            tbxStudentNameCreate.Text = string.Empty;
            tbxStudentSurnameCreate.Text = string.Empty;
        }

        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            Student student = new()
            {
                Id = Convert.ToInt32(dgvStudent.CurrentRow.Cells[0].Value),
                Name = tbxStudentNameUpdate.Text,
                Surname = tbxStudentSurnameUpdate.Text,
                DateOfBirth = dtpStudentDateOfBirthUpdate.Value,
                ModificationTime = DateTime.Now
            };

            _studentManager.Update(student);

            LoadStudents();

            MessageBox.Show("Student updated succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStudentDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvStudent.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"The student id {id} will be deleted. " +
                $"Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _studentManager.Delete(id);

                LoadStudents();

                MessageBox.Show($"The student id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No student were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CellClickStudent();
        }

        private void CellClickStudent()
        {
            try
            {
                tbxStudentNameUpdate.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString();
                tbxStudentSurnameUpdate.Text = dgvStudent.CurrentRow.Cells[2].Value.ToString();
                dtpStudentDateOfBirthUpdate.Value = Convert.ToDateTime(dgvStudent.CurrentRow.Cells[3].Value);
            }
            catch
            {
                MessageBox.Show("Error : Do not click to an empty cell!", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTeacherCreate_Click(object sender, EventArgs e)
        {
            Teacher teacher = new()
            {
                Name = tbxTeacherNameCreate.Text,
                Surname = tbxTeacherSurnameCreate.Text,
                DateOfBirth = dtpTeacherDateOfBirthCreate.Value,
                Profession = cbTeacherProfession.Text
            };

            if (string.IsNullOrEmpty(tbxTeacherNameCreate.Text) || string.IsNullOrEmpty(tbxTeacherSurnameCreate.Text) || string.IsNullOrEmpty(cbTeacherProfession.Text))
            {
                MessageBox.Show("No teacher were added! Teacher name, surname and profession name " +
                    "can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _teacherManager.Add(teacher);

                MessageBox.Show("Teacher added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadTeachers();

            tbxTeacherNameCreate.Text = string.Empty;
            tbxTeacherSurnameCreate.Text = string.Empty;
        }

        private void btnTeacherUpdate_Click(object sender, EventArgs e)
        {
            Teacher teacher = new()
            {
                Id = Convert.ToInt32(dgvTeacher.CurrentRow.Cells[0].Value),
                Name = tbxTeacherNameUpdate.Text,
                Surname = tbxTeacherSurnameUpdate.Text,
                DateOfBirth = dtpTeacherDateOfBirthUpdate.Value,
                Profession = cbTeacherProfessionUpdate.Text
            };

            _teacherManager.Add(teacher);

            LoadTeachers();

            MessageBox.Show("Teacher updated succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTeacherDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvTeacher.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"The teacher id {id} will be deleted. " +
                $"Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _teacherManager.Delete(id);

                LoadTeachers();

                MessageBox.Show($"The teacher id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No teacher were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CellClickSTeacher();
        }

        private void CellClickSTeacher()
        {
            try
            {
                tbxTeacherNameUpdate.Text = dgvTeacher.CurrentRow.Cells[1].Value.ToString();
                tbxTeacherSurnameUpdate.Text = dgvTeacher.CurrentRow.Cells[2].Value.ToString();
                dtpTeacherDateOfBirthUpdate.Value = Convert.ToDateTime(dgvTeacher.CurrentRow.Cells[3].Value);
                cbTeacherProfessionUpdate.Text = dgvTeacher.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Error : Do not click to an empty cell!", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetProfessions()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblCourse", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                cbTeacherProfession.Items.Add(reader.GetString(0));
            }

            reader.Close();
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

        private void cbTeacherProfession_Click(object sender, EventArgs e)
        {
            cbTeacherProfession.Items.Clear();

            GetProfessions();
        }

        private void cbTeacherProfessionUpdate_Click(object sender, EventArgs e)
        {
            cbTeacherProfessionUpdate.Items.Clear();

            GetProfessionsForUpdate();
        }

        private void GetProfessionsForUpdate()
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblCourse", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                cbTeacherProfessionUpdate.Items.Add(reader.GetString(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private int GetTeacherId(string name, string surname)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new($"Select Id from tblTeacher where Name = '{name}' and Surname = '{surname}'", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            int id = 0;

            while (sqlDataReader.Read())
            {
                id = Convert.ToInt32(sqlDataReader.GetInt32(0));
            }

            sqlDataReader.Close();
            ConnectionCloseControl();

            return id;
        }

        private int GetCourseId(string name)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new($"Select Id from tblCourse where Name = '{name}'", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            int id = 0;

            while (sqlDataReader.Read())
            {
                id = Convert.ToInt32(sqlDataReader.GetInt32(0));
            }

            sqlDataReader.Close();
            ConnectionCloseControl();

            return id;
        }

        private void btnCreateTeacherCourse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbTeacherIdTeacherCourse.Text) || string.IsNullOrEmpty(cbTeacherIdTeacherCourse.Text))
            {
                MessageBox.Show("No contact between teacher and course were added! " +
                                "Teacher id and course id can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var resultTeacher = GetTeacherId(cbTeacherIdTeacherCourse.Text.Split(' ')[0], cbTeacherIdTeacherCourse.Text.Split(' ')[1]);
                var resultCourse = GetCourseId(cbCourseIdTeacherCourse.Text);

                TeacherCourse teacherCourse = new()
                {
                    TeacherId = resultTeacher,
                    CourseId = resultCourse
                };

                _teacherCourseManager.Add(teacherCourse);

                MessageBox.Show("Contact between teacher and course added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadTeacherCourses();
        }

        private void GetTeachersNameAndSurname(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name, Surname from tblTeacher", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetString(0) + " " + reader.GetString(1));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbTeacherIdTeacherCourse_Click(object sender, EventArgs e)
        {
            cbTeacherIdTeacherCourse.Items.Clear();

            GetTeachersNameAndSurname(cbTeacherIdTeacherCourse);
        }

        private void GetCourseName(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblCourse", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetString(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbCourseIdTeacherCourse_Click(object sender, EventArgs e)
        {
            cbCourseIdTeacherCourse.Items.Clear();

            GetCourseName(cbCourseIdTeacherCourse);
        }

        private void btnDeleteTeacherCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvTeacherCourse.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"The contact between teacher and course id {id} will be deleted. " +
                $"Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _teacherCourseManager.Delete(id);

                LoadTeacherCourses();

                MessageBox.Show($"The contact between teacher and course id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No contact between teacher and course were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbCourseIdOngoingCourses_Click(object sender, EventArgs e)
        {
            cbCourseIdOngoingCourses.Items.Clear();

            GetCourseName(cbCourseIdOngoingCourses);
        }

        private void cbTeacherIdOngoingCourses_Click(object sender, EventArgs e)
        {
            cbTeacherIdOngoingCourses.Items.Clear();

            GetTeachersNameAndSurname(cbTeacherIdOngoingCourses);
        }

        private void btnPlanOngoingCourse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbCourseIdOngoingCourses.Text) || string.IsNullOrEmpty(cbTeacherIdOngoingCourses.Text) || string.IsNullOrEmpty(tbxCourseNameOngoingCourse.Text))
            {
                MessageBox.Show("No ongoing course were added! Teacher id, course id and name can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var resultCourse = GetCourseId(cbCourseIdOngoingCourses.Text);
                var resultTeacher = GetTeacherId(cbTeacherIdOngoingCourses.Text.Split(' ')[0], cbTeacherIdOngoingCourses.Text.Split(' ')[1]);

                OngoingCourses ongoingCourses = new()
                {
                    CourseId = resultCourse,
                    TeacherId = resultTeacher,
                    Name = tbxCourseNameOngoingCourse.Text,
                    PlannedStartDate = dtpPlannedStartDateOngoingCourses.Value,
                    PlannedEndDate = dtpPlannedEndDateOngoingCourse.Value
                };

                _ongoingCourseManager.Add(ongoingCourses);

                MessageBox.Show("Ongoing course added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadOngoingCourses();
        }

        private void btnDeleteOngoingCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvOngoingCourses.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"Ongoing course id {id} will be deleted. " +
                $"Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _ongoingCourseManager.Delete(id);

                LoadOngoingCourses();

                MessageBox.Show($"Ongoing course id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No ongoing course were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GetStudentsNameAndSurname(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name, Surname from tblStudent", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetString(0) + " " + reader.GetString(1));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbOngoingCourseId_Click(object sender, EventArgs e)
        {
            cbOngoingCourseId.Items.Clear();

            GetOngoingCourseName(cbOngoingCourseId);
        }

        private void cbStudentIdOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            cbStudentIdOngoingCourseStudents.Items.Clear();

            GetStudentsNameAndSurname(cbStudentIdOngoingCourseStudents);
        }

        private void GetOngoingCourseName(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblOngoingCourses", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetString(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private int GetOngoingCourseId(string name)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new($"Select Id from tblOngoingCourses where Name = '{name}'", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            int id = 0;

            while (sqlDataReader.Read())
            {
                id = Convert.ToInt32(sqlDataReader.GetInt32(0));
            }

            sqlDataReader.Close();
            ConnectionCloseControl();

            return id;
        }

        private int GetStudentId(string name, string surname)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new($"Select Id from tblStudent where Name = '{name}' and Surname = '{surname}'", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            int id = 0;

            while (sqlDataReader.Read())
            {
                id = Convert.ToInt32(sqlDataReader.GetInt32(0));
            }

            sqlDataReader.Close();
            ConnectionCloseControl();

            return id;
        }

        private void btnAddStudentOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbOngoingCourseId.Text) || string.IsNullOrEmpty(cbStudentIdOngoingCourseStudents.Text) || string.IsNullOrEmpty(tbxLessonName.Text))
            {
                MessageBox.Show("No ongoing course student were added! " +
                    "Student id, ongoing course id and lesson name can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var resultOngoingCourse = GetOngoingCourseId(cbOngoingCourseId.Text);
                var resultStudent = GetStudentId(cbStudentIdOngoingCourseStudents.Text.Split(' ')[0], cbStudentIdOngoingCourseStudents.Text.Split(' ')[1]);

                OngoingCourseStudents ongoingCourseStudents = new()
                {
                    OngoingCourseId = resultOngoingCourse,
                    LessonName = tbxLessonName.Text,
                    StudentId = resultStudent,
                    LessonDate = dtpLessonDate.Value,
                    Note = tbxNote.Text
                };

                if (rbInLesson.Checked == true)
                {
                    ongoingCourseStudents.IsInLesson = rbInLesson.Text;
                }
                else if (rbNotInLesson.Checked == true)
                {
                    ongoingCourseStudents.IsInLesson = rbNotInLesson.Text;
                }

                _ongoingCourseStudentsManager.Add(ongoingCourseStudents);

                MessageBox.Show("Ongoing course student added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadOngoingCourseStudents();
        }

        private void btnDeleteStudentOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvOngoingCourseStudents.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"Ongoing course student id {id} will be deleted. " +
                $"Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _ongoingCourseStudentsManager.Delete(id);

                LoadOngoingCourseStudents();

                MessageBox.Show($"Ongoing course student id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No ongoing course student were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable GetResult(string tableName, string text)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand($"Select * from {tableName} where Name like '{text}%'", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            DataTable result = new DataTable();
            result.Load(reader);

            reader.Close();
            ConnectionCloseControl();

            return result;
        }

        private void ResultForCourse()
        {
            string text = tbxSearchCourse.Text;

            dgvCourse.DataSource = GetResult("tblCourse", text);
        }

        private void tbxSearchCourse_TextChanged(object sender, EventArgs e)
        {
            if (tbxSearchCourse.Text.Length > 1)
            {
                ResultForCourse();
            }

            if (tbxSearchCourse.Text == string.Empty)
            {
                LoadCourses();
            }
        }

        private void ResultForStudent()
        {
            string text = tbxSearchStudent.Text;

            dgvStudent.DataSource = GetResult("tblStudent", text);
        }

        private void tbxSearchStudent_TextChanged(object sender, EventArgs e)
        {
            if (tbxSearchStudent.Text.Length > 1)
            {
                ResultForStudent();
            }

            if (tbxSearchStudent.Text == string.Empty)
            {
                LoadStudents();
            }
        }

        private void ResultForTeacher()
        {
            string text = tbxSearchTeacher.Text;

            dgvTeacher.DataSource = GetResult("tblTeacher", text);
        }

        private void tbxSearchTeacher_TextChanged(object sender, EventArgs e)
        {
            if (tbxSearchTeacher.Text.Length > 1)
            {
                ResultForTeacher();
            }

            if (tbxSearchTeacher.Text == string.Empty)
            {
                LoadTeachers();
            }
        }

        private void btnStartCourse_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvOngoingCourses.CurrentRow.Cells[0].Value);

            OngoingCourses ongoingCourses = new()
            {
                Id = id,
                StartDate = dtpStartCourseStartDate.Value,
                EndDate = dtpStartCourseEndDate.Value
            };

            _ongoingCourseManager.Update(ongoingCourses);

            LoadOngoingCourses();

            MessageBox.Show("Course started successfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
