const express = require('express');
const cors = require('cors');
const path = require('path');
require('dotenv').config();

const app = express();
const port = process.env.PORT || 3000;

// Middleware
app.use(cors());
app.use(express.json());

// Laad scenario's
const scenarios = require('./scenarios/ransomware_scenario.json');

// Routes
app.get('/api/scenarios', (req, res) => {
    res.json(scenarios);
});

app.post('/api/decisions', (req, res) => {
    const { decisionId, currentState } = req.body;

    // Simuleer een response
    const response = {
        newSituation: "Nieuwe situatie na beslissing...",
        reputationScore: 85,
        availableDecisions: [
            "Volgende beslissing 1",
            "Volgende beslissing 2"
        ]
    };

    res.json(response);
});

app.get('/api/post-mortem', (req, res) => {
    const postMortem = {
        score: 85,
        learningPoints: scenarios.learningPoints,
        bestPractices: scenarios.bestPractices
    };

    res.json(postMortem);
});

// Start server
app.listen(port, () => {
    console.log(`Server draait op poort ${port}`);
});