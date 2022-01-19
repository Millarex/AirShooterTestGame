using System.Collections;
using UnityEngine;

public class SpecLvl0 : MonoBehaviour
{
    public float startTimer;
    public float endTimer;     
    public Vector3 targetPos;

    public GameObject targetPref;
    public GameObject markPref;
    Vector3 markPos;
  
    void Start()
    {
        StartCoroutine(StartLvl());
    }
    IEnumerator StartLvl()
    {
        yield return new WaitForSeconds(startTimer);
        ObjectSpawner.instance.StartSpawn();
        yield return new WaitForSeconds(endTimer);       
        Instantiate(targetPref, targetPos, Quaternion.identity);
        markPos = new Vector3(Random.Range(-15, 15), 30, Random.Range(-15, 15));
        Instantiate(markPref, markPos, Quaternion.identity);        
    }
    public void On_Target_Dead()
    {
        GameManager.instance.SetState(GameState.Win);
    }
}
