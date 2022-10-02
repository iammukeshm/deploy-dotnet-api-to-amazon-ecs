using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookManager
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _book;
        private readonly MongoDBConfig _settings;
        public BookService(IOptions<MongoDBConfig> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _book = database.GetCollection<Book>(_settings.BookCollectionName);
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await _book.Find(c => true).ToListAsync();
        }
        public async Task<Book> CreateAsync(Book book)
        {
            await _book.InsertOneAsync(book);
            return book;
        }
    }
}
