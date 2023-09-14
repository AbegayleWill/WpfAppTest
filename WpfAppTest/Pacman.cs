using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest
{
    public class Pacman : Character
    {
        public int Lives { get; set; }
        public bool IsSuperPacman { get; set; }


        public  void Move()
        {
            // Pacman move logic
        }

        public void Eat(Pellet pellet)
        {
           pellet.IsEaten = true;

        }

        public void Die()
        {
            // Logic for when Pacman is caught by a ghost
        }
    }
}
