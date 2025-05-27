using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewScenario", menuName = "Incident Room/Scenario")]
public class Scenario : ScriptableObject
{
    [Serializable]
    public class Decision
    {
        public string id;
        public string text;
        public string[] consequences;
        public float reputationImpact;
    }

    [Serializable]
    public class ScenarioState
    {
        public string id;
        public string description;
        public Decision[] availableDecisions;
        public float timeLimit;
    }

    [Header("Scenario Info")]
    public string scenarioName;
    public string description;
    public float totalTimeInMinutes = 15f;

    [Header("Scenario States")]
    public List<ScenarioState> states = new List<ScenarioState>();

    [Header("Post Mortem")]
    public string[] learningPoints;
    public string[] bestPractices;

    public ScenarioState GetInitialState()
    {
        return states.Count > 0 ? states[0] : null;
    }

    public ScenarioState GetStateById(string stateId)
    {
        return states.Find(state => state.id == stateId);
    }

    public Decision GetDecisionById(string decisionId, ScenarioState currentState)
    {
        if (currentState == null) return null;
        return Array.Find(currentState.availableDecisions, decision => decision.id == decisionId);
    }
}