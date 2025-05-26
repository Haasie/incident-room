using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance { get; private set; }

    [SerializeField] private string apiBaseUrl = "https://incident-room-backend.onrender.com";

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

    public async Task<ScenarioResponse> GetScenario()
    {
        string url = $"{apiBaseUrl}/scenario";
        return await GetRequest<ScenarioResponse>(url);
    }

    public async Task<ScenarioResponse> SubmitChoice(string choiceId)
    {
        string url = $"{apiBaseUrl}/choice";
        var payload = new ChoicePayload { choiceId = choiceId };
        return await PostRequest<ScenarioResponse>(url, payload);
    }

    private async Task<T> GetRequest<T>(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<T>(request.downloadHandler.text);
            }
            else
            {
                throw new Exception($"API Error: {request.error}");
            }
        }
    }

    private async Task<T> PostRequest<T>(string url, object data)
    {
        string jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<T>(request.downloadHandler.text);
            }
            else
            {
                throw new Exception($"API Error: {request.error}");
            }
        }
    }
}

[Serializable]
public class ScenarioResponse
{
    public string situation;
    public float reputationScore;
    public List<Choice> availableChoices;
}

[Serializable]
public class Choice
{
    public string id;
    public string text;
}

[Serializable]
public class ChoicePayload
{
    public string choiceId;
}