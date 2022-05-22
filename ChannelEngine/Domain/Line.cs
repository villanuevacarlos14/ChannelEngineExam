namespace Domain;

[Serializable]
public class Line
{
    public Line(string merchantProductNo, string gtin, string description, int quantity)
    {
        MerchantProductNo = merchantProductNo;
        Gtin = gtin;
        Description = description;
        Quantity = quantity;
    }

    public void ModifyQuantiy(int quantity)
    {
        this.Quantity = quantity;
    }
    
    public string MerchantProductNo { get; private set; }

    public Product Product {
        get
        {
            return new Product(this.MerchantProductNo);
        }
    }
    public string Gtin { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
}