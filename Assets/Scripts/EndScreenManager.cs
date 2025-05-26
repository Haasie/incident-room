using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        endScreenPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowEndScreen(float finalScore, string feedback)
    {
        endScreenPanel.SetActive(true);
        finalScoreText.text = $"Eindscore: {finalScore:F0}%";
        feedbackText.text = feedback;
    }

    private void RestartGame()
    {
        endScreenPanel.SetActive(false);
        // Herstart het spel via de IncidentManager
        FindObjectOfType<IncidentManager>().RestartGame();
    }
}