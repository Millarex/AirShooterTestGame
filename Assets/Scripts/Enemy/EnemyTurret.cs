using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public float _speedRotate = 5f;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _speedRotate);
        }
    }

}
