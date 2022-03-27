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
        [SerializeField] private CoursesCard cardPrefab;
        [SerializeField] private RectTransform parent;

        void Awake()
        {
            foreach (var coursesListCourse in CacheArea.coursesList.courses)
            {
                var card = Instantiate(cardPrefab, parent);
                card.name = "Course Card " + coursesListCourse.name;
                Texture2D img = Texture2D.blackTexture;
                TextureLoader.Load(Path.Combine(Constants.BASE_URL, coursesListCourse.imagePath), texture2D => img = texture2D);
                card.Init(coursesListCourse.name, SpriteCreator.Create(img), coursesListCourse.description);
            }
        }

        private void OnEnable()
        {
            menuPanel.topPanel.SetLabel(menuName);
        }
    }
}