namespace Factory.Data.Modules;

public class batch
{
      public int Id { get; set; }

      public virtual Product? Product { get; set; }

      public virtual List<Material>? Materials { get; set; }


      public bool IsRun { get; set; }



}
