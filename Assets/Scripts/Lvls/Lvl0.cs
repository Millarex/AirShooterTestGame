using System.Collections;
using UnityEngine;

public class Lvl0 : MonoBehaviour
{
    [Header("Время перед стартом 1ой волны")]
    [SerializeField]
    private float timeToStart;
    [SerializeField]
    private WaveStruct [] waves;      
    [SerializeField]
    private bool is_infinite;
    private bool waveEnd;

    Wave curWavePref;
    int curWaveNumber;
    private void Start()
    {
        curWaveNumber = 0;
        if (is_infinite && waves.Length>1)
            StartCoroutine(StartInfiniteLvl(waves));
        else
            StartCoroutine(StartLvl(waves));
    }
    IEnumerator StartLvl(WaveStruct[] wave)
    {
        yield return new WaitForSeconds(timeToStart);
        for (int i = 0; i < wave.Length; i++)
        {            
            GameObject newWave = Instantiate(wave[i].wavePref, new Vector3(0,0,0), Quaternion.identity);
            curWavePref = newWave.GetComponent<Wave>();
            curWavePref.speed_Enemy = wave[i].speed_Enemy;
            curWavePref.time_Spawn = wave[i].time_Spawn;
            curWavePref.count_In_Wave = wave[i].count_In_Wave;
            curWavePref.is_Return = wave[i].is_Return;           
            curWavePref.returnPoint = wave[i].returnPoint;           
            while(i==curWaveNumber)
                yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(wave[i].waveDelay);
        }
    }
    IEnumerator StartInfiniteLvl(WaveStruct[] wave)
    {
        int i = Random.Range(0, wave.Length);
        yield return new WaitForSeconds(timeToStart);
        while (true)
        {
            waveEnd = false;            

            GameObject newWave = Instantiate(wave[i].wavePref, new Vector3(0, 0, 0), Quaternion.identity);
            curWavePref = newWave.GetComponent<Wave>();
            curWavePref.speed_Enemy = wave[i].speed_Enemy;
            curWavePref.time_Spawn = wave[i].time_Spawn;
            curWavePref.count_In_Wave = wave[i].count_In_Wave;
            curWavePref.is_Return = wave[i].is_Return;
            curWavePref.returnPoint = wave[i].returnPoint;

            while (!waveEnd)
                yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(wave[i].waveDelay);

            int nextI = Random.Range(0, wave.Length - 1);
            while (i == nextI)
                nextI = Random.Range(0, wave.Length - 1);
            i = nextI;
        }
    }
    public void On_Wave_End()
    {
        curWaveNumber++;
        waveEnd = true;
    }
}
