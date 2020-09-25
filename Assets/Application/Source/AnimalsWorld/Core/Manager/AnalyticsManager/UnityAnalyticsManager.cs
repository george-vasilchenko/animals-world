using MalenkiyApps.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace AnimalsWorld
{
   public class UnityAnalyticsManager : IAnalyticsManager
   {
      public const string LevelStartEventName = "LevelStart";
      public const string RewardGameStartEventName = "RewardGameStart";
      public const string RewardGameCloseEventName = "RewardGameClose";

      public void LogLevelInfo(ILevelInfo levelInfo)
      {
         var parameter = new Dictionary<string, object>
         {
            {"ChapterIndex", levelInfo.ChapterIndex},
            {"LevelIndex", levelInfo.LevelIndex}
         };
         var result = AnalyticsEvent.Custom(LevelStartEventName, parameter);

         AssertResult(LevelStartEventName, result);
      }

      public void LogRewardGameStartInfo(IRewardGameStartInfo rewardGameStartInfo)
      {
         var parameter = new Dictionary<string, object>
         {
            {"Name", rewardGameStartInfo.Name}
         };
         var result = AnalyticsEvent.Custom(RewardGameStartEventName, parameter);

         AssertResult(RewardGameStartEventName, result);
      }

      public void LogRewardGameCloseInfo(IRewardGameCloseInfo rewardGameCloseInfo)
      {
         var parameter = new Dictionary<string, object>
         {
            {"Name", rewardGameCloseInfo.Name}
         };
         var result = AnalyticsEvent.Custom(RewardGameCloseEventName, parameter);

         AssertResult(RewardGameCloseEventName, result);
      }

      private static void AssertResult(string eventName, AnalyticsResult result)
      {
         if (result != AnalyticsResult.Ok)
         {
            Debug.LogWarningFormat("Analytics event: {0} failed, reason: {1}.", eventName, result);
         }
         else
         {
            Debug.LogFormat("Analytics event: {0} sent.", eventName);
         }
      }
   }
}