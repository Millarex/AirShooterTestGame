using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public GameMode mode;
    public int lives = 0;
    public int curretMoney;
    //Общие переменные для связи между компонентами всей сцены 
    public UI ui;

    //
    [SerializeField]
    private GameData gd;
    private GameObject curPlayerInst;
    public GameObject curPlayerPref;

    private void Awake()
    {
        if (instance == null) instance = this;
    }  
    void Start()
    {
        switch (mode)
        {
            case GameMode.Debug:

                break;
            case GameMode.Default:
                lives = gd.lives;
                curPlayerPref = gd.GetCurPlayerPref();
                curPlayerInst = Instantiate(curPlayerPref, new Vector3(0, 30, -40), Quaternion.identity);
                break;
            case GameMode.Spec:
                lives = gd.lives;
                curPlayerPref = gd.GetCurPlayerPref();
                curPlayerInst = Instantiate(curPlayerPref, new Vector3(0, 30, -40), Quaternion.identity);
                break;
        }
        //чтобы экран не выключался во время игры
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Запускаем игровое время
        Time.timeScale = 1;
    }
    public void SetState(GameState value)
    {
        state = value;
        switch (state)
        {
            case GameState.Win:
                Time.timeScale = 0;
                ui.Win();
                break;
            case GameState.Loose:
                Time.timeScale = 0;
                ui.Loose();
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                ui.Pause();
                break;
            case GameState.Run:
                Time.timeScale = 1;
                ui.UnPause();
                break;
        }
    }
    public void SetMode(GameMode value)
    {
        mode = value;
        switch (mode)
        {
            case GameMode.Debug:

                break;
            case GameMode.Default:

                break;
            case GameMode.Spec:

                break;
        }
    }
    public void OnPlayerDead()
    {
        Debug.Log("Dead");
        if (lives > 0)
        {
            curPlayerInst = Instantiate(curPlayerPref, new Vector3(0, 30, -40), Quaternion.identity);
            lives -= 1;
        }
        else        
            SetState(GameState.Loose);        
    }
    public void LoadScene()
    {
        if (state!=GameState.Loose)
            gd.Money += curretMoney;
        gd.SaveData();
        SceneManager.LoadScene(0);
    }
    public void AddMoney(int newMoney)
    {
        if (newMoney != 0)        
        {
            curretMoney += newMoney;
            ui.UpdateMoney(newMoney);
        }        
    }
    public void PlayerDamage()
    {
        ui.StartHurtAnim();
    }
}
