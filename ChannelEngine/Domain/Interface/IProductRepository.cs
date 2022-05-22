namespace Domain.Interface;

public interface IProductRepository
{
    Task<bool> UpdateStock(Product product, int stock);
}