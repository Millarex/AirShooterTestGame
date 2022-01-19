using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField]
    float minVelocity = 3;
    [SerializeField]
    float compression = 5;

    public int damage = 1;

    Rigidbody rd;
    float velocity;
    Collider col;
    bool corStart;
    float defPos;

    private void Start()
    {
        col = this.GetComponent<Collider>();
        rd = this.GetComponent<Rigidbody>();
        corStart = false;
        defPos = transform.position.z;
    }
    private void FixedUpdate()
    {
        //получаем текущее ускорение пули
        velocity = rd.velocity.magnitude;

        //исходя из замедления пули убираем убойный эффект и симулируем гравитацию для ее падения
        if (velocity <= minVelocity && !corStart)
            if (this.transform.position.z - defPos > 1 || this.transform.position.z - defPos < -1)
                StartCoroutine(Squeeze());
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator Squeeze()
    {
        Destroy(col);
        corStart = true;
        while (transform.localScale.z > 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Lerp(transform.localScale.z, 0, compression * Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }
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
        else if (other.tag == "Static object" )
            Destroy(gameObject);
    }
}
