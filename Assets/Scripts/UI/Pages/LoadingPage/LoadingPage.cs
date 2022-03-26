using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.LoadingPage
{
    public class LoadingPage : Page
    {
        [SerializeField] private Button continueButton;
        private const float WaitingTime = 1f;

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

        private IEnumerator LoadingCoroutine()
        {
            yield return new WaitForSecondsRealtime(WaitingTime);
            continueButton.gameObject.SetActive(true);
        }
    }
}