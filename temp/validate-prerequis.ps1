# =====================================================================
# Script de Validation Prérequis Formation .NET 8 - COMPLET (5 JOURS)
# Usage: Copier-coller ce bloc complet dans PowerShell
# =====================================================================

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "   VALIDATION ENVIRONNEMENT FORMATION" -ForegroundColor Cyan
Write-Host "   (Programme 5 Jours Complet)" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

$checks = @()

# ---- Vérification Git ----
Write-Host "[1/6] Git..." -NoNewline
try {
    $gitVersion = git --version 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host " OK" -ForegroundColor Green
        Write-Host "      Version: $gitVersion" -ForegroundColor Gray
        $checks += @{Tool="Git"; Status="OK"; Version=$gitVersion; Required="Jours 1-5"}
    } else {
        throw "Commande échouée"
    }
} catch {
    Write-Host " MANQUANT" -ForegroundColor Red
    Write-Host "      Action requise: Installer Git depuis https://git-scm.com" -ForegroundColor Yellow
    $checks += @{Tool="Git"; Status="MANQUANT"; Version="N/A"; Required="Jours 1-5"}
}

# ---- Vérification .NET 8 SDK ----
Write-Host "`n[2/6] .NET 8 SDK..." -NoNewline
try {
    $dotnetSdks = dotnet --list-sdks 2>&1 | Out-String
    if ($dotnetSdks -match "8\.0\.\d+") {
        $net8Version = ($dotnetSdks -split "`n" | Where-Object {$_ -match "8\.0\.\d+"} | Select-Object -First 1).Trim()
        Write-Host " OK" -ForegroundColor Green
        Write-Host "      Version: $net8Version" -ForegroundColor Gray
        $checks += @{Tool=".NET 8 SDK"; Status="OK"; Version=$net8Version; Required="Jours 1-5"}
    } else {
        throw ".NET 8 introuvable"
    }
} catch {
    Write-Host " MANQUANT" -ForegroundColor Red
    Write-Host "      Action requise: Installer .NET 8 SDK depuis https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    $checks += @{Tool=".NET 8 SDK"; Status="MANQUANT"; Version="N/A"; Required="Jours 1-5"}
}

# ---- Vérification Visual Studio Code (MULTI-SOURCES) ----
Write-Host "`n[3/6] Visual Studio Code..." -NoNewline
$vscodeFound = $false
$vscodeVersion = "N/A"
$vscodeMethod = ""

# Méthode 1: Tester la commande `code` dans le PATH
try {
    $codeVersion = code --version 2>&1 | Select-Object -First 1
    if ($LASTEXITCODE -eq 0) {
        $vscodeFound = $true
        $vscodeVersion = $codeVersion
        $vscodeMethod = "PATH"
    }
} catch {}

# Méthode 2: Vérifier les emplacements d'installation standards
if (-not $vscodeFound) {
    $possiblePaths = @(
        "$env:LOCALAPPDATA\Programs\Microsoft VS Code\Code.exe",
        "$env:ProgramFiles\Microsoft VS Code\Code.exe",
        "${env:ProgramFiles(x86)}\Microsoft VS Code\Code.exe"
    )
    
    foreach ($path in $possiblePaths) {
        if (Test-Path $path) {
            $vscodeFound = $true
            $vscodeVersion = (Get-Item $path).VersionInfo.ProductVersion
            $vscodeMethod = "Fichier"
            break
        }
    }
}

# Méthode 3: Vérifier le registre Windows
if (-not $vscodeFound) {
    $regPaths = @(
        "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*",
        "HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*",
        "HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\*"
    )
    
    foreach ($regPath in $regPaths) {
        $apps = Get-ItemProperty $regPath -ErrorAction SilentlyContinue | Where-Object { $_.DisplayName -like "*Visual Studio Code*" }
        if ($apps) {
            $vscodeFound = $true
            $vscodeVersion = $apps[0].DisplayVersion
            $vscodeMethod = "Registre"
            break
        }
    }
}

if ($vscodeFound) {
    Write-Host " OK" -ForegroundColor Green
    Write-Host "      Version: $vscodeVersion (detection: $vscodeMethod)" -ForegroundColor Gray
    $checks += @{Tool="VS Code"; Status="OK"; Version=$vscodeVersion; Required="Jours 1-5"}
} else {
    Write-Host " MANQUANT" -ForegroundColor Red
    Write-Host "      Action requise: Installer VS Code depuis https://code.visualstudio.com" -ForegroundColor Yellow
    $checks += @{Tool="VS Code"; Status="MANQUANT"; Version="N/A"; Required="Jours 1-5"}
}

# ---- Vérification Accès Réseau NuGet ----
Write-Host "`n[4/6] Accès NuGet (Pare-feu)..." -NoNewline
try {
    $response = Invoke-WebRequest -Uri "https://api.nuget.org/v3/index.json" -UseBasicParsing -TimeoutSec 5 -ErrorAction Stop
    if ($response.StatusCode -eq 200) {
        Write-Host " OK" -ForegroundColor Green
        Write-Host "      Connexion NuGet operationnelle (HTTP $($response.StatusCode))" -ForegroundColor Gray
        $checks += @{Tool="Accès NuGet"; Status="OK"; Version="HTTP 200"; Required="Jours 1-5"}
    } else {
        throw "HTTP $($response.StatusCode)"
    }
} catch {
    Write-Host " BLOQUE" -ForegroundColor Red
    Write-Host "      Action requise: Verifier pare-feu / proxy entreprise" -ForegroundColor Yellow
    $checks += @{Tool="Accès NuGet"; Status="BLOQUE"; Version="N/A"; Required="Jours 1-5"}
}

