using UnityEngine;

public class PooledBall : MonoBehaviour
{
    public SimplePool2D pool;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = true;
        }
    }

    public void ReturnToPool()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        if (GameManager.GM != null)
            GameManager.GM.RegisterBallReturned(this);

        if (pool != null) pool.Return(gameObject);
        else gameObject.SetActive(false);
    }
}
