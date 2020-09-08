using System.Collections.Generic;
using Battleship.Enums;

namespace Battleship.Interfaces
{
    public interface IBoard
    {
        /// <summary>
        /// Owner of the Board 
        /// </summary>
        string Player { get; }

        /// <summary>
        /// Ship to be placed on the Board
        /// </summary>
        IShip Ship { get; }

        /// <summary>
        /// Adds ship to the matrix
        /// </summary>
        /// <param name="ship">Ship to add to matrix</param>
        void AddShipToMatrix(IShip ship);

        /// <summary>
        /// Update the status of coordinates
        /// </summary>
        /// <param name="shipCoordinates">Coordinates</param>
        /// <param name="status">updated status</param>
        void UpdateMatrix(List<Coordinates> shipCoordinates, CoordinateStatus status);

        /// <summary>
        /// Update the status of coordinates
        /// </summary>
        /// <param name="coordinates">Coordinates</param>
        /// <param name="status">updated status</param>
        void UpdateMatrix(Coordinates coordinates, CoordinateStatus status);

        /// <summary>
        /// Gets the Matrix
        /// </summary>
        Dictionary<Coordinates, CoordinateStatus> GetMatrix();

        /// <summary>
        /// Prints the matrix
        /// </summary>
        void PrintMatrix();
    }
}
