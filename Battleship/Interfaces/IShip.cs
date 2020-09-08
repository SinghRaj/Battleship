using System.Collections.Generic;
using Battleship.Enums;

namespace Battleship.Interfaces
{
    public interface IShip
    {
        List<Coordinates> CoordinatesList { get; }
        /// <summary>
        /// Initializes the ship
        /// </summary>
        /// <returns></returns>
        bool InitilizeShip();

        /// <summary>
        /// Finds if the shot is a hit or a miss
        /// </summary>
        /// <param name="coordinates">Coordinates of the shot fired</param>
        /// <returns>true, if the shot is a hit, else false</returns>
        bool ShotFired(Coordinates coordinates);

        /// <summary>
        /// Validates if same shot was fired earlier
        /// </summary>
        /// <param name="coordinates">Coordinates of the shot</param>
        /// <returns>True, if the shot is valid, else false</returns>
        bool ValidateDuplicateShotFired(Coordinates coordinates);

        /// <summary>
        /// Marks the shot as hit
        /// </summary>
        /// <param name="coordinates">Coordinates of the shot</param>
        /// <returns></returns>
        bool AddHitShot(Coordinates coordinates);

        /// <summary>
        /// Marks the shot as missed
        /// </summary>
        /// <param name="coordinates">Coordinates of the shot</param>
        /// <returns></returns>
        bool AddMissedShot(Coordinates coordinates);

        /// <summary>
        /// Counts the hit shots
        /// </summary>
        /// <returns>Count of the hit shots</returns>
        int HitShotsCount();

        /// <summary>
        /// Counts the missed shots
        /// </summary>
        /// <returnsCount of the missed shots></returns>
        int MissedShotsCount();
    }
}
