using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsimulation
{
    public enum Sex { Male = 1, Female = 2 }
    public class Individual
    {
        public Sex Sex { get; set; }
        public int YearOfBirth { get; set; }
        public byte NumberOfChildren { get; set; }
        public bool IsAlive { get; set; } = true;
    }
}
