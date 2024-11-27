using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CustomORMExample
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening SQL connection: {ex.Message}");
                throw;
            }
        }

        public List<T> ExecuteQuery<T>(string query) where T : new()
        {
            var results = new List<T>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = MapReaderToObject<T>(reader);
                        results.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
            return results;
        }

        public int ExecuteCommand(string query, List<SqlParameter> parameters)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
                throw;
            }
        }

        private T MapReaderToObject<T>(IDataRecord record) where T : new()
        {
            var obj = new T();
            foreach (var property in typeof(T).GetProperties())
            {
                if (!record.IsDBNull(record.GetOrdinal(property.Name)))
                {
                    property.SetValue(obj, record[property.Name]);
                }
            }
            return obj;
        }
    }
}
