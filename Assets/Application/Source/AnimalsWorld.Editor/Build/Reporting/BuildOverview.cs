using System;
using System.Globalization;
using System.Linq;
using UnityEditor.Build.Reporting;

namespace AnimalsWorld
{
   [Serializable]
   public class BuildOverview
   {
      public string buildStartedAt;
      public string buildEndedAt;
      public string outputPath;
      public string platform;
      public string result;
      public int totalErrors;
      public long totalSize;
      public string totalTime;
      public int totalWarnings;
      public OverviewBuildStep[] steps;

      public BuildOverview(BuildReport unityBuildReport)
      {
         var summary = unityBuildReport.summary;

         buildEndedAt = summary.buildEndedAt.ToString(CultureInfo.InvariantCulture);
         buildStartedAt = summary.buildStartedAt.ToString(CultureInfo.InvariantCulture);
         outputPath = summary.outputPath;
         platform = summary.platform.ToString();
         result = summary.result.ToString();
         totalErrors = summary.totalErrors;
         totalSize = (long)summary.totalSize;
         totalTime = summary.totalTime.ToString();
         totalWarnings = summary.totalWarnings;
         steps = unityBuildReport.steps.Select(o => new OverviewBuildStep
         {
            Name = o.name,
            Depth = o.depth,
            Duration = o.duration.ToString(),
            Messages = o.messages.Select(BuildStepMessageSelector).ToArray()
         }).ToArray();
      }

      private static OverviewBuildStepMessage BuildStepMessageSelector(BuildStepMessage m)
      {
         return new OverviewBuildStepMessage
         {
            Type = m.type.ToString(),
            Content = m.content
         };
      }
   }
}