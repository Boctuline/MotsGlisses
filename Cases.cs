using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotsGlisses
{
    public class Cases
    {
        List<Case> cases;
        public Cases(List<Case> cases) { this.cases = cases; }
        public bool Contient(Case case1) {
            bool ret = false;
            for (int i = 0; i < cases.Count; i++)
            {
                if (cases[i].I == case1.I && cases[i].J == case1.J) ret = true;
            }
            return ret;
        }
    }
}
