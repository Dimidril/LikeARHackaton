using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace API.Helpers
{
    public class VideoLoader : MonoBehaviour
    {
        private static List<string> loadingURLs = new List<string>();

        public UnityWebRequest request;

        public System.Action OnDownloaded;
        
        #region Methods

        #region UnityMethods

        #endregion

        #endregion
        
        #region Public Methods

        public static void Load(string link, string cacheSavePath, System.Action<string> onLoadCallback, System.Action<float> onProgress = null)
        {
            var loader = new GameObject().AddComponent<VideoLoader>();

            loader.StartCoroutine(loader.LoadVideoFromServer(link, cacheSavePath, onLoadCallback, onProgress));
            DontDestroyOnLoad(loader);
        }

        public static void Delete(string localPath, System.Action<string> onDeleteCallback = null)
        {
            if (!string.IsNullOrEmpty(localPath))
            {
                File.Delete(localPath);
                onDeleteCallback?.Invoke(localPath);
                Debug.Log($"Файл удалён {localPath}");
            }
        }

        public static bool IsDownolad(string url, string cacheSavePath)
        {
            var videoName = new Uri(url).ToString().Split('/').Last();
            var isLocalUrl = !Uri.IsWellFormedUriString(url, UriKind.Absolute);

            var filePath = isLocalUrl ? url : Path.Combine(cacheSavePath, videoName);

            return File.Exists(filePath);
        }

        public static bool IsLoading(string url)
        {
            foreach (var curUrl in loadingURLs)
            {
                if (curUrl == url)
                {
                    return true;
                }
            }
            
            return false;
        }

        public static VideoLoader GetLoaderFromURL(string url)
        {
            if (GameObject.Find(url) == null)
                return null;
            return GameObject.Find(url).GetComponent<VideoLoader>();
        }
        
        public static float GetProgressFromLoader(VideoLoader loader)
        {
            if (loader != null)
                return loader.request.downloadProgress;
            else
                return -1f;
        }

        #endregion

        #region Private Methods

        IEnumerator LoadVideoFromServer(string url, string cacheSavePath, System.Action<string> callback, System.Action<float> onLoadingProgress)
        {
            Application.runInBackground = true;
            loadingURLs.Add(url);

            var videoName = new Uri(url).ToString().Split('/').Last();
            gameObject.name = url;

            string loadedVideoPath = null;
            var ready = false;
            var isLocalUrl = !Uri.IsWellFormedUriString(url, UriKind.Absolute);

            var filePath = isLocalUrl ? url : Path.Combine(cacheSavePath, videoName);

            if (!File.Exists(filePath))
            {
                using (request = UnityWebRequest.Get(url))
                {
                    Debug.Log($"Начинаю загрузку видео: {url}");

                    yield return request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                    {
                        HandleAPIError.Handle(request);
                        ready = true;
                    }
                    else
                    {
                        File.WriteAllBytes(filePath, request.downloadHandler.data);

                        loadedVideoPath = filePath;
                        ready = true;
                        request.Dispose();
                        Debug.Log($"Видео загружено: {filePath}");
                    }
                }
            }
            else
            {
                Debug.Log($"Видео найдена в кеше: {filePath}");
                loadedVideoPath = filePath;
                ready = true;
            }

            yield return new WaitUntil(() => ready);
            loadingURLs.Remove(url);
            callback?.Invoke(loadedVideoPath);
            OnDownloaded?.Invoke();

            Resources.UnloadUnusedAssets();

            if (loadingURLs.Count <= 0)
                Application.runInBackground = false;
            

            Destroy(gameObject);
        }

        #endregion
    }
}