using BD_test;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BD : Form
    {
        static string connString = "server=localhost;port=3306;username=root;password=1234;database=db";

        MySqlConnection connection = new MySqlConnection(connString);

        DirectionRepository direction_repository;

        ProfileRepository profile_repository;

        Dictionary<int, Direction> directionsById;

        Dictionary<int, List<Profile>> profilesByDirectionId;

        private FlowLayoutPanel directionsDB;

        private FlowLayoutPanel cart;

        private Button saveButton;
        private Button clearCart;

        

        public BD()
        {
            connection.Open();

            direction_repository = new DirectionRepository(connection);

            profile_repository = new ProfileRepository(connection);

            InitializeComponent();

            Forma();
        }
        public void Forma()
        {
                

                List<Direction> directions = direction_repository.GetAll();

                List<Profile> profiles = profile_repository.GetAll();

                directionsById = new Dictionary<int, Direction>();

                profilesByDirectionId = new Dictionary<int, List<Profile>>();

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

                foreach (Direction direction in directions)
                {
                    directionsById.Add(direction.Id, direction);
                }

            foreach (var direction in directions)
            {
                HashSet<int> _id = new HashSet<int>();
                var directionProfiles = profilesByDirectionId[direction.Id];

                DirectionControl directionControl = new DirectionControl(direction, directionProfiles);

                directionsDB.Controls.Add(directionControl);

                directionControl.BtnUp.Visible = false;

                directionControl.BtnDown.Visible = false;

                directionControl.CloseControl.Visible = false;

                directionControl.Click += (s, e) =>
                    {
                        bool isFound = _id.Contains(direction.Id);
                        if (!isFound)
                        {
                            _id.Add(direction.Id);
                            AddToCart(direction);
                            saveButton.Enabled = true;
                        }

                    };
            }
            clearCart.Click += (s, e) => profile_repository.clearCartbd();
        }

        void AddToCart(Direction direction)
        {
            profile_repository.clearCartbd();

            var profiles = profilesByDirectionId[direction.Id];

            DirectionControl directionControl = new DirectionControl(direction, profiles);

            cart.Controls.Add(directionControl);

            List<int> p_id = directionControl.p_id;

            directionControl.Cursor = Cursors.Default;

            directionControl.CloseControl.Click += (s, e) =>
            {
                profile_repository.clearCartbd();
                cart.Controls.Remove(directionControl);
                directionControl = null;
                saveButton.Enabled = true;
            };

            saveButton.Click += (s, e) =>
            {
                profile_repository.Save(directionControl, p_id);
                saveButton.Enabled = false;
            };
        }

        private void InitializeComponent()
        {
            this.directionsDB = new System.Windows.Forms.FlowLayoutPanel();
            this.cart = new System.Windows.Forms.FlowLayoutPanel();
            this.clearCart = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // directionsDB
            // 
            this.directionsDB.Location = new System.Drawing.Point(4, 0);
            this.directionsDB.Name = "directionsDB";
            this.directionsDB.Size = new System.Drawing.Size(423, 505);
            this.directionsDB.TabIndex = 0;
            // 
            // cart
            // 
            this.cart.Location = new System.Drawing.Point(433, 0);
            this.cart.Name = "cart";
            this.cart.Size = new System.Drawing.Size(413, 505);
            this.cart.TabIndex = 1;
            // 
            // clearCart
            // 
            this.clearCart.Location = new System.Drawing.Point(322, 474);
            this.clearCart.Name = "clearCart";
            this.clearCart.Size = new System.Drawing.Size(105, 49);
            this.clearCart.TabIndex = 0;
            this.clearCart.Text = "Очистить";
            this.clearCart.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(433, 474);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(101, 49);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // BD
            // 
            this.ClientSize = new System.Drawing.Size(858, 535);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.clearCart);
            this.Controls.Add(this.cart);
            this.Controls.Add(this.directionsDB);
            this.Name = "BD";
            this.ResumeLayout(false);

        }   
    }
    
}
