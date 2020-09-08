using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship
{
    public class Ship: IShip
    {
        readonly ICoordinatesReader _coordinateReader;
        readonly IConsoleWrapper _console;
        public string PlayerName { get; private set; }
        public int ShipLength { get; private set; }
        public Orientation Orientation { get; private set; }
        public List<Coordinates> CoordinatesList { get; private set; }

        public List<Coordinates> HitShots { get; }
        public List<Coordinates> MissedShots { get; }

        public Ship(string playerName, int shipLength, ICoordinatesReader coordinateReader, IConsoleWrapper console)
        {
            if (coordinateReader == null)
                throw new ArgumentNullException(nameof(coordinateReader));

            if (console == null)
                throw new ArgumentNullException(nameof(console));

            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException(nameof(playerName));

            if (shipLength != 3)
                throw new ArgumentException(nameof(shipLength));

            _coordinateReader = coordinateReader;
            _console = console;
            PlayerName = playerName;
            ShipLength = shipLength;
            CoordinatesList = new List<Coordinates>();
            HitShots = new List<Coordinates>();
            MissedShots = new List<Coordinates>();
        }

        public bool InitilizeShip()
        {

            Orientation = GetOrientation();
            var coordinates = _coordinateReader.ReadShipCoordinates(Orientation, PlayerName);
            
            CoordinatesList.Add(coordinates);

            for (int i = 1; i < ShipLength; i++)
            {
                coordinates = GetNextCoordinates(coordinates);
                CoordinatesList.Add(coordinates);
            }
           
            return true;
        }

        public bool ShotFired(Coordinates coordinates)
        {
            if (CoordinatesList.Any(x=> x.XCoordinate == coordinates.XCoordinate && x.YCoordinate == coordinates.YCoordinate))
                return true;

            return false;
        }

        public List<Coordinates> GetShipCoordinates()
        {
            return CoordinatesList;
        }

        public bool ValidateDuplicateShotFired(Coordinates coordinates)
        {
            if (HitShots.Any(x => x.XCoordinate == coordinates.XCoordinate && x.YCoordinate == coordinates.YCoordinate)
                || MissedShots.Any(x => x.XCoordinate == coordinates.XCoordinate && x.YCoordinate == coordinates.YCoordinate))
                return false;
            return true;
        }

        public bool AddHitShot(Coordinates coordinates)
        {
            HitShots.Add(coordinates);
            return true;
        }

        public bool AddMissedShot(Coordinates coordinates)
        {
            MissedShots.Add(coordinates);
            return true;
        }

        public int HitShotsCount()
        {
            return HitShots.Count;
        }

        public int MissedShotsCount()
        {
            return MissedShots.Count;
        }

        Orientation GetOrientation()
        {
            Console.WriteLine($"{PlayerName}: Please enter the orientation for your ship. Press 1 for vertical, 2 for horizontal");
            Orientation orientation;
            bool success;
            bool valid;
            do
            {
                success = Enum.TryParse(_console.ReadLine(), out orientation);

                valid = success && (orientation == Orientation.Vertical || orientation == Orientation.Horizontal);

                if (!valid)
                    Console.WriteLine("Invalid input. Press 1 for vertical, 2 for horizontal");
            } while (!valid);

            return orientation;
        }

        Coordinates GetNextCoordinates(Coordinates coordinates)
        {
            if (Orientation == Orientation.Vertical)
                return new Coordinates(coordinates.XCoordinate, coordinates.YCoordinate - 1);
            else
                return new Coordinates((char)Convert.ToUInt16(coordinates.XCoordinate - 1), coordinates.YCoordinate);
        }
    }
}
