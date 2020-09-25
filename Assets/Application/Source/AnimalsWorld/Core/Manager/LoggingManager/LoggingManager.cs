using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class LoggingManager : MonoBehaviour, ILoggingManager
   {
      private ILocalizationManager _localizationManager;
      private IInfoLogger _logger;

      private void Awake()
      {
         _logger = GameManager.Instance.Logger;
         _localizationManager = GameManager.Instance.LocalizationManager;
      }

      public void LogMessage(string message)
      {
         _logger.Log(new InfoMessage(message));
      }

      public void LogLocalizedMessage(string key)
      {
         var message = _localizationManager.GetTranslation(key);
         _logger.Log(new InfoMessage(message));
      }
   }
}