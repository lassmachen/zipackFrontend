using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using zipackFrontend.Data;

namespace zipackFrontend.Data;

public class LanguageAPI
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly LanguageService _languageService;
    private readonly ILogger<LanguageAPI> _logger;
    
    public LanguageAPI(HttpClient httpClient, IMemoryCache memoryCache, LanguageService languageService, ILogger<LanguageAPI> logger)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _languageService = languageService;
        _logger = logger;
        
        _httpClient.BaseAddress = new Uri("https://cms.lassmachen.org/");
    }
    
    public async Task<LanguageRoot> GetLanguageRoot()
    {
        if (_memoryCache.TryGetValue("LanguageRoot", out LanguageRoot cachedLanguageRoot))
        {
            return cachedLanguageRoot;
        }
        
        LanguageRoot languageRoot = await _httpClient.GetFromJsonAsync<LanguageRoot>($"items/ZipackFrontendTranslations");
        
        _memoryCache.Set("LanguageRoot", languageRoot, TimeSpan.FromMinutes(5));

        return languageRoot;
    }
    
    public async Task<List<LanguageAsset>> GetLanguageAssets()
    {
        List<LanguageAsset> languageAssets = new();
        LanguageRoot languageRoot = await GetLanguageRoot();
        languageRoot.Data.ForEach(languageAsset => languageAssets.Add(languageAsset));
        return languageAssets;
    }
    
    
}