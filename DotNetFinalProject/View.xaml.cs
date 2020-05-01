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

namespace DotNetFinalProject
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        MapuaUniversityDataSet mapua = new MapuaUniversityDataSet();
        public View()
        {
            InitializeComponent();
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DotNetFinalProject.Properties.Settings.MapuaUniversity"].ConnectionString))
            {
                var command = new SqlCommand("dbo.ViewStudent", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var dataAdapter = new SqlDataAdapter(command);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                datagrid.IsReadOnly = true;
                datagrid.ItemsSource = dt.DefaultView;
                datagrid.SelectionChanged += Datagrid_SelectionChanged;

            }
        }

        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var items in datagrid.SelectedItems)
            {

                Console.Write(items);
            }
            Console.WriteLine();
        }
    }
}
