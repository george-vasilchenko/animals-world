using System;
using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class GameSettings : IGameSettings
   {
      public CommonSettings CommonSettings { get; }

      public GameSettings(ICommonSettings commonSettings)
      {
         CommonSettings = new CommonSettings(commonSettings);
      }

      ICommonSettings IGameSettings.CommonSettings => CommonSettings;
   }
}