using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnergyOrb : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float snapTime;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Image orbImage;

    public bool isCorrect { get; private set; }

    private Transform originalParent;
    private Vector3 originalPosition;
    private Vector3 homePosition;
    private string expectedAnswer;
    private Collider2D lastTouchedCollider;
    private bool canDrag;

    public void Initialize(Color questionColor, string _expectedAnswer)
    {
        originalParent = transform.parent;
        originalPosition = homePosition = transform.localPosition;
        expectedAnswer = _expectedAnswer;
        orbImage.color = questionColor;
        canDrag = true;
        isCorrect = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (lastTouchedCollider != null && circleCollider.IsTouching(lastTouchedCollider))
        {
            QuestionOutput selectedOutput = lastTouchedCollider.GetComponentInParent<QuestionOutput>();

            if (selectedOutput != null)
            {
                transform.SetParent(lastTouchedCollider.transform.parent, true);
                homePosition = lastTouchedCollider.transform.localPosition;
                selectedOutput.SetColor(orbImage.color);
                lastTouchedCollider.isTrigger = false;
                canDrag = false;
                isCorrect = selectedOutput.answer.Equals(expectedAnswer);
            }
        }

        StartCoroutine(SnapToHomePosition());
    }

    public void ResetOrb()
    {
        if (transform.parent == originalParent) return;

        Debug.Log(homePosition + ":" + originalPosition);
        transform.SetParent(originalParent, true);
        homePosition = originalPosition;
        canDrag = true;
        isCorrect = false;
        lastTouchedCollider.isTrigger = true;
        StartCoroutine(SnapToHomePosition());
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        lastTouchedCollider = other;
    }
}

