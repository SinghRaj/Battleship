using System;
using Battleship.Interfaces;
using Battleship.Wrappers;

namespace Battleship
{
    public class Battleship : IBattleship
    {
        ICoordinatesReader _coordinatesReader;
        IConsoleWrapper _console;
        public Battleship()
        {
        }

        public void StartBattleship()
        {
            var player1 = "Player 1";
            var player2 = "Player 2";
            _coordinatesReader = new CoordinatesReader();
            _console = new ConsoleWrapper();

            var player1Board = InitialiseBoard(player1, 3);
            var player2Board = InitialiseBoard(player2, 3);

            Run(player1Board, player2Board);
        }

        IBoard InitialiseBoard(string player, int shipLength)
        { 
            Console.WriteLine($"Initialising board for {player}....");
            IBoard board = new Board(player);

            Console.WriteLine($"Initialsed 8x8 grid board for {player}");
            //board.PrintMatrix();

            IShip ship = new Ship(player, shipLength, _coordinatesReader, _console);
            ship.InitilizeShip();

            board.AddShipToMatrix(ship);
            board.UpdateMatrix(ship.CoordinatesList, Enums.CoordinateStatus.Ship);
            Console.WriteLine($"Added ship on board for {player}");
            board.PrintMatrix();

            return board;
        }

        void Run(IBoard player1Board, IBoard player2Board)
        {
            while (true)
            {
                if (FireShot(player1Board, player2Board))
                    break;
                if (FireShot(player2Board, player1Board))
                    break;
            }

            Console.WriteLine("Game over....");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        bool FireShot(IBoard attackerBoard, IBoard otherPlayerBoard)
        {
            bool coordinatedValid = false;
            Coordinates firedCoordinates;
            do
            {
                firedCoordinates = _coordinatesReader.ReadFiredShotCoordinates(attackerBoard.Player, otherPlayerBoard.Player);

                if (attackerBoard.Ship.ValidateDuplicateShotFired(firedCoordinates))
                    coordinatedValid = true;
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Shot fired at this location previously. Please try again");
                    Console.ResetColor();
                }
            } while (!coordinatedValid);

            if (otherPlayerBoard.Ship.ShotFired(firedCoordinates))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{attackerBoard.Player}'s shot hits {otherPlayerBoard.Player}");
                attackerBoard.Ship.AddHitShot(firedCoordinates);
                attackerBoard.UpdateMatrix(firedCoordinates, Enums.CoordinateStatus.Hit);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{attackerBoard.Player}'s shot missed {otherPlayerBoard.Player}");
                attackerBoard.Ship.AddMissedShot(firedCoordinates);
                attackerBoard.UpdateMatrix(firedCoordinates, Enums.CoordinateStatus.Miss);
                Console.ResetColor();
            }

            if (attackerBoard.Ship.HitShotsCount() == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Congratulations {attackerBoard.Player}, you sunk my battleship");
                Console.ResetColor();
                attackerBoard.PrintMatrix();
                otherPlayerBoard.PrintMatrix();
                return true;
            }
            return false;
        }
    }
}
