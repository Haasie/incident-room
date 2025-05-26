const express = require('express');
const cors = require('cors');
const app = express();
const port = process.env.PORT || 3000;

app.use(cors());
app.use(express.json());

// Simpele scenario data
const scenarios = {
    initial: {
        situation: "Er is een verdachte activiteit gedetecteerd op het netwerk. Wat doe je?",
        reputationScore: 100,
        availableChoices: [
            { id: "isolate", text: "Systeem isoleren" },
            { id: "investigate", text: "Eerst onderzoeken" }
        ]
    },
    after_isolate: {
        situation: "Je hebt het systeem geïsoleerd. De directie vraagt om uitleg over de downtime.",
        reputationScore: 85,
        availableChoices: [
            { id: "explain", text: "Uitleg geven over de situatie" },
            { id: "ignore", text: "Negeren en doorgaan met onderzoek" }
        ]
    },
    after_investigate: {
        situation: "Je onderzoek wijst op een mogelijke phishing poging. Wat is je volgende stap?",
        reputationScore: 90,
        availableChoices: [
            { id: "alert", text: "Waarschuw alle medewerkers" },
            { id: "contain", text: "Beperk toegang tot gevoelige systemen" }
        ]
    }
};

// Routes
app.get('/scenario', (req, res) => {
    res.json(scenarios.initial);
});

app.post('/choice', (req, res) => {
    const { choiceId } = req.body;

    // Simpele logica voor scenario verloop
    if (choiceId === 'isolate') {
        res.json(scenarios.after_isolate);
    } else if (choiceId === 'investigate') {
        res.json(scenarios.after_investigate);
    } else {
        res.json(scenarios.initial);
    }
});

app.listen(port, () => {
    console.log(`Server draait op poort ${port}`);
});