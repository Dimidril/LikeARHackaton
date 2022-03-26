using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.InstructionsPage
{
    public class InstructionsPage : Page
    {
        [SerializeField] private Button continueButton;
        private const float WaitingTime = 2f;

        private void Awake()
        {
            continueButton.onClick.AddListener(() =>
            {
                NextPageShow();
                Hide();
            });
        }
    }
}