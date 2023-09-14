using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest
{
    public class Enemy
    {
        public string Color { get; set; }
        public bool IsScared { get; set; }

        public void RunAway() { }
        public void Chase() { }
        public void Draw() { }
    }
}