# ---- Vérification Docker Desktop ----
Write-Host "`n[5/6] Docker Desktop..." -NoNewline
$dockerFound = $false
$dockerVersion = "N/A"

try {
    $dockerVer = docker --version 2>&1
    if ($LASTEXITCODE -eq 0) {
        $dockerFound = $true
        $dockerVersion = $dockerVer
        
        # Vérifier que Docker daemon est en cours d'exécution
        $dockerInfo = docker info 2>&1
        if ($LASTEXITCODE -ne 0) {
            Write-Host " INSTALLE MAIS ARRETE" -ForegroundColor Yellow
            Write-Host "      Version: $dockerVersion" -ForegroundColor Gray
            Write-Host "      Action requise: Demarrer Docker Desktop" -ForegroundColor Yellow
            $checks += @{Tool="Docker Desktop"; Status="ARRETE"; Version=$dockerVersion; Required="Jour 4"}
        } else {
            Write-Host " OK" -ForegroundColor Green
            Write-Host "      Version: $dockerVersion" -ForegroundColor Gray
            $checks += @{Tool="Docker Desktop"; Status="OK"; Version=$dockerVersion; Required="Jour 4"}
        }
    } else {
        throw "Docker non trouve"
    }
} catch {
    Write-Host " MANQUANT" -ForegroundColor Red
    Write-Host "      Action requise: Installer Docker Desktop depuis https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
    Write-Host "      Note: Requis uniquement pour Jour 4 (Conteneurisation)" -ForegroundColor DarkGray
    $checks += @{Tool="Docker Desktop"; Status="MANQUANT"; Version="N/A"; Required="Jour 4"}
}

# ---- Vérification WSL 2 (Windows Subsystem for Linux) ----
Write-Host "`n[6/6] WSL 2 (Linux sur Windows)..." -NoNewline
$wslFound = $false
$wslVersion = "N/A"

try {
    $wslList = wsl --list --verbose 2>&1 | Out-String
    if ($wslList -match "VERSION\s+2") {
        $wslFound = $true
        $wslDistros = ($wslList -split "`n" | Where-Object {$_ -match "\*?\s+\w+.*\s+2"}).Count
        $wslVersion = "WSL 2 active ($wslDistros distribution(s))"
        Write-Host " OK" -ForegroundColor Green
        Write-Host "      $wslVersion" -ForegroundColor Gray
        $checks += @{Tool="WSL 2"; Status="OK"; Version=$wslVersion; Required="Jour 4"}
    } else {
        throw "WSL 2 non detecte"
    }
} catch {
    Write-Host " MANQUANT" -ForegroundColor Red
    Write-Host "      Action requise: Installer WSL 2 avec commande: wsl --install" -ForegroundColor Yellow
    Write-Host "      Note: Requis pour Jour 4 (Tests Linux + Docker)" -ForegroundColor DarkGray
    $checks += @{Tool="WSL 2"; Status="MANQUANT"; Version="N/A"; Required="Jour 4"}
}

# ---- Rapport Final ----
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "   RAPPORT FINAL" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

$okCount = ($checks | Where-Object {$_.Status -eq "OK"}).Count
$totalCount = $checks.Count
$jours13Count = ($checks | Where-Object {$_.Required -eq "Jours 1-5" -and $_.Status -eq "OK"}).Count
$jour4Count = ($checks | Where-Object {$_.Required -eq "Jour 4" -and $_.Status -eq "OK"}).Count

Write-Host "`nStatut Global: " -NoNewline
if ($okCount -eq $totalCount) {
    $statusMsg = "ENVIRONNEMENT PRET COMPLET ($okCount/$totalCount)"
    Write-Host $statusMsg -ForegroundColor Green -BackgroundColor DarkGreen
    Write-Host "`nVous pouvez demarrer la formation 5 jours sans probleme." -ForegroundColor Green
} else {
    $statusMsg = "CONFIGURATION PARTIELLE ($okCount sur $totalCount)"
    Write-Host $statusMsg -ForegroundColor Yellow -BackgroundColor DarkYellow
    
    # Détails par jour
    if ($jours13Count -eq 4) {
        Write-Host "`n[OK] Jours 1-3 : PRET (Git, .NET 8, VS Code, NuGet)" -ForegroundColor Green
    } else {
        Write-Host "`n[KO] Jours 1-3 : INCOMPLET" -ForegroundColor Red
    }
    
    if ($jour4Count -eq 2) {
        Write-Host "[OK] Jour 4 : PRET (Docker, WSL 2)" -ForegroundColor Green
    } else {
        Write-Host "[!!] Jour 4 : INCOMPLET (Docker et/ou WSL 2 manquants)" -ForegroundColor Yellow
        Write-Host "     Note: Vous pouvez commencer les Jours 1-3, installer Docker/WSL avant Jour 4" -ForegroundColor DarkGray
    }
}

Write-Host "`nDetails par Outil:" -ForegroundColor Cyan
foreach ($check in $checks) {
    $statusColor = switch ($check.Status) {
        "OK" { "Green" }
        "ARRETE" { "Yellow" }
        default { "Red" }
    }
    $line = "  - $($check.Tool): $($check.Status) ($($check.Version)) [$($check.Required)]"
    Write-Host $line -ForegroundColor $statusColor
}

Write-Host "`n========================================`n" -ForegroundColor Cyan