using System;
using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class CommonSettings : ICommonSettings
   {
      public float MusicVolume { get; }

      public float SfxVolume { get; }

      public CommonSettings(float musicVolume, float sfxVolume)
      {
         MusicVolume = musicVolume;
         SfxVolume = sfxVolume;
      }

      public CommonSettings(ICommonSettings commonSettings)
      {
         MusicVolume = commonSettings.MusicVolume;
         SfxVolume = commonSettings.SfxVolume;
      }
   }
}