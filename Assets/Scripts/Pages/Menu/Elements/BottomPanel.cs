using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.Menu.Elements
{
    public class BottomPanel : Page
    {
        [SerializeField] private Button[] menuButtons;
        [SerializeField] private Text[] menuText;

        [SerializeField] private Font regularFont;
        [SerializeField] private Font boldFont;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;

        public void SetActiveButton(int pos)
        {
            foreach (var menuButton in menuButtons)
            {
                var menuButtonColors = menuButton.colors;
                menuButtonColors.normalColor = inactiveColor;
            }

            foreach (var text in menuText)
            {
                text.font = regularFont;
            }

            var colorBlock = menuButtons[pos].colors;
            colorBlock.normalColor = activeColor;

            menuText[pos].font = boldFont;
        }
    }
}