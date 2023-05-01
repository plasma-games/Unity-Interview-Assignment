using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnapDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float snapTime;

    private Vector3 homePosition;

    private void Start()
    {
        homePosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(SnapToHomePosition());
    }

    public void SetHomePosition(Vector3 newPosition)
    {
        homePosition = newPosition;
    }

    private IEnumerator SnapToHomePosition()
    {
        float startTime = Time.time;
        while (transform.localPosition != homePosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, homePosition, Time.time - startTime);
            yield return null;
        }
    }
}
