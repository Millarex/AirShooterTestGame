using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField]
    private GameEvent OnTargetDead;
    public float speed;
    public float rotSpeed;

    Vector3 direction;
    Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }
    void Update()
    {
        if (target)
        {
            direction = (target.transform.position - transform.position).normalized;
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotSpeed);
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
            Destroy(this.gameObject);  
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            OnTargetDead.Raise();
            Destroy(this.gameObject);
        }     
    }
}
