using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Указание насколько отступать от границ экрана")]
    public float minX = 1.1f;
    public float maxX = 1.1f;
    public float minZ = 1.1f;
    public float maxZ = 1.1f;
    public float rotationSpeed;
    public float speed;
    public float offset = 1.5f;

    Vector3 nextPosition;
    Plane plane;
    Vector3 startPos;
    bool alloveMove;

    void Start()
    {
        startPos = new Vector3(0, 30, -15);
        //Создаем планку на высоте персонажа для перемещения по ней
        plane = new Plane(Vector3.up, new Vector3(0, this.transform.position.y, 0));
        StartCoroutine(MoveToGame());
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && alloveMove)
        {
            //Получаем новую точку на пересечении планки
            float planeDistance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out planeDistance))
            {
                nextPosition = ray.GetPoint(planeDistance);
            }
            //Наклоны самолета при движении влево и вправо
            if (nextPosition.x - transform.position.x > 0.1) //transform.position.x < nextPosition.x
            {
                Quaternion rightRot = Quaternion.Euler(0, 0, -40);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rightRot, Time.deltaTime * rotationSpeed);
            }
            else if (nextPosition.x - transform.position.x < -0.1)//transform.position.x > nextPosition.x
            {
                Quaternion leftRot = Quaternion.Euler(0, 0, 40);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, leftRot, Time.deltaTime * rotationSpeed);
            }
            else
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, Time.deltaTime * rotationSpeed);
            //Движение на позицию мыши
            nextPosition.z += offset;
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * speed);
            transform.position = Borders(minX, maxX, minZ, maxZ);
        }
        else
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, Time.deltaTime * rotationSpeed);

    }
    // ограничиваем объект в плоскости XZ
    Vector3 Borders(float minX, float maxX, float minZ, float maxZ)
    {
        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        // отрицание потому что игровые объекты в данном случае находятся ниже камеры по оси y
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;
        // вершины "среза" пирамиды видимости камеры на необходимом расстоянии от камеры
        Vector3 leftBot = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        // границы в плоскости XZ, т.к. камера стоит выше остальных объектов
        float x_left = leftBot.x + minX;
        float x_right = rightTop.x - maxX;
        float z_top = rightTop.z - maxZ;
        float z_bot = leftBot.z + minZ;
        Vector3 clampedPos = this.transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, x_left, x_right);
        clampedPos.z = Mathf.Clamp(clampedPos.z, z_bot, z_top);
        return clampedPos;
    }

    IEnumerator MoveToGame()
    {
        alloveMove = false;
        while (Vector3.Distance(transform.position, startPos) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 10 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        alloveMove = true;
        yield return new WaitForSeconds(Player.instance.immortalTime);
        Player.instance.in_game = true;
    }
}
