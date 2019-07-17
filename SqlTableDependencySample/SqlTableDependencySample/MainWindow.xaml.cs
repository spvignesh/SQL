using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;

namespace SqlTableDependencySample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _con = @"data source=(LocalDb)\MSSQLLocalDB; initial catalog=Customer; integrated security=True";
        private SqlTableDependency<Customer>  dep;
        public ObservableCollection<Customer> GridSource { get; set; } = new ObservableCollection<Customer>();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var mapper = new ModelToTableMapper<Customer>();
            mapper.AddMapping(c => c.Surname, "Last Name");
            mapper.AddMapping(c => c.Name, "First Name");
            
                dep = new SqlTableDependency<Customer>(_con, "Customers", mapper: mapper);

                dep.OnChanged += Changed;
                dep.Start();

            using (SqlConnection connection = new SqlConnection(_con))
            using (SqlCommand command = new SqlCommand("Select * from Customers", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cust = new Customer();
                        cust.Id = Convert.ToInt32(reader["Id"]);
                        cust.Name = reader["First Name"].ToString();
                        cust.Surname = reader["Last Name"].ToString();
                        GridSource.Add(cust);
                    }
                }
            }

            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            dep.Stop();
        }

        public void Changed(object sender, RecordChangedEventArgs<Customer> e)
        {
            var changedEntity = e.Entity;

            if(e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
                Application.Current.Dispatcher.Invoke(() => { GridSource.Add(changedEntity); });
            else if(e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Delete)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var entity = GridSource.FirstOrDefault(x => x.Id == changedEntity.Id);
                    GridSource.Remove(entity);
                }
                );
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var entity = GridSource.FirstOrDefault(x => x.Id == changedEntity.Id);
                    GridSource.Remove(entity);
                    GridSource.Add(changedEntity);
                });
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
