using UnityEngine;

public enum CameraDirection
{
    Left = -1,
    Right = 1
}


// This simple motor class moves the camera object to enable the parallax background.
// Speed and direction can be adjusted in the inspector.
public class CameraMotor : MonoBehaviour
{

    [SerializeField] private float cameraSpeed = 5;
    [SerializeField] private CameraDirection cameraDirection = CameraDirection.Right;


    void Update()
    {
        transform.position += new Vector3 ((float)cameraDirection * cameraSpeed * Time.deltaTime, 0, 0);
    }
}
