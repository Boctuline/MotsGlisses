using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotsGlisses
{
    public class Case
    {
        int i;
        int j;
        public int I
        {
            get { return i; }
            set { i = value; }
        }
        public int J
        {
            get { return j; }
            set { j = value; }
        }
        public Case(int i, int j, int l,int c) {
            this.i = i%c;
            this.j = j%c;
        }
        public string toString()
        {
            return i + " et " + j;
        }
    }
}
