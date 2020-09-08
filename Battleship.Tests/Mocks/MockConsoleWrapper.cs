using System;
using Battleship.Interfaces;

namespace Battleship.Tests.Mocks
{
    public class MockConsoleWrapper : IConsoleWrapper
    {
        public Func<string> MockReadLine { get; set; }
        public string ReadLine()
        {
            if (MockReadLine != null)
            {
                return MockReadLine();
            }
            return null;
        }
    }
}
