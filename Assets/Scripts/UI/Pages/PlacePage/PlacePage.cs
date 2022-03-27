using Cache;
using Common;
using UnityEngine;

namespace UI.Pages.PlacePage
{
    public class PlacePage : Page
    {
        [SerializeField] private Menu.Menu menuPanel;
        [SerializeField] private string menuName;

        void Awake()
        {
            //CacheArea.placesList
        }

        private void OnEnable()
        {
            menuPanel.topPanel.SetLabel(menuName);
        }
    }
}