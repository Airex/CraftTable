﻿using System;

namespace CraftTable.Exceptions
{
    public class CraftSuccessException : CraftTableException
    {
        public CraftSuccessException(bool isHighQuality, int chance)
        {
            IsHighQuality = isHighQuality;
            Chance = chance;
        }

        public int Chance { get; }

        public bool IsHighQuality { get; }
    }
}