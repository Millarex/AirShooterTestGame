using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("Массив всех экранов меню")]
    [SerializeField]
    GameObject[] panels; 
    [Header("Money")]
    [SerializeField]
    Text moneyText;   
    [Header("Данные игры")]
    [SerializeField]
    GameData gd;
    //
    public int curretAgeIndex;    
    public int curretPlaneIndex;      
    public int curretSceneIndex;
    public PlayerData curSpecPlayer;
    public PlayerData curPlayer;
    public WeaponsData curWeapon;
    public WeaponsData curOptWeapon;
    //

    Stack<int> history = new Stack<int>();

    private void Awake()
    {
        if (instance == null) instance = this;      
        if (Application.platform == RuntimePlatform.Android)
            gd.LoadData();        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;        
        Time.timeScale = 1;
    }
    private void Start()
    {
        if (gd.sceneCount > 0) //Мы внутри игры
        {
            panels[0].SetActive(false);
            //Menu load
            history = gd.history;
            curretAgeIndex = gd.curretAgeIndex;
            curretPlaneIndex = gd.curretPlaneIndex;           
            curretSceneIndex = gd.curretSceneIndex;          
            //Player load
            curSpecPlayer = gd.curSpecPlayer;
            curPlayer = gd.curPlayer;            
            //Weapons load
            curWeapon = curPlayer.curWeapon;
            curOptWeapon = curPlayer.curOptWeapon;

            LoadPanel(history.Pop());
        }
        else //Игра только что запущена
        {
            //Menu load
            history.Clear();
            history.Push(0);
            curretAgeIndex = 0;
            curretPlaneIndex = 0;
            curretSceneIndex = 0;
            //Player load
            curSpecPlayer = null;
            curPlayer = null;
            //Weapons load
            curWeapon = null;
            curOptWeapon = null;
            LoadPanel(history.Peek());
        }
        gd.sceneCount++;
    }
    void Update()
    {
        moneyText.text = gd.Money.ToString() + "$";
        //если используется системная кнопка на андроиде назад то вызываем мой метод назад
        if (Input.GetKeyDown(KeyCode.Escape))        
            Btn_Back();       
    }
    #region Buttons
    public void LoadPanel(int index)
    {
        panels[history.Peek()].SetActive(false);
        panels[index].SetActive(true);
        history.Push(index);
    }
    public void Btn_Back()
    {
        panels[history.Pop()].SetActive(false);
        panels[history.Peek()].SetActive(true);
    }     
    public void Btn_SelectLvl(int index)
    {        
        gd.is_spec = true;
        curretSceneIndex = index;        
        gd.lives = 3;
        LoadPanel(5);
    }    
    public void Btn_SelectSpecLvl(int index)
    {        
        gd.is_spec = false;
        curSpecPlayer = gd.GetSpecPlayer(index);
        curretSceneIndex = index;
        gd.lives = 2;        
        LoadPanel(6);
    }    
    public void StartLvl()
    {
        Save();        
        SceneManager.LoadScene(curretSceneIndex);
    }
    #endregion
    public void Save()
    {
        //Menu
        gd.history = history;
        gd.curretAgeIndex = curretAgeIndex;
        gd.curretPlaneIndex = curretPlaneIndex;
        gd.curretSceneIndex = curretSceneIndex;
        //Player
        gd.curPlayer = curPlayer;
        gd.curSpecPlayer = curSpecPlayer;
        //Weapons
        if (curWeapon==null || curOptWeapon==null)
        {
            curWeapon = curPlayer.MainGuns[0];
            curOptWeapon = curPlayer.OptionalGuns[0];
            gd.curPlayer.curWeapon = curWeapon;
            gd.curPlayer.curOptWeapon = curOptWeapon;
        }
        else
        {
            gd.curPlayer.curWeapon = curWeapon;
            gd.curPlayer.curOptWeapon = curOptWeapon;
        }       

        gd.SaveData();
    }
    public bool Buy(int value)
    {
        if (gd.Money - value >= 0)
        {
            gd.Money -= value;
            return true;
        }
        else
            return false;            
    }
    public List<PlayerData> GetPlanes()
    {
        return gd.GetPermittedPlanes(curretAgeIndex);
    }
    public WeaponsData[] GetMainWeapon()
    {
        return curPlayer.MainGuns;
    }
    public WeaponsData[] GetSecondaryWeapon()
    {
        return curPlayer.OptionalGuns;
    }
    //Debug
    public void DeleteGameSave()
    {
        gd.ResetData();
    }
    private void OnApplicationQuit()
    {
        gd.sceneCount = 0;
    }
}
