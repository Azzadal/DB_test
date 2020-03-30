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
        List<SelectedProfile> selectedProfiles = new List<SelectedProfile>();
        public BD()
        {
            InitializeComponent();

            //string connString = "server=localhost;port=3306;username=root;password=1234;database=db";
            //MySqlConnection connection = new MySqlConnection(connString);
            textBoxList.Add(directMan);
            textBoxList.Add(directLaw);
            textBoxList.Add(directInfoTech);
            connection.Open();

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
                profileInfoTech2.Text += profilesInfoTech[j].Name + '\r' + '\n';
                j++;
                profileInfoTech3.Text += profilesInfoTech[j].Name + '\r' + '\n';
                j++;
            }

        }

        private void test(string a)
        {
            MessageBox.Show(a);
        }
        
        private void directMan_Click(object sender, EventArgs e)
        {
            DirectionRepository direction_repository = new DirectionRepository(connection);

            ProfileRepository profile_repository = new ProfileRepository(connection);

            List<Direction> directions = direction_repository.GetAll();

            List<Profile> profiles = profile_repository.GetProfilesManagement();

            Point point = new Point(3, 3);

            ListBox listBox1 = new ListBox();

            listBox1.Size = new System.Drawing.Size(200, 42);

            listBox1.Location = new System.Drawing.Point(130, 40);

            listBox1.MultiColumn = true;

            TextBox fieldDir = new TextBox();

            TextBox fieldProf1 = new TextBox();

            TextBox fieldProf2 = new TextBox();

            fieldDir.Location = point;

            fieldProf1.Left = point.X + 50;

            fieldProf1.Top = point.Y + 40;

            fieldProf2.Left = point.X + 50;

            fieldProf2.Top = point.Y + 60;

            fieldDir.Multiline = true;

            fieldDir.Size = new System.Drawing.Size(379, 38);

            fieldProf1.Size = new System.Drawing.Size(248, 20);

            fieldProf2.Size = new System.Drawing.Size(248, 20);

            panel5.Controls.Add(fieldDir);

            panel5.Controls.Add(listBox1);

            fieldDir.Text = directions[0].Name + '\r' + '\n';

            listBox1.Items.Add(profiles[0].Name);

            listBox1.Items.Add(profiles[1].Name);

            fieldProf1.Text += profiles[0].Name;

            fieldProf2.Text += profiles[1].Name;

            fieldDir.Enabled = false;
        }
    }
}
