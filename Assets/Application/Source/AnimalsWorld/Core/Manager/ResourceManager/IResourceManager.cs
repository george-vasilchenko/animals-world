using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public interface IResourceManager : IPreloadable
   {
      PreloadedResource<string> LocalizationPreloadedResource { get; }
   }
}