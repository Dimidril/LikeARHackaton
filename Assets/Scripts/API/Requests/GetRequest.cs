using System.Collections;
using API.Helpers;
using UnityEngine;
using UnityEngine.Networking;

namespace API.Requests
{
    public class GetRequest : MonoBehaviour
    {
        static readonly string AUTH_TOKEN = $"Vm1wR1lXSXhVWGxTV0d4VlYwZDRWRmxzYUZOWl";

        public static void Send(string url, System.Action<string, bool> callback)
        {
            var loader = new GameObject().AddComponent<GetRequest>();

            loader.StartCoroutine(loader.Get(url, callback));
        }

        IEnumerator Get(string url, System.Action<string, bool> callback)
        {
            using (var webRequest = UnityWebRequest.Get(url))
            {
                Debug.Log($"Отправляю GET запрос: {url}");

                webRequest.SetRequestHeader("Authorization", AUTH_TOKEN);

                yield return webRequest.SendWebRequest();

                var isNetworkError = webRequest.result == UnityWebRequest.Result.ConnectionError;
                var isHttpError = webRequest.result == UnityWebRequest.Result.ProtocolError;

                if (isNetworkError || isHttpError)
                    HandleAPIError.Handle(webRequest);

                callback?.Invoke(webRequest.downloadHandler.text, isNetworkError || isHttpError);

                webRequest.Dispose();

                Destroy(gameObject);
            }
        }
    }
}