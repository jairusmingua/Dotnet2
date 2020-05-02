using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DotNetFinalProject.Model;
using DotNetFinalProject.ViewModel;
namespace DotNetFinalProject
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {

        StudentViewModel student;
        SqlConnection con;
        SqlCommand cmd;
        int selectedItem = -1;
        public View()
        {
            InitializeComponent();
            student = new StudentViewModel();
            lstBox.DataContext = student;
        }

        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (lstBox.DataContext != null)
            {
                try
                {

                    var item = (ListBox)sender;
                    Student selectedStudent =student.SelectedItem((Student)item.SelectedItem);
                    if (selectedStudent != null)
                    {

                        this.selectedItem = selectedStudent.ID;
                        nameLbl.Content = selectedStudent.Name;
                        emailLbl.Content = selectedStudent.Email;
                        phoneLbl.Content = selectedStudent.PhoneNo;
                        officeLbl.Content = selectedStudent.OfficeNo;
                        courseLbl.Content = selectedStudent.CourseTitle;
                        courseFeeLbl.Content = selectedStudent.CourseFee;
                    }
                }
                catch
                {

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result =MessageBox.Show("Are you sure to delete this item", "Delete Selected", MessageBoxButton.YesNoCancel);
            if (result.ToString() == "Yes")
            {

                if (selectedItem != -1)
                {
                    try
                    {
                        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString);
                        cmd = new SqlCommand("dbo.DeleteStudent", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", selectedItem);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        student.RefreshList();
                        student.FillList();
                        con.Close();
                    }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = (TextBox)sender;
            student.RefreshList();
            student.SearchStudent(query.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure to delete this item", "Delete Selected", MessageBoxButton.YesNoCancel);
            if (result.ToString() == "Yes")
            {

              
                    try
                    {
                        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString);
                        cmd = new SqlCommand("dbo.DeleteAll", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        student.RefreshList();
                        student.FillList();
                        con.Close();
                    }
                
            }
        }
    }
}
