using System.Collections.Generic;

namespace Titan.DataProvider.Domain.Models.GalaxyOfHeroes.PlayerProfile;

public class PlayerProfileResponse
{
    public string? Name { get; set; }
    public long AllyCode { get; set; }
    public string? PlayerId { get; set; }
    public List<Unit> RosterUnits { get; set; } = new();
    public List<PlayerPvpProfile> PvpProfiles { get; set; } = new();
    public List<Datacron> Datacrons { get; set; } = new();
}
