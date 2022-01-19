using System.Collections;
using UnityEngine;

public class PlayerMainGun : MonoBehaviour
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

    public void Start()
    {       
        //Параметры для пули
        foreach (Weapon item in weapons)
        {
            StartCoroutine(MakeAShot(weapon.BulletPref, item.gunPositions,item.gunHeatings, weapon));
        }        
    }

    //Метод выстрела
    IEnumerator MakeAShot(GameObject bulletPref, GameObject gunPosition, GameObject gunHeating, WeaponsData gun)
    {
        while (true)
        {
            float alfa = (float)1 / gun.Holder;
            float temp = 0;
            for (int i = 0; i < gun.Holder; i++)
            {                  
                //Shot
                Vector3 pos = new Vector3(gunPosition.transform.position.x, this.transform.position.y, gunPosition.transform.position.z);
                tempBul = Instantiate(bulletPref, pos, Quaternion.identity);
                rd = tempBul.GetComponent<Rigidbody>();                
                float disp = Random.Range(-weapon.Dispersion, weapon.Dispersion);
                direction = new Vector3(disp + transform.forward.x, transform.forward.y, transform.forward.z);
                rd.AddForce(direction * Random.Range(weapon.BulletSpeed - randomBulletSpeed, weapon.BulletSpeed + randomBulletSpeed), ForceMode.Impulse);
                tempBul.transform.rotation = Quaternion.LookRotation(direction);

                //Heating
                temp += alfa;
                if (temp > 1)
                    temp = 1;
                Color color = new Color(1, 0, 0, temp);
                gunHeating.GetComponent<Renderer>().material.color = color;
                yield return new WaitForSeconds(gun.FireRate);
            }
            yield return new WaitForSeconds(gun.Reloading / 4);
            gunHeating.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.75f);
            yield return new WaitForSeconds(gun.Reloading / 4);
            gunHeating.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(gun.Reloading / 4);
            gunHeating.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.25f);
            yield return new WaitForSeconds(gun.Reloading / 4);
            gunHeating.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        }
    }
}
