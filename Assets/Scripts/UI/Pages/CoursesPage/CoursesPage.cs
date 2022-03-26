using Common;
using UnityEngine;

namespace UI.Pages.CoursesPage
{
    public class CoursesPage : Page
    {
        [SerializeField] private Menu.Menu menuPanel;
        [SerializeField] private string menuName;

        private void OnEnable()
        {
            menuPanel.topPanel.SetLabel(menuName);
        }
    }
}