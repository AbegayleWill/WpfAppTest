using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest
{
    public class Pellet
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsEaten { get; set; }
        public bool IsSuperPellet { get; set; }

        public void Eat()
        {
            // Pellet eat logic
        }

        public void Draw()
        {
            // Draw Pellet
        }

        public void Die()
        {
            // Die
        }
    }
}
