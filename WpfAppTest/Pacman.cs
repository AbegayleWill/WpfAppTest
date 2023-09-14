using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfAppTest
{
    public class Pacman : Character
    {
        public int Lives { get; set; }
        public bool IsSuperPacman { get; set; }


        public override void Move()
        {
            // Pacman move logic
        }

        public void Eat(Pellet pellet)
        {
            //If pellet hasnt been eaten
            if(!pellet.IsEaten)
            {
                pellet.IsEaten = true;//Mark pellet as eaten
                CurrentGame.Score += 10;//Add points to score
            }
        }

        public void Die()
        {
            // Logic for when Pacman is caught by a ghost
        }
    }
}
