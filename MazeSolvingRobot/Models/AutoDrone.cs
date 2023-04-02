namespace MazeSolvingRobot.Models
{
    public class AutoDrone
    {
        const int North = 0;
        const int East = 1;
        //const int Up = 2;
        const int South = 3;
        const int West = 4;

        const int maxWidth = 4;
        const int maxLength = 4;
        const int minimumMazeSize = 2;
        public static int Width { get; set; } = 0;
        public static int Length { get; set; } = 0;

        public static IDictionary<string, int> TreasureCoordinates { get; set; } = new Dictionary<string, int>();
        //const int Down = 5;
        //void Move(int direction)
        //{

        //}
        //bool IsTreasureRoom()
        //{ // To be implemented

        //}
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
                        array[i, j].Up = true;
                    }
                    if (j == 0)
                    {
                        array[i, j].Left = true;
                    }
                    if (i == Length - 1)
                    {
                        array[i, j].Down = true;
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
                wall.Down = downRoom.Up;
            }
            if (wall.Down == null)
            {
                wall.Down = GenerateBoolean();
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
            if (upperRoom.Down != null)
            {
                wall.Up = upperRoom.Down;
            }
            if (wall.Up == null)
            {
                wall.Up = GenerateBoolean();
            }
        }

        private static bool NoDoorwaysInRoom(Wall wall)
        {
            return wall.Up == true && wall.Down == true && wall.Left == true
                 && wall.Right == true;
        }

        public static bool GenerateBoolean()
        {
            Random rnd = new();
            return Convert.ToBoolean(rnd.Next(0, 2));
        }

        public static void HideTreasure()
        {
            int xCoordinate = GenerateRandomNumber(0, Length);
            int yCoordinate = GenerateRandomNumber(0, Width);
            TreasureCoordinates.Add("Length", xCoordinate);
            TreasureCoordinates.Add("Width", yCoordinate);
        }
    }
}
