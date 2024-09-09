namespace WebApplication1.Services
{
    public interface IDemoService
    {
        Task<List<Model>> GetAllAsync();

        Task InsertRandomProductsAsync(int count);
    }
}
