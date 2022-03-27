using System.IO;
using API;
using API.Helpers;
using Cache;
using Common;
using UI.Cards.CoursesCard;
using UnityEngine;
using Utils;

namespace UI.Pages.CoursesPage
{
    public class CoursesPage : Page
    {
        [SerializeField] private Menu.Menu menuPanel;
        [SerializeField] private string menuName;
        [SerializeField] private CardButton cardPrefab;
        [SerializeField] private RectTransform parent;
        [SerializeField] private CoursesPage coursesPage;
        [SerializeField] private CoursesCard coursesCard;

        void Awake()
        {
            foreach (var coursesListCourse in CacheArea.coursesList.courses)
            {
                var card = Instantiate(cardPrefab, parent);
                card.name = "Course Card " + coursesListCourse.name;
                Texture2D img = Texture2D.blackTexture;
                TextureLoader.Load(Constants.BASE_URL+'/' +coursesListCourse.image, texture2D =>
                {
                    img = texture2D;
                    card.Init(coursesListCourse.name, SpriteCreator.Create(img), coursesListCourse.description, coursesCard, coursesPage);
                });
            }
        }

        private void OnEnable()
        {
            menuPanel.topPanel.SetLabel(menuName);
        }
    }
}