using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.Menu.Elements
{
    public class TopPanel : Page
    {
        [SerializeField] private Text label;

        public void SetLabel(string text)
        {
            label.text = text;
        }
    }
}