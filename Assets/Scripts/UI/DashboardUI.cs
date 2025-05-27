using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DashboardUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scenarioText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform decisionButtonsContainer;
    [SerializeField] private GameObject decisionButtonPrefab;
    [SerializeField] private Slider reputationSlider;
    [SerializeField] private GameObject postMortemPanel;
    [SerializeField] private TextMeshProUGUI postMortemText;

    private List<GameObject> activeDecisionButtons = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.OnTimeUpdated += UpdateTimer;
        GameManager.Instance.OnScoreUpdated += UpdateScore;
        GameManager.Instance.OnScenarioUpdated += UpdateScenario;
        GameManager.Instance.OnGameEnded += ShowPostMortem;

        postMortemPanel.SetActive(false);
        UpdateInitialUI();
    }

    private void UpdateInitialUI()
    {
        UpdateTimer(GameManager.Instance.CurrentTime);
        UpdateScore(GameManager.Instance.ReputationScore);
        UpdateScenario(GameManager.Instance.CurrentScenario);
    }

    private void UpdateTimer(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        timerText.text = $"Tijd: {minutes:00}:{seconds:00}";
    }

    private void UpdateScore(float score)
    {
        scoreText.text = $"Reputatie: {score:F0}%";
        reputationSlider.value = score / 100f;
    }

    private void UpdateScenario(string scenario)
    {
        scenarioText.text = scenario;
    }

    public void UpdateDecisionButtons(string[] decisions)
    {
        // Verwijder oude knoppen
        foreach (var button in activeDecisionButtons)
        {
            Destroy(button);
        }
        activeDecisionButtons.Clear();

        // Maak nieuwe knoppen
        foreach (var decision in decisions)
        {
            GameObject buttonObj = Instantiate(decisionButtonPrefab, decisionButtonsContainer);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = decision;
            button.onClick.AddListener(() => OnDecisionButtonClicked(decision));

            activeDecisionButtons.Add(buttonObj);
        }
    }

    private async void OnDecisionButtonClicked(string decision)
    {
        bool success = await GameManager.Instance.MakeDecision(decision);
        if (!success)
        {
            // Toon foutmelding aan gebruiker
            Debug.LogWarning("Kon beslissing niet verwerken");
        }
    }

    private void ShowPostMortem()
    {
        postMortemPanel.SetActive(true);
        // Haal post mortem data op van de API
        // postMortemText.text = "Post mortem analyse...";
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTimeUpdated -= UpdateTimer;
            GameManager.Instance.OnScoreUpdated -= UpdateScore;
            GameManager.Instance.OnScenarioUpdated -= UpdateScenario;
            GameManager.Instance.OnGameEnded -= ShowPostMortem;
        }
    }
}