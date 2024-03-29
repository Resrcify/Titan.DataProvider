using System.Collections.Generic;
using Titan.DataProvider.Domain.Models.GalaxyOfHeroes.Common;

namespace Titan.DataProvider.Domain.Models.GalaxyOfHeroes.GameData;

public class UnitTierDef
{
    public UnitTier Tier { get; set; }
    public StatDef? BaseStat { get; set; }
    public List<string> EquipmentSets { get; set; } = new();
}
