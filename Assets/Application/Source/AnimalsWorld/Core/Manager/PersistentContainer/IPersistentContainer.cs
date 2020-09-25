using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public interface IPersistentContainer
   {
      IPersistentPreferencesService PersistentPreferencesService { get; }
      IPurchaseManager PurchaseManager { get; }
      IResourceManager ResourceManager { get; }
      ISystemManager SystemManager { get; }
   }
}