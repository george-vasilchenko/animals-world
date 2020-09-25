#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS

// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security
{
   public class GooglePlayTangle
   {
      private static byte[] data = System.Convert.FromBase64String("tkoBAFjtEku6QnA3ZuawWMyQbTocNGelv03XcIMa3QI0deePTzHimhf/aAOuAWf9CjiR2T14Qc2b7l/xl+hlZWqd5R30A/AGIdKhBLliYXQAkC0uD+wl4G9aocVLGMp1pNmgl78DFEXllI/xuxrgNR+hTGzhGlsJyrV1mmzQG0e9rNDjN/vuLJdqY27VVlhXZ9VWXVXVVlZXsLh4wwn802fVVnVnWlFefdEf0aBaVlZWUldUQy3c22zeBYQwdpRHUBd1Und2tAVFv8/MjHGMuHMp/vA2emRZL9oJ3/0DrHgAe9oM9o9D8YnpH95tDXcW4SYoA960KJJ4wnHWlQ3CChpxhSfflKx78POyPCbxiXOxWptqBBg8lfMBOSLUKdl+3FVUVldW");
      private static int[] order = new int[] { 4, 5, 3, 13, 11, 6, 7, 7, 11, 11, 13, 13, 13, 13, 14 };
      private static int key = 87;

      public static readonly bool IsPopulated = true;

      public static byte[] Data()
      {
         if (IsPopulated == false)
            return null;
         return Obfuscator.DeObfuscate(data, order, key);
      }
   }
}

#endif