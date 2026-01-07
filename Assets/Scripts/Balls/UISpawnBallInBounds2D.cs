using UnityEngine;

public class UISpawnBallInBounds2D : MonoBehaviour
{
    [Header("References")]
    public SimplePool2D pool;
    public Collider2D spawnBounds;

  
       public void DropBall()
{
    if (GameManager.GM == null) return;

    // Only allow 3 drops per round
    if (!GameManager.GM.TryRegisterBallDrop()) return;

    // Then spawn from your pool (your existing logic)
    // pool.Get(spawnPos);
}

    }

