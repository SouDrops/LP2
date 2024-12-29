using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    private int score = 0;

    [SerializeField] private int scoreThreshold = 10; // Editable in Inspector

    public PlayerController playerController; // Reference to the PlayerController

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); // Ensure only one instance exists
    }

   void Start()
    {
        UpdateScoreUI(); // Initialize the score display
    }
    public void AddPoints(int points)
    {
        score += points;
        Debug.Log($"Score: {score}");
        UpdateScoreUI();

        if (score >= scoreThreshold)
        {
            TriggerRagdoll();
        }
    }

    private void TriggerRagdoll()
    {
        if (playerController != null)
        {
            playerController.ActivateRagdoll();
        }
    }


    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}