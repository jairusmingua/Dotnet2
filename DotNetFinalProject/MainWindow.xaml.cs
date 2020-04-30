using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DotNetFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            
        }
        
        private void timer_Tick(object sender, EventArgs e)
        {
            clockLbl.Text = DateTime.Now.ToLongTimeString();
            dateLbl.Text = DateTime.Now.ToLongDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> invalid = new List<string>();
            try
            {
                if (nametxt.Text=="")
                {
                    invalid.Add("Name");
                }
                if (addressTxt.Text == "")
                {
                    invalid.Add("Address");
                }
                if (officeTxt.Text == "")
                {
                    invalid.Add("Res/Ofc No.");
                }
                if (mobileTxt.Text == "")
                {
                    invalid.Add("Phone No.");
                }
                if (emailTxt.Text == "")
                {
                    invalid.Add("Email");
                }
                if (studentTxt.Text == "")
                {
                    invalid.Add("Student No.");
                }
                if (invalid.Count > 0)
                {
                    string x = "";
                    foreach(string fields in invalid)
                    {
                        x += fields + "\n";
                    }
                    throw new Exception("Missing Fields!!!\n\n"+x);
                }
                CCESCDialog dialog = new CCESCDialog(this,nametxt.Text, addressTxt.Text, officeTxt.Text, mobileTxt.Text, emailTxt.Text, DateTime.Now,tinTxt.Text,studentTxt.Text);
                dialog.Show();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void clearFields()
        {
            nametxt.Clear();
            addressTxt.Clear();
            officeTxt.Clear();
            mobileTxt.Clear();
            emailTxt.Clear();
            tinTxt.Clear();
            studentTxt.Clear();
        }

        
    }
}
