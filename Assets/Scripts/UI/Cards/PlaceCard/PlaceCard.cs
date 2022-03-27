using Common;
using UI.Pages.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Cards.PlaceCard
{
    public class PlaceCard : Page
    {
        [SerializeField] private Menu menuPanel;
        [SerializeField] private Image image;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Button openARButton;

        private void Awake()
        {
            openARButton.onClick.AddListener(() => SceneManager.LoadScene("ARGuide"));
        }

        private void OnEnable()
        {
            menuPanel.topPanel.BackButtonActive(true);
        }

        public void Init(string title, string description)
        {
            menuPanel.topPanel.SetLabel(title);
            descriptionText.text = description;
        }
    }
}