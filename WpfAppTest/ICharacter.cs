using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest
{
    //An interface is a checklist of things that a class must implement
    public interface ICharacter
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }

        //Direction CurrentDirection { get; set; }
        public void Move();
    }
}

