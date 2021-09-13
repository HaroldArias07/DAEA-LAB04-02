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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSemana04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("Data Source=HAROLD_ARIAS\\SQLEXPRESS; Initial Catalog=DAEA; Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedCommand e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<People> people = new List<People>();

            connection.Open();
            SqlCommand command = new SqlCommand("BuscarPersonaNombre", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;

            parameter2.Value = "";
            parameter2.ParameterName = "@FirstName";

            command.Parameters.Add(parameter2);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new People
                {
                    PersondId = dataReader["PersonID"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),

                    Fullname = string.Concat(dataReader["FirstName"].ToString(), " ",

                    dataReader["LastName"].ToString())
                });
            }

            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}
