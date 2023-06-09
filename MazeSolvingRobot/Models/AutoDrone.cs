﻿namespace MazeSolvingRobot.Models
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

        public static Coordinates TreasureCoordinates { get; set; } = new Coordinates();
        public static IDictionary<string, int> DroneCoordinates { get; set; } = new Dictionary<string, int>();

        public static List<Coordinates> Log { get; set; } = new List<Coordinates>();

        public static int PreviousDirection { get; set; } = 999;

        static void Move(int direction)
        {
            if (direction == 0)
            {
                DroneCoordinates["Length"] = DroneCoordinates["Length"] - 1;
            }
            else if (direction == 1)
            {
                DroneCoordinates["Width"] = DroneCoordinates["Width"] + 1;
            }
            else if (direction == 3)
            {
                DroneCoordinates["Length"] = DroneCoordinates["Length"] + 1;
            }
            else if (direction == 4)
            {
                DroneCoordinates["Width"] = DroneCoordinates["Width"] - 1;
            }
            LoggingDronesCurrentLocation(DroneCoordinates["Length"], DroneCoordinates["Width"]);
        }

        public static void LoggingDronesCurrentLocation(int length, int width)
        {
            Log.Add(new Coordinates(length, width));
        }

        static bool IsTreasureRoom()
        {
            return DroneCoordinates["Length"] == TreasureCoordinates.Length && DroneCoordinates["Width"] == TreasureCoordinates.Width;
        }
        //bool IsDoorway(int direction)
        //{ // To be implemented

        //}
        public static void FindTreasure(Wall[,] array)
        { 
            while(!IsTreasureRoom())
            {
                Console.WriteLine("Treasure is not here! :(\n");
                Console.WriteLine("Finding the possible directions...\n");
                var possibleDirections = FindDoorways(array[DroneCoordinates["Length"], DroneCoordinates["Width"]]);
                int directionToMove;
                if (possibleDirections.Count == 1)
                {
                    Console.WriteLine($"There's only one way to go without turning back...\n");
                    directionToMove = possibleDirections[0];                   
                }
                else if (possibleDirections.Count == 0)
                {
                    Console.WriteLine($"Reached a dead end, let's turn back...\n");
                    directionToMove = PreviousDirection;
                }
                else
                {
                    Console.WriteLine("Choosing a random direction...\n");
                    var randomNumber = GenerateRandomNumber(0, possibleDirections.Count);
                    directionToMove = possibleDirections[randomNumber];
                }
                Console.WriteLine($"Direction has been selected: {directionToMove} North = 0, East = 1, South = 3, West = 4...\n");
                Console.WriteLine($"Let's move!\n");
                Move(directionToMove);
                PreviousDirection = directionToMove;
                Console.WriteLine($"Drone has been moved! New coordinates: {DroneCoordinates["Length"]},{DroneCoordinates["Width"]}\n");
                foreach (var item in Log)
                {
                    Console.WriteLine($"Log: [{item.Length}, {item.Width}]");
                }
                Console.ReadKey();

            }
            Console.WriteLine("Treasure has been found!\n");
        }

        private static List<int> FindDoorways(Wall droneCoordinates)
        {
            List<int> possibleDirections = new();
            if (PreviousDirection != 3 && droneCoordinates.South == false)
            {
                possibleDirections.Add(3);
            }
            if (PreviousDirection != 4 && droneCoordinates.Left == false)
            {
                possibleDirections.Add(4);
            }
            if (PreviousDirection != 0 && droneCoordinates.North == false)
            {
                possibleDirections.Add(0);
            }
            if (PreviousDirection != 1 && droneCoordinates.Right == false)
            {
                possibleDirections.Add(1);
            }
            return possibleDirections;
        }

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
            TreasureCoordinates.Length = GenerateRandomNumber(0, Length);
            TreasureCoordinates.Width = GenerateRandomNumber(0, Width);
        }

        public static void PlaceDrone()
        {
            DroneCoordinates.Add("Length", GenerateRandomNumber(0, Length));
            DroneCoordinates.Add("Width", GenerateRandomNumber(0, Width));
        }
    }
}
