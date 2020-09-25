namespace MalenkiyApps.Interfaces
{
   public interface IAnalyticsEvent<out TParameter>
   {
      string Name { get; }

      TParameter Parameter { get; }
   }
}