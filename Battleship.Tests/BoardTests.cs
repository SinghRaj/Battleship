using Xunit;
using Battleship.Tests.Mocks;
using Shouldly;
using System.Collections.Generic;
using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship.Tests
{
    public class BoardTests
    {
        [Fact]
        public void TestBoard()
        {
            var coordinateReader = new MockCoordinatesReader();
            var console = new MockConsoleWrapper();
            var ship = new MockShip();

            IBoard board = new Board("player 1");
            var matrix = board.GetMatrix();
            foreach (var kvp in matrix)
            {
                kvp.Value.ShouldBe(CoordinateStatus.Empty);
            }

            ship.MockCoordinatesList += () =>
            {
                return new List<Coordinates> { new Coordinates('A', 3), new Coordinates('A', 2), new Coordinates('A', 1) };
            };

            board.AddShipToMatrix(ship);

            board.Ship.CoordinatesList.Count.ShouldBe(3);

            board.UpdateMatrix(board.Ship.CoordinatesList, CoordinateStatus.Ship);
            matrix = board.GetMatrix();

            List<int> yCoordinateList = new List<int> { 1, 2, 3 }; 
            foreach (var kvp in matrix)
            {
                if (kvp.Key.XCoordinate == 'A' && yCoordinateList.Contains(kvp.Key.YCoordinate))
                {
                    kvp.Value.ShouldBe(CoordinateStatus.Ship);
                }
                else
                    kvp.Value.ShouldBe(CoordinateStatus.Empty);
            }

            board.UpdateMatrix(new Coordinates('A', 3), CoordinateStatus.Hit);
            board.UpdateMatrix(new Coordinates('A', 4), CoordinateStatus.Miss);
            matrix = board.GetMatrix();

            foreach (var kvp in matrix)
            {
                if (kvp.Key.XCoordinate == 'A' && kvp.Key.YCoordinate == 3)
                    kvp.Value.ShouldBe(CoordinateStatus.Hit);
                else if (kvp.Key.XCoordinate == 'A' && kvp.Key.YCoordinate == 4)
                    kvp.Value.ShouldBe(CoordinateStatus.Miss);
                else if (kvp.Key.XCoordinate == 'A' && ((kvp.Key.YCoordinate == 1) || kvp.Key.YCoordinate == 2))
                    kvp.Value.ShouldBe(CoordinateStatus.Ship);
                else
                    kvp.Value.ShouldBe(CoordinateStatus.Empty);
            }
        }
    }
}
