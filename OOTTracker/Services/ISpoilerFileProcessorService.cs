using OOTTracker.Services.Models;

namespace OOTTracker.Services
{
    public interface ISpoilerFileProcessorService
    {
        public Task<SpoilerDataModel?> ProcessFileAsync(IFormFile spoilerFile);
    }
}
