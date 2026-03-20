# =====================================================================
# Script d'Installation Automatisé - Formation .NET 8 Modernization
# Durée estimée : 60-90 minutes (selon débit Internet)
# =====================================================================

# PRÉREQUIS : Exécuter ce script en ADMINISTRATEUR
if (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Host "❌ ERREUR : Ce script doit être exécuté en ADMINISTRATEUR" -ForegroundColor Red
    Write-Host "   Solution : Clic droit sur PowerShell → Exécuter en tant qu'administrateur" -ForegroundColor Yellow
    Exit 1
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "   INSTALLATION ENVIRONNEMENT FORMATION" -ForegroundColor Cyan
Write-Host "   Formation .NET 8 Modernization" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

$installLog = @()
$errorCount = 0

# =====================================================================
# Étape 1 : Déblocage PowerShell
# =====================================================================
Write-Host "[1/9] Configuration PowerShell..." -NoNewline
try {
    Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser -Force
    Write-Host " OK" -ForegroundColor Green
    $installLog += @{Step="PowerShell Policy"; Status="OK"}
} catch {
    Write-Host " ERREUR" -ForegroundColor Red
    Write-Host "      $_" -ForegroundColor DarkRed
    $installLog += @{Step="PowerShell Policy"; Status="ERREUR"}
    $errorCount++
}

# =====================================================================
# Étape 2 : Activation WSL 2
# =====================================================================
Write-Host "`n[2/9] Activation WSL 2..." -NoNewline
try {
    $wslStatus = wsl --list 2>&1
    if ($LASTEXITCODE -ne 0) {
        wsl --install --no-launch
        Write-Host " INSTALLE (Redémarrage requis)" -ForegroundColor Yellow
        $installLog += @{Step="WSL 2"; Status="INSTALLE - REBOOT REQUIS"}
    } else {
        Write-Host " DEJA INSTALLE" -ForegroundColor Green
        $installLog += @{Step="WSL 2"; Status="DEJA INSTALLE"}
    }
} catch {
    Write-Host " ERREUR" -ForegroundColor Red
    Write-Host "      $_" -ForegroundColor DarkRed
    $installLog += @{Step="WSL 2"; Status="ERREUR"}
    $errorCount++
}

# =====================================================================
# Étape 3 : Installation .NET 8 SDK
# =====================================================================
Write-Host "`n[3/9] .NET 8 SDK..." -NoNewline
try {
    $dotnetCheck = dotnet --version 2>&1
    if ($dotnetCheck -match "8\.0\.\d+") {
        Write-Host " DEJA INSTALLE ($dotnetCheck)" -ForegroundColor Green
        $installLog += @{Step=".NET 8 SDK"; Status="DEJA INSTALLE"}
    } else {
        Write-Host " TELECHARGEMENT EN COURS..." -ForegroundColor Yellow
        $dotnetUrl = "https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-8.0.100-windows-x64-installer"
        $dotnetInstaller = "$env:TEMP\dotnet-sdk-8.0-installer.exe"
        Invoke-WebRequest -Uri $dotnetUrl -OutFile $dotnetInstaller -UseBasicParsing
        Start-Process -FilePath $dotnetInstaller -ArgumentList "/quiet /norestart" -Wait
        Remove-Item $dotnetInstaller
        Write-Host " INSTALLE" -ForegroundColor Green
        $installLog += @{Step=".NET 8 SDK"; Status="INSTALLE"}
    }
} catch {
    Write-Host " ERREUR" -ForegroundColor Red
    Write-Host "      Installation manuelle requise : https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Yellow
    $installLog += @{Step=".NET 8 SDK"; Status="ERREUR - MANUEL REQUIS"}
    $errorCount++
}

# =====================================================================
# Étape 4 : Google Chrome (Skip si déjà installé)
# =====================================================================
Write-Host "`n[4/9] Google Chrome..." -NoNewline
$chromePath = "C:\Program Files\Google\Chrome\Application\chrome.exe"
if (Test-Path $chromePath) {
    Write-Host " DEJA INSTALLE" -ForegroundColor Green
    $installLog += @{Step="Chrome"; Status="DEJA INSTALLE"}
} else {
    Write-Host " NON INSTALLE" -ForegroundColor Yellow
    Write-Host "      Installation manuelle recommandée : https://www.google.com/chrome/" -ForegroundColor DarkGray
    $installLog += @{Step="Chrome"; Status="SKIP - MANUEL REQUIS"}
}

# =====================================================================
# Étape 5 : VS Code (Skip si déjà installé)
# =====================================================================
Write-Host "`n[5/9] Visual Studio Code..." -NoNewline
try {
    $codeVersion = code --version 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host " DEJA INSTALLE" -ForegroundColor Green
        $installLog += @{Step="VS Code"; Status="DEJA INSTALLE"}
    } else {
        Write-Host " NON INSTALLE" -ForegroundColor Yellow
        Write-Host "      Installation manuelle requise : https://code.visualstudio.com" -ForegroundColor Yellow
        Write-Host "      IMPORTANT : Cocher 'Add to PATH' pendant l'installation" -ForegroundColor Yellow
        $installLog += @{Step="VS Code"; Status="SKIP - MANUEL REQUIS"}
    }
} catch {
    Write-Host " NON INSTALLE" -ForegroundColor Yellow
    $installLog += @{Step="VS Code"; Status="SKIP - MANUEL REQUIS"}
}

