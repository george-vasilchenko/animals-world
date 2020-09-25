using System.IO;
using MalenkiyApps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace AnimalsWorld
{
   public class LogoComponent : MonoBehaviour
   {
      private VideoPlayer _videoPlayer = default;
      private bool _isStartedLoading = false;

      private void Awake()
      {
         Debug.Log("Playing Logo animation.");

         _videoPlayer = GetComponent<VideoPlayer>();
         Validation.Run(() => _videoPlayer != null, nameof(_videoPlayer), "Must not be null.");

         _videoPlayer.source = VideoSource.Url;
         _videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "Feather.mp4");
      }

      private void Start()
      {
         _videoPlayer.Play();
      }

      private void Update()
      {
         if (_videoPlayer.isPlaying)
         {
            return;
         }

         if (_isStartedLoading)
         {
            return;
         }

         _isStartedLoading = true;
         Debug.Log("Loading Preload scene...");
         SceneManager.LoadScene("Preload");
      }
   }
}