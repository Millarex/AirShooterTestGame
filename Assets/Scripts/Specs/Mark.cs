using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mark : MonoBehaviour
{
    public int timer;
    public GameObject bombPref;
    
    Text timerText;
    bool work = false;

    private void Start()
    {
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !work)
        {            
            StartCoroutine(Timer(timer));
        }
           
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            work = false;
            if (timerText)
                timerText.text = " ";
        } 
           
    }
    IEnumerator Timer( int time)
    {
        work = true;
        for (int i = time; i > 0; i--)
        {
            if (timerText)
                timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        if (timerText)
            timerText.text = " ";
        Instantiate(bombPref, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        work = false;
    }
}
