using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Profile
    {
        private int id;
        private string name;
        private string faculty;
        private int direction_id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Faculty
        {
            get
            {
                return faculty;
            }
            set
            {
                faculty = value;
            }
        }

        public int Direction_id
        {
            get
            {
                return direction_id;
            }
            set
            {
                direction_id = value;
            }
        }
    }
}
