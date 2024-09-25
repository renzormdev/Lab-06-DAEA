using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace crud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListarEmpleado(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1507-10\\SQLEXPRESS02;Initial Catalog=NeptunoB;User ID=usuario01;Password=123456;";
            ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetEmpleados", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empleados.Add(new Empleado
                    {
                        IdEmpleado = (int)reader["IdEmpleado"],
                        Apellidos = reader["Apellidos"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Cargo = reader["Cargo"].ToString(),
                        Tratamiento = reader["Tratamiento"].ToString(),
                        FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? ((DateTime)reader["FechaNacimiento"]).ToString("yyyy-MM-dd") : null,
                        FechaContratacion = reader["FechaContratacion"] != DBNull.Value ? ((DateTime)reader["FechaContratacion"]).ToString("yyyy-MM-dd") : null,
                        Direccion = reader["Direccion"].ToString(),
                        Ciudad = reader["Ciudad"].ToString(),
                        Region = reader["Region"].ToString(),
                        CodPostal = reader["CodPostal"].ToString(),
                        Pais = reader["Pais"].ToString(),
                        TelDomicilio = reader["TelDomicilio"].ToString(),
                        Extension = reader["Extension"].ToString(),
                        Notas = reader["Notas"].ToString(),
                        Jefe = reader["Jefe"] != DBNull.Value ? (int)reader["Jefe"] : (int?)null,
                        SueldoBasico = reader["SueldoBasico"] != DBNull.Value ? (decimal)reader["SueldoBasico"] : (decimal?)null
                    });

                }
            }
            dgvEmpleados.ItemsSource = empleados;
        }

        private void AgregarEmpleado(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1507-10\\SQLEXPRESS02;Initial Catalog=NeptunoB;User ID=usuario01;Password=123456;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEmpleado", 9);
                cmd.Parameters.AddWithValue("@Apellidos", "García");
                cmd.Parameters.AddWithValue("@Nombre", "Carlos");
                cmd.Parameters.AddWithValue("@Cargo", "Desarrollador");
                cmd.Parameters.AddWithValue("@Tratamiento", "Sr.");
                cmd.Parameters.AddWithValue("@FechaNacimiento", DateTime.Parse("1990-01-01"));
                cmd.Parameters.AddWithValue("@FechaContratacion", DateTime.Now);
                cmd.Parameters.AddWithValue("@Direccion", "Calle Falsa 123");
                cmd.Parameters.AddWithValue("@Ciudad", "Lima");
                cmd.Parameters.AddWithValue("@Region", "Lima");
                cmd.Parameters.AddWithValue("@CodPostal", "12345");
                cmd.Parameters.AddWithValue("@Pais", "Perú");
                cmd.Parameters.AddWithValue("@TelDomicilio", "123456789");
                cmd.Parameters.AddWithValue("@Extension", "123");
                cmd.Parameters.AddWithValue("@Notas", "Empleado excelente");
                cmd.Parameters.AddWithValue("@Jefe", 1);
                cmd.Parameters.AddWithValue("@SueldoBasico", 2500);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Empleado agregado correctamente!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar empleado: {ex.Message}");
                }
            }
            ListarEmpleado(null, null);
        }

        private void ActualizarEmpleado(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1507-10\\SQLEXPRESS02;Initial Catalog=NeptunoB;User ID=usuario01;Password=123456;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEmpleado", 1);
                cmd.Parameters.AddWithValue("@Apellidos", "Miguel");
                cmd.Parameters.AddWithValue("@Nombre", "Gimenez");
                cmd.Parameters.AddWithValue("@Cargo", "Desarrollador Senior");
                cmd.Parameters.AddWithValue("@Tratamiento", "Sr.");
                cmd.Parameters.AddWithValue("@FechaNacimiento", new DateTime(1990, 5, 20));
                cmd.Parameters.AddWithValue("@FechaContratacion", new DateTime(2022, 1, 15));
                cmd.Parameters.AddWithValue("@Direccion", "Av. Siempre Viva 123");
                cmd.Parameters.AddWithValue("@Ciudad", "Lima");
                cmd.Parameters.AddWithValue("@Region", "Lima");
                cmd.Parameters.AddWithValue("@CodPostal", "15001");
                cmd.Parameters.AddWithValue("@Pais", "Perú");
                cmd.Parameters.AddWithValue("@TelDomicilio", "555-1234");
                cmd.Parameters.AddWithValue("@Extension", "101");
                cmd.Parameters.AddWithValue("@Notas", "Empleado con alta experiencia en desarrollo.");
                cmd.Parameters.AddWithValue("@Jefe", 2);
                cmd.Parameters.AddWithValue("@SueldoBasico", 7500.00M);


                con.Open();
                cmd.ExecuteNonQuery();
            }
            ListarEmpleado(null, null);
        }

        private void EliminarEmpleado(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1507-10\\SQLEXPRESS02;Initial Catalog=NeptunoB;User ID=usuario01;Password=123456;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEmpleado", 9);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            ListarEmpleado(null, null);
        }



    }
}