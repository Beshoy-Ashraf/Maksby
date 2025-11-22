namespace Factory.Data.Modules;

public class Seller
{
      public int Id { get; set; }
      public required string SellerName { get; set; }

      public virtual Account? Account { get; set; }

}
