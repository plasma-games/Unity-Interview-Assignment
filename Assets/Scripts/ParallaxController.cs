using UnityEngine;


// There are many ways to implement a parallax effect. I used this approach due to
// its simplicity and ability to meet the needs of this project. It works by
// adjusting an object's position as the camera moves. The "closer" an object is
// to the camera (as definied by an "effectMagnitude" value closer to 0), the more
// it moves with the camera. The "farther" an object is from the camera (as defined
// by an "effectMagnitude" value closer to 1), the less it moves with the camera.
public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;

    [Tooltip("0 is closest to the camera, 1 is furthest away.")]
    [SerializeField] [Range(0, 1)] private float effectMagnitude;

    private float startingXPos;
    private float imageLength;

    void Start()
    {
        startingXPos = transform.position.x;
        imageLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Calcualte the distance the object should be from its starting position,
        // as well as the inverse value to check if the position needs to be reset.
        float distance = (sceneCamera.transform.position.x * effectMagnitude);
        float inverseDistance = (sceneCamera.transform.position.x * (1 - effectMagnitude));

        // Set the object's position according to the current position of the camera
        // and the parallax effect magnitude. 
        transform.position = new Vector3(startingXPos + distance, transform.position.y, transform.position.z);

        // Adjust the object's starting position to stay in sync with the moving camera.
        // This ensures that the object will remain in the camera's display instead of
        // sliding offscreen entirely.
        if (inverseDistance > startingXPos + imageLength)
        {
            startingXPos += imageLength;
        }
        else if (inverseDistance < startingXPos - imageLength)
        {
            startingXPos -= imageLength;
        }
    }
}
