using System;
using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship.Tests.Mocks
{
    public class MockCoordinatesReader : ICoordinatesReader
    {
        public Func<string, string, Coordinates> MockReadFiredShotCoordinates { get; set; }
        public Func<Orientation, string, Coordinates> MockReadShipCoordinates { get; set; }

        public Coordinates ReadFiredShotCoordinates(string player, string otherPlayer)
        {
            if (MockReadFiredShotCoordinates != null)
            {
                return MockReadFiredShotCoordinates(player, otherPlayer);
            }
            return null;
        }

        public Coordinates ReadShipCoordinates(Orientation orientation, string player)
        {
            if (MockReadShipCoordinates != null)
            {
                return MockReadShipCoordinates(orientation, player);
            }
            return null;
        }
    }
}
