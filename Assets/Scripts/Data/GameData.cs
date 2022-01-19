using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "Create Game Data", order = 51)]
public class GameData : ScriptableObject
{
    [Header("Массив заготовок игрока")]
    [SerializeField]
    PlayerData[] players;
    [Header("Массив заготовок игрока  для спецмиссий")]
    [SerializeField]
    PlayerData[] Splayers;
    [Header("Массив всего модифицируемого оружия")]
    [SerializeField]
    WeaponsData[] weapons;
    [SerializeField]
    int money;
    [SerializeField]
    public int lives;

    //Данные для хранения в процессе игры, но удаляемые при выходе из нее    
    [NonSerialized]
    public Stack<int> history;    
    [NonSerialized]
    public int curretAgeIndex;    
    [NonSerialized]
    public int curretPlaneIndex;  
    [NonSerialized]
    public int curretSceneIndex;
    [NonSerialized]  
    public int sceneCount=0;   
    [NonSerialized]
    public PlayerData curSpecPlayer;    
    [NonSerialized]
    public PlayerData curPlayer;
    [NonSerialized]
    public bool is_spec;
    //Данные для хранения в процессе игры, но удаляемые при выходе из нее

    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    #region player    
    public List<PlayerData> GetPermittedPlanes(int age)
    {
        List<PlayerData> permittedPlanes = new List<PlayerData>();
        permittedPlanes.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].Age == age)
            {
                permittedPlanes.Add(players[i]);
            }
        }
        return permittedPlanes;
    }
    public PlayerData GetSpecPlayer(int index)
    {
        //сопоставление уровней и самолетов в спецмиссиях
        switch (index)
        {
            case 2:
                curSpecPlayer = Splayers[0];
                break;
        }
        return curSpecPlayer;
    }
    //Получить игровую заготовку для старта уровня
    public GameObject GetCurPlayerPref()
    {
        GameObject player;
        if (is_spec)
            player = curPlayer.PlayerPref;
        else
            player = curSpecPlayer.PlayerPref;    
        player.GetComponent<Player>().health = curPlayer.Health;
        player.GetComponent<PlayerInput>().speed = curPlayer.Speed;

        if (player.GetComponent<PlayerMainGun>())
            player.GetComponent<PlayerMainGun>().weapon = curPlayer.curWeapon;
        if (player.GetComponent<PlayerOptionalGun>())
            player.GetComponent<PlayerOptionalGun>().wd = curPlayer.curOptWeapon;
        return player;
    }
    #endregion 
    public void SaveData()
    {
        ResetData();
        for (int i = 0; i < players.Length; i++)
        {
            PlayerPrefs.SetString("p" + i, JsonUtility.ToJson(players[i]));
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            PlayerPrefs.SetString("w" + i, JsonUtility.ToJson(weapons[i]));
        }
        PlayerPrefs.SetString("game", JsonUtility.ToJson(this));
    }
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("game")) 
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("game"), this);
            for (int i = 0; i < players.Length; i++)
            {
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("p" + i), players[i]);
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("w" + i), weapons[i]);
            }
        }       
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

}
