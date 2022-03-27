using System;
using System.Collections;
using System.IO;
using Cache;
using Siccity.GLTFUtility;
using UnityEngine;
using UnityEngine.Networking;

namespace API.Helpers
{
    public class ModelLoader : MonoBehaviour
    {
        static string filePath => CacheDestination.ModelFolderPath;

        /// <summary>
        /// Чисто загрузка модели в кэш
        /// </summary>
        /// <param name="url">Ссылка из сервера</param>
        public static void DownloadModel(string url, Action<GameObject> callback)
        {
            var loader = new GameObject().AddComponent<ModelLoader>();
            loader.DownloadFile(url, callback);
        }
        
        void DownloadFile(string url, Action<GameObject> callback)
        {
            string path = GetFilePath(url);
            if (File.Exists(path))
            {
                Debug.Log("Found file locally, loading...");
                callback?.Invoke(GetModel(url));
                Destroy(gameObject);
                return;
            }

            StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
            {
                if (req.isNetworkError || req.isHttpError)
                {
                    // Log any errors that may happen
                    Debug.Log($"{req.error} : {req.downloadHandler.text}");
                }
                callback?.Invoke(GetModel(url));
                Destroy(gameObject);
            }));
        }
        
        /// <summary>
        /// Забрать модель из файловой системы
        /// </summary>
        /// <param name="url">Ссылка которая приходит с сервера</param>
        /// <returns>Возвращает саму модель</returns>
        public static GameObject GetModel(string url)
        {
            return Importer.LoadFromFile(GetFilePath(url));
        }

        static string GetFilePath(string url)
        {
            string[] pieces = url.Split('/');
            string filename = pieces[pieces.Length - 1];

            return $"{filePath}/{filename}";
        }

        IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
        {
            using(UnityWebRequest req = UnityWebRequest.Get(url))
            {
                req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
                yield return req.SendWebRequest();
                callback(req);
            }
        }
    }
}