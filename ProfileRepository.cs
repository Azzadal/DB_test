using BD_test;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class ProfileRepository
    {
        MySqlConnection connection;

        public ProfileRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Profile> GetAll()
        {

            MySqlCommand command = new MySqlCommand("SELECT id, name, faculty, direction_id from profiles", connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<Profile> profiles = new List<Profile>();

            while (reader.Read())
            {
                profiles.Add(new Profile()
                {
                    Id = reader.GetInt16(0),

                    Name = reader.GetString(1),

                    Faculty = reader.GetString(2),

                    Direction_id = reader.GetInt16(3)
                });
            }
            reader.Close();

            return profiles;
        }
        
        public void Save(DirectionControl directionControl, List<int> p_id)
        {
            int u = 0;
            if(directionControl != null)
            {
                while (u < directionControl.ListBox.Items.Count)
                {
                    MySqlCommand command = new MySqlCommand(
                                            "INSERT INTO cart (user_id, position, prof_id)" +
                                            " VALUES (1, @p, @j)", connection
                                            );

                    command.Parameters.Add("@p", MySqlDbType.Int16).Value = u;

                    command.Parameters.Add("@j", MySqlDbType.Int16).Value = p_id[u];

                    MySqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    u++;
                }
            }
            
        }

        public void clearCartbd()
        {
            MySqlCommand command = new MySqlCommand("TRUNCATE TABLE cart", connection);

            MySqlDataReader reader = command.ExecuteReader();

            reader.Close();
        }
    }
}
