# maze-solving-robot

## The Maze
The maze is three dimensional and comprises identical cube-shaped rooms.
Each room may or may not have a doorway on any of it’s six sides.
Each room within the maze contains at least one doorway.
The maze is a closed system with no way out.
The maze is of unknown, but finite, size.
There is one special room which contains some treasure.

## The Drone
We have at our disposal an autonomous drone.
The drone will start in a random room in the maze.
The drone has ample memory to keep track of information about the maze.
The drone can detect treasure and will stop when it finds it.

## The Task
The drone must be programmed to find the treasure.
You may use any tools you wish to complete this exercise, although C# is preferred.
Your solution does not have to be optimal, but approaches based on random movement are
not sufficient.
We will chat through your solution at your interview.

You may use the following code as a starting point if you wish:
```
class AutoDrone {
  const int North = 0;
  const int East = 1;
  const int Up = 2;
  const int South = 3;
  const int West = 4;
  const int Down = 5;
  void Move(int direction) { // To be implemented
  ...
  }
  bool IsTreasureRoom() { // To be implemented
  ...
  }
  bool IsDoorway(int direction) { // To be implemented
  ...
  }
  FindTreasure() { // To be implemented
  ...
  }
}
```

