using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Page : MonoBehaviour
    {
        public Button backButton;
        public Page[] nextPage;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public void NextPageShow()
        {
            foreach (var page in nextPage)
            {
                page.Show();
            }
        }
    }
}