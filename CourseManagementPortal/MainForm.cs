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
using CourseManagementPortal.PlannedCoursesSection;
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

        PlannedCoursesManager _plannedCoursesManager = new PlannedCoursesManager();

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

        private void LoadPlannedCourses()
        {
            dgvPlannedCourses.DataSource = _plannedCoursesManager.GetAllPlannedCourses();
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

            LoadPlannedCourses();

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
                MessageBox.Show("No course were added! Course name can't be empty and duration and price must be greater than 0", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var dialogResult = MessageBox.Show($"The student id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                MessageBox.Show("No teacher were added! Teacher name, surname and profession name can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var dialogResult = MessageBox.Show($"The teacher id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

        private void btnCreateTeacherCourse_Click(object sender, EventArgs e)
        {
            TeacherCourse teacherCourse = new()
            {
                TeacherId = Convert.ToInt32(cbTeacherIdTeacherCourse.Text),
                CourseId = Convert.ToInt32(cbCourseIdTeacherCourse.Text)
            };

            if (string.IsNullOrEmpty(cbTeacherIdTeacherCourse.Text) || string.IsNullOrEmpty(cbCourseIdTeacherCourse.Text))
            {
                MessageBox.Show("No contact between teacher and course were added! Teacher id and course id can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _teacherCourseManager.Add(teacherCourse);

                MessageBox.Show("Contact between teacher and course added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadTeacherCourses();
        }

        private void GetTeachersId(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Id from tblTeacher", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetInt32(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbTeacherIdTeacherCourse_Click(object sender, EventArgs e)
        {
            cbTeacherIdTeacherCourse.Items.Clear();

            GetTeachersId(cbTeacherIdTeacherCourse);
        }

        private void GetCourseId(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Id from tblCourse", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetInt32(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbCourseIdTeacherCourse_Click(object sender, EventArgs e)
        {
            cbCourseIdTeacherCourse.Items.Clear();

            GetCourseId(cbCourseIdTeacherCourse);
        }

        private void GetTeacherName(TextBox textBox, int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblTeacher where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                textBox.Text = sqlDataReader.GetString("Name");
            }

            sqlDataReader.Close();
            ConnectionCloseControl();
        }

        private void GetTeacherSurname(TextBox textBox, int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Surname from tblTeacher where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                textBox.Text = sqlDataReader.GetString("Surname");
            }

            sqlDataReader.Close();
            ConnectionCloseControl();
        }

        private void btnGetTeacherData_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbTeacherIdTeacherCourse.Text);

            GetTeacherName(tbxTeacherNameTeacherCourse, id);

            GetTeacherSurname(tbxTeacherSurnameTeacherCourse, id);
        }

        private void GetCourseName(TextBox textBox, int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblCourse where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                textBox.Text = sqlDataReader.GetString("Name");
            }

            sqlDataReader.Close();
            ConnectionCloseControl();
        }

        private void btnGetCourseData_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbCourseIdTeacherCourse.Text);

            GetCourseName(tbxCourseNameTeacherCourse, id);
        }

        private void btnDeleteTeacherCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvTeacherCourse.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"The contact between teacher and course id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

        private void btnGetCourseDataPlannedCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbCourseIdPlannedCourses.Text);

            GetCourseName(tbxCourseNamePlannedCourse, id);
        }

        private void cbCourseIdPlannedCourses_Click(object sender, EventArgs e)
        {
            cbCourseIdPlannedCourses.Items.Clear();

            GetCourseId(cbCourseIdPlannedCourses);
        }

        private void cbTeacherIdPlannedCourses_Click(object sender, EventArgs e)
        {
            cbTeacherIdPlannedCourses.Items.Clear();

            GetTeachersId(cbTeacherIdPlannedCourses);
        }

        private void btnGetTeacherDataPlannedCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbTeacherIdPlannedCourses.Text);

            GetTeacherName(tbxTeacherNamePlannedCourses, id);

            GetTeacherSurname(tbxTeacherSurnamePlannedCourses, id);
        }

        private void btnPlanNewCourse_Click(object sender, EventArgs e)
        {
            PlannedCourses plannedCourses = new()
            {
                CourseId = Convert.ToInt32(cbCourseIdPlannedCourses.Text),
                TeacherId = Convert.ToInt32(cbTeacherIdPlannedCourses.Text),
                PlannedStartDate = dtpPlannedStartDate.Value,
                PlannedEndDate = dtpPlannedEndDate.Value
            };

            if (string.IsNullOrEmpty(cbCourseIdPlannedCourses.Text) || string.IsNullOrEmpty(cbTeacherIdPlannedCourses.Text))
            {
                MessageBox.Show("No planned course were added! Teacher id and course id can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _plannedCoursesManager.Add(plannedCourses);

                MessageBox.Show("Planned course added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadPlannedCourses();
        }

        private void btnDeletePlannedCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvPlannedCourses.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"Planned course id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                _plannedCoursesManager.Delete(id);

                LoadPlannedCourses();

                MessageBox.Show($"Planned course id {id} deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No planned course were deleted.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbCourseIdOngoingCourses_Click(object sender, EventArgs e)
        {
            cbCourseIdOngoingCourses.Items.Clear();

            GetCourseId(cbCourseIdOngoingCourses);
        }

        private void btnGetCourseDataOngoingCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbCourseIdOngoingCourses.Text);

            GetCourseName(tbxCourseNameOngoingCourses, id);
        }

        private void cbTeacherIdOngoingCourses_Click(object sender, EventArgs e)
        {
            cbTeacherIdOngoingCourses.Items.Clear();

            GetTeachersId(cbTeacherIdOngoingCourses);
        }

        private void btnGetTeacherDataOngoingCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbTeacherIdOngoingCourses.Text);

            GetTeacherName(tbxTeacherNameOngoingCourse, id);

            GetTeacherSurname(tbxTeacherSurnameOngoingCourse, id);
        }

        private void btnStartOngoingCourse_Click(object sender, EventArgs e)
        {
            OngoingCourses ongoingCourses = new()
            {
                CourseId = Convert.ToInt32(cbCourseIdOngoingCourses.Text),
                TeacherId = Convert.ToInt32(cbTeacherIdOngoingCourses.Text),
                StartDate = dtpStartDateOngoingCourses.Value,
                EndDate = dtpEndDateOngoingCourse.Value
            };

            if (string.IsNullOrEmpty(cbCourseIdOngoingCourses.Text) || string.IsNullOrEmpty(cbTeacherIdOngoingCourses.Text))
            {
                MessageBox.Show("No ongoing course were added! Teacher id and course id can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _ongoingCourseManager.Add(ongoingCourses);

                MessageBox.Show("Ongoing course added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadOngoingCourses();
        }

        private void btnDeleteOngoingCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvOngoingCourses.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"Ongoing course id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

        private void GetOngoingCourseId(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Id from tblOngoingCourses", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetInt32(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbOngoingCourseId_Click(object sender, EventArgs e)
        {
            cbOngoingCourseId.Items.Clear();

            GetOngoingCourseId(cbOngoingCourseId);
        }

        private void GetStudentId(ComboBox comboBox)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Id from tblStudent", _sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader.GetInt32(0));
            }

            reader.Close();
            ConnectionCloseControl();
        }

        private void cbStudentIdOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            cbStudentIdOngoingCourseStudents.Items.Clear();

            GetStudentId(cbStudentIdOngoingCourseStudents);
        }

        private void GetStudentName(TextBox textBox, int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Name from tblStudent where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                textBox.Text = sqlDataReader.GetString("Name");
            }

            sqlDataReader.Close();
            ConnectionCloseControl();
        }

        private void GetStudentSurname(TextBox textBox, int id)
        {
            ConnectionOpenControl();

            SqlCommand sqlCommand = new SqlCommand("Select Surname from tblStudent where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                textBox.Text = sqlDataReader.GetString("Surname");
            }

            sqlDataReader.Close();
            ConnectionCloseControl();
        }

        private void btnGetStudentDataOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbStudentIdOngoingCourseStudents.Text);

            GetStudentName(tbxStudentNameOngoingCourseStudents, id);

            GetStudentSurname(tbxStudentSurnameOngoingCourseStudents, id);
        }

        private void btnAddStudentOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            OngoingCourseStudents ongoingCourseStudents = new()
            {
                OngoingCourseId = Convert.ToInt32(cbOngoingCourseId.Text),
                LessonName = tbxLessonName.Text,
                StudentId = Convert.ToInt32(cbStudentIdOngoingCourseStudents.Text),
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

            if (string.IsNullOrEmpty(cbOngoingCourseId.Text) || string.IsNullOrEmpty(cbStudentIdOngoingCourseStudents.Text))
            {
                MessageBox.Show("No ongoing course student were added! Student id and ongoing course id can't be empty", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _ongoingCourseStudentsManager.Add(ongoingCourseStudents);

                MessageBox.Show("Ongoing course student added succesfully.", "Course management portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadOngoingCourseStudents();
        }

        private void btnDeleteStudentOngoingCourseStudents_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvOngoingCourseStudents.CurrentRow.Cells[0].Value);

            var dialogResult = MessageBox.Show($"Ongoing course student id {id} will be deleted. Are you sure about that?", "Course management portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
    }
}
