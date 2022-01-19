using UnityEngine;

public class BG : MonoBehaviour
{ 
    // Переменная скорости перемещения объекта скрипта
    [Header("Скорость движения по оси Z")]
    public float speed;
    // Переменная для хранения высоты объекта в юнитах.    
    [Header("Длина объекта")]
    public float verical_Size;
    [Header("Объекты фона")]
    public Transform[] BGs;
    // переменная для расчета высоты на которую должен подняться объект
    private Vector3 _offSet_Up;  
    private void Update()
    {
        for (int i = 0; i < BGs.Length; i++)
        {
            //Перемещение по вертикальной плоскости, направление вверх или вниз зависит от скорости (+ или -)
            BGs[i].Translate(Vector3.back * speed * Time.deltaTime);
            //Условие для проверки находится ли спрайт ниже своей высоты
            if (BGs[i].position.z < -verical_Size)
            {
                RepeatBackground(BGs[i]);
            }
        }       
    }
    void RepeatBackground(Transform obj)
    {
        //Умножаем на колличество объектов для определения необходимой высоты смещения
        _offSet_Up = new Vector3(0, 0, verical_Size * BGs.Length);
        //переместить объект наверх
        obj.position = (Vector3)obj.position + _offSet_Up;
    }
}
