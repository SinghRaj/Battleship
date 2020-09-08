using System;
using Battleship.Interfaces;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Battleship");
            IBattleship battleship = new Battleship();
            battleship.StartBattleship();
        }
    }
}
