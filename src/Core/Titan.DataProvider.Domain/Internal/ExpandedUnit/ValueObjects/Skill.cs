using System.Linq;
using System.Collections.Generic;
using Titan.DataProvider.Domain.Primitives;
using Titan.DataProvider.Domain.Shared;
using PlayerSkill = Titan.DataProvider.Domain.Models.GalaxyOfHeroes.PlayerProfile.Skill;
using Unit = Titan.DataProvider.Domain.Models.GalaxyOfHeroes.PlayerProfile.Unit;
using Titan.DataProvider.Domain.Errors;
using UnitData = Titan.DataProvider.Domain.Internal.BaseData.ValueObjects.UnitData.UnitData;
using Titan.DataProvider.Domain.Models.GalaxyOfHeroes.GameData;

namespace Titan.DataProvider.Domain.Internal.ExpandedUnit.ValueObjects;

public sealed class Skill : ValueObject
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string NameKey { get; private set; }
    public string Image { get; private set; }
    public int Tier { get; private set; }
    public int MaxTier { get; private set; }
    public bool HasActivatedZeta { get; private set; }
    public bool HasActivatedOmicron { get; private set; }
    public OmicronMode OmicronRestriction { get; private set; }
    public string OmicronRestrictionName { get; private set; }

    private Skill(string id, string name, string nameKey, string image, int tier, int maxTier, bool hasActivatedZeta, bool hasActivatedOmicron, OmicronMode omicronMode, string omicronModeName)
    {
        Id = id;
        Name = name;
        NameKey = nameKey;
        Image = image;
        Tier = tier;
        MaxTier = maxTier;
        HasActivatedOmicron = hasActivatedOmicron;
        HasActivatedZeta = hasActivatedZeta;
        OmicronRestriction = omicronMode;
        OmicronRestrictionName = omicronModeName;
    }

    public static Result<Skill> Create(PlayerSkill skill, UnitData data)
    {
        var skillData = data.Skills.FirstOrDefault(x => x.Id == skill.Id);
        if (skillData is null) return Result.Failure<Skill>(DomainErrors.Skill.UnableToFindSkillInGameData);
        bool hasActivatedZeta = false;
        bool hasActivatedOmicron = false;
        var skillTier = skill.Tier + 2;

        foreach (var tag in skillData.PowerOverrideTags)
        {
            if (skillData.IsZeta && tag.Value.Contains("zeta") && skillTier >= int.Parse(tag.Key))
                hasActivatedZeta = true;

            if (skillData.IsOmicron && tag.Value.Contains("omicron") && skillTier >= int.Parse(tag.Key))
                hasActivatedOmicron = true;
        }
        return new Skill(skillData.Id, skillData.Name, skillData.NameKey, skillData.Image, skillTier, (int)skillData.MaxTier, hasActivatedZeta, hasActivatedOmicron, skillData.OmicronMode, skillData.OmicronModeName);
    }
    public static Result<List<Skill>> Create(Unit unit, UnitData data)
    {
        var skillDict = new List<Skill>();
        foreach (var skill in unit.Skill)
        {
            var newSkill = Create(skill, data);
            if (newSkill.IsSuccess)
                skillDict.Add(newSkill.Value);
        }
        return skillDict;
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return Name;
        yield return NameKey;
        yield return Image;
        yield return Tier;
        yield return MaxTier;
        yield return HasActivatedZeta;
        yield return HasActivatedOmicron;
        yield return OmicronRestriction;
        yield return OmicronRestrictionName;
    }
}