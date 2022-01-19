using UnityEngine;

public class Player : MonoBehaviour
{   
    public static Player instance;
    public int health;
    public int immortalTime = 5;
    public bool in_game;
    [SerializeField]
    private GameEvent OnPlayerDead;

    private void Awake()
    {
       if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void GetDamage(int damage)
    {
        GameManager.instance.PlayerDamage();
        health -= damage;
        if (health <= 0)
            StartFall();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (in_game)                    
            if (collision.CompareTag("Enemy") || collision.CompareTag ("Static object"))
            {
                if (collision.GetComponent<Enemy>())
                    collision.GetComponent<Enemy>().Destruction();
                Destruction();
            }
                
    }
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
    void Destruction()
    {
        if (GetComponent<Death>())
            GetComponent<Death>().Destroy();
        else
            Destroy(this.gameObject);
    }
    private void OnDestroy()
    {        
        OnPlayerDead.Raise();        
    }
}
