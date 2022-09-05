using System.Collections.Concurrent;
using BMW.CloudAdoption.BOM.Core.Helpers;
using BMW.CloudAdoption.BOM.Core.Models;

namespace BMW.CloudAdoption.BOM.Persistence.Cache;

public class PartsCache : IPartsCache
{
    private const string NUllValue = "null";
    private readonly ConcurrentDictionary<string, Lazy<string>> _cache = new();

    public void Upsert(string key, string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && !value.Equals(NUllValue, StringComparison.OrdinalIgnoreCase))
            _cache.AddOrUpdate(key, new Lazy<string>(value), (_, _) => new Lazy<string>(value));
        else
            _cache.TryRemove(key, out _);
    }

    public Part? Get(string key) 
        => _cache.TryGetValue(key, out var partValue) ? partValue.Value.Deserialize<Part>() : default;
    
    public IEnumerable<Part?> GetAll() 
        => _cache.ToList().Select(item => item.Value.Value.Deserialize<Part>());

    public bool Exists(string key)
        => _cache.ContainsKey(key);
}

public interface IPartsCache
{
    void Upsert(string key, string value);
    Part? Get(string key);
    IEnumerable<Part?> GetAll();
    bool Exists(string key);
}