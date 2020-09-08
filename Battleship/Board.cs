using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship
{
    public class Board : IBoard
    {
        public string Player { get; private set; }
        public IShip Ship { get; private set; }
        Dictionary<Coordinates, CoordinateStatus> Matrix { get; set; }

        public Board( string player)
        {
            if (string.IsNullOrWhiteSpace(player))
                throw new ArgumentException(nameof(player));

            Player = player;

            Matrix = new Dictionary<Coordinates, CoordinateStatus>();
            CreateMatrix();
        }

        void CreateMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var coordinate = new Coordinates((char)('A' + i), 1 + j);
                    Matrix.Add(coordinate, CoordinateStatus.Empty);
                }
            }
        }

        public void AddShipToMatrix(IShip ship)
        {
            Console.WriteLine($"Adding ship to {Player}'s board");
            if (ship == null)
                throw new ArgumentNullException(nameof(ship));
            Ship = ship;
        }

        public void UpdateMatrix(List<Coordinates> shipCoordinates, CoordinateStatus status)
        {
            foreach (var coordinates in shipCoordinates)
            {
                var kvp = Matrix.Where(x => x.Key.XCoordinate == coordinates.XCoordinate && x.Key.YCoordinate == coordinates.YCoordinate).FirstOrDefault();
                Matrix[kvp.Key] = status;
            }
        }

        public void UpdateMatrix(Coordinates coordinates, CoordinateStatus status)
        {
            var kvp = Matrix.Where(x => x.Key.XCoordinate == coordinates.XCoordinate && x.Key.YCoordinate == coordinates.YCoordinate).FirstOrDefault();
            Matrix[kvp.Key] = status;
        }

        public Dictionary<Coordinates, CoordinateStatus> GetMatrix()
        {
            return Matrix;
        }

        public void PrintMatrix()
        {
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{(char)('A' + i)} ");
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                var yCoordinate = 1 + i;
                Console.Write($"{i +1} ");
                for (int j = 0; j < 8; j++)
                {
                    var xCoordinate = (char)('A' + j);
                    var status = Matrix.Where(x => x.Key.XCoordinate == xCoordinate && x.Key.YCoordinate == yCoordinate).FirstOrDefault().Value;
                    Console.Write($"{CoordinateStatusReader.Read(status)} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
