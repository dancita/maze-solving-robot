namespace MazeSolvingRobot.Models
{
    public class AutoDrone
    {
        const int North = 0;
        const int East = 1;
        //const int Up = 2;
        const int South = 3;
        const int West = 4;
        //const int Down = 5;

        const int maxWidth = 5;
        const int maxLength = 5;
        const int minimumMazeSize = 2;
        public static int Width { get; set; } = 0;
        public static int Length { get; set; } = 0;

        public static IDictionary<string, int> TreasureCoordinates { get; set; } = new Dictionary<string, int>();
        public static IDictionary<string, int> DroneCoordinates { get; set; } = new Dictionary<string, int>();

        //void Move(int direction)
        //{

        //}
        static bool IsTreasureRoom()
        {
            return DroneCoordinates["Length"] == TreasureCoordinates["Length"] && DroneCoordinates["Width"] == TreasureCoordinates["Width"];
        }
        //bool IsDoorway(int direction)
        //{ // To be implemented

        //}
        //FindTreasure()
        //{ // To be implemented

        //}
        public static Wall[,] CreateMaze()
        {
            var array = CreateMazeFrame();
            BuildExternalWalls(array);
            BuildInternalWalls(array);
            return array;
        }
        public static Wall[,] CreateMazeFrame()
        {
            int length = GenerateRandomNumber(minimumMazeSize, maxLength);
            int width = GenerateRandomNumber(minimumMazeSize, maxWidth);
            Wall[,] array = new Wall[length, width];
            Width = width;
            Length = length;
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    array[i, j] = new Wall();
                }
            }
            return array;
        }
        public static int GenerateRandomNumber(int minimumValue, int maximumValue)
        {
            Random rnd = new();
            return rnd.Next(minimumValue, maximumValue);
        }
        public static void BuildExternalWalls(Wall[,] array)
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == 0)
                    {
                        array[i, j].North = true;
                    }
                    if (j == 0)
                    {
                        array[i, j].Left = true;
                    }
                    if (i == Length - 1)
                    {
                        array[i, j].South = true;
                    }
                    if (j == Width - 1)
                    {
                        array[i, j].Right = true;
                    }
                }
            }
        }
        public static void BuildInternalWalls(Wall[,] array)
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    do
                    {
                        var upperRoom = i == 0 ? null : array[i - 1, j];
                        if (upperRoom != null)
                        {
                            GenerateUpDoorway(array[i, j], upperRoom);
                        }


                        var leftRoom = j == 0 ? null : array[i, j - 1];
                        if (leftRoom != null)
                        {
                            GenerateLeftDoorway(array[i, j], leftRoom);
                        }


                        var rightRoom = j == Width - 1 ? null : array[i, j + 1];
                        if (rightRoom != null)
                        {
                            GenerateRightDoorway(array[i, j], rightRoom);
                        }

                        var downRoom = i == Length - 1 ? null : array[i + 1, j];
                        if (downRoom != null)
                        {
                            GenerateDownDoorway(array[i, j], downRoom);
                        }
                    }
                    while (NoDoorwaysInRoom(array[i, j]));
                }
            }
        }

        private static void GenerateDownDoorway(Wall wall, Wall downRoom)
        {
            if (downRoom.Right != null)
            {
                wall.South = downRoom.North;
            }
            if (wall.South == null)
            {
                wall.South = GenerateBoolean();
            }
        }

        private static void GenerateRightDoorway(Wall wall, Wall rightRoom)
        {
            if (rightRoom.Right != null)
            {
                wall.Right = rightRoom.Left;
            }
            if (wall.Right == null)
            {
                wall.Right = GenerateBoolean();
            }
        }

        private static void GenerateLeftDoorway(Wall wall, Wall leftRoom)
        {
            if (leftRoom.Right != null)
            {
                wall.Left = leftRoom.Right;
            }
            if (wall.Left == null)
            {
                wall.Left = GenerateBoolean();
            }
        }

        private static void GenerateUpDoorway(Wall wall, Wall upperRoom)
        {
            if (upperRoom.South != null)
            {
                wall.North = upperRoom.South;
            }
            if (wall.North == null)
            {
                wall.North = GenerateBoolean();
            }
        }

        private static bool NoDoorwaysInRoom(Wall wall)
        {
            return wall.North == true && wall.South == true && wall.Left == true
                 && wall.Right == true;
        }

        public static bool GenerateBoolean()
        {
            Random rnd = new();
            return Convert.ToBoolean(rnd.Next(0, 2));
        }

        public static void HideTreasure()
        {
            TreasureCoordinates.Add("Length", GenerateRandomNumber(0, Length));
            TreasureCoordinates.Add("Width", GenerateRandomNumber(0, Width));
        }

        public static void PlaceDrone()
        {
            DroneCoordinates.Add("Length", GenerateRandomNumber(0, Length));
            DroneCoordinates.Add("Width", GenerateRandomNumber(0, Width));
        }
    }
}
