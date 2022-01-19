using UnityEngine;

public class ObjMoving : MonoBehaviour
{  
    public float speed;
    [Header("По оси Z или против")]
    public bool forward;
    
    void FixedUpdate()
    {
        if (forward)
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (this.transform.position.z <= -45)
            Destroy(this.gameObject);
    }
}
