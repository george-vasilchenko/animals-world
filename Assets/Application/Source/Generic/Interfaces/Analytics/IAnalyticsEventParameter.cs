using System.Collections.Generic;

namespace MalenkiyApps.Interfaces
{
   public interface IAnalyticsEventParameter<out TParamter> where TParamter : IDictionary<string, object>
   {
      TParamter Data { get; }
   }
}