using System;

namespace AnimalsWorld
{
   [Serializable]
   public class OverviewBuildStep
   {
      public string Name;
      public int Depth;
      public string Duration;
      public OverviewBuildStepMessage[] Messages;
   }
}