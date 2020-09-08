using System;

namespace Battleship
{
    public class Coordinates
    {
        public Coordinates(char xCoordinate, int yCoordinate)
        {
            if (!char.IsLetter(xCoordinate))
                throw new ArgumentException(nameof(xCoordinate));

            if (yCoordinate < 1 || yCoordinate > 8)
                throw new ArgumentException(nameof(yCoordinate));

            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public char XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }
    }
}
