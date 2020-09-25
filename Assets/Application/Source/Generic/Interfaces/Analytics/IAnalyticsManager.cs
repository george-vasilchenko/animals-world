namespace MalenkiyApps.Interfaces
{
   public interface IAnalyticsManager
   {
      void LogLevelInfo(ILevelInfo levelInfo);

      void LogRewardGameStartInfo(IRewardGameStartInfo rewardGameStartInfo);

      void LogRewardGameCloseInfo(IRewardGameCloseInfo rewardGameCloseInfo);
   }
}