using System;
using System.ComponentModel;

namespace Battleship.Enums
{
    public enum CoordinateStatus
    {
        [Description("-")]
        Empty = 0,

        [Description("S")]
        Ship = 1,

        [Description("H")]
        Hit = 2,

        [Description("X")]
        Miss = 3
    }

    public static class CoordinateStatusReader
    {
        static readonly string[] CoordinateStatusLabels = new string[] { "-", "S", "H", "X" };

        public static string Read(CoordinateStatus status)
        {
            return CoordinateStatusLabels[(int)status];
        }
    }

}
