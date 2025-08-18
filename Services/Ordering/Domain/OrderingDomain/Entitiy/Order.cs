using OrderingDomain.Common;

namespace OrderingDomain.Entitiy;

public class Order : EntityBase
{
    public string UserName { get; set; }
    public decimal TotalPrice  { get; set; }


    //address
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddres { get; set; }
    public string Country { get; set; }
    public string City { get; set; }


    // payment
    public string BankName { get; set; }
    public string RefCode { get; set; }
    public int PaymentMethod { get; set; }




}
