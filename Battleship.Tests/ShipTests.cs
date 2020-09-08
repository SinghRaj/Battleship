using Xunit;
using Battleship.Tests.Mocks;
using Shouldly;
using System.Linq;

namespace Battleship.Tests
{
    public class ShipTests
    {
        [Fact]
        public void TestInitilizeVerticalShip()
        {
            var coordinateReader = new MockCoordinatesReader();
            var console = new MockConsoleWrapper();
            var ship = new Ship("player1", 3, coordinateReader, console);

            coordinateReader.MockReadShipCoordinates += (o, p) =>
             {
                 return new Coordinates('A', 3);
             };

            console.MockReadLine += () =>
             {
                 return "1";
             };

            ship.InitilizeShip().ShouldBeTrue();
            ship.CoordinatesList.Count.ShouldBe(3);
            ship.Orientation.ShouldBe(Enums.Orientation.Vertical);
            ship.CoordinatesList.Any(x => x.XCoordinate == 'A' && x.YCoordinate == 3).ShouldBe(true);
            ship.CoordinatesList.Any(x => x.XCoordinate == 'A' && x.YCoordinate == 2).ShouldBe(true);
            ship.CoordinatesList.Any(x => x.XCoordinate == 'A' && x.YCoordinate == 1).ShouldBe(true);
        }

        [Fact]
        public void TestInitilizeHorizontalShip()
        {
            var coordinateReader = new MockCoordinatesReader();
            var console = new MockConsoleWrapper();
            var ship = new Ship("player1", 3, coordinateReader, console);

            coordinateReader.MockReadShipCoordinates += (o, p) =>
            {
                return new Coordinates('H', 8);
            };

            console.MockReadLine += () =>
            {
                return "2";
            };

            ship.InitilizeShip().ShouldBeTrue();
            ship.CoordinatesList.Count.ShouldBe(3);
            ship.Orientation.ShouldBe(Enums.Orientation.Horizontal);
            ship.CoordinatesList.Any(x => x.XCoordinate == 'H' && x.YCoordinate == 8).ShouldBe(true);
            ship.CoordinatesList.Any(x => x.XCoordinate == 'G' && x.YCoordinate == 8).ShouldBe(true);
            ship.CoordinatesList.Any(x => x.XCoordinate == 'F' && x.YCoordinate == 8).ShouldBe(true);
        }


        [Fact]
        public void TestShotFired()
        {
            var coordinateReader = new MockCoordinatesReader();
            var console = new MockConsoleWrapper();
            var ship = new Ship("player1", 3, coordinateReader, console);

            ship.CoordinatesList.Add(new Coordinates('A', 3));
            ship.CoordinatesList.Add(new Coordinates('A', 2));
            ship.CoordinatesList.Add(new Coordinates('A', 1));

            ship.CoordinatesList.Count.ShouldBe(3);
            ship.ShotFired(new Coordinates('A', 3)).ShouldBe(true);
            ship.ShotFired(new Coordinates('A', 4)).ShouldBe(false);
            ship.ShotFired(new Coordinates('B', 3)).ShouldBe(false);
        }

        [Fact]
        public void TestAddHitShot()
        {
            var coordinateReader = new MockCoordinatesReader();
            var console = new MockConsoleWrapper();
            var ship = new Ship("player1", 3, coordinateReader, console);

            ship.CoordinatesList.Add(new Coordinates('A', 3));
            ship.CoordinatesList.Add(new Coordinates('A', 2));
            ship.CoordinatesList.Add(new Coordinates('A', 1));

            ship.CoordinatesList.Count.ShouldBe(3);

            ship.AddHitShot(new Coordinates('A', 3)).ShouldBe(true);
            ship.AddHitShot(new Coordinates('A', 2)).ShouldBe(true);

            ship.HitShotsCount().ShouldBe(2);
        }

        [Fact]
        public void TestAddMissedShot()
        {
            var coordinateReader = new MockCoordinatesReader();
            var console = new MockConsoleWrapper();
            var ship = new Ship("player1", 3, coordinateReader, console);

            ship.CoordinatesList.Add(new Coordinates('A', 3));
            ship.CoordinatesList.Add(new Coordinates('A', 2));
            ship.CoordinatesList.Add(new Coordinates('A', 1));

            ship.CoordinatesList.Count.ShouldBe(3);

            ship.AddMissedShot(new Coordinates('B', 3)).ShouldBe(true);
            ship.AddMissedShot(new Coordinates('B', 2)).ShouldBe(true);

            ship.MissedShotsCount().ShouldBe(2);
        }
    }
}
