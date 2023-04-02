using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolvingRobot
{
    public class Wall
    {
        public Wall() {
            Up = WallStatus.NotBeenSet;
            Left = WallStatus.NotBeenSet;
            Down = WallStatus.NotBeenSet;
            Right = WallStatus.NotBeenSet;
        }
        public WallStatus Up { get; set; } = WallStatus.NotBeenSet;
        public WallStatus Left { get; set; } = WallStatus.NotBeenSet;
        public WallStatus Down { get; set; } = WallStatus.NotBeenSet; 
        public WallStatus Right { get; set; } = WallStatus.NotBeenSet;
    }
}
