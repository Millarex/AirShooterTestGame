using System.Collections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header("Пул моделек врагов для данной волны. Кто может так летать")]
    [SerializeField]
    GameObject[] obj_Enemy;
    [Header("Пул точек движения врага")]
    [SerializeField]
    Transform[] path_Points;
    [Header("Рандомизация по Х для красоты")]
    [SerializeField]
    float randomizeX;
    [Header("Если самолет летит хвостом")]
    [SerializeField]
    public bool isInvert;     
    [SerializeField]
    private GameEvent OnWaveEnd;
    

    [HideInInspector]
    public float speed_Enemy;    
    [HideInInspector]
    public float time_Spawn;
    [HideInInspector]
    public bool is_Return;
    [HideInInspector]
    public int count_In_Wave; 
    [HideInInspector]
    public float deltaSpeed;
    [HideInInspector]
    public int returnPoint;


    //Переменная для передачи данных врагу
    private EnemyPointMove enemyMoving;
    Vector3 startPos;
    Transform[] repeatPos;

    private void Start()
    {
        startPos = this.transform.position;        
        if (is_Return && returnPoint != 0)
        {
            repeatPos = new Transform[path_Points.Length - returnPoint];
            for (int i = 0; i < path_Points.Length - returnPoint; i++)
            {
                repeatPos[i] = path_Points[i + returnPoint];
            }
        }
            //Запуск программы генерации волны
            StartCoroutine(CreateEnemyWave());       
        IEnumerator CreateEnemyWave()
        {
            for (int i = 0; i < count_In_Wave; i++)
            {
                int temp = Random.Range(0, obj_Enemy.Length);
                GameObject new_enemy = Instantiate(obj_Enemy[temp], path_Points[0].position, Quaternion.identity);

                enemyMoving = new_enemy.GetComponent<EnemyPointMove>();               
                enemyMoving.speed_enemy =Random.Range(-deltaSpeed,deltaSpeed) + speed_Enemy;
                enemyMoving.is_return = is_Return;
                enemyMoving._new_Position = NewPositionsByPath(path_Points);
                enemyMoving.isInvert = isInvert;
                if (is_Return && returnPoint != 0)
                {
                    enemyMoving.repeatPos = NewPositionsByPath(repeatPos);
                }
                else
                    enemyMoving.repeatPos = null;

                //Сделаем врага видимым 
                new_enemy.SetActive(true);
                //Каждое время спауна создаем нового врага
                yield return new WaitForSeconds(time_Spawn);
                //Меняем положение волны в пределах спауна для генерации нового противника 
                this.transform.position = new Vector3(Random.Range(-randomizeX, randomizeX), startPos.y, startPos.z);
            }
            //Событие окончания волны
            OnWaveEnd.Raise();
            yield return new WaitForSeconds(1);            
            Destroy(gameObject);
        }
    }
    #region PathDraw
    //Соеденим точки пути для их визуализации и удобной настройки
    void OnDrawGizmos()
    {
        NewPositionByGizmos(path_Points);
    }

    void NewPositionByGizmos(Transform[] path)
    {
        Vector3[] pathPosition = NewPositionsByPath(path);
        for (int i = 0; i < pathPosition.Length - 1; i++)
        {
            Gizmos.DrawLine(pathPosition[i], pathPosition[i + 1]);
        }
    }
    Vector3[] NewPositionsByPath(Transform[] pathPos)
    {
        Vector3[] pathPosition = new Vector3[pathPos.Length];
        for (int i = 0; i < pathPos.Length; i++)
        {
            pathPosition[i] = pathPos[i].position;
        }
        //Сгладим линии пути и сделаем их красивыми, добавляя промежуточные точки. Вызываем метод 3 раза чтобы полностью сгладить углы
        pathPosition = Smoothing(pathPosition);
        pathPosition = Smoothing(pathPosition);
        pathPosition = Smoothing(pathPosition);
        pathPosition = Smoothing(pathPosition);
        pathPosition = Smoothing(pathPosition);
        pathPosition = Smoothing(pathPosition);
        //pathPosition = Smoothing(pathPosition);
        return pathPosition;
    }
    //Метод сглаживания
    Vector3[] Smoothing(Vector3[] pathPositions)
    {
        //создаем новый вектор с рассчетом размера массива. -2 тк убираем начальную и конечную точки (У нас 5 вэй поинтов а в новом массиве будет 8)
        //для каждой точки карты пути мы создадим 2 дополнительные точки для ее сглаживания
        Vector3[] new_pathPositions = new Vector3[(pathPositions.Length - 2) * 2 + 2];
        //Заполняем начальную точку и конечную точку пути, тк их сглаживать не нужно
        new_pathPositions[0] = pathPositions[0];
        new_pathPositions[new_pathPositions.Length - 1] = pathPositions[pathPositions.Length - 1];
        //переменная для корректной работы с итерацией нового массива
        int j = 1;
        for (int i = 0; i < pathPositions.Length - 2; i++)
        {
            //Создаем сглаживание для каждой новой точки основного массива пути создается 2 дополнительные точки
            //где доп точка = координаты основной точки 0 + (координаты осн точки 1 - координаты 0)* 0,75
            //аналогично для 2ой доп точки 
            new_pathPositions[j] = pathPositions[i] + (pathPositions[i + 1] - pathPositions[i]) * 0.75f;
            new_pathPositions[j + 1] = pathPositions[i + 1] + (pathPositions[i + 2] - pathPositions[i + 1]) * 0.25f;
            //Увеличиваем итерацию чтобы указывать на новые точки
            j += 2;
        }
        return new_pathPositions;
    }
    #endregion
}
