using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int damage = 1;

    [SerializeField]
    bool is_homingMissiles;
    [SerializeField]
    float speed;
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    float deathTime;

    GameObject target;

    private void Start()
    {
        StartCoroutine(DeathTimer(deathTime));
        if (is_homingMissiles)
            target = GameObject.FindGameObjectWithTag("Enemy");
    }
    private void FixedUpdate()
    {
        if (is_homingMissiles && target)
        {
            Vector3 direction = Vector3.zero;
            if (target)
                direction = (target.transform.position - transform.position).normalized;
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator DeathTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.CompareTag("Enemy Bullet"))
        {
            other.GetComponent<Player>().GetDamage(damage);
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy" && this.CompareTag("Player Bullet"))
        {
            other.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
        else if (other.tag == "Static object")
            Destroy(gameObject);
    }
}
