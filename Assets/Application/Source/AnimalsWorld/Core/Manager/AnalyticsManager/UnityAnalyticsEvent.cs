using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class UnityAnalyticsEvent : IAnalyticsEvent<UnityAnalyticsEventParameter>
   {
      public string Name { get; private set; }

      public UnityAnalyticsEventParameter Parameter { get; private set; }

      public UnityAnalyticsEvent(string name, UnityAnalyticsEventParameter parameter)
      {
         Name = name;
         Parameter = parameter;
      }
   }
}