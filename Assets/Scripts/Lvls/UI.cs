using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject btnReturn;
    public GameObject addMoney;
    public Image Damaged;
    public Text curMoney;
    public Text lives;
    public Text state;

    bool coroutineStart;

    void Start()
    {
        curMoney.text = "0$";
    }
    void Update()
    {
        lives.text = "Lives:\n" + GameManager.instance.lives.ToString();
    }
    public void Btn_Pause()
    {
        if (GameManager.instance.state != GameState.Pause)
            GameManager.instance.SetState(GameState.Pause);
        else
            Btn_ReturnGame();
    }
    public void Btn_ReturnGame()
    {
        GameManager.instance.SetState(GameState.Run);
    }
    public void Btn_ReturnMenu()
    {
        GameManager.instance.LoadScene();
    }
    public void Loose()
    {
        //Отобразим игровую панель
        pauseMenu.SetActive(true);
        btnReturn.SetActive(false);
        state.text = "You Loose";
    }
    public void Win()
    {
        //Отобразим игровую панель
        pauseMenu.SetActive(true);
        btnReturn.SetActive(false);
        state.text = "You Win";
    }
    public void Pause()
    {
        state.text = "Game Pause";
        //Отобразим игровую панель
        pauseMenu.SetActive(true);
    }
    public void UnPause()
    {
        pauseMenu.SetActive(false);
    }
    public void StartHurtAnim()
    {
        if (!coroutineStart)
        {
            StartCoroutine(PlayerHurt());
        }
    }
    public void UpdateMoney(int newMoney)
    {
        curMoney.text = GameManager.instance.curretMoney.ToString() + "$";
        addMoney.GetComponent<Text>().text = "+" + newMoney.ToString() + "$";
        StartCoroutine(TimeWait(0.9f));
    }
    IEnumerator TimeWait(float delay)
    {
        addMoney.SetActive(true);
        if (delay != 0) yield return new WaitForSeconds(delay);
        addMoney.SetActive(false);
    }
    IEnumerator PlayerHurt()
    {
        coroutineStart = true;
        while (Damaged.color.a <= 0.2)
        {
            Damaged.color = new Color(1, 0, 0, Damaged.color.a + 0.02f);
            yield return null;
        }
        while (Damaged.color.a >= 0)
        {
            Damaged.color = new Color(1, 0, 0, Damaged.color.a - 0.02f);
            yield return null;
        }
        coroutineStart = false;
    }
}
