using UnityEngine;

public class AgePanel : MonoBehaviour
{
    [Header("Массив экранов веков")] [SerializeField] 
    GameObject[] ages;

    Vector2 startPos;
    Vector2 direction;
    bool is_swipe;
    int curretAgeIndex = 0;

    private void OnEnable()
    {
        MenuManager.instance.curretPlaneIndex = 0;
    }    
    void Update()
    {
        //Обработка свайпов 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    if (direction.x > 200 || direction.x < -200)
                       is_swipe = true;
                    else
                       is_swipe = false;
                    break;
                case TouchPhase.Ended:
                    if (direction.x > 200)
                        Change_Age(false);
                    else if (direction.x < -200)
                        Change_Age(true);
                    break;
            }
        }
    }
    public void Btn_SelectAge(int index)
    {
        if (!is_swipe)
        {
            MenuManager.instance.curretAgeIndex = curretAgeIndex;
            MenuManager.instance.LoadPanel(index + 1);
        }
    }
    public void Change_Age(bool next)
    {
        if (next)
        {
            ages[curretAgeIndex].SetActive(false);
            if (curretAgeIndex + 1 == ages.Length)
                curretAgeIndex = 0;
            else
                curretAgeIndex++;
            ages[curretAgeIndex].SetActive(true);
        }
        else
        {
            ages[curretAgeIndex].SetActive(false);
            if (curretAgeIndex - 1 < 0)
                curretAgeIndex = ages.Length - 1;
            else
                curretAgeIndex--;
            ages[curretAgeIndex].SetActive(true);
        }
    }
}
