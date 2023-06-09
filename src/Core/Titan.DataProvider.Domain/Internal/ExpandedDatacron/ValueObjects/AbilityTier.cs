using System.Collections.Generic;
using Titan.DataProvider.Domain.Internal.BaseData.ValueObjects.DatacronData;
using Titan.DataProvider.Domain.Models.GalaxyOfHeroes.Common;
using Titan.DataProvider.Domain.Models.GalaxyOfHeroes.PlayerProfile;
using Titan.DataProvider.Domain.Primitives;
using Titan.DataProvider.Domain.Shared;

namespace Titan.DataProvider.Domain.Internal.ExpandedDatacron.ValueObjects;
public sealed class AbilityTier : ValueObject
{
    public string AbilityId { get; private set; }
    public string TargetRule { get; private set; }
    public int Tier { get; private set; }
    public UnitTier RequiredUnitTier { get; private set; }
    public RelicTier RequiredRelicTier { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }

    private AbilityTier(string abilityId, string targetRule, int tier, UnitTier requiredUnitTier, RelicTier requiredRelicTier, string name, string description, string image)
    {
        AbilityId = abilityId;
        TargetRule = targetRule;
        Tier = tier;
        RequiredUnitTier = requiredUnitTier;
        RequiredRelicTier = requiredRelicTier;
        Name = name;
        Image = image;
        Description = description;
    }

    public static Result<AbilityTier> Create(int tier, DatacronAffix playerAffix, Target gameDataAbility)
    {
        return new AbilityTier(playerAffix.AbilityId!, playerAffix.TargetRule!, tier, playerAffix.RequiredUnitTier, playerAffix.RequiredRelicTier, gameDataAbility.NameKey, gameDataAbility.DescKey, gameDataAbility.IconKey);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return AbilityId;
        yield return TargetRule;
        yield return Tier;
        yield return RequiredUnitTier;
        yield return RequiredRelicTier;
        yield return Name;
        yield return Description;
        yield return Image;
    }
}