using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{  
    [SerializeField]
    Text [] weaponParameters;
    [SerializeField]
    GameObject [] buttons;
    [SerializeField]
    Dropdown [] drops;
    [SerializeField]
    GameObject noMoney;

    WeaponsData[] mainWeapons;
    WeaponsData[] secondaryWeapons;
    int mwIndex;
    int swIndex;

    GameObject tempModel;
    Vector3 centerPos;
    List<string> options = new List<string>();

    public void Change_Primary_Weapon()
    {
        mwIndex = drops[0].value;
        weaponParameters[0].text = mainWeapons[mwIndex].Description;
        if (mainWeapons[mwIndex].modifiable)
        {
            if (mainWeapons[mwIndex].Open)
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(false);
                weaponParameters[1].text = "Curret upgrade lvl: " + mainWeapons[mwIndex].UpgradeLvl.ToString() + " " + "Upgrade Cost:" + mainWeapons[mwIndex].UpgradeCost.ToString();
            }
            else
            {
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
                weaponParameters[1].text = "Open cost: " + mainWeapons[mwIndex].OpenCost.ToString() + "$";
            }
        }
        else
        {
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            weaponParameters[1].text = "Non modifiable";
        }
    }
    public void Change_Secondary_Weapon()
    {
        swIndex = drops[1].value;
        weaponParameters[2].text = secondaryWeapons[swIndex].Description;
        if (secondaryWeapons[swIndex].modifiable)
        {
            if (secondaryWeapons[swIndex].Open)
            {
                buttons[2].SetActive(true);
                buttons[3].SetActive(false);
                weaponParameters[3].text = "Curret upgrade lvl: " + secondaryWeapons[swIndex].UpgradeLvl.ToString() + " " + "Upgrade Cost:" + secondaryWeapons[swIndex].UpgradeCost.ToString();
            }
            else
            {
                buttons[2].SetActive(false);
                buttons[3].SetActive(true);
                weaponParameters[3].text = "Open cost: " + secondaryWeapons[swIndex].OpenCost.ToString() + "$";
            }
        }
        else
        {
            buttons[2].SetActive(false);
            buttons[3].SetActive(false);
            weaponParameters[3].text = "Non modifiable";
        }
      
    }
    void InitPanel()
    {
        centerPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 3 / 4, 9));
        tempModel = Instantiate(MenuManager.instance.curPlayer.PlayerModelsForMenu, centerPos, Quaternion.Euler(19, 180, 0));
        mainWeapons = MenuManager.instance.GetMainWeapon();
        secondaryWeapons = MenuManager.instance.GetSecondaryWeapon();
        drops[0].ClearOptions();
        drops[1].ClearOptions();
        for (int i = 0; i < mainWeapons.Length; i++)
        {
            options.Add(mainWeapons[i].WeaponName);
            if (MenuManager.instance.curWeapon && mainWeapons[i].WeaponName == MenuManager.instance.curWeapon.WeaponName)
                mwIndex = i;
        }     
        drops[0].AddOptions(options);
        drops[0].value = mwIndex;
        options.Clear();
        Change_Primary_Weapon();

        if (secondaryWeapons.Length == 1)
        {
            options.Add("None");
            swIndex = 0;
        }
        else
        {
            for (int i = 0; i < secondaryWeapons.Length; i++)
            {
                options.Add(secondaryWeapons[i].WeaponName);
                if (MenuManager.instance.curOptWeapon && secondaryWeapons[i].WeaponName == MenuManager.instance.curOptWeapon.WeaponName)
                    swIndex = i;
            }
        }             
        drops[1].AddOptions(options);
        drops[1].value = swIndex;
        options.Clear();
        Change_Secondary_Weapon();
    }
    public void Btn_SelectWeapon()
    {
        if (mainWeapons[mwIndex].Open && secondaryWeapons[swIndex].Open)
        {
            MenuManager.instance.curWeapon = mainWeapons[mwIndex];
            MenuManager.instance.curOptWeapon = secondaryWeapons[swIndex];
            MenuManager.instance.Save();
            MenuManager.instance.Btn_Back();
        }
        else
            StartCoroutine(Timer("Выбранные оружия недоступны"));

    }
    public void Btn_BuyWeapon(bool secondary)
    {
        if (secondary)
        {
            if (MenuManager.instance.Buy(secondaryWeapons[swIndex].OpenCost))
            {
                secondaryWeapons[swIndex].Open = true;
                Change_Secondary_Weapon();
            }
            else
                StartCoroutine(Timer("Нужно больше золота"));
        }
        else
        {
            if (MenuManager.instance.Buy(mainWeapons[mwIndex].OpenCost))
            {
                mainWeapons[mwIndex].Open = true;
                Change_Primary_Weapon();
            }
            else
                StartCoroutine(Timer("Нужно больше золота"));
        }            
    }
    public void Btn_UpgradeWeapon(bool secondary)
    {
        if (secondary)
        {
            if (MenuManager.instance.Buy(secondaryWeapons[swIndex].OpenCost))
            {
                secondaryWeapons[swIndex].UpgradeWeapon();
                Change_Secondary_Weapon();
            }
            else
                StartCoroutine(Timer("Нужно больше золота"));
        }
        else
        {
            if (MenuManager.instance.Buy(mainWeapons[mwIndex].OpenCost))
            {
                mainWeapons[mwIndex].UpgradeWeapon();
                Change_Primary_Weapon();
            }
            else
                StartCoroutine(Timer("Нужно больше золота"));
        }
    }
    private void OnDisable()
    {
        Destroy(tempModel);
    }
    private void OnEnable()
    {
        mwIndex = 0;
        swIndex = 0;
        InitPanel();
    }
    IEnumerator Timer(string text)
    {
        noMoney.GetComponent<Text>().text = text;
        noMoney.SetActive(true);
        yield return new WaitForSeconds(2);
        noMoney.SetActive(false);
    }
}
