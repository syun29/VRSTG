using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private Parameta _status;
    private Vector3 _offset;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

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

    public void Setup(Vector2 size,Vector3 offset)
    {
        _offset = offset;
        _rectTransform.sizeDelta = size;
    }

    private void Refresh()
    {
        if (_status.m_HpMax <= 0) return;
        fillImage.fillAmount = Mathf.Clamp01((float)_status.m_Hp / _status.m_HpMax);

        transform.position = _status.transform.position + _offset;
        transform.rotation = Quaternion.LookRotation(-(transform.position - _camera.transform.position));

        //var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        //Vector2 localPoint;

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);
        //transform.localPosition = localPoint + new Vector2(0, 80);
    }
}
