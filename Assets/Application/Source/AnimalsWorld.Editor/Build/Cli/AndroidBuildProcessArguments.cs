using System;

namespace MalenkiyApps
{
   public class AndroidBuildProcessArguments : BuildProcessArgumentsBase
   {
      // Android specific

      [CliOption("bundleVersionCode")]
      public string BundleVersionCode { get; set; }

      [CliOption("keyAliasName")]
      public string KeyAliasName { get; set; }

      [CliOption("keyAliasPass")]
      public string KeyAliasPass { get; set; }

      [CliOption("keyStoreName")]
      public string KeyStoreName { get; set; }

      [CliOption("keyStorePass")]
      public string KeyStorePass { get; set; }

      [CliOption("targetSdkVersion")]
      public string TargetSdkVersion { get; set; }

      [CliOption("minSdkVersion")]
      public string MinSdkVersion { get; set; }

      public override void Validate()
      {
         base.Validate();

         // Android specific

         Validation.Run(() => Convert.ToInt32(BundleVersionCode) > 0,
            nameof(BundleVersionCode), "Must be greater than 0.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(KeyAliasName),
            nameof(KeyAliasName), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(KeyAliasPass),
            nameof(KeyAliasPass), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(KeyStoreName),
            nameof(KeyStoreName), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(KeyStorePass),
            nameof(KeyStorePass), "Must not be null or white space.");

         Validation.Run(() => Convert.ToInt32(TargetSdkVersion) >= 0,
            nameof(TargetSdkVersion), "Must be a positive integer.");

         Validation.Run(() => Convert.ToInt32(MinSdkVersion) >= 0,
            nameof(MinSdkVersion), "Must be a positive integer.");
      }
   }
}