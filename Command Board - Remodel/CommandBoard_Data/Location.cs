using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandBoard_Data
{
    [Serializable]
    public class Location : IEquatable<Location>
    {
        public int column { set; get; }
        public int row { set; get; }

        public Location(int x, int y)
        {
            column = x;
            row = y;

        }

        override public string ToString()
        {
            return "Row " + ((char)((int)'A' + row)).ToString() + " and Column " + column;
        }

        public bool Equals(Location other)
        {
            if (this.row == other.row && this.column == other.column)
                return true;
            else
                return false;
        }
    }
}
