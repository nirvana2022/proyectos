using System;
using System.Data;
using MySqlConnector;

namespace ETLExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceConnectionString = "Server=127.0.0.1;Database=etl;Uid=root;Pwd=;AllowLoadLocalInfile=true;";
            string destinationConnectionString = "Server=127.0.0.1;Database=etl;Uid=root;Pwd=;AllowLoadLocalInfile=true;";

            // Extraer datos de la fuente
            DataTable sourceData = ExtractData(sourceConnectionString);

            // Transformar datos
            DataTable transformedData = TransformData(sourceData);

            // Cargar datos en el destino
            LoadData(transformedData, destinationConnectionString);

            Console.WriteLine("Proceso ETL completado.");
            Console.ReadLine();
        }

        static DataTable ExtractData(string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM TablaOrigen";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        static DataTable TransformData(DataTable sourceData)
        {
            DataTable transformedData = new DataTable();
            transformedData.Columns.Add("Id", typeof(int));
            transformedData.Columns.Add("NombreCompleto", typeof(string));
            transformedData.Columns.Add("Edad", typeof(int));

            foreach (DataRow row in sourceData.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string nombre = row["Nombre"].ToString();
                string apellido = row["Apellido"].ToString();
                int edad = Convert.ToInt32(row["Edad"]);

                string nombreCompleto = $"{nombre} {apellido}";

                transformedData.Rows.Add(id, nombreCompleto, edad);
            }

            return transformedData;
        }

        static void LoadData(DataTable data, string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Eliminar datos existentes en la tabla de destino
                string deleteQuery = "DELETE FROM TablaDestino";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.ExecuteNonQuery();

                // Cargar datos en la tabla de destino
                MySqlBulkCopy bulkCopy = new MySqlBulkCopy(connection);
                {
                    bulkCopy.DestinationTableName = "TablaDestino";
                    bulkCopy.WriteToServer(data);
                }
            }
        }
    }
}
