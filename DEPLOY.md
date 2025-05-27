# Deploy Instructies voor The Incident Room

## 1. Backend Deployen naar Render.com

### 1.1 Voorbereiding
```bash
# Navigeer naar de backend directory
cd backend

# Installeer dependencies
npm install

# Test de applicatie lokaal
npm run dev
```

### 1.2 GitHub Repository Setup
```bash
# Initialiseer git repository
git init

# Voeg alle bestanden toe
git add .

# Commit de wijzigingen
git commit -m "Initial backend commit"

# Voeg de remote repository toe (vervang [jouw-username] met je GitHub username)
git remote add origin https://github.com/[jouw-username]/incident-room-api.git

# Push naar GitHub
git push -u origin main
```

### 1.3 Render.com Setup
1. Ga naar [Render.com](https://render.com) en log in
2. Klik op "New +" en selecteer "Web Service"
3. Verbind met je GitHub repository
4. Configureer de service:
   - Name: `incident-room-api`
   - Environment: `Node`
   - Region: `Frankfurt` (of dichtstbijzijnde regio)
   - Branch: `main`
   - Build Command: `npm install`
   - Start Command: `node server.js`
   - Plan: `Free` (of betaald plan indien nodig)

### 1.4 Environment Variables
Voeg de volgende environment variables toe in Render.com:
- `NODE_ENV`: `production`
- `PORT`: `3000`

### 1.5 CORS Configuratie
Update `server.js` met de juiste CORS configuratie:
```javascript
app.use(cors({
    origin: [
        'https://[jouw-username].github.io',
        'http://localhost:3000',
        'http://localhost:8080'  // Unity WebGL development server
    ],
    methods: ['GET', 'POST'],
    credentials: true
}));
```

## 2. Frontend Deployen naar GitHub Pages

### 2.1 Unity Build Settings
1. Open Unity en ga naar `File > Build Settings`
2. Selecteer platform: `WebGL`
3. Configureer Player Settings:
   - Resolution: `1920x1080`
   - WebGL Template: `Default`
   - Compression Format: `Disabled` (voor development)
   - Publishing Settings:
     - Compression Format: `Gzip`
     - Data Caching: `Enabled`

### 2.2 Build Process
1. Klik op `Build`
2. Kies een map (bijv. `webgl-build`)
3. Wacht tot de build klaar is
4. Controleer de build output op errors

### 2.3 GitHub Pages Setup
1. Maak een nieuwe repository aan op GitHub:
   - Naam: `incident-room-webgl`
   - Public repository
   - Initialiseer met README

2. Push de build naar GitHub:
```bash
# Navigeer naar de build directory
cd webgl-build

# Initialiseer git
git init

# Voeg alle bestanden toe
git add .

# Commit
git commit -m "Initial WebGL build"

# Voeg remote toe
git remote add origin https://github.com/[jouw-username]/incident-room-webgl.git

# Push
git push -u origin main
```

3. Configureer GitHub Pages:
   - Ga naar repository settings
   - Scroll naar "GitHub Pages"
   - Bij "Source", kies "main" branch
   - Klik "Save"

## 3. Verificatie en Testing

### 3.1 Backend Testing
```bash
# Test de API endpoints
curl https://incident-room-api.onrender.com/api/scenarios
curl -X POST https://incident-room-api.onrender.com/api/decisions -H "Content-Type: application/json" -d '{"decisionId": "test", "currentState": "test"}'
curl https://incident-room-api.onrender.com/api/post-mortem
```

### 3.2 Frontend Testing
1. Open je GitHub Pages URL
2. Controleer de browser console voor errors
3. Test de volledige flow:
   - Scenario laden
   - Beslissingen maken
   - Timer functionaliteit
   - Score berekening
   - Post-mortem rapport

### 3.3 Cross-Origin Testing
1. Test de applicatie op verschillende browsers:
   - Chrome
   - Firefox
   - Safari
   - Edge
2. Controleer CORS headers in browser developer tools
3. Verifieer dat alle API calls succesvol zijn

## 4. Monitoring en Onderhoud

### 4.1 Render.com Monitoring
1. Controleer dagelijks:
   - CPU gebruik
   - Geheugengebruik
   - Response tijden
   - Error logs

2. Stel alerts in voor:
   - Hoge CPU/geheugengebruik
   - Frequente errors
   - Lange response tijden

### 4.2 GitHub Pages Monitoring
1. Controleer deployment status
2. Monitor build logs
3. Verifieer asset loading

### 4.3 Backup Procedures
1. Maak wekelijkse backups van:
   - Scenario bestanden
   - API logs
   - Build artifacts
2. Bewaar backups op een veilige locatie

## 5. Troubleshooting

### 5.1 Backend Issues
1. API niet bereikbaar:
   - Controleer Render.com status
   - Verifieer environment variables
   - Check CORS configuratie

2. Performance problemen:
   - Monitor resource gebruik
   - Check voor memory leaks
   - Optimaliseer database queries

### 5.2 Frontend Issues
1. WebGL niet laadt:
   - Controleer browser compatibiliteit
   - Verifieer asset loading
   - Check build settings

2. API connectie problemen:
   - Controleer CORS headers
   - Verifieer API URL
   - Test netwerk connectie

## 6. Security Best Practices

### 6.1 API Security
1. Implementeer rate limiting
2. Voeg basis authenticatie toe
3. Gebruik HTTPS voor alle calls
4. Valideer alle input

### 6.2 Frontend Security
1. Sanitize user input
2. Implementeer CSP headers
3. Gebruik secure cookies
4. Voeg XSS bescherming toe

## 7. Performance Optimalisatie

### 7.1 Backend Optimalisatie
1. Implementeer caching
2. Optimaliseer database queries
3. Gebruik compression
4. Monitor response tijden

### 7.2 Frontend Optimalisatie
1. Comprimeer assets
2. Implementeer lazy loading
3. Optimaliseer WebGL build
4. Cache statische resources

## 8. Update Procedures

### 8.1 Backend Updates
```bash
# Lokale wijzigingen
git add .
git commit -m "Update backend"
git push

# Render.com zal automatisch opnieuw deployen
```

### 8.2 Frontend Updates
1. Update in Unity
2. Nieuwe WebGL build maken
3. Test lokaal
4. Push naar GitHub:
```bash
git add .
git commit -m "Update frontend"
git push
```

## 9. Contact en Support

### 9.1 Noodcontacten
- Render.com Support: support@render.com
- GitHub Support: support@github.com
- Unity Support: support@unity3d.com

### 9.2 Documentatie
- [Render.com Documentation](https://render.com/docs)
- [GitHub Pages Documentation](https://docs.github.com/en/pages)
- [Unity WebGL Documentation](https://docs.unity3d.com/Manual/webgl.html)