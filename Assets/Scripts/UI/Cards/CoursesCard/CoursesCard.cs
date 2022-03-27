using System;
using Common;
using Pages.Menu;
using UI.Pages.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Cards.CoursesCard
{
    public class CoursesCard : Page
    {
        [SerializeField] private Menu menuPanel;
        [SerializeField] private Image image;
        [SerializeField] private Text descriptionText;

        private void OnEnable()
        {
            menuPanel.topPanel.BackButtonActive(true);
        }

        public void Init(string title, Sprite img, string description)
        {
            menuPanel.topPanel.SetLabel(title);
            image.sprite = img;
            descriptionText.text = description;
        }
    }
}