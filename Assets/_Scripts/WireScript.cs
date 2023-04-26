using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wireEnd; //serializeField makes private variables visible in the Inspector
    private Vector3 startPoint, startPosition;
    private float startWidth;

    private void Start()
    {
        //get the initial position of the wire
        startPoint = transform.parent.position;
        startPosition = transform.position;
        startWidth = wireEnd.size.x;
    }

    private void OnMouseDrag()
    {
        //convert the mouse position to world coordinates
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        //if no nearby connection points are found, update the wire position
        UpdateWire(newPosition);
    }

    private void OnMouseUp()
    {
        //check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f);
        foreach (Collider2D collider in colliders)
        {
            //make sure it's not the same collider
            if (collider.gameObject.tag == "Socket")
            {
                //update the wire and connect the two wires if they match
                UpdateWire(collider.transform.position);
                // if (answer wire connects to question wire) for scoring purposes
                // {
                // }
                //prevents socket from being used again
                Destroy(collider);
                //prevents wire from being dragged again
                Destroy(this);
                return;
            }
        }
        //reset wire position when mouse button is released
        UpdateWire(startPosition);
    }

    private void UpdateWire(Vector3 newPosition)
    {
        //update the wire position
        transform.position = newPosition;

        //update the wire direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        //update the wire length
        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
        if (newPosition == startPosition)
            wireEnd.size = new Vector2(startWidth, wireEnd.size.y);
    }
}