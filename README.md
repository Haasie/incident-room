# The Incident Room

Een educatieve WebGL-applicatie die een cybersecurity-incident simuleert. De speler neemt de rol aan van een CISO (Chief Information Security Officer) en moet beslissingen nemen tijdens een realtime incident.

## Technische Specificaties

- Unity versie: 6000.1.4f1
- Platform: WebGL
- Frontend: Unity WebGL build
- Backend: REST API (Node.js/Express)

## Projectstructuur

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── APIManager.cs
│   │   └── ScenarioManager.cs
│   ├── UI/
│   │   ├── DashboardUI.cs
│   │   ├── TimerUI.cs
│   │   └── ScoreUI.cs
│   └── Models/
│       ├── Scenario.cs
│       └── IncidentResponse.cs
├── Scenes/
│   ├── MainMenu.unity
│   └── IncidentRoom.unity
├── Prefabs/
│   ├── UI/
│   └── Effects/
└── Resources/
    └── Scenarios/
```

## Setup Instructies

1. Clone deze repository
2. Open het project in Unity 6000.1.4f1
3. Open de `Scenes/IncidentRoom.unity` scene
4. Druk op Play om de applicatie te testen

## Development

### Frontend (Unity)
- Gebruik Unity 6000.1.4f1
- Alle webverzoeken via UnityWebRequest
- WebGL-compatibele code (geen threading)

### Backend (Node.js/Express)
- REST API voor scenario management
- Stateless design voor gratis hosting
- JSON responses voor Unity client

## Deployment

### Frontend
1. Build WebGL versie in Unity
2. Deploy naar GitHub Pages

### Backend
1. Deploy naar Render.com (gratis tier)
2. Configureer environment variables

## Licentie

MIT License