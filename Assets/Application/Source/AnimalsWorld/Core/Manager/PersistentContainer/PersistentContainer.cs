using MalenkiyApps.Interfaces;
using UnityEngine;

namespace AnimalsWorld
{
   [CreateAssetMenu(menuName = "AnimalsWorld/PersistentContainer", order = 0)]
   public class PersistentContainer : ScriptableObject, IPersistentContainer
   {
      public IPersistentPreferencesService PersistentPreferencesService { get; set; }

      public ISystemManager SystemManager { get; set; }

      public IResourceManager ResourceManager { get; set; }

      public IPurchaseManager PurchaseManager { get; set; }
   }
}