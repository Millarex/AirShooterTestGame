using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject exp;
    public Component[] destroyOnDeath;
    Rigidbody rd;
    public float fallSpeed;
    public float rotSpeed;
    public float timer;

    void Start()
    {
        rd = this.GetComponent<Rigidbody>();
    }
    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(rd);
        this.tag = "Untagged";
        foreach (Component item in destroyOnDeath)
        {
            Destroy(item);
        }
        Instantiate(exp, this.transform.position, Quaternion.identity);
        StartCoroutine(TimerKill());
    }
    public void Fall()
    {
        this.tag = "Untagged";
        if (rd)
        {
            rd.isKinematic = false;
            foreach (Component item in destroyOnDeath)
            {
                Destroy(item);
            }
            StartCoroutine(StartFall());
        }        
    }
    IEnumerator StartFall()
    {
        while (this.transform.rotation.eulerAngles.x <= 45)
        {
            rd.AddForce(transform.forward * fallSpeed, ForceMode.Acceleration);
            rd.AddTorque(transform.right * rotSpeed, ForceMode.Acceleration);
            yield return new WaitForFixedUpdate();
        }
        rd.angularVelocity = new Vector3(rd.angularVelocity.x, 0, rd.angularVelocity.z);
        rd.AddTorque(transform.up * Random.Range(-20, 20) * 10, ForceMode.Acceleration);
        while (true)
        {
            rd.AddForce(transform.forward * fallSpeed, ForceMode.Acceleration);
            rd.AddTorque(transform.forward * rotSpeed * 5, ForceMode.Acceleration);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator TimerKill()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
