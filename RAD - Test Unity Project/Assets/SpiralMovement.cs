using UnityEngine;

public class DynamicOrganicSpiral : MonoBehaviour
{
    [Header("Базовое движение")]
    public float baseFlySpeed = 5f;
    public float speedVariation = 2f; // Пульсация скорости

    [Header("Динамика спирали")]
    public float baseRadius = 1.5f;
    public float radiusPulseSpeed = 1f;  // Скорость изменения радиуса
    public float wobbleSpeed = 4f;       // Скорость закручивания

    [Header("Органическое плавание (Perlin Noise)")]
    public float noiseScale = 0.5f;      // Масштаб шума (чем больше, тем чаще завихрения)
    public float turnSensitivity = 45f;  // Максимальный угол отклонения в секунду

    [Header("Крен при повороте (Banking)")]
    public float bankAmount = 30f;       // Насколько сильно наклоняется в повороте
    public float bankSmoothing = 5f;

    private float timer;
    private Vector2 noiseSeed;
    private float currentBankAngle;

    void Start()
    {
        // Уникальный сид шума для каждого объекта
        noiseSeed = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    void Update()
    {
        timer += Time.deltaTime;

        UpdateSteeringAndBanking();
        MoveAndSpiral();
    }

    private void UpdateSteeringAndBanking()
    {
        // 1. Вычисляем плавное отклонение направления с помощью Perlin Noise
        float yawNoise = (Mathf.PerlinNoise(noiseSeed.x, timer * noiseScale) - 0.5f) * 2f;
        float pitchNoise = (Mathf.PerlinNoise(noiseSeed.y, timer * noiseScale) - 0.5f) * 2f;

        // Поворачиваем объект локально
        float yaw = yawNoise * turnSensitivity * Time.deltaTime;
        float pitch = pitchNoise * turnSensitivity * Time.deltaTime;

        // 2. Расчет крена (Roll) в зависимости от того, насколько резко поворачиваем по Yaw
        float targetBank = -yawNoise * bankAmount;
        currentBankAngle = Mathf.Lerp(currentBankAngle, targetBank, Time.deltaTime * bankSmoothing);

        // Применяем вращение
        transform.Rotate(Vector3.up, yaw, Space.World);
        transform.Rotate(transform.right, pitch, Space.World);

        // Выравниваем Z-ось для эффекта крена
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentBankAngle);
    }

    private void MoveAndSpiral()
    {
        // 1. Динамический радиус и скорость (пульсация)
        float currentRadius = baseRadius + Mathf.Sin(timer * radiusPulseSpeed) * (baseRadius * 0.4f);
        float currentSpeed = baseFlySpeed + Mathf.Sin(timer * 0.7f) * speedVariation;

        // 2. Вектор движения вперед
        Vector3 forwardMove = transform.forward * (currentSpeed * Time.deltaTime);

        // 3. Локальное смещение спирали (вокруг направления полета)
        Vector3 localSpiralOffset = new Vector3(
            Mathf.Cos(timer * wobbleSpeed) * currentRadius,
            Mathf.Sin(timer * wobbleSpeed) * currentRadius,
            0f
        );

        // Перевод спирали в мировые координаты
        Vector3 worldSpiralOffset = transform.TransformDirection(localSpiralOffset);

        // 4. Применяем смещение
        transform.position += forwardMove + worldSpiralOffset * Time.deltaTime;
    }
}