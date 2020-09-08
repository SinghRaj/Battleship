using System;
using System.Text.RegularExpressions;
using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship
{
    public class CoordinatesReader : ICoordinatesReader
    {
        public Coordinates ReadFiredShotCoordinates(string player, string otherPlayer)
        {
            Console.WriteLine($"{player} Please provide the location, between A1 and H8, to hit {otherPlayer} ship");

            Coordinates coordinates;
            var isValidInput = false;

            do
            {
                var input = Console.ReadLine().ToUpper();
                if (input.Length == 2)
                {
                    var xCoordinate = ReadFiredShotXCoordinate(input.Substring(0, 1));
                    if (xCoordinate == '0')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid input. {player} Please provide x coordinate, between A and H");
                        Console.ResetColor();
                        continue;
                    }
                    var yCoordinate = ReadFiredShotYCoordinate(input.Substring(1, 1));

                    if (yCoordinate == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid input. {player} Please provide y coordinate, between 1 and 8");
                        Console.ResetColor();
                        continue;
                    }
                    coordinates = new Coordinates(xCoordinate, yCoordinate);
                    return coordinates;
                }
                Console.WriteLine($"Invalid input. {player} Please provide a location, between A1 and H8, to hit {otherPlayer} ship");
            } while (!isValidInput);

            throw new Exception($"Invalid location provided by {player}");
        }

        public Coordinates ReadShipCoordinates(Orientation orientation, string player)
        {
            Console.WriteLine($"{player} Please provide the location of your ship in the format CharacterInteger eg A1, H8, B5 etc");

            string lowerXValue = orientation == Orientation.Horizontal ? "C" : "A";
            int lowerYValue = orientation == Orientation.Vertical ? 3 : 1;

            Coordinates coordinates;
            var isValidInput = false;
            do
            {
                var input = Console.ReadLine().ToUpper();
                if (input.Length == 2)
                {
                    var xCoordinate = GetXCoordinate(input.Substring(0, 1), orientation, lowerXValue);
                    if (xCoordinate == '^')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Since the ship is {orientation}, with provided location, the ship will be positioned out of the Grid");
                        Console.WriteLine($"{player} Please provide x coordinate, between C and H");
                        Console.ResetColor();
                        continue;
                    }
                    if (xCoordinate == '0')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid input. {player} Please provide x coordinate, between {lowerXValue} and H");
                        Console.ResetColor();
                        continue;
                    }

                    var yCoordinate = GetYCoordinate(input.Substring(1, 1), orientation, lowerYValue);
                    if (yCoordinate == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Since the ship is {orientation}, with provided location, the ship will be positioned out of the Grid");
                        Console.WriteLine($"{player} Please provide y coordinate, between 3 and 8");
                        Console.ResetColor();
                        continue;
                    }
                    if (yCoordinate == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid input. {player} Please provide y coordinate, between {lowerYValue} and 8");
                        Console.ResetColor();
                        continue;
                    }
                    coordinates = new Coordinates(xCoordinate, yCoordinate);
                    return coordinates;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid input. {player} Please provide a location, between A1 and H8");
                Console.ResetColor();
            } while (!isValidInput);

            throw new Exception($"Invalid location provided by {player}");
        }

        char GetXCoordinate(string xCoordinateStr, Orientation orientation, string lowerXValue)
        {
            bool validXCoordinate = char.TryParse(xCoordinateStr, out char xCoordinate);
            if (validXCoordinate && orientation == Orientation.Horizontal && (xCoordinate == 'A' || xCoordinate == 'B'))
                return '^';

            var regex = $"[{lowerXValue}-H]";

            if (validXCoordinate && Regex.IsMatch(xCoordinate.ToString(), regex))
                return xCoordinate;

            return '0';
        }

        int GetYCoordinate(string yCoordinateStr, Orientation orientation, int lowerYValue)
        {
            bool validYCoordinate = int.TryParse(yCoordinateStr, out int yCoordinate);
            if (validYCoordinate && orientation == Orientation.Vertical && (yCoordinate == 1 || yCoordinate == 2))
                return -1;

            if (validYCoordinate && (yCoordinate >= lowerYValue && yCoordinate <= 8))
                return yCoordinate;

            return 0;
        }

        char ReadFiredShotXCoordinate(string xCoordinateStr)
        {
            bool validXCoordinate;

            var regex = "[A-H]";
            validXCoordinate = char.TryParse(xCoordinateStr, out char xCoordinate);
            if (validXCoordinate && Regex.IsMatch(xCoordinate.ToString(), regex))
                return xCoordinate;

            return '0';
        }

        int ReadFiredShotYCoordinate(string yCoordinateStr)
        {
            bool validYCoordinate;

            validYCoordinate = int.TryParse(yCoordinateStr, out int yCoordinate);
            if (validYCoordinate && yCoordinate >= 1 && yCoordinate <= 8)
                return yCoordinate;

            return 0;
        }
    }
}
