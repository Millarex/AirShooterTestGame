using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIMove : MonoBehaviour
{
    public Transform pointL;
    public Transform pointR;
    public float speed;
    public float rotSpeed;
    public float rotAngle;
    public float maneuverDistance;
    public float maneuverTime;

    GameObject target;
    bool manoeuvring;
    bool right;

    void Start()
    {
        manoeuvring = false;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (!manoeuvring)
        {
            if (Physics.Raycast(pointL.position, transform.forward, out hit, maneuverDistance))
            {
                if (hit.collider.CompareTag("Static object"))
                {
                    Debug.DrawRay(pointL.position, transform.forward * hit.distance, Color.yellow);
                    StartCoroutine(ManeuverTimer());
                    right = true;
                }
            }
            else if (Physics.Raycast(pointR.position, transform.forward, out hit, maneuverDistance))
            {
                if (hit.collider.CompareTag("Static object"))
                {
                    Debug.DrawRay(pointR.position, transform.forward * hit.distance, Color.yellow);
                    StartCoroutine(ManeuverTimer());
                    right = false;
                }
            }
            else
            {
                Vector3 direction = Vector3.zero;
                Debug.DrawRay(pointR.position, transform.forward * maneuverDistance, Color.white);
                Debug.DrawRay(pointL.position, transform.forward * maneuverDistance, Color.white);
                if (target)
                     direction = (target.transform.position - transform.position).normalized;
                this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (right)
                this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotAngle, 0), rotSpeed * Time.deltaTime);
            else
                this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -rotAngle, 0), rotSpeed * Time.deltaTime);
        }

        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    IEnumerator ManeuverTimer()
    {
        manoeuvring = true;
        yield return new WaitForSeconds(maneuverTime);
        manoeuvring = false;
    }
}
