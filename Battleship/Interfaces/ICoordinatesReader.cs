using Battleship.Enums;

namespace Battleship.Interfaces
{
    public interface ICoordinatesReader
    {
        /// <summary>
        /// Method to read coordinates for the shot fired
        /// </summary>
        /// <param name="player">Player who fired the shot</param>
        /// <param name="otherPlayer">Other player in the game</param>
        /// <returns>coordinate of the shot fired</returns>
        Coordinates ReadFiredShotCoordinates(string player, string otherPlayer);

        /// <summary>
        /// Method to read coordinates to place ship
        /// </summary>
        /// <param name="orientation">Orientation of the ship</param>
        /// <param name="player">Name of the player</param>
        /// <returns>coordinate of the ship</returns>
        Coordinates ReadShipCoordinates(Orientation orientation, string player);
    }
}