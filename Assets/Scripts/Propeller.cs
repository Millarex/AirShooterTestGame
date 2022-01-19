using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {       
        transform.Rotate(Vector3.forward, Mathf.SmoothStep(0, 360, Time.deltaTime * rotationSpeed));
    }
}
