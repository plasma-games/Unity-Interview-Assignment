using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// The EnergyOrb is the main interactable in this scene. The player drags it
// from its input to the corresponding output. Once it has been slotted into an output
// it is no longer draggable, but the player can reset the orbs using the Reset button.
// If the orb is dropped anywhere other than on an output, it snaps back to its
// starting position.
public class EnergyOrb : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip connectSound;
    [SerializeField] private float snapTime;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Image orbImage;

    public bool isCorrect { get; private set; }

    private SoundManager soundManager;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Vector3 homePosition;
    private string expectedAnswer;
    private Collider2D lastTouchedCollider;
    private bool canDrag;

    public void Initialize(Color questionColor, string _expectedAnswer, SoundManager _soundManager)
    {
        soundManager = _soundManager;
        originalParent = transform.parent;
        originalPosition = homePosition = transform.localPosition;
        expectedAnswer = _expectedAnswer;
        orbImage.color = questionColor;
        canDrag = true;
        isCorrect = false;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        soundManager.PlayClip(pickupSound);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        // Only add the orb to an output if the player stops dragging it while
        // it overlaps with an output
        if (lastTouchedCollider != null && circleCollider.IsTouching(lastTouchedCollider))
        {
            QuestionOutput selectedOutput = lastTouchedCollider.GetComponentInParent<QuestionOutput>();

            // If the orb was dropped on an output...
            if (selectedOutput != null)
            {
                // ... make the orb a child of the output, update the output's color
                // to match the orb, turn off the output's trigger to prevent additional
                // orbs from interacting with it, disable the orbs draggability,
                // check to see if the orb was correctly placed, and play
                // the appropriate sound effect.
                transform.SetParent(lastTouchedCollider.transform.parent, true);
                homePosition = lastTouchedCollider.transform.localPosition;
                selectedOutput.SetColor(orbImage.color);
                lastTouchedCollider.isTrigger = false;
                canDrag = false;
                isCorrect = selectedOutput.answer.Equals(expectedAnswer);
                soundManager.PlayClip(connectSound);
            }
        }
        else
        {
            soundManager.PlayClip(dropSound);
        }

        // Anytime an orb is dropped, snap it to its home position (either its
        // starting input position or the output position where it was dropped)
        StartCoroutine(SnapToHomePosition());
    }

    public void ResetOrb()
    {
        // No need to reset an orb that is still in its starting position
        if (transform.parent == originalParent) return;

        // To reset an orb, re-parent it to its input, set its home position back
        // to its starting position, re-enable dragging, mark it as incorrect,
        // and re-enable the trigger for the output where it was previously placed.
        // Finally, play the sound effect and move it back to its starting position.
        transform.SetParent(originalParent, true);
        homePosition = originalPosition;
        canDrag = true;
        isCorrect = false;
        lastTouchedCollider.isTrigger = true;

        soundManager.PlayClip(dropSound);
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
        // Keep track of which output collider the orb has collided with most recently
        lastTouchedCollider = other;
    }
}

