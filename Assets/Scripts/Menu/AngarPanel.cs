using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngarPanel : MonoBehaviour
{
    [Header("Plane name")]
    [SerializeField]
    Text nameText;
    [Header("Plane description")]
    [SerializeField]
    GameObject descriptionText;
    [SerializeField]
    Text costText;
    [SerializeField]
    GameObject btnBuy;
    [SerializeField]
    GameObject btnUpgrade;
    [SerializeField]
    GameObject noMoney;

    List<PlayerData> permittedPlanes = new List<PlayerData>();
    GameObject tempModel;
    int curretPlaneIndex;
    Vector3 centerPos;
    bool click;

    public void Change_Plane(bool next)
    { 
        if (permittedPlanes.Count > 1)
        {
            if (next)
            {
                Destroy(tempModel);
                if (curretPlaneIndex + 1 == permittedPlanes.Count)
                    curretPlaneIndex = 0;
                else
                    curretPlaneIndex++;
                tempModel = Instantiate(permittedPlanes[curretPlaneIndex].PlayerModelsForMenu, centerPos, Quaternion.Euler(19, 180, 0));
                nameText.text = permittedPlanes[curretPlaneIndex].PlaneName;
                descriptionText.GetComponent<Text>().text = permittedPlanes[curretPlaneIndex].Description;
                if (permittedPlanes[curretPlaneIndex].open)
                {
                    btnBuy.SetActive(false);
                    btnUpgrade.SetActive(true);
                }               
                else
                {
                    costText.text = permittedPlanes[curretPlaneIndex].openCost.ToString();
                    btnBuy.SetActive(true);
                    btnUpgrade.SetActive(false);
                }                    
            }
            else
            {
                Destroy(tempModel);
                if (curretPlaneIndex - 1 < 0)
                    curretPlaneIndex = permittedPlanes.Count - 1;
                else
                    curretPlaneIndex--;
                tempModel = Instantiate(permittedPlanes[curretPlaneIndex].PlayerModelsForMenu, centerPos, Quaternion.Euler(19, 180, 0));
                nameText.text = permittedPlanes[curretPlaneIndex].PlaneName;
                descriptionText.GetComponent<Text>().text = permittedPlanes[curretPlaneIndex].Description;
                if (permittedPlanes[curretPlaneIndex].open)
                {
                    btnBuy.SetActive(false);
                    btnUpgrade.SetActive(true);
                }                
                else
                {
                    costText.text = permittedPlanes[curretPlaneIndex].openCost.ToString();
                    btnBuy.SetActive(true);
                    btnUpgrade.SetActive(false);
                }
            }           
        }        
    }
    public void Btn_StartLvl()
    {
        if (permittedPlanes[curretPlaneIndex].open)
        {
            MenuManager.instance.curPlayer = permittedPlanes[curretPlaneIndex];
            MenuManager.instance.StartLvl();
        }
        else
            StartCoroutine(Timer("Самолет не куплен"));           
    }
    public void Btn_Buy()
    {
       if (MenuManager.instance.Buy(permittedPlanes[curretPlaneIndex].openCost))
       {
            MenuManager.instance.Buy(permittedPlanes[curretPlaneIndex].openCost);
            permittedPlanes[curretPlaneIndex].open = true;            
            btnBuy.SetActive(false);
            btnUpgrade.SetActive(true);
        }
       else
            StartCoroutine(Timer("Нужно больше денег"));
    }
    public void Btn_UpgradeWeapon()
    {
        MenuManager.instance.curPlayer = permittedPlanes[curretPlaneIndex];
        MenuManager.instance.LoadPanel(7);
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
        curretPlaneIndex = MenuManager.instance.curretPlaneIndex;
        permittedPlanes = MenuManager.instance.GetPlanes();
        tempModel = Instantiate(permittedPlanes[curretPlaneIndex].PlayerModelsForMenu, centerPos, Quaternion.Euler(19, 180, 0));
        nameText.text = permittedPlanes[curretPlaneIndex].PlaneName;
        descriptionText.GetComponent<Text>().text = permittedPlanes[curretPlaneIndex].Description;
    }
    private void OnDisable()
    {
        Destroy(tempModel);
    }
    private void OnEnable()
    {
        InitPlane();
    }
    IEnumerator Timer(string text)
    {
        noMoney.GetComponent<Text>().text = text;
        noMoney.SetActive(true);
        yield return new WaitForSeconds(2);
        noMoney.SetActive(false);
    }
}
