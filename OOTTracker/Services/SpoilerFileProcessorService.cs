using OOTTracker.Services.Models;
using System.Text.Json;

namespace OOTTracker.Services
{
    public class SpoilerFileProcessorService : ISpoilerFileProcessorService
    {
        public async Task<SpoilerDataModel?> ProcessFileAsync(IFormFile spoilerFile)
        {
            using (var streamReader = new StreamReader(spoilerFile.OpenReadStream()))
            {
                var _jsonContent = await streamReader.ReadToEndAsync();

                var _spoilerData = JsonSerializer.Deserialize<SpoilerDataModel>(_jsonContent, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (_spoilerData == null)
                    return null;

                if (_spoilerData.Locations == null)
                    _spoilerData.Locations = new Dictionary<string, ILocationItem>();

                using (var jsonDocument = JsonDocument.Parse(_jsonContent))
                {
                    var _rootElement = jsonDocument.RootElement;
                    if (_rootElement.TryGetProperty("locations", out JsonElement locationsElement))
                    {
                        foreach (var location in locationsElement.EnumerateObject())
                        {
                            var _key = location.Name;
                            var _value = location.Value;

                            if (_value.ValueKind == JsonValueKind.String)
                                _spoilerData.Locations[_key] = new SimpleLocationItemModel() { Item = _value.GetString() };

                            else if (_value.ValueKind == JsonValueKind.Object && _value.TryGetProperty("price", out _))
                            {
                                var _shopItem = _value.Deserialize<ShopItemModel>();

                                if (_shopItem != null)
                                    _spoilerData.Locations[_key] = _shopItem;
                            }
                            else if (_value.ValueKind == JsonValueKind.Object && _value.TryGetProperty("trickName", out _))
                            {
                                var _trickItem = _value.Deserialize<TrickItemModel>();

                                if (_trickItem != null)
                                    _spoilerData.Locations[_key] = _trickItem;
                            }

                        }

                    }
                }

                return _spoilerData;
            }
        }
    }
}
