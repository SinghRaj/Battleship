using System;
using Battleship.Interfaces;

namespace Battleship.Wrappers
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleWrapper()
        {

        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