# =====================================================================
# Étape 6 : SQL Server LocalDB
# =====================================================================
Write-Host "`n[6/9] SQL Server LocalDB..." -NoNewline
try {
    $sqlCheck = sqllocaldb info 2>&1
    if ($sqlCheck -match "MSSQLLocalDB") {
        Write-Host " DEJA INSTALLE" -ForegroundColor Green
        $installLog += @{Step="SQL LocalDB"; Status="DEJA INSTALLE"}
    } else {
        Write-Host " NON INSTALLE" -ForegroundColor Yellow
        Write-Host "      Installation manuelle requise : https://www.microsoft.com/sql-server/sql-server-downloads" -ForegroundColor Yellow
        $installLog += @{Step="SQL LocalDB"; Status="SKIP - MANUEL REQUIS"}
    }
} catch {
    Write-Host " NON INSTALLE" -ForegroundColor Yellow
    $installLog += @{Step="SQL LocalDB"; Status="SKIP - MANUEL REQUIS"}
}

# =====================================================================
# Étape 7 : FIX SSD/NVMe (Si SQL Server installé)
# =====================================================================
Write-Host "`n[7/9] Fix SSD/NVMe pour SQL Server..." -NoNewline
$registryPath = "HKLM:\SYSTEM\CurrentControlSet\Services\MSSQLSERVER"
$registryPathExpress = "HKLM:\SYSTEM\CurrentControlSet\Services\MSSQL`$SQLEXPRESS"

if (Test-Path $registryPath) {
    try {
        New-ItemProperty -Path $registryPath -Name "ForcedPhysicalSectorSizeInBytes" -Value 512 -PropertyType DWORD -Force | Out-Null
        Write-Host " OK (MSSQLSERVER)" -ForegroundColor Green
        $installLog += @{Step="Fix SSD"; Status="OK"}
    } catch {
        Write-Host " ERREUR" -ForegroundColor Red
        $installLog += @{Step="Fix SSD"; Status="ERREUR"}
        $errorCount++
    }
} elseif (Test-Path $registryPathExpress) {
    try {
        New-ItemProperty -Path $registryPathExpress -Name "ForcedPhysicalSectorSizeInBytes" -Value 512 -PropertyType DWORD -Force | Out-Null
        Write-Host " OK (SQLEXPRESS)" -ForegroundColor Green
        $installLog += @{Step="Fix SSD"; Status="OK"}
    } catch {
        Write-Host " ERREUR" -ForegroundColor Red
        $installLog += @{Step="Fix SSD"; Status="ERREUR"}
        $errorCount++
    }
} else {
    Write-Host " SKIP (SQL Server non installé)" -ForegroundColor DarkGray
    $installLog += @{Step="Fix SSD"; Status="SKIP"}
}

