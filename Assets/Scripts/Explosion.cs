using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;
    public float force;
    public string targetTag;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag(targetTag))
            {
                if (!hitColliders[i].attachedRigidbody)
                {
                    hitColliders[i].gameObject.AddComponent<Rigidbody>();
                    hitColliders[i].attachedRigidbody.AddExplosionForce(force, transform.position, radius);
                }
                else
                    hitColliders[i].attachedRigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
