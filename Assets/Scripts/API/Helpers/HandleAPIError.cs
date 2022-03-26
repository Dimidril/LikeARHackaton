using UnityEngine;
using UnityEngine.Networking;

namespace API.Helpers
{
    public static class HandleAPIError
    {
        public static void Handle(UnityWebRequest webRequest)
        {
            Debug.LogError("URL: " + webRequest.url);
            Debug.LogError("Error: " + webRequest.error);
            Debug.LogError("Result: " + webRequest.result);
        }
    }
}