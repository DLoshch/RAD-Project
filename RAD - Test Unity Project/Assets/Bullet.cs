using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        BulletMove();
    }

    public void BulletMove()
    {
        transform.position = transform.up * 50f * Time.deltaTime + transform.position;
        if (Vector3.Distance(startPosition, transform.position) > 100f)
        {
            Destroy(gameObject);
        }
    }
}
