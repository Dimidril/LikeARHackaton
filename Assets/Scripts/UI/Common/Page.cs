using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Page : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Button backButton;
        [SerializeField] private Page[] nextPage;

        #endregion

        #region Private Fields

        [SerializeField] private static Page currentPage;
        [SerializeField] private static Page previousPage;

        #endregion

        #region Methods

        #region Unity Methods

        private void Awake()
        {
            // ReSharper disable once Unity.NoNullPropagation
            backButton?.onClick.AddListener(BackButtonAction);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonAction();
            }
        }

        #endregion
        
        public void NextPageShow()
        {
            foreach (var page in nextPage)
            {
                page.Show();
            }
        }

        #region Virtual Methods

        public virtual void Show()
        {
            previousPage = currentPage;
            currentPage = this;

            if(previousPage != null)
            {
                previousPage.Hide();
            }

            gameObject.SetActive(true);

        }

        public virtual void Hide()
        {
            previousPage = this;
            gameObject.SetActive(false);
        }

        public virtual void BackButtonActive(bool state)
        {
            backButton.gameObject.SetActive(state);
        }

        public virtual void BackButtonAction()
        {
            previousPage?.Show();
        }

        #endregion

        #endregion
    }
}