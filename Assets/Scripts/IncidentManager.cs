using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

public class IncidentManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI situationText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider reputationSlider;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 300f; // 5 minuten
    [SerializeField] private string apiBaseUrl = "https://api.incidentroom.com"; // Vervang met echte API URL

    private float currentTime;
    private bool isGameActive;
    private List<GameObject> activeChoiceButtons = new List<GameObject>();

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    private void InitializeGame()
    {
        currentTime = timeLimit;
        isGameActive = true;
        reputationSlider.value = 100f;
        LoadInitialScenario();
    }

    private async void LoadInitialScenario()
    {
        try
        {
            var response = await APIManager.Instance.GetScenario();
            UpdateUI(response);
        }
        catch (Exception e)
        {
            Debug.LogError($"Fout bij laden scenario: {e.Message}");
            // Fallback naar test scenario
            ShowTestScenario();
        }
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timerText.text = $"Tijd: {Mathf.Floor(currentTime / 60)}:{(currentTime % 60):00}";

        if (currentTime <= 0)
        {
            EndGame();
        }
    }

    private void UpdateUI(ScenarioResponse response)
    {
        situationText.text = response.situation;
        reputationSlider.value = response.reputationScore;

        ClearChoices();
        foreach (var choice in response.availableChoices)
        {
            CreateChoiceButton(choice);
        }
    }

    private void CreateChoiceButton(Choice choice)
    {
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choicesContainer);
        Button button = buttonObj.GetComponent<Button>();
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        buttonText.text = choice.text;
        button.onClick.AddListener(() => OnChoiceSelected(choice.id));

        activeChoiceButtons.Add(buttonObj);
    }

    private void ClearChoices()
    {
        foreach (var button in activeChoiceButtons)
        {
            Destroy(button);
        }
        activeChoiceButtons.Clear();
    }

    private async void OnChoiceSelected(string choiceId)
    {
        try
        {
            var response = await APIManager.Instance.SubmitChoice(choiceId);
            UpdateUI(response);
        }
        catch (Exception e)
        {
            Debug.LogError($"Fout bij verwerken keuze: {e.Message}");
        }
    }

    private void EndGame()
    {
        isGameActive = false;
        // Toon eindscherm met scores
        ShowEndScreen();
    }

    private void ShowEndScreen()
    {
        // TODO: Implementeer eindscherm
    }

    private void ShowTestScenario()
    {
        // Fallback test scenario
        situationText.text = "Er is een verdachte activiteit gedetecteerd op het netwerk. Wat doe je?";
        reputationSlider.value = 100f;

        CreateChoiceButton(new Choice { id = "isolate", text = "Systeem isoleren" });
        CreateChoiceButton(new Choice { id = "investigate", text = "Eerst onderzoeken" });
    }
}