# =====================================================================
# Étape 8 : Docker Desktop (Skip - Installation manuelle)
# =====================================================================
Write-Host "`n[8/9] Docker Desktop..." -NoNewline
try {
    $dockerVersion = docker --version 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host " DEJA INSTALLE" -ForegroundColor Green
        $installLog += @{Step="Docker Desktop"; Status="DEJA INSTALLE"}
    } else {
        Write-Host " NON INSTALLE" -ForegroundColor Yellow
        Write-Host "      Installation manuelle requise : https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
        Write-Host "      IMPORTANT : Cocher 'Use WSL 2 instead of Hyper-V'" -ForegroundColor Yellow
        $installLog += @{Step="Docker Desktop"; Status="SKIP - MANUEL REQUIS"}
    }
} catch {
    Write-Host " NON INSTALLE" -ForegroundColor Yellow
    $installLog += @{Step="Docker Desktop"; Status="SKIP - MANUEL REQUIS"}
}

# =====================================================================
# Étape 9 : Clone Repository
# =====================================================================
Write-Host "`n[9/9] Clone du repository..." -NoNewline
$devPath = "C:\dev"
$repoPath = "$devPath\net-mod-legacy-formation"

try {
    if (-not (Test-Path $devPath)) {
        New-Item -ItemType Directory -Path $devPath -Force | Out-Null
    }
    
    if (Test-Path $repoPath) {
        Write-Host " DEJA CLONE" -ForegroundColor Green
        $installLog += @{Step="Clone Repo"; Status="DEJA CLONE"}
    } else {
        cd $devPath
        git clone https://github.com/mounirelouali/net-mod-legacy-formation.git 2>&1 | Out-Null
        if (Test-Path $repoPath) {
            Write-Host " OK" -ForegroundColor Green
            $installLog += @{Step="Clone Repo"; Status="OK"}
        } else {
            throw "Clone failed"
        }
    }
} catch {
    Write-Host " ERREUR" -ForegroundColor Red
    Write-Host "      Vérifier : Git installé et repository PUBLIC" -ForegroundColor Yellow
    $installLog += @{Step="Clone Repo"; Status="ERREUR"}
    $errorCount++
}

# =====================================================================
# RAPPORT FINAL
# =====================================================================
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "   RAPPORT D'INSTALLATION" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

Write-Host "`nRésumé par étape :" -ForegroundColor Cyan
foreach ($log in $installLog) {
    $statusColor = switch ($log.Status) {
        {$_ -like "*OK*" -or $_ -like "*INSTALLE*"} { "Green" }
        {$_ -like "*ERREUR*"} { "Red" }
        {$_ -like "*SKIP*"} { "Yellow" }
        default { "Gray" }
    }
    Write-Host "  - $($log.Step): $($log.Status)" -ForegroundColor $statusColor
}

Write-Host "`nActions Manuelles Requises :" -ForegroundColor Yellow
Write-Host "  1. Visual Studio Code : https://code.visualstudio.com" -ForegroundColor DarkGray
Write-Host "     → Installer l'extension 'C# Dev Kit' après installation" -ForegroundColor DarkGray
Write-Host "  2. SQL Server Express : https://www.microsoft.com/sql-server/sql-server-downloads" -ForegroundColor DarkGray
Write-Host "  3. SSMS : https://aka.ms/ssmsfullsetup" -ForegroundColor DarkGray
Write-Host "  4. Docker Desktop : https://www.docker.com/products/docker-desktop" -ForegroundColor DarkGray
Write-Host "  5. Google Chrome : https://www.google.com/chrome/" -ForegroundColor DarkGray

if ($errorCount -eq 0) {
    Write-Host "`nSTATUT : INSTALLATION PARTIELLE REUSSIE" -ForegroundColor Green
    Write-Host "Completez les installations manuelles ci-dessus pour finaliser." -ForegroundColor Yellow
} else {
    Write-Host "`nSTATUT : $errorCount ERREUR(S) DETECTEE(S)" -ForegroundColor Red
    Write-Host "Consultez le rapport ci-dessus pour corriger les erreurs." -ForegroundColor Yellow
}

# Vérifier si redémarrage requis
$rebootNeeded = $installLog | Where-Object { $_.Status -like "*REBOOT REQUIS*" }
if ($rebootNeeded) {
    Write-Host "`n⚠️  REDÉMARRAGE REQUIS pour finaliser WSL 2" -ForegroundColor Yellow -BackgroundColor DarkYellow
    Write-Host "   Redémarrer maintenant ? (Y/N)" -ForegroundColor Yellow -NoNewline
    $response = Read-Host
    if ($response -eq "Y" -or $response -eq "y") {
        Restart-Computer -Force
    }
}

Write-Host "`n========================================`n" -ForegroundColor Cyan
