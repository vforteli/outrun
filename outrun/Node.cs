using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outrun
{
    public class Node
    {
        public Int32 Weight
        {
            get;
            set;
        }
        public Int32 Value
        {
            get;
            set;
        }
        public Int32 Level
        {
            get;
            set;
        }
        public Node Parent
        { get; set; }
    }

}
