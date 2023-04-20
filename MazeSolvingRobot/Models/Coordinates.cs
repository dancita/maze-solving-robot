namespace MazeSolvingRobot.Models
{
    public class Coordinates
    {
        public Coordinates(int length, int width) {
            Length = length;
            Width = width;
        }
        public int Length { get; set; }
        public int Width { get; set; }
    }
}
