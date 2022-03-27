using System;
using System.Collections;
using System.IO;
using API;
using API.Requests;
using Cache;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.LoadingPage
{
    public class LoadingPage : Page
    {
        [SerializeField] private Button continueButton;
        
        
        private string placesResponse = string.Empty;
        private string coursesResponse = string.Empty;

        private const float WaitingTime = 0.1f;

        private void Awake()
        {
            continueButton.gameObject.SetActive(false);
            
            continueButton.onClick.AddListener(() =>
            {
                NextPageShow();
                Hide();
            });

            StartCoroutine(LoadingCoroutine());
        }

        private void OnEnable()
        {
            
        }

        private IEnumerator LoadingCoroutine()
        {
            GetRequest.Send(API.Constants.GET_PLACES_REQUEST_PATH, (placesText, isError) =>
            {
                if (isError)
                {
                    Debug.LogError($"Проблемы с запросом: {API.Constants.GET_PLACES_REQUEST_PATH}");
                    TryLoadFromCache();
                }
                else
                {
                    Debug.Log($"Успешно получил ответ от запроса {API.Constants.GET_PLACES_REQUEST_PATH}: {placesText}");

                    placesResponse = placesText;

                    File.WriteAllText(API.Constants.PLACES_SAVE_PATH, placesText);
                    
                    GetRequest.Send(API.Constants.GET_COUESES_REQUEST_PATH, (coursesText, coursesIsError) =>
                    {
                        Debug.Log($"Успешно получил ответ от запроса {API.Constants.GET_COUESES_REQUEST_PATH}: {coursesText}");

                        coursesResponse = coursesText;

                        File.WriteAllText(API.Constants.COUESES_SAVE_PATH, coursesText);
                        ApplyData();
                    });
                }
            });
            yield return new WaitForSecondsRealtime(WaitingTime);
            continueButton.gameObject.SetActive(true);
        }
        
        void TryLoadFromCache()
        {
            try
            {
                if (string.IsNullOrEmpty(placesResponse))
                    placesResponse = File.ReadAllText(API.Constants.PLACES_SAVE_PATH);

                if (string.IsNullOrEmpty(coursesResponse))
                    coursesResponse = File.ReadAllText(API.Constants.COUESES_SAVE_PATH);


                ApplyData();
            }
            catch (Exception e)
            {
                Debug.LogError($"Нет сохраненных данных и нет интернета чтобы их скачать");
            }
        }
        
        void ApplyData()
        {
            if (!string.IsNullOrEmpty(placesResponse) && !string.IsNullOrEmpty(coursesResponse))
            {
                CacheArea.placesList = JsonUtility.FromJson<Wrappers.PlacesList>(placesResponse);
                CacheArea.coursesList = JsonUtility.FromJson<Wrappers.CoursesList>(coursesResponse);
            };
        }
    }
}