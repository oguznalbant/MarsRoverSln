using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Rover
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var coordinatesStr = Console.ReadLine();
            var plateuLeftCoordinatesArr = new int[0, 0];
            int[] plateuRightCoordinatesArr = null;

            var splittedCoordinatesArr = coordinatesStr.Split(' ');
            if (splittedCoordinatesArr.Length > 0)
            {
                try
                {
                    plateuRightCoordinatesArr = splittedCoordinatesArr.Select(x => int.Parse(x)).ToArray();
                }
                catch (Exception)
                {
                }
            }

            if (!(plateuRightCoordinatesArr.Length > 0)) // if plateu's coordinates not defined
            {
                //error

            }

            List<Rover> rovers = new List<Rover>();
            for (int i = 0; i < 2; i++)
            {
                var rover = new Rover();
                var roverInitInfos = Console.ReadLine();
                var roverInitInfosArr = roverInitInfos.Split(' ');
                if (roverInitInfosArr.Length != 3)
                {
                    //error 
                }

                try
                {
                    rover.XCoordinate = int.Parse(roverInitInfosArr[0]);
                    rover.YCoordinate = int.Parse(roverInitInfosArr[1]);
                    rover.RoverPosition = Enum.Parse<RoverPositions>(roverInitInfosArr[2]);
                }
                catch (Exception)
                {
                }

                switch (rover.RoverPosition)
                {
                    //positive
                    case RoverPositions.N:
                    case RoverPositions.E:

                        break;

                    //negative
                    case RoverPositions.S:
                    case RoverPositions.W:

                        break;
                }

                var roverMovesInfos = Console.ReadLine();
                foreach (var item in roverMovesInfos)
                {
                    RoverDirections direction;
                    if (Enum.TryParse(item.ToString(), out direction)) // is it direction 
                    {
                        rover.RoverPosition = ChangeDirection(rover.RoverPosition, direction);
                    }
                    else if (item == 'M') // it is move
                    {
                        rover = Move(rover);
                    }
                }
                rovers.Add(rover);
            }

            Console.WriteLine('\n');

            foreach (var item in rovers)
            {
                Console.WriteLine($"{item.XCoordinate} {item.YCoordinate} {item.RoverPosition.ToString()}");
            }
        }

        public static Rover Move(Rover rover)
        {
            switch (rover.RoverPosition)
            {
                //positive
                case RoverPositions.N:
                    rover.YCoordinate++;
                    break;
                case RoverPositions.E:
                    rover.XCoordinate++;
                    break;
                //negative
                case RoverPositions.S:
                    rover.YCoordinate--;
                    break;
                case RoverPositions.W:
                    rover.XCoordinate--;
                    break;
            }

            return rover;
        }

        public static RoverPositions ChangeDirection(RoverPositions currentPos, RoverDirections turnDirection)
        {
            switch (turnDirection)
            {
                case RoverDirections.L:
                    if (currentPos == RoverPositions.N)
                    {
                        return RoverPositions.W;
                    }
                    else
                    {
                        return (RoverPositions)((int)currentPos - 1);
                    }
                case RoverDirections.R:
                    if (currentPos == RoverPositions.W)
                    {
                        return RoverPositions.N;
                    }
                    else
                    {
                        return (RoverPositions)((int)currentPos + 1);
                    }
                default:
                    return currentPos;
            }
        }
    }

    public enum RoverPositions
    {
        N = 1,
        E = 2,
        S = 3,
        W = 4
    }

    public enum RoverDirections
    {
        R,
        L,
    }

    public class Rover
    {
        public RoverPositions RoverPosition { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }
}
