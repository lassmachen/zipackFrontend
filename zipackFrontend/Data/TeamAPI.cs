using Microsoft.Extensions.Caching.Memory;

namespace zipackFrontend.Data;

public class TeamAPI
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<TeamAPI> _logger;
    
    public TeamAPI(HttpClient httpClient, IMemoryCache memoryCache, ILogger<TeamAPI> logger)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _logger = logger;
        
        _httpClient.BaseAddress = new Uri("https://cms.lassmachen.org/");
    }
    
    public async Task<TeamRoot> GetTeamRoot()
    {
        if (_memoryCache.TryGetValue("TeamRoot", out TeamRoot cachedTeamRoot))
        {
            return cachedTeamRoot;
        }
        
        TeamRoot teamRoot = await _httpClient.GetFromJsonAsync<TeamRoot>($"items/ZipackFrontendTeam");
        
        _memoryCache.Set("TeamRoot", teamRoot, TimeSpan.FromMinutes(5));

        return teamRoot;
    }
    
    public async Task<List<TeamMember>> GetTeamMembers()
    {
        List<TeamMember> teamMembers = new();
        TeamRoot teamRoot = await GetTeamRoot();
        teamRoot.Data.ForEach(teamMember => teamMembers.Add(teamMember));
        
        _logger.LogInformation("Got {0} team members", teamMembers.Count);
        return teamMembers;
    }

    public string GetLinkFromUUID(string uuid)
    {
        return _httpClient.BaseAddress + "assets/" + uuid;
    } 
}