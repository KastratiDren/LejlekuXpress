using LejlekuXpress.Models;
using MongoDB.Driver;

namespace LejlekuXpress.Services
{
    public class LogService
    {
        private readonly IMongoCollection<Log> _logCollection;

        public LogService(IMongoDatabase database)
        {
            _logCollection = database.GetCollection<Log>("Logs");
        }

        public async Task CreateLog(Log log)
        {
            await _logCollection.InsertOneAsync(log);
        }
    }

}
