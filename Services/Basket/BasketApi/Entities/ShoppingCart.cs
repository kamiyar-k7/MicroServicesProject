namespace BasketApi.Entities;

public class ShoppingCart
{

   

    public ShoppingCart(string userName)
    {
            UserName = userName;        
    }

    public string? UserName { get; set; }

    public List<ShoppingCartItem> Items { get; set; }

    public decimal? TotalProce 
    {
        get
        {
            decimal? totalprice = 0;

            if(Items != null && Items.Count > 0)
            {
                foreach (ShoppingCartItem item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
            }

           
            return totalprice;
        }
    }

}
