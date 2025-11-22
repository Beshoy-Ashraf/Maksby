using System.Diagnostics;

namespace Factory.Middelwares;

public class SpeedTestMiddelWare
{
      private readonly RequestDelegate Next;
      private readonly ILogger<SpeedTestMiddelWare> Logger;
      public SpeedTestMiddelWare(RequestDelegate next, ILogger<SpeedTestMiddelWare> logger)
      {
            Next = next;
            Logger = logger;
      }


      public async Task Invoke(HttpContext context)
      {
            var StopWatch = new Stopwatch();
            StopWatch.Start();
            await Next(context);
            StopWatch.Stop();
            Logger.LogInformation($"Requist '{context.Request.Path}' took '{StopWatch.ElapsedMilliseconds}'");

      }

}
