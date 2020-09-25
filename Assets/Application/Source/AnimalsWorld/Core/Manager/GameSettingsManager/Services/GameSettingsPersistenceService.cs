using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class GameSettingsPersistenceService : IGameSettingsPersistenceService
   {
      private readonly IPersistentPreferencesService _persistentPreferencesService;

      public GameSettingsPersistenceService(IPersistentPreferencesService persistentPreferencesService)
      {
         _persistentPreferencesService = persistentPreferencesService;
      }

      public IGameSettings Load()
      {
         var musicVolume =
            _persistentPreferencesService.HasKey(GameSettingsConstants.CommonSettingsMusicVolumePlayerPrefsKey)
               ? _persistentPreferencesService.GetFloatEntry(GameSettingsConstants.CommonSettingsMusicVolumePlayerPrefsKey)
               : _persistentPreferencesService.SetFloatEntry(GameSettingsConstants.CommonSettingsMusicVolumePlayerPrefsKey,
                  GameSettingsConstants.CommonSettingsMusicVolumeDefaultValue);

         var sfxVolume =
            _persistentPreferencesService.HasKey(GameSettingsConstants.CommonSettingsSfxVolumePlayerPrefsKey)
               ? _persistentPreferencesService.GetFloatEntry(GameSettingsConstants.CommonSettingsSfxVolumePlayerPrefsKey)
               : _persistentPreferencesService.SetFloatEntry(GameSettingsConstants.CommonSettingsSfxVolumePlayerPrefsKey,
                  GameSettingsConstants.CommonSettingsSfxVolumeDefaultValue);

         var commonSettings = new CommonSettings(musicVolume, sfxVolume);
         return new GameSettings(commonSettings);
      }

      public void Save(IGameSettings gameSettings)
      {
         _persistentPreferencesService.SetFloatEntry(GameSettingsConstants.CommonSettingsMusicVolumePlayerPrefsKey,
            gameSettings.CommonSettings.MusicVolume);

         _persistentPreferencesService.SetFloatEntry(GameSettingsConstants.CommonSettingsSfxVolumePlayerPrefsKey,
            gameSettings.CommonSettings.SfxVolume);
      }
   }
}