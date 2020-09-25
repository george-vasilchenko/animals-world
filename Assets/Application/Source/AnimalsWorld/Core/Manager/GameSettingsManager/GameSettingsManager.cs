using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class GameSettingsManager : MonoBehaviour, IGameSettingsManager
   {
      [SerializeField]
      private CommonSettingsService _commonSettingsService = default;

      public ICommonSettingsService CommonSettingsService => _commonSettingsService;

      private IGameSettingsPersistenceService _gameSettingsPersistenceService;

      private void Awake()
      {
         Validation.Run(() => CommonSettingsService != null, nameof(CommonSettingsService), "Must not be null.");

         _gameSettingsPersistenceService = new GameSettingsPersistenceService(GameManager.Instance.PersistentPreferencesService);
      }

      private void OnEnable() => LoadSettings();

      private void OnDisable() => SaveSettings();

      private void LoadSettings()
      {
         var model = _gameSettingsPersistenceService.Load();

         CommonSettingsService.MusicVolume = model.CommonSettings.MusicVolume;
         CommonSettingsService.SfxVolume = model.CommonSettings.SfxVolume;
      }

      private void SaveSettings()
      {
         var commonSettingsModel = new CommonSettings(musicVolume: CommonSettingsService.MusicVolume, sfxVolume: CommonSettingsService.SfxVolume);
         var gameSettingsModel = new GameSettings(commonSettingsModel);

         _gameSettingsPersistenceService.Save(gameSettingsModel);
      }
   }
}