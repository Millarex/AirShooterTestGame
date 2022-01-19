using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData", menuName = "Create Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    [Header("Префаб игрока для игры")]
    [SerializeField]
    GameObject playerPref;
    [Header("Префаб игрока для меню")]
    [SerializeField]
    GameObject playerModelsForMenu;
    [Header("Main guns")]
    [SerializeField]
    WeaponsData[] mainGuns;
    [Header("Optional guns")]
    [SerializeField]
    WeaponsData[] optionalGuns;
    [Header("Разрешенная эпоха")]
    [SerializeField]
    int age;
    [Header("Скорость движения")]
    [SerializeField]
    int speed;
    [Header("Количество ХП")]
    [SerializeField]
    int health;
    [Header("Описание самолета (ТТХ)")]
    [SerializeField]
    string description;
    [Header("Наименование)")]
    [SerializeField]
    string planeName;
    [SerializeField]
    public WeaponsData curWeapon;
    [SerializeField]
    public WeaponsData curOptWeapon;
    [SerializeField]
    public bool open;
    [SerializeField]
    public int openCost;

    public GameObject PlayerPref
    {
        get
        {
            return playerPref;
        }
    }
    public GameObject PlayerModelsForMenu
    {
        get
        {
            return playerModelsForMenu;
        }
    }
    public WeaponsData[] MainGuns
    {
        get
        {
            return mainGuns;
        }
    }
    public WeaponsData[] OptionalGuns
    {
        get
        {
            return optionalGuns;
        }
    }
    public int Age
    {
        get
        {
            return age;
        }
    }
    public int Speed
    {
        get
        {
            return speed;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
    }
    public string PlaneName
    {
        get
        {
            return planeName;
        }
    }  
}
