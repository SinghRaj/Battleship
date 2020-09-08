using System;
using System.Collections.Generic;
using Battleship.Interfaces;

namespace Battleship.Tests.Mocks
{
    public class MockShip: IShip
    {
        public Func<Coordinates, bool> MockAddHitShot { get; set; }
        public Func<Coordinates, bool> MockAddMissedShot { get; set; }
        public Func<int> MockHitShotsCount { get; set; }
        public Func<bool> MockInitilizeShip { get; set; }
        public Func<int> MockMissedShotsCount { get; set; }
        public Func<Coordinates, bool> MockShotFired { get; set; }
        public Func<Coordinates, bool> MockValidateDuplicateShotFired { get; set; }
        public Func<List<Coordinates>> MockCoordinatesList { get; set; }

        public List<Coordinates> CoordinatesList => MockCoordinatesList != null ? MockCoordinatesList() : null;

        public bool AddHitShot(Coordinates coordinates)
        {
            if (MockAddHitShot != null)
                return MockAddHitShot(coordinates);

            return false;
        }

        public bool AddMissedShot(Coordinates coordinates)
        {
            if (MockAddMissedShot != null)
                return MockAddMissedShot(coordinates);

            return false;
        }

        public int HitShotsCount()
        {
            if (MockHitShotsCount != null)
                return MockHitShotsCount();
            return -1;
        }

        public bool InitilizeShip()
        {
            if (MockInitilizeShip != null)
                return MockInitilizeShip();

            return false;
        }

        public int MissedShotsCount()
        {
            if (MockMissedShotsCount != null)
                return MockMissedShotsCount();
            return -1;
        }

        public bool ShotFired(Coordinates coordinates)
        {
            if (MockShotFired != null)
                return MockShotFired(coordinates);

            return false;
        }

        public bool ValidateDuplicateShotFired(Coordinates coordinates)
        {
            if (MockValidateDuplicateShotFired != null)
                return MockValidateDuplicateShotFired(coordinates);

            return false;
        }
    }
}
