using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luffarschack.Models
{
    public class Game
    {
        public int Id { get; set; }
        public List<int> Players { get; set; }
        public int[] Board { get; set; }
        public int HSize { get; set; }
        public int VSize { get; set; }
        public int Winner { get; set; }
        public int CurrentPlayer { get; set; }
    }
}