using UnityEngine;

public enum RotationDirection
{
    x,z,y,UnX,UnY, UnZ
}

public class ObjRotation : MonoBehaviour
{
    public RotationDirection rDir;
    public float rotationSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (rDir)
        {
            case RotationDirection.x:
                transform.Rotate(Vector3.right, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
                break;
            case RotationDirection.z:
                transform.Rotate(Vector3.forward, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
                break;
            case RotationDirection.y:
                transform.Rotate(Vector3.up, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
                break;
            case RotationDirection.UnX:
                transform.Rotate(Vector3.left, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
                break;
            case RotationDirection.UnZ:
                transform.Rotate(Vector3.back, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
                break;
            case RotationDirection.UnY:
                transform.Rotate(Vector3.down, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
                break;
        }
       
    }
}
