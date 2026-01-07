using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("Scoring")]
    public int totalPoints;
    public int roundPoints;

    [Header("Round Flow")]
    public int ballsPerRound = 3;

    // How many balls the player can still drop this round
    public int ballsLeftToDrop { get; private set; }

    // How many dropped balls are still moving and have not landed yet
    public int ballsInPlay { get; private set; }

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalPoints = 0;
        BeginRound();
    }

    public void BeginRound()
    {
        roundPoints = 0;
        ballsLeftToDrop = ballsPerRound;
        ballsInPlay = 0;
        Debug.Log("Round started. Balls available: " + ballsLeftToDrop);
    }

    // Call this right when you spawn or release a ball from the pool
    public bool TryRegisterBallDrop()
    {
        if (ballsLeftToDrop <= 0) return false;

        ballsLeftToDrop--;
        ballsInPlay++;
        return true;
    }

    // Call this when a ball lands in a bucket
    public void RegisterBallLanded(int pointsFromBucket)
    {
        roundPoints += pointsFromBucket;
        ballsInPlay = Mathf.Max(0, ballsInPlay - 1);

        // Round is complete only when all drops are used AND all dropped balls have landed
        if (ballsLeftToDrop == 0 && ballsInPlay == 0)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        // “Calculate score” step happens here
        totalPoints += roundPoints;

        Debug.Log("Round complete. RoundPoints: " + roundPoints + " TotalPoints: " + totalPoints);

        // Reset for next round
        BeginRound();
    }
}
