using System.Collections;
using MalenkiyApps;
using MalenkiyApps.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimalsWorld
{
   public class PreloadManager : MonoBehaviour, IPreloadManager
   {
      public PersistentContainer PersistentContainer;

      public IPersistentPreferencesService PersistentPreferencesService { get; private set; }
      public IPurchaseManager PurchaseManager { get; private set; }
      public ISystemManager SystemManager => _systemManager;
      public IResourceManager ResourceManager => _resourceManager;

      [SerializeField] private SystemManager _systemManager = default;
      [SerializeField] private ResourceManager _resourceManager = default;
      [SerializeField] private LoadingProgressBehaviour _loadingProgress = default;

      private ILoadingProgressBehaviour LoadingProgress => _loadingProgress;

      private void Awake()
      {
         Validation.Run(() => SystemManager != null, nameof(SystemManager), "Must not be null.");
         Validation.Run(() => ResourceManager != null, nameof(ResourceManager), "Must not be null.");
         Validation.Run(() => LoadingProgress != null, nameof(LoadingProgress), "Must not be null.");

         PersistentPreferencesService = new PersistentPreferencesService();
         PurchaseManager = new PurchaseManager(PersistentPreferencesService);
      }

      private IEnumerator Start()
      {
         ////var persistentPreferencesService = new PersistentPreferencesService();
         ////IPurchaseStatusService purchaseRegistryService = new PurchaseStatusService(persistentPreferencesService);
         ////var productId = "ma_aw_unlock_all_boards_83ab35e310cdaca3741e7f68c01d42c5";

         ////if (purchaseRegistryService.IsOpenForDelivery(productId))
         ////{
         ////   Debug.Log("open");
         ////}
         ////else
         ////{
         ////   Debug.Log("closed");
         ////}

         yield return PreloadServices();
      }

      IEnumerator IPreloadManager.PreloadServices() => PreloadServices();

      private IEnumerator PreloadServices()
      {
         LoadingProgress.Initialize(5);

         // Resources
         Debug.Log("Loading resource manager.");
         yield return ResourceManager.PreloadRoutine();
         LoadingProgress.SetProgress(25);

         // System
         Debug.Log("Loading system manager.");
         yield return SystemManager.PreloadRoutine();
         LoadingProgress.SetProgress(50);

         // Purchasing
         Debug.Log("Loading purchases manager.");
         if (SystemManager.IsConnectedToInternet)
         {
            Debug.Log("IAP is ONLINE.");
            yield return PurchaseManager.PreloadRoutine();
         }
         else
         {
            Debug.Log("IAP is OFFLINE.");
         }
         LoadingProgress.SetProgress(75);

         // Persist data
         PersistentContainer.PersistentPreferencesService = PersistentPreferencesService;
         PersistentContainer.PurchaseManager = PurchaseManager;
         PersistentContainer.SystemManager = SystemManager;
         PersistentContainer.ResourceManager = ResourceManager;

         // Load Main scene
         Debug.Log("Loading Main scene...");
         yield return LoadMainScene();
      }

      private IEnumerator LoadMainScene()
      {
         var loadOperation = SceneManager.LoadSceneAsync("Main");
         loadOperation.allowSceneActivation = false;

         while (!loadOperation.isDone)
         {
            if (loadOperation.progress >= 0.9f && !loadOperation.allowSceneActivation)
            {
               LoadingProgress.SetProgress(100);
               loadOperation.allowSceneActivation = true;
            }

            yield return null;
         }
      }
   }
}