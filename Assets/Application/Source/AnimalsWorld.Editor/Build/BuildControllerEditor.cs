using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Networking;

namespace MalenkiyApps
{
   public class BuildControllerEditor
   {
      public static void BuildAndroidProjectExtended()
      {
         var arguments = CliParser.ProcessArguments<AndroidBuildProcessArguments>(Environment.GetCommandLineArgs());

         if (arguments == null)
         {
            throw new Exception("CliOptions must not be null.");
         }

         arguments.Validate();

         PlayerSettings.bundleVersion = arguments.BundleVersion;
         PlayerSettings.companyName = arguments.CompanyName;
         PlayerSettings.applicationIdentifier = arguments.ApplicationIdentifier;
         PlayerSettings.productName = arguments.ProductName;

         PlayerSettings.Android.keyaliasName = arguments.KeyAliasName;
         PlayerSettings.Android.keyaliasPass = arguments.KeyAliasPass;
         PlayerSettings.Android.keystoreName = arguments.KeyStoreName;
         PlayerSettings.Android.keystorePass = arguments.KeyStorePass;
         PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(arguments.BundleVersionCode);
         PlayerSettings.Android.minSdkVersion = (AndroidSdkVersions)Convert.ToInt32(arguments.MinSdkVersion);
         PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions)Convert.ToInt32(arguments.TargetSdkVersion);

         // IL2CPP
         PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;
         PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
         PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android,
            Convert.ToInt32(arguments.Development) == 1
               ? Il2CppCompilerConfiguration.Debug
               : Il2CppCompilerConfiguration.Release);

         var buildPlayerOptions = new BuildPlayerOptions
         {
            target = BuildTarget.Android,
            scenes = arguments.GetScenes(),
            targetGroup = BuildTargetGroup.Android,
            locationPathName = arguments.BuildOutputLocation
         };

         EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
         EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
         EditorUserBuildSettings.development = Convert.ToInt32(arguments.Development) == 1;

         // build
         var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
         HandleReport(report);

         EditorApplication.Exit(0);
      }

      public static void BuildIOSProject()
      {
         var args = Environment.GetCommandLineArgs();

         Debug.Log(!args.Any() ? "args->empty" : string.Join("\r\n", args));

         var arguments = CliParser.ProcessArguments<IOSBuildProcessArguments>(args);

         if (arguments == null)
         {
            throw new Exception("CliOptions must not be null.");
         }

         arguments.Validate();

         PlayerSettings.bundleVersion = arguments.BundleVersion;
         PlayerSettings.companyName = arguments.CompanyName;
         PlayerSettings.applicationIdentifier = arguments.ApplicationIdentifier;
         PlayerSettings.productName = arguments.ProductName;

         PlayerSettings.iOS.applicationDisplayName = arguments.ProductName;
         PlayerSettings.iOS.buildNumber = arguments.BuildNumber;
         PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
         PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
         PlayerSettings.iOS.targetOSVersionString = arguments.TargetOsVersionString;
         PlayerSettings.iOS.appleDeveloperTeamID = arguments.AppleDeveloperTeamID;
         PlayerSettings.iOS.appleEnableAutomaticSigning = true;
         PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 2);

         // IL2CPP
         PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
         PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.iOS,
            Convert.ToInt32(arguments.Development) == 1
               ? Il2CppCompilerConfiguration.Debug
               : Il2CppCompilerConfiguration.Release);

         var buildPlayerOptions = new BuildPlayerOptions
         {
            target = BuildTarget.iOS,
            scenes = arguments.GetScenes(),
            targetGroup = BuildTargetGroup.iOS,
            locationPathName = arguments.BuildOutputLocation
         };

         EditorUserBuildSettings.development = Convert.ToInt32(arguments.Development) == 1;

         // build
         var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
         HandleReport(report);

         EditorApplication.Exit(0);
      }

      private static void HandleReport(BuildReport report)
      {
         var overview = new AnimalsWorld.BuildOverview(report);
         var overviewString = JsonUtility.ToJson(overview);

         Debug.Log($"Build overview: {overviewString}");

         switch (report.summary.result)
         {
            case BuildResult.Succeeded:
               Debug.Log("Build succeeded!");
               break;

            case BuildResult.Failed: throw new Exception("Build failed.");
            case BuildResult.Unknown: throw new Exception("Build result unknown.");
            case BuildResult.Cancelled: throw new Exception("Build cancelled.");
            default:
               throw new ArgumentOutOfRangeException();
         }
      }
   }
}