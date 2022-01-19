using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Gun", menuName = "Create Gun", order = 51)]
public class WeaponsData : ScriptableObject
{
    [Header("Урон")]
    [Range(1, 10)]
    [SerializeField]
    private int bulletDamage;

    [Header("Префаб пули")]
    [SerializeField]
    private GameObject bulletPref;

    [Header("скорость полета пули")]
    [Range(10,150)]
    [SerializeField]
    private float bulletSpeed;

    [Header("Тип оружия")]
    [SerializeField]
    private WeaponType wType;

    [Header("Разброс от -х до х в обе стороны")]
    [Range(0, 0.5f)]
    [SerializeField]
    private float dispersion;

    [Header("Темп стрельбы")]
    [Range(0, 1)]
    [SerializeField]
    private float fireRate;

    [Header("Скорость перезарядки")]
    [Range(0, 5)]
    [SerializeField]
    private float reloading;

    [Header("Длина ленты")]
    [SerializeField]
    private int holder;

    [Header("Бесконечные патроны без перезарядки (для врага)")]    
    [SerializeField]
    private bool endless;

    [Header("Основное оружие или опциональное")]
    [SerializeField]
    private bool optional;

    [Header("Открыто ли оружие")]
    [SerializeField]
    private bool open;

    [Header("Цена открытия")]
    [SerializeField]
    private int openCost;

    [Header("Уровень апгрейда")]
    [SerializeField]
    private int upgradeLvl;

    [Header("Цена апгрейда")]
    [SerializeField]
    private int upgradeCost;

    [Header("Описание оружия")]
    [SerializeField]
    private string description;

    [Header("Наименование оружия")]
    [SerializeField]
    private string weaponName;

    public bool modifiable;
   
    public void UpgradeWeapon()
    {
        //
        upgradeLvl++;
        UpgradeCost *= 2;
    }

    public float Dispersion
    {
        get
        {
            return dispersion;
        }
    }
    public WeaponType WType
    {
        get
        {
            return wType;
        }
    }
    public int BulletDamage
    {
        get
        {
            return bulletDamage;
        }
    }
    public int OpenCost
    {
        get
        {
            return openCost;
        }
    }
    public int UpgradeCost
    {
        get
        {
            return upgradeCost;
        }
        set
        {
            upgradeCost = value;
        }
    }
    public int UpgradeLvl
    {
        get
        {
            return upgradeLvl;
        }
        set
        {
            upgradeLvl = value;
        }
    }
    public GameObject BulletPref
    {
        get
        {
            if (bulletPref.GetComponent<Bullet>())
                bulletPref.GetComponent<Bullet>().damage = bulletDamage;
            if (bulletPref.GetComponent<Shell>())
                bulletPref.GetComponent<Shell>().damage = bulletDamage;
            if (bulletPref.GetComponent<Rocket>())
                bulletPref.GetComponent<Rocket>().damage = bulletDamage;
            return bulletPref;
        }
    }
    public bool Endless
    {
        get
        {
            return endless;
        }
    } 
    public bool Open
    {
        get
        {
            return open;
        }
        set
        {
            open = value;
        }
    }
    public bool Optional
    {
        get
        {
            return optional;
        }
    }
    public float BulletSpeed
    {
        get
        {
            return bulletSpeed;
        }
    }    
        
    public float FireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }
    public float Reloading
    {
        get
        {
            return reloading;
        }
        set
        {
            reloading = value;
        }
    }
    public int Holder
    {
        get
        {
            return holder;
        }
        set
        {
            holder = value;
        }
    }   
    public string Description
    {
        get
        {
            return description;
        }
    }
    public string WeaponName
    {
        get
        {
            return weaponName;
        }
    }

}
