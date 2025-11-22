namespace Factory.Data.Modules;

public class Material
{
      public int id { get; set; }

      public string MaterialName { get; set; }

      public string Details { get; set; }

      public double CurrentQuantity => IncomingMaterial - OutgoingMaterial;

      public double PricePerOneKilo { get; set; }

      public double TotalAmountPerMaterials => CurrentQuantity * PricePerOneKilo;

      public double IncomingMaterial { get; set; }
      public double OutgoingMaterial { get; set; }

}
