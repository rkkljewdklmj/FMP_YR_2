using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MoneySystem moneySystem;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Optional: persist between scenes
        // DontDestroyOnLoad(gameObject);
    }

    // Add any other GameManager logic here
}
