﻿using Titan.DataProvider.Domain.Shared;

namespace Titan.DataProvider.Domain.Errors
{
    public static class DomainErrors
    {

        public static class ExpandedUnit
        {
            public static readonly Error CombatTypeNotFound = new(
                "ExpandedUnit.CombatTypeNotFound",
                $"Undefined combat type. Unable to proceed.");
        }
        public static class Stat
        {
            public static readonly Error AllStatValuesZero = new(
                "Stat.AllStatValuesZero",
                $"All stat values cannot be zero. Unable to proceed.");
        }
    }
}
