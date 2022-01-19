using System.Collections;
using UnityEngine;

public class EnemyMainGun : MonoBehaviour
{
    [Header("Точка выстрела/Положение оружия")]
    [SerializeField]
    private Weapon[] weapons;
    [SerializeField]
    float randomBulletSpeed = 5;

    public WeaponsData weapon;

    GameObject tempBul;
    Vector3 direction;
    Rigidbody rd;

    bool is_Shoot;

    public void Start()
    {
        is_Shoot = false;      
    }
    public void Update()
    {
        if (this.transform.position.y>=29)
        {            
            if (!is_Shoot)
            {
                foreach (Weapon item in weapons)
                {
                    StartCoroutine(EnemySchoot(weapon.BulletPref, item.gunPositions, weapon));
                }
                is_Shoot = true;
            }           
        }
        else
        {
            if (is_Shoot)
            {
                StopAllCoroutines();
                is_Shoot = false;
            }           
        }
            
    }
    //Метод выстрела
    IEnumerator EnemySchoot(GameObject bulletPref, GameObject gunPosition, WeaponsData gun)
    {
        while (true)
        {           
            if (gun.Endless)
            {
                Shot(bulletPref, gunPosition);
                yield return new WaitForSeconds(gun.FireRate);
            }
            else
            {
                for (int i = 0; i < gun.Holder; i++)
                {
                    Shot(bulletPref, gunPosition);
                    yield return new WaitForSeconds(gun.FireRate);
                }
                yield return new WaitForSeconds(gun.Reloading);
            }
        }
    }
    private void Shot(GameObject bulletPref, GameObject gunPosition)
    {
        Vector3 pos = new Vector3(gunPosition.transform.position.x, this.transform.position.y, gunPosition.transform.position.z);
        float disp = Random.Range(-weapon.Dispersion, weapon.Dispersion);
        direction = new Vector3(disp + transform.forward.x, transform.forward.y, transform.forward.z);
        tempBul = Instantiate(bulletPref, pos, Quaternion.identity);
        rd = tempBul.GetComponent<Rigidbody>();
        rd.AddForce(direction * Random.Range(weapon.BulletSpeed - randomBulletSpeed, weapon.BulletSpeed + randomBulletSpeed), ForceMode.Impulse);
        tempBul.transform.rotation = Quaternion.LookRotation(direction);
    }
}
