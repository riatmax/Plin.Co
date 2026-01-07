using UnityEngine;

public class PooledBall : MonoBehaviour
{
    [Header("Assign in Inspector")]
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
            // Use velocity for broad Unity 2D compatibility
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

        if (pool != null) pool.Return(gameObject);
        else gameObject.SetActive(false);
    }
}
