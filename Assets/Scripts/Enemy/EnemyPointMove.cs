using UnityEngine;

public class EnemyPointMove : MonoBehaviour
{   
    // Переменная скорости врага
    [HideInInspector]
    public float speed_enemy;
    //Флаг согласно которому в последней точке пути враг либо перейдет в 1ую точку либо будет уничтожен
    [HideInInspector]
    public bool is_return;
    // Массив векторов для пути 
    [HideInInspector]
    public Vector3[] _new_Position = null;
    [HideInInspector]
    public Vector3[] repeatPos = null;   
    [HideInInspector]
    public bool isInvert;
    
    int cur_pos;
    bool is_repeat;    

    // Update is called once per frame
    private void Start()
    {            
        is_repeat = false;
        cur_pos = 0;
    }
    void Update()
    {
        if (!is_repeat)        
         Move(_new_Position);        
        else       
            Move(repeatPos); 
    }
    
    void Move(Vector3[] pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos[cur_pos], speed_enemy * Time.deltaTime);
        if (!isInvert)
            transform.LookAt(pos[cur_pos]);
        if (Vector3.Distance(transform.position, pos[cur_pos]) < 0.3f)
        {
            cur_pos++;
            //СОздание бесконечного движения если is_return true. Lenght-1 тк длина считается не от нуля, а элементы в массиве от 0
            if (is_return && Vector3.Distance(transform.position, pos[pos.Length - 1]) < 0.3f)
            {
                if (repeatPos != null)
                {
                    is_repeat = true;
                }
                cur_pos = 0;
            }
            //Если враг добрался до конечной точки пути и is_return false мы уничтожаем его 
            if (!is_return && Vector3.Distance(transform.position, pos[pos.Length - 1]) < 0.3f)
            {
                Destroy(gameObject);
            }
        }
    }    
}
