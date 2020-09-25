using System.Collections;
using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public interface IPreloadManager
   {
      ISystemManager SystemManager { get; }

      IResourceManager ResourceManager { get; }

      IPurchaseManager PurchaseManager { get; }

      IEnumerator PreloadServices();
   }
}