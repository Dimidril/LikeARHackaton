using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Plugins.ARFoundationPlaceModelOnGround.Scripts.AR.UI
{
    public class ArTipPanel : MonoBehaviour
    {
        [SerializeField] [TextArea] string findPlaneTip = "Медленно двигайте устройство чтобы разместить AR объект";

        [SerializeField] [TextArea] string tapTip = "Коснитесь, чтобы разместить AR объект";

        [SerializeField] [TextArea] string editorTapTip = "Нажмите ЛКМ, чтобы разместить AR объект";

        [SerializeField] Text arTipText;
        [SerializeField] ArTapToPlaceObject arTapToPlaceObject;
        [SerializeField] VideoClip findPlaneVideoClip;
        [SerializeField] VideoClip tapVideoClip;
        [SerializeField] VideoPlayer videoPlayer;

        void OnEnable()
        {
            ArTapToPlaceObject.OnObjectPlaced += OnObjectPlaced;
            ArTapToPlaceObject.OnPlaneFinded += OnPlaneFinded;
            ArTapToPlaceObject.OnPlaneLosed += OnPlaneLosed;

            arTapToPlaceObject.Reset();
        }

        void OnDisable()
        {
            ArTapToPlaceObject.OnObjectPlaced -= OnObjectPlaced;
            ArTapToPlaceObject.OnPlaneFinded -= OnPlaneFinded;
            ArTapToPlaceObject.OnPlaneLosed -= OnPlaneLosed;
        }

        void OnPlaneFinded()
        {
            videoPlayer.clip = tapVideoClip;
            videoPlayer.Play();

            arTipText.text = tapTip;
        }

        void OnPlaneLosed()
        {
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();

            arTipText.gameObject.SetActive(true);

#if UNITY_EDITOR
            arTipText.text = editorTapTip;
            videoPlayer.clip = tapVideoClip;
            return;
#endif

            arTipText.text = findPlaneTip;
            videoPlayer.clip = findPlaneVideoClip;
        }

        void OnObjectPlaced()
        {
            arTipText.gameObject.SetActive(false);
            videoPlayer.gameObject.SetActive(false);
        }
    }
}