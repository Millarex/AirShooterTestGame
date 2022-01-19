using System.Collections;
using UnityEngine;

public class PlayerAutoTurret : MonoBehaviour
{
    public float searchRange;
    public float fireRange;
    public float rotSpeed;
    public bool forwardTurret;

    [Header("Точка выстрела/Положение оружия")]
    [SerializeField]
    Weapon weaponPos;
    [SerializeField]
    float randomBulletSpeed = 5;
    [SerializeField]
    WeaponsData weapon;

    GameObject tempBul;
    Vector3 bulletDirection;
    Rigidbody rd;

    GameObject target;
    Vector3 direction;
    Vector3 defDir;
    bool coroutineStart;

    private void Start()
    {
      
        if (forwardTurret)
            defDir =Vector3.forward;
        else
            defDir = Vector3.back;       
    }
    void FixedUpdate()
    {
        if (Search())
            Destroy();
        else
            Search();
    }

    bool Search()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                direction = (hitCollider.gameObject.transform.position - transform.position).normalized;
                if (forwardTurret)
                {
                    if (direction.z >= 0)
                    {
                        target = hitCollider.gameObject;
                        return true;
                    }
                }
                else
                {
                    if (direction.z <= 0)
                    {
                        target = hitCollider.gameObject;
                        return true;
                    }
                }
            }
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(defDir), rotSpeed/2 * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward * fireRange, Color.red);
        StopAllCoroutines();
        coroutineStart = false;
        return false;
    }
    void Destroy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, fireRange))
        {
            if (!coroutineStart)
                StartCoroutine(Schoot(weapon.BulletPref, weaponPos.gunPositions, weapon));

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            Debug.DrawRay(transform.position, transform.forward * fireRange, Color.red);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, searchRange);       
    }
    IEnumerator Schoot(GameObject bulletPref, GameObject gunPosition, WeaponsData gun)
    {
        coroutineStart = true;
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
        bulletDirection = new Vector3(disp + transform.forward.x, transform.forward.y, transform.forward.z);
        tempBul = Instantiate(bulletPref, pos, Quaternion.identity);
        rd = tempBul.GetComponent<Rigidbody>();
        rd.AddForce(bulletDirection * Random.Range(weapon.BulletSpeed - randomBulletSpeed, weapon.BulletSpeed + randomBulletSpeed), ForceMode.Impulse);
        tempBul.transform.rotation = Quaternion.LookRotation(bulletDirection);
    }
}
