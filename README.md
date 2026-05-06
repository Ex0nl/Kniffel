# INFORMATION
 **Ex0nl, Lyvt-Dev, lavender1510 - 05.06.2026**
Benutzt:

 - Visual Studio 2022 Community Edition (Windows Forms, C# .NET 6.0)
 - https://www.flaticon.com (Für die Bild-Ressourcen der Würfel)

# INSTALLATION
**Windows (PowerShell)**
> 1. Downloaden der Zip Datei:

    Invoke-WebRequest -Uri "https://github.com/Ex0nl/Kniffel/releases/download/v1.0-Final-Release/Kniffel-Main-Release-Winx86_64-net6.0.zip" -OutFile "Kniffel.zip"
    
> 2. Extrahieren

    Expand-Archive -Path "Kniffel.zip" -DestinationPath ".\Kniffel"

> 3. Clean-Up

    Remove-Item "Kniffel.zip"

> oder

    iwr "URL" -OutFile "K.zip"; Expand-Archive "K.zip" -Dest "Kniffel"; rm "K.zip"

**Linux/macOS (Shell) | Nicht-Kompatibel**
> 1. Downloaden der Zip Datei:

    url -L -o Kniffel.zip "https://github.com/Ex0nl/Kniffel/releases/download/v1.0-Final-Release/Kniffel-Main-Release-Winx86_64-net6.0.zip"
    
> 2. Extrahieren

    unzip Kniffel.zip -d Kniffel

> 3. Clean-Up

    rm Kniffel.zip

> oder

    curl -L "URL" -o K.zip && unzip K.zip -d Kniffel && rm K.zip


# BENUTZEROBERFLÄCHE
![Kniffel Benutzeroberfläche](https://i.imgur.com/q1p1ayj.png)
