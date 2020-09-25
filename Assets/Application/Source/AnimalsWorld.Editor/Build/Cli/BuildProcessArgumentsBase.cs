using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalenkiyApps
{
   public class BuildProcessArgumentsBase
   {
      // App generic

      [CliOption("scenes")]
      public string Scenes { get; set; }

      [CliOption("development")]
      public string Development { get; set; }

      [CliOption("applicationIdentifier")]
      public string ApplicationIdentifier { get; set; }

      [CliOption("productName")]
      public string ProductName { get; set; }

      [CliOption("companyName")]
      public string CompanyName { get; set; }

      [CliOption("buildOutputLocation")]
      public string BuildOutputLocation { get; set; }

      [CliOption("bundleVersion")]
      public string BundleVersion { get; set; }

      public virtual void Validate()
      {
         Validation.Run(() => !string.IsNullOrWhiteSpace(Scenes),
            nameof(Scenes), "Must not be null or white space.");

         Validation.Run(() => Convert.ToInt32(Development) == 0 || Convert.ToInt32(Development) == 1,
            nameof(Scenes), "Must be either 0 or 1.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(ApplicationIdentifier),
            nameof(ApplicationIdentifier), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(ProductName),
            nameof(ProductName), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(CompanyName),
            nameof(CompanyName), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(BuildOutputLocation),
            nameof(BuildOutputLocation), "Must not be null or white space.");

         Validation.Run(() => !string.IsNullOrWhiteSpace(BundleVersion),
            nameof(BundleVersion), "Must not be null or white space.");
      }

      public string[] GetScenes()
      {
         const char separator = ',';
         return Scenes.Split(separator);
      }
   }
}