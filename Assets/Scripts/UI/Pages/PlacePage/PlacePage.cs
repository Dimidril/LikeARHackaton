using API;
using API.Helpers;
using Cache;
using Common;
using UI.Cards.PlaceCard;
using UnityEngine;
using Utils;

namespace UI.Pages.PlacePage
{
    public class PlacePage : Page
    {
        [SerializeField] private Menu.Menu menuPanel;
        [SerializeField] private string menuName;
        [SerializeField] private PlaceCardButton cardPrefab;
        [SerializeField] private RectTransform parent;
        [SerializeField] private PlacePage coursesPage;
        [SerializeField] private PlaceCard coursesCard;

        void Awake()
        {
            foreach (var placesListPlace in CacheArea.placesList.places)
            {
                var card = Instantiate(cardPrefab, parent);
                card.name = "Course Card " + placesListPlace.name;
                Texture2D img = Texture2D.blackTexture;
                AudioClip clip;
                AudioClipLoader.Load(Constants.BASE_URL + '/' + placesListPlace.audio, CacheDestination.AudioFolderPath,
                    s =>
                    {
                        
                    });
                card.Init(placesListPlace.name, placesListPlace.descruption, placesListPlace.audioText, coursesCard, coursesPage, placesListPlace.models);
            }
        }

        private void OnEnable()
        {
            menuPanel.topPanel.SetLabel(menuName);
        }
    }
}