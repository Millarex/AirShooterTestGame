using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;
    [SerializeField]
    private SpawnTarget[] targets;
    public void Awake()
    {
        if (instance == null) instance = this;
    }

    public void StartSpawn()
    {
        foreach (SpawnTarget target in targets)
        {
            StartCoroutine(StartObjSpawn(target));
        }        
    }
    IEnumerator StartObjSpawn(SpawnTarget st)
    {       
        for (int i = 0; i < st.count; i++)
        {
            GameObject tempObj = st.Prefs[Random.Range(0, st.Prefs.Length)];
            Transform rndPos = st.positions[Random.Range(0, st.positions.Length)];
            Instantiate(tempObj, rndPos.position, Quaternion.identity);
            yield return new WaitForSeconds(st.time_Spawn);
        }        
    }
}
