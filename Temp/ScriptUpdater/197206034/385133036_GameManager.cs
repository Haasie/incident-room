using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float scenarioTimeInMinutes = 15f;
    [SerializeField] private string apiBaseUrl = "https://incident-room-api.onrender.com/api";

    [Header("Game State")]
    public float CurrentTime { get; private set; }
    public float ReputationScore { get; private set; }
    public bool IsGameActive { get; private set; }
    public string CurrentScenario { get; private set; }

    public event Action<float> OnTimeUpdated;
    public event Action<float> OnScoreUpdated;
    public event Action<string> OnScenarioUpdated;
    public event Action OnGameEnded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        CurrentTime = scenarioTimeInMinutes * 60f;
        ReputationScore = 100f;
        IsGameActive = true;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while (IsGameActive && CurrentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            CurrentTime -= 1f;
            OnTimeUpdated?.Invoke(CurrentTime);

            if (CurrentTime <= 0)
            {
                EndGame();
            }
        }
    }

    public async Task<bool> MakeDecision(string decision)
    {
        if (!IsGameActive) return false;

        try
        {
            using (UnityWebRequest request = UnityWebRequest.PostWwwForm($"{apiBaseUrl}/decisions", decision))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var response = JsonUtility.FromJson<IncidentResponse>(request.downloadHandler.text);
                    UpdateGameState(response);
                    return true;
                }
                else
                {
                    Debug.LogError($"API Error: {request.error}");
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error making decision: {e.Message}");
            return false;
        }
    }

    private void UpdateGameState(IncidentResponse response)
    {
        ReputationScore = response.reputationScore;
        CurrentScenario = response.newSituation;

        OnScoreUpdated?.Invoke(ReputationScore);
        OnScenarioUpdated?.Invoke(CurrentScenario);
    }

    private void EndGame()
    {
        IsGameActive = false;
        OnGameEnded?.Invoke();
    }
}

[Serializable]
public class IncidentResponse
{
    public string newSituation;
    public float reputationScore;
    public string[] availableDecisions;
}