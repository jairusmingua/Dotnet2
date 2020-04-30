using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace DotNetFinalProject
{
    /// <summary>
    /// Interaction logic for CCESCDialog.xaml
    /// </summary>
    public partial class CCESCDialog : Window
    {
        private string Name,Address, OfficeNo, MobilePhoneNo,Email,TinNo,studentNo;
        DateTime time;
        List<int> price;
        MainWindow main;
        public CCESCDialog(MainWindow main,string Name,string Address,string OfficeNo,string MobilePhoneNo,string Email,DateTime time,string TinNo,string studentNo)
        {
            InitializeComponent();
            this.Name = Name;
            this.Address = Address;
            this.OfficeNo = OfficeNo;
            this.MobilePhoneNo = MobilePhoneNo;
            this.Email = Email;
            this.time = time;
            this.TinNo = TinNo;
            this.studentNo = studentNo;
            this.main = main;
            price = new List<int>();
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString))
            {
                var command = new SqlCommand("dbo.GetCourse", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    courseList.Items.Add(reader[0].ToString());
                    price.Add(Convert.ToInt32(reader[1]));
                }
                command = new SqlCommand("dbo.GetControlNo", conn);
                conn.Close();
                conn.Open();

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    controlNoTxt.Text = (Convert.ToInt32(reader[0])+1).ToString();
                  
                }
                conn.Close();
                dateTxt.Text = time.ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString))
            using (var command = new SqlCommand("dbo.ADDStudent", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();

                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(this.studentNo));
                    command.Parameters.AddWithValue("@Name", this.Name);
                    command.Parameters.AddWithValue("@Email", this.Email);
                    command.Parameters.AddWithValue("@Vat", this.withVatCheckBox.IsChecked.ToString());
                    command.Parameters.AddWithValue("@TinNo", this.TinNo != "" ? Convert.ToInt32(this.TinNo) : 0);
                    command.Parameters.AddWithValue("@CrseTitle", courseList.Items[courseList.SelectedIndex].ToString());
                    command.Parameters.AddWithValue("@CrseFee", Convert.ToInt32(courseFeeTxt.Text));
                    command.Parameters.AddWithValue("@DateReg", this.time);
                    command.Parameters.AddWithValue("@PhoneNo", this.MobilePhoneNo);
                    command.Parameters.AddWithValue("@OfficeNo", this.OfficeNo);
                    command.Parameters.AddWithValue("@Address", this.Address);
                    command.ExecuteNonQuery();
                    main.clearFields();
                    conn.Close();
                    MessageBox.Show("Registration Complete!!!");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                

            }



        }
        private void CourseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            courseFeeTxt.Text = price[courseList.SelectedIndex].ToString();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

    }
}
