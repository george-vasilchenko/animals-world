using System.Collections;
using MalenkiyApps.Interfaces;
using UnityEngine;

namespace AnimalsWorld
{
   public class SystemManager : MonoBehaviour, ISystemManager
   {
      bool ISystemManager.IsConnectedToInternet => _isConnectedToInternet;

      private bool _isConnectedToInternet;

      IEnumerator IPreloadable.PreloadRoutine()
      {
         var internetConnectionControlRoutine = Routine<bool>.Create(Common.CheckInternetConnectionRoutine());
         yield return internetConnectionControlRoutine.Invoke();

         if (internetConnectionControlRoutine.Exception != null)
         {
            throw internetConnectionControlRoutine.Exception;
         }

         _isConnectedToInternet = internetConnectionControlRoutine.Result;

         Debug.Log(_isConnectedToInternet ? "Connected to internet." : "Disconnected from internet.");
      }
   }
}