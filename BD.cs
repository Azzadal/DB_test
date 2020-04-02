using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BD : Form
    {
        static string connString = "server=localhost;port=3306;username=root;password=1234;database=db";
        MySqlConnection connection = new MySqlConnection(connString);
        List<TextBox> textBoxList = new List<TextBox>();
        List<TextBox> textBoxListPanelRight = new List<TextBox>();
        List<SelectedProfile> selectedProfiles = new List<SelectedProfile>();
        
        public BD()
        {
            InitializeComponent();
            textBoxList.Add(directMan);
            textBoxList.Add(directLaw);
            textBoxList.Add(directInfoTech);
            connection.Open();

            directLaw.Enabled = true;
            directInfoTech.Enabled = true;

            DirectionRepository direction_repository = new DirectionRepository(connection);

            List<Direction> directions = direction_repository.GetAll();

            directMan.Text += directions[0].Name;
            directLaw.Text += directions[1].Name;
            directInfoTech.Text += directions[2].Name;
            
            ProfileRepository profile_repository = new ProfileRepository(connection);

            List<Profile> profiles = profile_repository.GetAll();
            List<Profile> profilesMan = profile_repository.GetProfilesManagement();
            List<Profile> profilesLaw = profile_repository.GetProfilesLaw();
            List<Profile> profilesInfoTech = profile_repository.GetProfilesInfoTech();
            int j = 0;
            while (j < profilesMan.Count)
            {
                profileMan.Text += profilesMan[j].Name + '\r' + '\n';
                facultyMan.Text += profilesMan[j].Faculty + '\r' + '\n';
                j++;
                profileMan2.Text += profilesMan[j].Name + '\r' + '\n';
                facultyMan.Text += profilesMan[j].Faculty + '\r' + '\n';
                j++;
            }
            j = 0;
            while (j < profilesLaw.Count)
            {
                profileLaw.Text += profilesLaw[j].Name + '\r' + '\n';
                facultyLaw.Text += profilesLaw[j].Faculty + '\r' + '\n';
                j++;
            }
            j = 0;
            while (j < profilesInfoTech.Count)
            {
                profileInfoTech.Text += profilesInfoTech[j].Name + '\r' + '\n';
                facultyinfoTech.Text += profilesInfoTech[j].Faculty + '\r' + '\n';
                j++;
                profileInfoTech2.Text += profilesInfoTech[j].Name + '\r' + '\n' + '\n';
                facultyinfoTech.Text += profilesInfoTech[j].Faculty + '\r' + '\n';
                j++;
                profileInfoTech3.Text += profilesInfoTech[j].Name + '\r' + '\n';
                facultyinfoTech.Text += profilesInfoTech[j].Faculty + '\r' + '\n';
                j++;
            }

        }

        private void test(string a)
        {
            MessageBox.Show(a);
        }

        private void directMan_Click(object sender, EventArgs e)
        {
            ListBox listBox1 = new ListBox();

            

            DirectionRepository direction_repository = new DirectionRepository(connection);

            ProfileRepository profile_repository = new ProfileRepository(connection);

            List<Direction> directions = direction_repository.GetAll();

            List<Profile> profiles = profile_repository.GetProfilesManagement();

            Point point = new Point(3, 3);

            listBox1.Size = new System.Drawing.Size(200, 42);

            listBox1.Location = new System.Drawing.Point(130, 40);

            listBox1.MultiColumn = true;

            TextBox fieldDir = new TextBox();

            fieldDir.ReadOnly = true;

            fieldDir.Location = point;

            fieldDir.Multiline = true;

            fieldDir.Size = new System.Drawing.Size(379, 38);

            panel5.Controls.Add(fieldDir);

            panel5.Controls.Add(listBox1);

            fieldDir.Text = directions[0].Name + '\r' + '\n';

            listBox1.Items.Add(profiles[0].Name);

            listBox1.Items.Add(profiles[1].Name);

            int profInd = listBox1.Items.Count;
            int i = 1, j;
            while (i <= profInd)
            {
                j = i;
                MySqlCommand command = new MySqlCommand("INSERT INTO cart (user_id, position, prof_id)" +
                " VALUES (1, @p, @j)", connection);

                command.Parameters.Add("@p", MySqlDbType.Int16).Value = i;
                command.Parameters.Add("@j", MySqlDbType.Int16).Value = j;


                MySqlDataReader reader = command.ExecuteReader();

                reader.Close();
                i++;
            }



            directMan.ReadOnly = false;
            fieldDir.Enabled = false;
            directMan.Enabled = false;
        }

        private void directLaw_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO cart (user_id, position, prof_id)" +
                " VALUES (1, 1, 3)", connection);

            MySqlDataReader reader = command.ExecuteReader();

            reader.Close();

            DirectionRepository direction_repository = new DirectionRepository(connection);

            ProfileRepository profile_repository = new ProfileRepository(connection);

            List<Direction> directions = direction_repository.GetAll();

            List<Profile> profiles = profile_repository.GetProfilesLaw();

            Point point = new Point(3, 75);

            ListBox listBox1 = new ListBox();

            listBox1.Size = new System.Drawing.Size(200, 30);

            listBox1.Location = new System.Drawing.Point(130, 112);

            listBox1.MultiColumn = true;

            TextBox fieldDir = new TextBox();

            fieldDir.ReadOnly = true;

            fieldDir.Location = point;

            fieldDir.Multiline = true;

            fieldDir.Size = new System.Drawing.Size(379, 38);

            panel5.Controls.Add(fieldDir);

            panel5.Controls.Add(listBox1);

            fieldDir.Text = directions[1].Name + '\r' + '\n';

            listBox1.Items.Add(profiles[0].Name);

            directLaw.ReadOnly = false;
            fieldDir.Enabled = false;
            directLaw.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();

            MySqlCommand command = new MySqlCommand("TRUNCATE TABLE cart", connection);

            MySqlDataReader reader = command.ExecuteReader();

            reader.Close();

            directMan.ReadOnly = true;
            directLaw.ReadOnly = true;
            directInfoTech.ReadOnly = true;

            directMan.Enabled = true;
            directLaw.Enabled = true;
            directInfoTech.Enabled = true;

        }

        private void directInfoTech_Click(object sender, EventArgs e)
        {
            ListBox listBox1 = new ListBox();

            DirectionRepository direction_repository = new DirectionRepository(connection);

            ProfileRepository profile_repository = new ProfileRepository(connection);

            List<Direction> directions = direction_repository.GetAll();

            List<Profile> profiles = profile_repository.GetProfilesInfoTech();

            Point point = new Point(3, 147);

            listBox1.Size = new System.Drawing.Size(200, 60);

            listBox1.Location = new System.Drawing.Point(130, 184);

            listBox1.MultiColumn = true;

            TextBox fieldDir = new TextBox();

            fieldDir.ReadOnly = true;

            fieldDir.Location = point;

            fieldDir.Multiline = true;

            fieldDir.Size = new System.Drawing.Size(379, 38);

            panel5.Controls.Add(fieldDir);

            panel5.Controls.Add(listBox1);

            fieldDir.Text = directions[2].Name + '\r' + '\n';

            listBox1.Items.Add(profiles[0].Name);

            listBox1.Items.Add(profiles[1].Name);

            listBox1.Items.Add(profiles[2].Name);

            int profInd = listBox1.Items.Count;
            int i = 1, j = 4;
            while (i <= profInd)
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO cart (user_id, position, prof_id)" +
                " VALUES (1, @p, @j)", connection);

                command.Parameters.Add("@p", MySqlDbType.Int16).Value = i;
                command.Parameters.Add("@j", MySqlDbType.Int16).Value = j;


                MySqlDataReader reader = command.ExecuteReader();

                reader.Close();
                i++;
                j++;
            }
            directInfoTech.ReadOnly = false;
            fieldDir.Enabled = false;
            directInfoTech.Enabled = false;
        }
    }
    
}
