using System;
using Common;
using UI.Pages.CoursesPage;
using UI.Pages.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Cards.CoursesCard
{
    public class CardButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private Button button;
        private CoursesCard cardPage;
        private CoursesPage coursesPage;
        private string title;
        private Sprite img;
        private string description;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                cardPage.Show();
                cardPage.Init(title, img, description);
                coursesPage.Hide();
            });
        }

        public void Init(string title, Sprite img, string description, CoursesCard cardPage, CoursesPage coursesPage)
        {
            this.cardPage = cardPage;
            this.coursesPage = coursesPage;
            this.title = title;
            this.img = img;
            this.description = description;

            image.sprite = img;
            nameText.text = title;
        }
    }
}