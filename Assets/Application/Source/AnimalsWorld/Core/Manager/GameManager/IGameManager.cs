using MalenkiyApps;
using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public interface IGameManager
   {
      IPanelManager PanelManager { get; }

      ISoundManager SoundManager { get; }

      IUiManager UiManager { get; }

      IAnalyticsManager AnalyticsManager { get; }

      ILevelManager LevelManager { get; }

      IPurchaseManager PurchaseManager { get; }

      IInfoLogger Logger { get; }

      IResourceManager ResourceManager { get; }

      ILocalizationManager LocalizationManager { get; }

      IPersistentPreferencesService PersistentPreferencesService { get; }

      ISystemManager SystemManager { get; }

      IGameSettingsManager GameSettingsManager { get; }

      IAnimationManager AnimationManager { get; }

      IConfirmationDialogService ConfirmationDialogService { get; }

      ILoggingManager LoggingManager { get; }
   }
}