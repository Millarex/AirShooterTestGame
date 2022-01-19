using UnityEngine;
using UnityEngine.UI;

public class AngarSpec : MonoBehaviour
{
    [Header("Plane name")]
    [SerializeField]
    Text nameText;
    [Header("Plane description")]
    [SerializeField]
    GameObject descriptionText;
    [Header("Lvl description")]
    [SerializeField]
    GameObject lvlDescription;

    GameObject tempModel;
    PlayerData specPlayer;
    Vector3 centerPos;
    bool click;
   
    public void Btn_StartLvl()
    {
        MenuManager.instance.StartLvl();
    }
    public void Btn_Desc()
    {
        if (click)
        {
            descriptionText.SetActive(false);
            click = false;
        }
        else
        {
            descriptionText.SetActive(true);
            click = true;
        }
    }    
    void InitPlane()
    {
        centerPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 3 / 4, 9));
        specPlayer = MenuManager.instance.curSpecPlayer;
        tempModel = Instantiate(specPlayer.PlayerModelsForMenu, centerPos, Quaternion.Euler(19, 180, 0));
        nameText.text = specPlayer.PlaneName;
        descriptionText.GetComponent<Text>().text = specPlayer.Description;
    }
    private void OnDisable()
    {
        Destroy(tempModel);
    }
    private void OnEnable()
    {
        InitPlane();
    }
}
