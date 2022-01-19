using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int money;
    bool is_death;

    private void Start()
    {
        is_death = false;
    }
    private void Update()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position); //Записываем положение объекта к границам камеры, X и Y это будут как раз верхние и нижние границы камеры
        if ((point.y < 0f || point.y > 1f || point.x > 1f || point.x < 0f) && is_death)
        {
            Destroy(this.gameObject);
        }

    }
    public void GetDamage(int damage)
    {
        Debug.Log("hit");
        health -= damage;
        if (health <= 0 && !is_death)
        {
            is_death = true;
            GameManager.instance.AddMoney(money);
            this.gameObject.tag = "Untagged";
            StartFall();
        }
    }
    /*private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")        
            Destruction();               
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Eath"))
            Destruction();
    }
    void StartFall()
    {        
        if (GetComponent<Death>())
            GetComponent<Death>().Fall();
        else
            Destroy(this.gameObject);
    }
    public void Destruction()
    {
        if (GetComponent<Death>())
            GetComponent<Death>().Destroy();
        else
            Destroy(this.gameObject);
    }
}



