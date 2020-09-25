#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS

// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security
{
   public class AppleTangle
   {
      private static byte[] data = System.Convert.FromBase64String("zsud1cbM3szc4xihj1y+wTY8o0Wy+ErJvvjGzsud1cfJyTfMzMvKyf36+fz4+/6S38X7/fj6+PH6+fz4pqzoq6emrKG8oaemu+inrui9u628oa6hq6m8reiqseipprHouKm6vPv+kviq+cP4wc7LnczO28qdm/nbqqSt6Lu8qaasqbqs6LytuqW76Kl98mU8x8bIWsN56d7mvB30xROq3o2214SjmF6JQQy8qsPYS4lP+0JJ7CojGX+4F8eNKe8COaWwJS99399ja7laj5udCWfniXswMyu4BS5rhOJOgE4/xcnJzc3I+Kr5w/jBzsudgRC+V/vcrWm/XAHlysvJyMlrSsn+UYTlsH8lRFMUO79TOr4av/iHCeiLifhKyer4xc7B4k6ATj/FycnJ5/hJC87A487Jzc3Pysr4SX7SSXvHVfU744Hg0gA2Bn1xxhGW1B4D9eippqzoq626vKGuoaupvKGnpui4YBS26v0C7R0Rxx6jHGrs69k/aWS8oKe6obyx+d743M7LnczL28WJuMCW+ErJ2c7LndXozErJwPhKycz4xc7B4k6ATj/FycnNzcjLSsnJyJShrqGrqbyhp6boib28oKe6obyx+fXur+hC+6I/xUoHFiNr5zGbopOsSsnIzsHiToBOP6uszcn4STr44s5D0UEWMYOkPc9j6vjKIND2MJjBG5FvzcG034ie2da8G39D6/OPax2nSNzjGKGPXL7BNjyjReaIbj+PhbfO+MfOy53V28nJN8zN+MvJyTf41a9HwHzoPwNk5OinuH73yfhEf4sH3vjczsudzMvbxYm4uKSt6Jqnp7y3iWBQMRkCrlTso9kYa3Ms0+IL1/hKzHP4SstraMvKycrKycr4xc7Bf9N1W4rs2uIPx9V+hVSWqwCDSN/u+OzOy53Mw9vVibi4pK3oi626vAir+78/8s/kniMSx+nGEnK70Yd9Ef63CU+dEW9RcfqKMxAduVa2aZqs/evdg92R1XtcPz5UVgeYcgmQmM3Iy0rJx8j4SsnCykrJycgsWWHBzM7byp2b+dv42c7LnczC28KJuLjXWRPWj5gjzSWWsUzlI/5qn4SdJOaIbj+PhbfAlvjXzsud1evM0PjeXVayxGyPQ5Mc3v/7AwzHhQbcoRn42c7LnczC28KJuLikreiBpqvm+b+/5qm4uKSt5qunpeepuLikrauppK3ogaar5vnu+OzOy53Mw9vVibi4pK3omqenvOiLifjW38X4/vj8+nn4kCSSzPpEoHtH1Ratuzevlq10djy7UyYarMcDsYf8EGr2MbA3owC6qau8oaut6Lu8qbytpa2mvLvm+LikreiLrbq8oa6hq6m8oaem6Im9wOPOyc3Nz8rJ3tagvLy4u/Ln57/XTUtN01H1j/86YVOIRuQceVjaEEe7SagO05PB51p6MIyAOKjwVt09mq2koammq63op6bovKChu+irrbqx6Km7u72lrbvoqaurrbi8qaarrc8ktfFLQ5voG/AMeXdSh8KjN+M0AdG6PZXGHbeXUzrty3KdR4WVxTnk6KuturyhrqGrqbyt6LinpKGrseinrui8oK3ovKCtpuipuLikoaupmGJCHRIsNBjBz/94vb3p");
      private static int[] order = new int[] { 30, 28, 7, 43, 39, 8, 42, 23, 25, 53, 24, 28, 27, 48, 48, 37, 22, 45, 54, 25, 33, 47, 28, 56, 34, 28, 52, 32, 29, 47, 32, 45, 32, 59, 34, 57, 58, 59, 48, 56, 59, 45, 54, 53, 58, 53, 54, 52, 50, 54, 56, 57, 54, 55, 56, 59, 57, 57, 58, 59, 60 };
      private static int key = 200;

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