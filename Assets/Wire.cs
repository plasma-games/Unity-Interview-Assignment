using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition; 

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag() {
        // get the mouse position to world point
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // check for nearby connection point
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders) {
            //make sure its not my own collider
            if (collider.gameObject != gameObject) {
                // update wire to the connection point position
                UpdateWire(collider.transform.position);

                // check if wires are the same color
                if (transform.parent.name.Equals(collider.transform.parent.name)){
                    // count connection
                    VictoryConditions.Instance.WireConnected(1);

                    // finish the step
                    collider.GetComponent<Wire>()?.Done();
                    Done();
                }
                return;
            }
        }

        UpdateWire(newPosition);
    }

    void Done() {
        // turn on light
        lightOn.SetActive(true);

        // destroy the script
        Destroy(this);
    }

    private void OnMouseUp() {
        // reset wire poistion
        UpdateWire(startPosition);
        
    }

    void UpdateWire(Vector3 newPosition) {
        // update wire object ("MovingParts" game object)
        // update position to where the mouse is
        transform.position = newPosition;

        // update direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        // update scale // this is what is broken for stretching
        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
