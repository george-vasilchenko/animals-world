using MalenkiyApps;
using MalenkiyApps.Interfaces;
using UnityEngine;

namespace AnimalsWorld
{
   public class GameManager : MonoBehaviour, IGameManager
   {
      public static IGameManager Instance => _instance;

      private static GameManager _instance;

      private IPanelManager _panelManager;
      private ISoundManager _soundManager;
      private IUiManager _uiManager;
      private ILevelManager _levelManager;
      private IResourceManager _resourceManager;
      private ILocalizationManager _localizationManager;
      private IAnalyticsManager _analyticsManager;
      private IPurchaseManager _purchaseManager;
      private IPersistentPreferencesService _persistentPreferencesService;
      private ISystemManager _systemManager;
      private IGameSettingsManager _gameSettingsManager;
      private IAnimationManager _animationManager;
      private ILoggingManager _loggingManager;

      [SerializeField] private InfoLogger _logger = default;
      [SerializeField] private ConfirmationDialogService _confirmationDialogService = default;
      [SerializeField] private PersistentContainer _persistentContainer = default;

      private void Prepare() => _panelManager.HideAllPanels();

      private void Bootstrap()
      {
         _purchaseManager = _persistentContainer.PurchaseManager;
         _resourceManager = _persistentContainer.ResourceManager;
         _systemManager = _persistentContainer.SystemManager;

         _persistentPreferencesService = new PersistentPreferencesService();
         _soundManager = GetComponent<SoundManager>();
         _panelManager = GetComponent<PanelManager>();
         _uiManager = GetComponent<UiManager>();
         _levelManager = GetComponent<LevelManager>();
         _analyticsManager = new UnityAnalyticsManager();
         _localizationManager = GetComponent<LocalizationManager>();
         _gameSettingsManager = GetComponent<GameSettingsManager>();
         _animationManager = GetComponent<AnimationManager>();
         _loggingManager = GetComponent<LoggingManager>();
      }

      #region Unity API

      private void Awake()
      {
         _instance = this;

         Validation.Run(() => _persistentContainer != null, "_persistentContainer", "Must not be null.");

         Bootstrap();

         Validation.Run(() => _persistentPreferencesService != null, "PersistentPreferencesService", "Must not be null.");
         Validation.Run(() => _soundManager != null, "SoundManager", "Must not be null.");
         Validation.Run(() => _panelManager != null, "PanelManager", "Must not be null.");
         Validation.Run(() => _uiManager != null, "UiManager", "Must not be null.");
         Validation.Run(() => _levelManager != null, "LevelManager", "Must not be null.");
         Validation.Run(() => _purchaseManager != null, "PurchaseManager", "Must not be null.");
         Validation.Run(() => _resourceManager != null, "ResourceManager", "Must not be null.");
         Validation.Run(() => _systemManager != null, "SystemManager", "Must not be null.");
         Validation.Run(() => _analyticsManager != null, "AnalyticsManager", "Must not be null.");
         Validation.Run(() => _localizationManager != null, "LocalizationManager", "Must not be null.");
         Validation.Run(() => _gameSettingsManager != null, "GameSettingsManager", "Must not be null.");
         Validation.Run(() => _animationManager != null, "AnimationManager", "Must not be null.");
         Validation.Run(() => _loggingManager != null, "LoggingManager", "Must not be null.");

         Validation.Run(() => _logger != null, "_logger", "Must not be null.");
         Validation.Run(() => _confirmationDialogService != null, "_confirmationDialogService", "Must not be null.");
         Validation.Run(() => _persistentContainer != null, "_persistentContainer", "Must not be null.");

         Prepare();
      }

      #endregion Unity API

      #region IGameManager

      IPanelManager IGameManager.PanelManager => _panelManager;

      ISoundManager IGameManager.SoundManager => _soundManager;

      IUiManager IGameManager.UiManager => _uiManager;

      ILevelManager IGameManager.LevelManager => _levelManager;

      IAnalyticsManager IGameManager.AnalyticsManager => _analyticsManager;

      IPurchaseManager IGameManager.PurchaseManager => _purchaseManager;

      IInfoLogger IGameManager.Logger => _logger;

      ILocalizationManager IGameManager.LocalizationManager => _localizationManager;

      ISystemManager IGameManager.SystemManager => _systemManager;

      IResourceManager IGameManager.ResourceManager => _resourceManager;

      IPersistentPreferencesService IGameManager.PersistentPreferencesService => _persistentPreferencesService;

      IGameSettingsManager IGameManager.GameSettingsManager => _gameSettingsManager;

      IAnimationManager IGameManager.AnimationManager => _animationManager;

      IConfirmationDialogService IGameManager.ConfirmationDialogService => _confirmationDialogService;

      ILoggingManager IGameManager.LoggingManager => _loggingManager;

      #endregion IGameManager
   }
}