using Common;
using Pages.Menu.Elements;
using UnityEngine;

namespace UI.Pages.Menu
{
    public class Menu : MonoBehaviour
    {
        public TopPanel topPanel;
        public BottomPanel bottomPanel;

        [SerializeField] private Page coursesPage;
        [SerializeField] private Page placesPage;
        [SerializeField] private Page instructionsPage;

        private const int CoursesNumber = 0;
        private const int PlacesNumber = 1;
        private const int InstructionsNumber = 2;

        private void Awake()
        {
            bottomPanel.menuButtons[CoursesNumber].onClick.AddListener(() =>
            {
                coursesPage.Show();
                bottomPanel.SetActiveButton(CoursesNumber);
            });
            
            bottomPanel.menuButtons[PlacesNumber].onClick.AddListener(() =>
            {
                placesPage.Show();
                bottomPanel.SetActiveButton(PlacesNumber);
            });
            
            bottomPanel.menuButtons[InstructionsNumber].onClick.AddListener(() =>
            {
                instructionsPage.Show();
                gameObject.SetActive(false);
            });
            
        }
    }
}