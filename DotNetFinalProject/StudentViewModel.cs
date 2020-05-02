using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DotNetFinalProject.Model;
namespace DotNetFinalProject.ViewModel
{
    class StudentViewModel : INotifyPropertyChanged
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        public ObservableCollection<Student> student { get; set; }

        public StudentViewModel()
        {
            FillList();
        }
        public void RefreshList()
        {
            if (student != null)
            {
                student.Clear();
            }
        }
        public void FillList()
        {
           
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString);
                cmd = new SqlCommand("dbo.ViewStudents", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds);
                if (student == null)
                    student = new ObservableCollection<Student>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    student.Add(new Student
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        StudentID = Convert.ToInt32(dr[1].ToString()),
                        Name = dr[2].ToString(),
                        CourseTitle = dr[3].ToString()
                        //Email = dr[3].ToString(),
                        //PhoneNo = dr[4].ToString(),
                        //OfficeNo = dr[5].ToString(),
                        //AddressStud = dr[6].ToString()
                    }); ;
                }
            }
            catch { }
            finally {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }
       
        public Student SearchStudent(string query)
        {

            if (query != null)
            {

                try
                {
                    con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString);
                    cmd = new SqlCommand("dbo.SearchStudent", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", query);
                    adapter = new SqlDataAdapter(cmd); 
                    ds = new DataSet();
                    adapter.Fill(ds);
                    if (student == null)
                        student = new ObservableCollection<Student>();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        student.Add(new Student
                        {
                            ID = Convert.ToInt32(dr[0].ToString()),
                            StudentID = Convert.ToInt32(dr[1].ToString()),
                            Name = dr[2].ToString(),
                            CourseTitle = dr[3].ToString()
                     
                        }); ;
                    }
                }
                catch { }
                finally
                {
                    ds = null;
                    adapter.Dispose();
                    con.Close();
                    con.Dispose();
                }
                return null;
            }
            return null;
        }
        public Student SelectedItem(Student student)
        {
            if (student != null)
            {

                try
                {
                    con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString);
                    cmd = new SqlCommand("dbo.ViewStudent", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", student.ID);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    return new Student {
                        ID = Convert.ToInt32(reader["id"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        Name =reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        OfficeNo = reader["OfficeNo"].ToString(),
                        AddressStud = reader["AddressStud"].ToString(),
                        CourseTitle = reader["CourseTitle"].ToString(),
                        CourseFee = reader["CourseFee"].ToString()
                    

                    };
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return null;
            }
            return null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
