namespace Factory.Middelwares;

public class RateLimitedMiddelWare
{
      private readonly RequestDelegate Next;
      private static DateTime _lastRequiestDate = DateTime.Now;

      private static int counter = 0;
      public RateLimitedMiddelWare(RequestDelegate next)
      {
            Next = next;
      }

      public async Task Invoke(HttpContext context)
      {
            counter++;
            if (DateTime.Now.Subtract(_lastRequiestDate).Seconds > 10)
            {
                  counter = 1;
                  await Next(context);
            }
            else
            {
                  if (counter > 5)
                  {
                        _lastRequiestDate = DateTime.Now;
                        await context.Response.WriteAsync("Rate Limit Exceeded");
                  }
                  else
                  {
                        _lastRequiestDate = DateTime.Now;
                        await Next(context);
                  }

            }
      }


}
