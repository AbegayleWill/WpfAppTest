﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest
{
    public abstract class Character
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }

        public abstract void Move();
        public abstract void Draw();
    }
}

