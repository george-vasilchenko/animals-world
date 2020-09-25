using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public interface IInfoLogger : ILogger
   {
      void Log(string message);
   }
}