using System;
using System.Collections;
using System.IO;
using System.Linq;
using Cache;
using UnityEngine;
using UnityEngine.Networking;

namespace API.Helpers
{
    public class TextureLoader : MonoBehaviour
    {
        private string savePath;

        private void Awake()
        {
            savePath = CacheDestination.ImagesFolderPath;
        }

        public static void Load(string link, System.Action<Texture2D> onLoadCallback)
        {
            var loader = new GameObject().AddComponent<TextureLoader>();

            loader.StartCoroutine(loader.LoadTextureFromServer(link, onLoadCallback));
        }

        IEnumerator LoadTextureFromServer(string url, System.Action<Texture2D> callback)
        {
            var textureName = new Uri(url).ToString().Split('/').Last();
            gameObject.name = url;

            Texture2D loadedTexture = null;
            var ready = false;
            var isLocalUrl = !Uri.IsWellFormedUriString(url, UriKind.Absolute);

            var filePath = isLocalUrl ? url : Path.Combine(savePath, textureName);

            if (!File.Exists(filePath))
            {
                using (var textureWebRequest = UnityWebRequestTexture.GetTexture(url))
                {
                    Debug.Log($"Начинаю загрузку текстуры: {url}");

                    yield return textureWebRequest.SendWebRequest();

                    if (textureWebRequest.result == UnityWebRequest.Result.ConnectionError || textureWebRequest.result == UnityWebRequest.Result.ProtocolError)
                    {
                        HandleAPIError.Handle(textureWebRequest);
                    }
                    else
                    {
                        var texture = ((DownloadHandlerTexture) textureWebRequest.downloadHandler).texture;

                        File.WriteAllBytes(filePath, texture.EncodeToPNG());
                        Debug.Log($"Сохраняю текстуру {textureName} в кеш {filePath}");

                        loadedTexture = texture;
                        ready = true;
                        textureWebRequest.Dispose();
                    }
                }
            }
            else
            {
                Debug.Log($"Текстура найдена в кеше: {filePath}");
                loadedTexture = new Texture2D(2, 2);
                loadedTexture.LoadImage(File.ReadAllBytes(filePath));
                ready = true;
            }

            yield return new WaitUntil(() => ready);

            callback?.Invoke(loadedTexture);

            Resources.UnloadUnusedAssets();
            Destroy(gameObject);
        }
    }
}