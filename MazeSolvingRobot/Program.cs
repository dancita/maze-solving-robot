﻿// See https://aka.ms/new-console-template for more information
using MazeSolvingRobot.Models;

Console.WriteLine("Maze solving robot in C#\r");
Console.WriteLine("------------------------\n");

var array = AutoDrone.CreateMaze();

Console.WriteLine("Maze has been created!\r");
Console.WriteLine("------------------------\n");
for (int i = 0; i < AutoDrone.Length; i++)
{
    for (int j = 0; j < AutoDrone.Width; j++)
    {
        Console.WriteLine($"i: {i}");
        Console.WriteLine($"j: {j}");
        Console.WriteLine($"Up:{array[i,j].North}");
        Console.WriteLine($"Down:{array[i, j].South}");
        Console.WriteLine($"Left:{array[i, j].Left}");
        Console.WriteLine($"Right:{array[i, j].Right}");
    }
}
Console.WriteLine("------------------------\n");

Console.WriteLine("Hiding treasure in one of the rooms....\r");
Console.WriteLine("------------------------\n");
AutoDrone.HideTreasure();
Console.WriteLine($"Treasure has been hidden at: {AutoDrone.TreasureCoordinates.Length}, {AutoDrone.TreasureCoordinates.Width}\r");
Console.WriteLine("------------------------\n");
Console.WriteLine("Placing the drone in randomly...\r");
Console.WriteLine("------------------------\n");
AutoDrone.PlaceDrone();
Console.WriteLine($"Drone has been placed at: {AutoDrone.DroneCoordinates["Length"]}, {AutoDrone.DroneCoordinates["Width"]}\r");
Console.WriteLine("------------------------\n");
AutoDrone.LoggingDronesCurrentLocation(AutoDrone.DroneCoordinates["Length"], AutoDrone.DroneCoordinates["Width"]);
Console.WriteLine($"Drone's starting coordinates have been logged\n");
Console.WriteLine("------------------------\n");
AutoDrone.FindTreasure(array);
Console.ReadKey();
