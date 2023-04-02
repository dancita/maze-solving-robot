namespace MazeSolvingRobot.Models
{
    public class Wall
    {
        public Wall()
        {
            Up = WallStatus.NotBeenSet;
            Left = WallStatus.NotBeenSet;
            Down = WallStatus.NotBeenSet;
            Right = WallStatus.NotBeenSet;
        }
        public WallStatus Up { get; set; }
        public WallStatus Left { get; set; }
        public WallStatus Down { get; set; }
        public WallStatus Right { get; set; }
    }
}
