using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Physics Settings")]
    [SerializeField] private float speed = 150f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float maxTravelDistance = 200f;
    [SerializeField] private LayerMask hitLayers;

    [Header("Ricochet Settings")]
    [SerializeField] private bool canBounce = true;
    [SerializeField] private int maxBounces = 3;
    [SerializeField] private float maxRicochetAngle = 35f;
    [SerializeField] private float velocityRetainedOnBounce = 0.65f;

    private Vector3 currentPos;
    private Vector3 prevPos;
    private Vector3 velocity;
    private Vector3 startPosition;

    private int currentBounces = 0;
    private bool initialized = false;

    private void Awake()
    {
        startPosition = transform.position;
        currentPos = transform.position;
        prevPos = transform.position;
        velocity = transform.forward * speed;
    }

    public void Initialize(Vector3 spawnPosition, Vector3 direction)
    {
        startPosition = spawnPosition;
        currentPos = spawnPosition;
        prevPos = spawnPosition;

        velocity = direction.normalized * speed;
        transform.position = spawnPosition;
        transform.forward = direction.normalized;

        initialized = true;
    }

    private void Update()
    {
        BulletMove();
    }

    public void BulletMove()
    {
        prevPos = currentPos;

        velocity.y -= gravity * Time.deltaTime;
        currentPos += velocity * Time.deltaTime;

        Vector3 stepVector = currentPos - prevPos;
        float stepDistance = stepVector.magnitude;

        if (Physics.Raycast(prevPos, stepVector.normalized, out RaycastHit hit, stepDistance, hitLayers))
        {
            float impactAngle = Vector3.Angle(-stepVector.normalized, hit.normal);
            float grazingAngle = 90f - impactAngle;

            if (canBounce && currentBounces < maxBounces && grazingAngle <= maxRicochetAngle)
            {
                HandleRicochet(hit, grazingAngle);
                return;
            }

            OnImpact(hit);
            return;
        }

        transform.position = currentPos;

        if (velocity != Vector3.zero)
        {
            transform.forward = velocity.normalized;
        }

        if (Vector3.Distance(startPosition, currentPos) >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    private void HandleRicochet(RaycastHit hit, float grazingAngle)
    {
        currentBounces++;

        currentPos = hit.point;
        prevPos = hit.point;

        velocity = Vector3.Reflect(velocity, hit.normal);
        velocity *= velocityRetainedOnBounce;

        transform.forward = velocity.normalized;
    }

    private void OnImpact(RaycastHit hit)
    {
        Destroy(gameObject);
    }
}