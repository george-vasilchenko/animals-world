namespace AnimalsWorld
{
   public interface ICommonSettingsService
   {
      event OnCommonSettingsVolumeChangedDelegate OnCommonSettingsMusicVolumeChanged;

      event OnCommonSettingsVolumeChangedDelegate OnCommonSettingsSfxVolumeChanged;

      float MusicVolume { get; set; }

      float SfxVolume { get; set; }
   }
}