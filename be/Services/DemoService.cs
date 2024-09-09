using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services;

public class DemoService : IDemoService
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Model> _model;
    public DemoService(IMongoDatabase database)
    {
        _database = database;
        _model = _database.GetCollection<Model>("YourCollection");
    }

    private class RandomDataGenerator
    {
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenerateRandomText(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public async Task<List<Model>> GetAllAsync()
    {
        var result = new List<Model>();
        using (var cursor = await _model.Find(item => true).ToCursorAsync())
        {
            while (await cursor.MoveNextAsync())
            {
                result.AddRange(cursor.Current);
            }
        }
        return result;
    }
    public async Task InsertRandomProductsAsync(int count)
    {
        var models = new List<Model>();

        for (int i = 0; i < count; i++)
        {
            var model = new Model
            {
                RandomText = RandomDataGenerator.GenerateRandomText(10),
            };
            models.Add(model);

            if (models.Count % 1000 == 0)
            {
                await _model.InsertManyAsync(models);
                models.Clear();
            }
        }

        // Chèn các bản ghi còn lại
        if (models.Count > 0)
        {
            await _model.InsertManyAsync(models);
        }
    }
}
