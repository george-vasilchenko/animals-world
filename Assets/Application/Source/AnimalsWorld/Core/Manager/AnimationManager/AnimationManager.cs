using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class AnimationManager : MonoBehaviour, IAnimationManager
   {
      [SerializeField]
      private AnimationClip _woodTitleDropAnimation = default;

      [SerializeField]
      private AnimationClip _popAnimation = default;

      public AnimationClip WoodTitleDropAnimationClip => _woodTitleDropAnimation;
      public AnimationClip PopAnimationClip => _popAnimation;

      private void Awake()
      {
         Validation.Run(() => _popAnimation != null, nameof(_popAnimation), "Must not be null");
         Validation.Run(() => _woodTitleDropAnimation != null, nameof(_woodTitleDropAnimation), "Must not be null");
      }
   }
}