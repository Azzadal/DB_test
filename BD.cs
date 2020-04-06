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
        List<TextBox> textBoxList1 = new List<TextBox>();
        List<ListBox> listBoxList = new List<ListBox>();
        List<GroupBox> groupBoxList = new List<GroupBox>();
        //GroupBox groupBox = new GroupBox();
        List<SelectedProfile> selectedProfiles = new List<SelectedProfile>();
        
        public BD()
        {
            InitializeComponent();
            connection.Open();
            DirectionRepository direction_repository = new DirectionRepository(connection);
            ProfileRepository profile_repository = new ProfileRepository(connection);
            List<Direction> directions = direction_repository.GetAll();
            List<Profile> profiles = profile_repository.GetAll();
            Dictionary<int, Direction> directionsById = new Dictionary<int, Direction>();


            Dictionary<int, List<Profile>> profilesByDirectionId = new Dictionary<int, List<Profile>>();
            foreach (Profile profile in profiles)
            {
                List<Profile> directionProfiles;
                if (!profilesByDirectionId.TryGetValue(profile.Direction_id, out directionProfiles))
                {
                    directionProfiles = new List<Profile>();
                    profilesByDirectionId.Add(profile.Direction_id, directionProfiles);
                    
                }
                directionProfiles.Add(profile);
            }


            List<int> ids = profile_repository.GetId();
            int i = 0;
            int[] a = new int[ids.Count()];
            while (i < ids.Count())
            {
                a[i] = ids[i];
                i++;
            }
            Console.WriteLine(a[0]);


            foreach (Direction direction in directions)
            {
                directionsById.Add(direction.Id, direction);
            }

            foreach (var direction in directions)
            {
                var directionProfiles = profilesByDirectionId[direction.Id];
                var textBox = new TextBox();
                var listBox = new ListBox();
                var groupBox = new GroupBox();
                var btnUp = new Button();
                var btnDown = new Button();
                groupBox.AutoSize = true;
                groupBox.Text = "Направление";
                btnUp.Location = new Point(15, 65);
                btnDown.Location = new Point(15, 90);
                textBox.Location = new Point(5, 15);
                listBox.Location = new Point(35, 65);
                listBox.SelectedIndexChanged += (s, e) =>
                {
                    //MessageBox.Show(listBox.SelectedIndex.ToString());
                };
                textBox.Text += direction.Name + '\r' + '\n';
                btnUp.BackgroundImage = Image.FromFile(@"C:\Users\Andrey\source\repos\BD_test\WindowsFormsApp1\images\arrow_up.ico");
                btnUp.BackgroundImageLayout = ImageLayout.Stretch;
                btnDown.BackgroundImage = Image.FromFile(@"C:\Users\Andrey\source\repos\BD_test\WindowsFormsApp1\images\arrow_down.ico");
                btnDown.BackgroundImageLayout = ImageLayout.Stretch;
                foreach (var profile in directionProfiles)
                {
                    listBox.Items.Add(profile.Direction_id + "\t\t" + profile.Name + "\t\t" + profile.Faculty);
                }
                textBox.Size = new Size(391, 50);
                listBox.Size = new Size(340, 50);
                btnUp.Size = new Size(17, 17);
                btnDown.Size = new Size(17, 17);
                textBox.Multiline = true;
                textBox.ReadOnly = true;
                btnUp.Click += (s, e) => {
                    
                        object Item = listBox.SelectedItem;
                        int ItemIndex = listBox.SelectedIndex;

                        if(ItemIndex > 0)
                        {
                            listBox.Items.Remove(Item);
                            listBox.Items.Insert(ItemIndex - 1, Item);
                        }
                        else
                        {
                            MessageBox.Show("Достигнуто начало списка");
                        }

                };
                btnDown.Click += (s, e) => {

                    object Item = listBox.SelectedItem;
                    int ItemIndex = listBox.SelectedIndex;

                    if (ItemIndex < listBox.Items.Count - 1)
                    {
                        listBox.Items.Remove(Item);
                        listBox.Items.Insert(ItemIndex + 1, Item);
                    }
                    else
                    {
                        MessageBox.Show("Достигнут конец списка");
                    }

                };
                groupBox.Cursor = Cursors.Hand;
                groupBox.Controls.Add(textBox);
                groupBox.Controls.Add(listBox);
                groupBox.Controls.Add(btnUp);
                groupBox.Controls.Add(btnDown);
                groupBox.FlatStyle = FlatStyle.Standard;
                int u = 1;
                groupBox.Click += (s, e) =>
                {
                    while (u < listBox.Items.Count)
                    {
                        MySqlCommand command = new MySqlCommand(
                                                "INSERT INTO cart (user_id, position, prof_id)" +
                                                " VALUES (1, @p, @j)", connection
                                                );

                        command.Parameters.Add("@p", MySqlDbType.Int16).Value = u;
                        command.Parameters.Add("@j", MySqlDbType.Int16).Value = u;


                        MySqlDataReader reader = command.ExecuteReader();

                        reader.Close();
                       // MessageBox.Show(u.ToString());
                        u++;
                    }
                    MessageBox.Show(profiles[u].Direction_id.ToString());
                    cart.Controls.Add(groupBox);
                };
                groupBoxList.Add(groupBox);
                DirectionsDB.Controls.Add(groupBox);
            }

            int y = 0;
            //while (y < groupBoxList.Count)
            //{
            //    DirectionsDB.Controls.Add(groupBoxList[y]);
            //    groupBoxList[y].Click += AddToCart;
            //    y++;
            //}

        }

        //private void AddToCart(object sender, EventArgs e)
        //{
        //    GroupBox direction = (GroupBox)sender;
        //    cart.Controls.Add(direction);
        //}

        






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



            //directMan.ReadOnly = false;
            //fieldDir.Enabled = false;
            //directMan.Enabled = false;
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

            fieldDir.Text = directions[1].Name + '\r' + '\n';

            listBox1.Items.Add(profiles[0].Name);

            //directLaw.ReadOnly = false;
            //fieldDir.Enabled = false;
            //directLaw.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MySqlCommand command = new MySqlCommand("TRUNCATE TABLE cart", connection);

            MySqlDataReader reader = command.ExecuteReader();

            reader.Close();

            //directMan.ReadOnly = true;
            //directLaw.ReadOnly = true;
            //directInfoTech.ReadOnly = true;

            //directMan.Enabled = true;
            //directLaw.Enabled = true;
            //directInfoTech.Enabled = true;

        }

        private void directInfoTech_Click(object sender, EventArgs e)
        {
            ListBox listBox1 = new ListBox();

            DirectionRepository direction_repository = new DirectionRepository(connection);

            ProfileRepository profile_repository = new ProfileRepository(connection);

            List<Direction> directions = direction_repository.GetAll();

            List<Profile> profiles = profile_repository.GetProfilesInfoTech();

            Button up = new Button();
            up.Location = new System.Drawing.Point(115, 184);
            up.Size = new System.Drawing.Size(15, 15);

            Point point = new Point(3, 147);

            listBox1.Size = new System.Drawing.Size(200, 60);

            listBox1.Location = new System.Drawing.Point(130, 184);

            listBox1.MultiColumn = true;

            TextBox fieldDir = new TextBox();

            fieldDir.ReadOnly = true;

            fieldDir.Location = point;

            fieldDir.Multiline = true;

            fieldDir.Size = new System.Drawing.Size(379, 38);

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
            //directInfoTech.ReadOnly = false;
            //fieldDir.Enabled = false;
            //directInfoTech.Enabled = false;
        }
    }
    
}
