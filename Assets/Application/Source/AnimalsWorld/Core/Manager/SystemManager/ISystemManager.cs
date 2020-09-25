using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public interface ISystemManager : IPreloadable
   {
      bool IsConnectedToInternet { get; }
   }
}