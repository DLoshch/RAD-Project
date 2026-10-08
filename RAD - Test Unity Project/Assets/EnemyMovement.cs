using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Движение якоря (общая траектория)")]
    public float anchorSpeed = 3f;      // Скорость движения самого якоря
    public bool moveRight = true;       // Направление движения якоря (вправо/влево)

    [Header("Хаос движения голубя вокруг якоря")]
    public float wanderRadiusX = 2f;    // Насколько далеко голубь может отлетать по X от якоря
    public float wanderRadiusY = 1.5f;  // Насколько далеко голубь может отлетать по Y от якоря
    public float changeDirectionSpeed = 1.5f; // Как часто голубь меняет направление суеты

    private Vector3 anchorPosition;     // Координаты движущегося якоря
    private float seedX;
    private float seedY;

    void Start()
    {
        // Инициализируем якорь на текущей позиции голубя в момент спавна
        anchorPosition = transform.position;

        // Задаем случайные «семена» для генератора шума, чтобы каждый голубь летал по-своему
        seedX = Random.Range(0f, 100f);
        seedY = Random.Range(200f, 300f);

        // Корректируем скорость якоря в зависимости от выбранного направления
        if (!moveRight)
        {
            anchorSpeed = -Mathf.Abs(anchorSpeed);
        }
        else
        {
            anchorSpeed = Mathf.Abs(anchorSpeed);
        }
    }

    void Update()
    {
        // 1. Двигаем якорь по оси X (и фиксируем его Z)
        anchorPosition.x += anchorSpeed * Time.deltaTime;

        // Обязательно держим Z таким же, каким он был при спавне
        anchorPosition.z = transform.position.z;

        // 2. Вычисляем хаотичное смещение вокруг якоря с помощью Mathf.PerlinNoise
        // Это дает плавное, но непредсказуемое дерганье во все стороны (вверх, вниз, влево, вправо)
        float offsetX = (Mathf.PerlinNoise((Time.time + seedX) * changeDirectionSpeed, 0f) - 0.5f) * 2f * wanderRadiusX;
        float offsetY = (Mathf.PerlinNoise(0f, (Time.time + seedY) * changeDirectionSpeed) - 0.5f) * 2f * wanderRadiusY;

        // 3. Итоговая позиция голубя = движущийся якорь + случайная суета
        Vector3 targetPosition = anchorPosition + new Vector3(offsetX, offsetY, 0f);

        // Применяем позицию к голублю
        transform.position = targetPosition;

        // 4. Удаляем объект, если якорь улетел слишком далеко за пределы экрана
        if (Mathf.Abs(anchorPosition.x) > 25f)
        {
            Destroy(gameObject);
        }
    }
}