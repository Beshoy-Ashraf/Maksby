namespace Factory.Data.Modules;

public class Product
{

      public int id { get; set; }

      public string ProductName { get; set; }

      public string Details { get; set; }

      public double CurrentQuantity => IncomingProduct - OutgoingProduct;

      public double PricePerOneKilo { get; set; }

      public double TotalAmountPerProducts => CurrentQuantity * PricePerOneKilo;

      public double IncomingProduct { get; set; }
      public double OutgoingProduct { get; set; }

}


