using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LaCantine.Model
{
    public class Menu
    {
        public int id { get; set; }

        public string name { get; set; }
        public List<Plats> plats { get; set; }

        public int prix { get; set; }

/*        public string Date { get; set; }
*/
    }
}
