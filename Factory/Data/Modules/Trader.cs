namespace Factory.Data.Modules;

public class Trader
{
      public int Id { get; set; }
      public required string TraderName { get; set; }

      public virtual Account? Account { get; set; }
}
