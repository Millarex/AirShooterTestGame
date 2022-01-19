using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpFunction
{
  
}

[System.Serializable]
public struct WaveStruct
{
    public GameObject wavePref;
    [Header("Скорость врагов")]
    public float speed_Enemy;
    [Header("Рандомайз скорости +-")]
    public float deltaSpeed;
    [Header("Задержка между спавном врагов")]
    public float time_Spawn;
    [Header("колличество врагов в волне")]
    public int count_In_Wave;
    [Header("Задержка после окончания волны")]
    public float waveDelay;
    [Header("Поведение в последней точке маршрута")]
    public bool is_Return;
    [Header("Если нужно сделать циклическое движение не с 0 точки")]
    [SerializeField]
    public int returnPoint;
}
[System.Serializable]
public struct SpawnTarget
{
    [Header("Объект спавна")]
    public GameObject[] Prefs;
    [Header("Точки спавна")]
    public Transform[] positions;
    [Header("Задержка между спавном")]
    public float time_Spawn;
    [Header("колличество")]
    public int count;
}
[System.Serializable]
struct Weapon
{
    public GameObject gunPositions;
    public GameObject gunHeatings;
}
public enum GameState
{
    Win, Pause, Loose, Run
}
public enum WeaponType
{
    Mashinegun, Cannon, Rocket, HomingRocket, None
}
public enum GameMode
{
    Debug, Default, Spec
}

