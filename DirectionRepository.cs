using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class DirectionRepository
    {
        private MySqlConnection connection;

        
        public DirectionRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Direction> GetAll()
        {

            MySqlCommand command = new MySqlCommand("SELECT id, name FROM directions", connection);

            MySqlDataReader reader = command.ExecuteReader();
        
            List<Direction> directions = new List<Direction>();

            while (reader.Read())
            {
                directions.Add(new Direction()
                {
                    Id = reader.GetInt16(0),
                    Name = reader.GetString(1)
                });
            }
            reader.Close();

            return directions;

            
        }
    
    }
}
