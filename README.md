# The Incident Room - Cybersecurity Simulator

Een educatieve WebGL-applicatie die een cybersecurity-incident simuleert. De speler neemt de rol aan van een CISO (Chief Information Security Officer) en moet beslissingen nemen tijdens een realtime incident.

## Technische Vereisten

- Unity 2022.3 LTS of nieuwer
- TextMeshPro package
- WebGL build support

## Project Setup

1. Clone deze repository
2. Open het project in Unity
3. Installeer de benodigde packages via Package Manager:
   - TextMeshPro
   - WebGL Build Support

## Projectstructuur

```
Assets/
├── Scripts/
│   ├── IncidentManager.cs    # Hoofdlogica van het spel
│   ├── APIManager.cs         # API communicatie
│   └── EndScreenManager.cs   # Eindscherm beheer
├── Prefabs/
│   └── UI/                   # UI prefabs
└── Scenes/
    └── Main.unity           # Hoofdscene
```

## API Integratie

De applicatie communiceert met een REST API voor:
- Laden van scenario's
- Verwerken van keuzes
- Ophalen van feedback

API Endpoints:
- GET /scenario - Laadt het huidige scenario
- POST /choice - Verwerkt een gemaakte keuze

## WebGL Build

1. Open Build Settings (File > Build Settings)
2. Selecteer WebGL als platform
3. Klik op "Switch Platform"
4. Configureer de WebGL settings:
   - Enable "Development Build" voor debugging
   - Set "Compression Format" naar "Disabled" voor development
5. Klik op "Build" en kies een output directory

## Development

Voor lokale ontwikkeling zonder API:
1. Gebruik de test scenario's in IncidentManager.cs
2. Pas de apiBaseUrl aan naar een lokale mock server

## Licentie

MIT License