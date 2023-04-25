using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wireEnd; //serializeField makes private variables visible in the Inspector
    private Vector3 startPoint;
    private Vector3 startPosition;
    private bool isConnected;

    private void Start()
    {
        //get the initial position of the wire
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        //convert the mouse position to world coordinates
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        if (!isConnected)
        {
            //check for nearby connection points
            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                //make sure it's not the same collider
                if (collider.gameObject != gameObject)
                {
                    //update the wire and connect the two wires if they match
                    UpdateWire(collider.transform.position);
                    // if (answer wire connects to question wire) for scoring purposes
                    // {
                         collider.GetComponent<WireScript>()?.Done();
                         Done();
                    // }
                    return;
                }
            }

            //if no nearby connection points are found, update the wire position
            UpdateWire(newPosition);
        }
    }

    private void OnMouseUp()
    {
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
    }

    private void Done()
    {
        //destroy the script once the wire is connected
        Destroy(this);
    }
}