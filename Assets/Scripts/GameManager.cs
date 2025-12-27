using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoints;
    public static GameManager GM;

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
    }
}
