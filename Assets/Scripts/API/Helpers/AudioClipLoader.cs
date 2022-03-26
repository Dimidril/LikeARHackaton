using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace API.Helpers
{
    public class AudioClipLoader : MonoBehaviour
    {
        public static void LoadCouple(string link1, string link2, string savePath, System.Action<string, string> onLoadCallback)
        {
            Load(link1, savePath, (filepath1 => Load(link2, savePath, (filepath2 => onLoadCallback?.Invoke(filepath1, filepath2)))));
        }

        public static void Load(string link, string savePath, System.Action<string> onLoadCallback)
        {
            if(CheckIfCached(Path.Combine(savePath, new Uri(link).ToString().Split('/').Last())))
                return;

            var loader = new GameObject().AddComponent<AudioClipLoader>();

            loader.StartCoroutine(loader.LoadAudioClip(link, savePath, onLoadCallback));
        }

        public static bool CheckIfCached(string filepath)
        {
            return File.Exists(filepath);
        }

        public static void DeleteCached(string filepath, System.Action OnDeletedCallback)
        {
            if (CheckIfCached(filepath))
            {
                File.Delete(filepath);
                OnDeletedCallback?.Invoke();
            }
        }

        IEnumerator LoadAudioClip(string url, string cacheSavePath, System.Action<string> callback)
        {
            Debug.Log("Начинаю загрузку аудио: " + url);
            var audioName = new Uri(url).ToString().Split('/').Last();
            gameObject.name = url;

            AudioClip loadedAudioClip = null;
            var ready = false;

            var isLocalUrl = !Uri.IsWellFormedUriString(url, UriKind.Absolute);

            var filePath = isLocalUrl ? url : Path.Combine(cacheSavePath, audioName);

            if (!File.Exists(filePath))
            {
                using (var audioClipWebRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS))
                {
                    yield return audioClipWebRequest.SendWebRequest();

                    if (audioClipWebRequest.result == UnityWebRequest.Result.ConnectionError)
                    {
                        StartCoroutine(LoadAudioClip(url, cacheSavePath, callback));
                        HandleAPIError.Handle(audioClipWebRequest);
                    }
                    else
                    {
                        loadedAudioClip = DownloadHandlerAudioClip.GetContent(audioClipWebRequest);

                        if (!string.IsNullOrEmpty(filePath))
                        {
                            Debug.Log($"Сохраняю звук: {filePath}");
                            File.WriteAllBytes(filePath, audioClipWebRequest.downloadHandler.data);
                            ready = true;
                        }

                        audioClipWebRequest.Dispose();
                    }
                }
            }
            else
            {
                Debug.Log($"Аудио найдено в кеше: {filePath}");
                ready = true;
            }

            yield return new WaitUntil(() => ready);

            callback?.Invoke(filePath);

            Resources.UnloadUnusedAssets();
            Destroy(gameObject);
        }
    }
}