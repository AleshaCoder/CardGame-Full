using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _startPosition;
    private Vector2 _clickPosition;
    private Vector2 _movementVector;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _clickPosition = eventData.pointerCurrentRaycast.worldPosition;
        _startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _movementVector = (Vector2)eventData.pointerCurrentRaycast.worldPosition - _clickPosition;
        transform.position = _startPosition + _movementVector;
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        transform.position = _startPosition;
    }
}
