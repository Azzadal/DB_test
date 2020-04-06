using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ProfileRepository
    {
        MySqlConnection connection;

public ProfileRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public List<int> GetId()
        {
            MySqlCommand command = new MySqlCommand("SELECT id FROM directions", connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<int> ids = new List<int>();

            while (reader.Read())
            {
                ids.Add(reader.GetInt16(0));
            }
            reader.Close();

            return ids;
        }

        static string ConvertIntArrayToStringJoin(int[] array)
        {
            string result = string.Join(",", array);
            return result;
        }

        public List<Profile> GetAll()
        {

            MySqlCommand command = new MySqlCommand("SELECT id, name, faculty, direction_id from profiles", connection);

            //List<int> ids = GetId();
            //int i = 0;
            //int[] a = new int[ids.Count()];
            //while (i < ids.Count())
            //{
            //    a[i] = ids[i];
            //    i++;
            //}
            //string result = ConvertIntArrayToStringJoin(a);

            //command.Parameters.Add("@id", MySqlDbType.VarString).Value = result;
            //Console.WriteLine(result);

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

        public List<Profile> GetProfilesManagement()
        {

            MySqlCommand command = new MySqlCommand(
                "SELECT id, name, faculty, direction_id from profiles where direction_id = 2",
                connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<Profile> profilesMan = new List<Profile>();

            while (reader.Read())
            {
                profilesMan.Add(new Profile()
                {
                    Id = reader.GetInt16(0),
                    Name = reader.GetString(1),
                    Faculty = reader.GetString(2),
                    Direction_id = reader.GetInt16(3)
                });
            }
            reader.Close();

            return profilesMan;
        }

        public List<Profile> GetProfilesLaw()
        {

            MySqlCommand command = new MySqlCommand(
                "SELECT id, name, faculty, direction_id from profiles where direction_id = 3",
                connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<Profile> profilesLaw = new List<Profile>();

            while (reader.Read())
            {
                profilesLaw.Add(new Profile()
                {
                    Id = reader.GetInt16(0),
                    Name = reader.GetString(1),
                    Faculty = reader.GetString(2),
                    Direction_id = reader.GetInt16(3)
                });
            }
            reader.Close();

            return profilesLaw;
        }

        public List<Profile> GetProfilesInfoTech()
        {

            MySqlCommand command = new MySqlCommand(
                "SELECT id, name, faculty, direction_id from profiles where direction_id = 4",
                connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<Profile> profilesInfoTech = new List<Profile>();

            while (reader.Read())
            {
                profilesInfoTech.Add(new Profile()
                {
                    Id = reader.GetInt16(0),
                    Name = reader.GetString(1),
                    Faculty = reader.GetString(2),
                    Direction_id = reader.GetInt16(3)
                });
            }
            reader.Close();

            return profilesInfoTech;
        }
    }
}
