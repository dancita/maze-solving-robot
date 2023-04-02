namespace MazeSolvingRobot.Models
{
    public class AutoDrone
    {
        const int North = 0;
        const int East = 1;
        //const int Up = 2;
        const int South = 3;
        const int West = 4;

        const int maxWidth = 3;
        const int maxLength = 10;
        public static int Width { get; set; } = 0;
        public static int Length { get; set; } = 0;
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
            int length = GenerateRandomNumber(maxLength);
            int width = GenerateRandomNumber(maxWidth);
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
        public static int GenerateRandomNumber(int maximumValue)
        {
            Random rnd = new();
            return rnd.Next(2, maximumValue);
        }
        public static void BuildExternalWalls(Wall[,] array)
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == 0)
                    {
                        array[i, j].Up = WallStatus.True;
                    }
                    if (j == 0)
                    {
                        array[i, j].Left = WallStatus.True;
                    }
                    if (i == Length - 1)
                    {
                        array[i, j].Down = WallStatus.True;
                    }
                    if (j == Width - 1)
                    {
                        array[i, j].Right = WallStatus.True;
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
            if (downRoom.Right != WallStatus.NotBeenSet)
            {
                wall.Down = downRoom.Up;
            }
            if (wall.Down == WallStatus.NotBeenSet)
            {
                wall.Down = GenerateBoolean();
            }
        }

        private static void GenerateRightDoorway(Wall wall, Wall rightRoom)
        {
            if (rightRoom.Right != WallStatus.NotBeenSet)
            {
                wall.Right = rightRoom.Left;
            }
            if (wall.Right == WallStatus.NotBeenSet)
            {
                wall.Right = GenerateBoolean();
            }
        }

        private static void GenerateLeftDoorway(Wall wall, Wall leftRoom)
        {
            if (leftRoom.Right != WallStatus.NotBeenSet)
            {
                wall.Left = leftRoom.Right;
            }
            if (wall.Left == WallStatus.NotBeenSet)
            {
                wall.Left = GenerateBoolean();
            }
        }

        private static void GenerateUpDoorway(Wall wall, Wall upperRoom)
        {
            if (upperRoom.Down != WallStatus.NotBeenSet)
            {
                wall.Up = upperRoom.Down;
            }
            if (wall.Up == WallStatus.NotBeenSet)
            {
                wall.Up = GenerateBoolean();
            }
        }

        private static bool NoDoorwaysInRoom(Wall wall)
        {
            return wall.Up == WallStatus.True && wall.Down == WallStatus.True && wall.Left == WallStatus.True
                 && wall.Right == WallStatus.True;
        }

        public static void GenerateRandomWalls(Wall wall)
        {
            if (wall.Down == WallStatus.NotBeenSet)
            {
                wall.Down = GenerateBoolean();
            }
            if (wall.Left == WallStatus.NotBeenSet)
            {
                wall.Left = GenerateBoolean();
            }
            if (wall.Right == WallStatus.NotBeenSet)
            {
                wall.Right = GenerateBoolean();
            }

        }

        public static WallStatus GenerateBoolean()
        {
            Random rnd = new();
            return (WallStatus)rnd.Next(0, 2);
        }
    }
}
