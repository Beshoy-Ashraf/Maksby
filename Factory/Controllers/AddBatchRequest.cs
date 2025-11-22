namespace Factory.Controllers;

public class AddBatchRequest
{
      public List<int> MaterialIds { get; set; } = new();
      public int ProductId { get; set; }
      public bool IsRun { get; set; }
}
