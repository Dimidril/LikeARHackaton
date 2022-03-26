using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private RectTransform _panelSafeArea;
    private Rect _currentSafeArea = new Rect();
    private ScreenOrientation _currentOrientation = ScreenOrientation.AutoRotation;

    void Start()
    {
        _panelSafeArea = GetComponent<RectTransform>();

        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        if (_panelSafeArea == null)
            return;

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;

        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;

        _panelSafeArea.anchorMin = anchorMin;
        _panelSafeArea.anchorMax = anchorMax;
    }

    private void Update()
    {
        if((_currentOrientation != Screen.orientation) || (_currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}