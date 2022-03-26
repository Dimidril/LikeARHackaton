using UnityEngine;
using UnityEngine.UI;

public class SliderUIElement : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnObject;
    [SerializeField] private RectTransform _contentObject;
    [SerializeField] private Button _buttonPrefab;

    private float _currentPositionValue;
    private float _previousPositionValue = 0f;

    private void Awake()
    {
        var childrenCount = _contentObject.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            var button = Instantiate(_buttonPrefab, _spawnObject);
            button.gameObject.name = "ItemButton_" + (i + 1);
        }
    }

    public void ImagePositionChanged(Vector2 value)
    {
        _currentPositionValue = value.x;
    }

    public void ImageChanged()
    {
        if(_currentPositionValue > _previousPositionValue)
        {
            //increase
        }
        else if(_currentPositionValue < _previousPositionValue)
        {
            //decrease
        }
        _previousPositionValue = _currentPositionValue;
    }
}