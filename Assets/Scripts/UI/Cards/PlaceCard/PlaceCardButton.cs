using API;
using UI.Pages.PlacePage;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Cards.PlaceCard
{
    public class PlaceCardButton : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Button button;
        private PlaceCard placeCard;
        private PlacePage placePage;
        private string title;
        private string audioText;
        private string description;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                placeCard.Show();
                placeCard.Init(title, description);
                placePage.Hide();
            });
        }

        public void Init(string title, string description, string audioText, PlaceCard placeCard, PlacePage placePage, Wrappers.ARObject[] models)
        {
            this.title = title;
            this.description = description;
            this.audioText = audioText;

            this.placeCard = placeCard;
            this.placePage = placePage;
            
            nameText.text = title;
        }
    }
}