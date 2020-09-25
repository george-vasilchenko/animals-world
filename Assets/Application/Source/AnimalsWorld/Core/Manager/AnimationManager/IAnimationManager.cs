using UnityEngine;

namespace AnimalsWorld
{
   public interface IAnimationManager
   {
      AnimationClip WoodTitleDropAnimationClip { get; }
      AnimationClip PopAnimationClip { get; }
   }
}