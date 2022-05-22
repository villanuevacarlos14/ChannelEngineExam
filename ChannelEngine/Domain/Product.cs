using Domain.Interface;

namespace Domain;

public class Product
{
    public Product(string merchantProductNo)
    {
        MerchantProductNo = merchantProductNo;
    }

    public string MerchantProductNo { get; private set; }

    public async Task<bool> UpdateProductStock(int stock, IProductRepository _repository)
    {
        return await _repository.UpdateStock(this, stock);
    }
}