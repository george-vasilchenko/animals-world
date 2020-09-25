using MalenkiyApps.Interfaces;
using System.Collections.Generic;

namespace AnimalsWorld
{
   public class UnityAnalyticsEventParameter : IAnalyticsEventParameter<Dictionary<string, object>>
   {
      public Dictionary<string, object> Data { get; private set; }

      public UnityAnalyticsEventParameter(Dictionary<string, object> paramter)
      {
         Data = paramter;
      }
   }
}