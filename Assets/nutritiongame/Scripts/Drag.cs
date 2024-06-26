using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _image;
    private Vector3 _position;
    private Transform _parent;

    public Transform StartingParent { get => _parent; }

    public static Action Dragging;

    private void Awake() => Plate.Clear += ResetParent;

    private void OnDestroy() => Plate.Clear -= ResetParent;

    private void ResetParent() => transform.SetParent(_parent);

    private void Start()
    {
        _image = GetComponent<Image>();
        _position = transform.position;
        _parent = transform.parent;
    }

    private void SetAplpha(float a)
    {
        Color color = _image.color;
        color.a = a;
        _image.color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Dragging?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetAplpha(0.5f);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
        if (transform.parent != _parent)
        {
            transform.parent.GetComponent<Drop>().EmptySlot();
            Plate.Unfill?.Invoke();
        }

        ResetParent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetAplpha(1f);
        _image.raycastTarget = true;
    }
}
