using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private Parameta _status;

    private void Update()
    {
        Refresh();
    }

    public void Initialize(RectTransform parentRectTransform, Camera camera, Parameta status)
    {
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
        Refresh();
    }

    private void Refresh()
    {
        fillImage.fillAmount = _status.m_Hp / _status.m_HpMax;

        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 80);
    }
}
