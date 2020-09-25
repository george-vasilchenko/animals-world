using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AnimalsWorld
{
   public static class Common
   {
      public static IEnumerator CheckInternetConnectionRoutine()
      {
         const string uri = @"https://www.google.com/";
         var isConnected = false;

         using (var request = new UnityWebRequest(uri))
         {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
               Debug.Log(request.error);
            }
            else
            {
               isConnected = true;
            }
         }

         yield return isConnected;
      }

      ////[DllImport("wininet.dll")]
      ////public static extern bool InternetGetConnectedState(out int description, int reservedValue);

      ////public static void TestConnection()
      ////{
      ////   var isConnected = InternetGetConnectedState(out var description, 0);
      ////   Debug.Log("Internet connection test returned: " + description);
      ////}

      ////public static IEnumerator CheckInternetConnectionInternal()
      ////{
      ////   var isConnected = InternetGetConnectedState(out var description, 0);

      ////   Debug.Log("Internet connection test returned: ");

      ////   yield return isConnected;
      ////}

      ////[System.Runtime.InteropServices.DllImport("wininet.dll")]
      ////private static extern bool InternetGetConnectedState(out int description, int reservedValue);

      ////private Dictionary<int, string> ConnectionStates = new Dictionary<int, string>()
      ////{
      ////   { 0x40, "INTERNET_CONNECTION_CONFIGURED: Local system has a valid connection to the Internet, but it might or might not be currently connected."},
      ////   { 0x02, "INTERNET_CONNECTION_LAN: Local system uses a local area network to connect to the Internet."},
      ////   { 0x01, "INTERNET_CONNECTION_MODEM: Local system uses a local area network to connect to the Internet."},
      ////   { 0x02, "INTERNET_CONNECTION_LAN: Local system uses a local area network to connect to the Internet."},
      ////   { 0x02, "INTERNET_CONNECTION_LAN: Local system uses a local area network to connect to the Internet."},
      ////   { 0x02, "INTERNET_CONNECTION_LAN: Local system uses a local area network to connect to the Internet."},
      ////   { 0x02, "INTERNET_CONNECTION_LAN: Local system uses a local area network to connect to the Internet."},
      ////   { 0x02, "INTERNET_CONNECTION_LAN: Local system uses a local area network to connect to the Internet."},
      ////};
      //// https://docs.microsoft.com/en-us/windows/desktop/api/wininet/nf-wininet-internetgetconnectedstate
   }
}