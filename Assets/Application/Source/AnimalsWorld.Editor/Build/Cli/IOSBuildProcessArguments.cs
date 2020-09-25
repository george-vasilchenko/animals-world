namespace MalenkiyApps
{
   public class IOSBuildProcessArguments : BuildProcessArgumentsBase
   {
      [CliOption("buildNumber")]
      public string BuildNumber { get; set; }

      [CliOption("targetOSVersionString")]
      public string TargetOsVersionString { get; set; }

      [CliOption("appleDeveloperTeamID")]
      public string AppleDeveloperTeamID { get; set; }

      public override void Validate()
      {
         base.Validate();

         Validation.Run(() => !string.IsNullOrWhiteSpace(BuildNumber),
            nameof(BuildNumber), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(TargetOsVersionString),
            nameof(TargetOsVersionString), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(AppleDeveloperTeamID),
            nameof(AppleDeveloperTeamID), "Must not be null or white space.");
      }
   }
}