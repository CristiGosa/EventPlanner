# EventPlanner

## Aplicatii recomandate pentru deschiderea si rularea proiectelor
   - Visual Studio (proiectul de backend)
   - Visual Studio Code (proiectul de frontend)
   - MySql v8.2
   - npm 10.7.0

## Instructiuni de compilare a proiectului de frontend
   - deschideti folderul "\EventPlanner\angular-app\event-planner\src\app" intr-un terminal si inserati urmatoarele comenzi in acesta 
            - npm install
            - ng buld

## Instructini de rulare a proiectului de frontend 
    - in terminalul deschis anterior introduceti comanda "ng serve -o" pentru a deschide interfata cu utilizatorul intr-o fereastra a browser-ului pe care l-ati setat ca fiind cel default

## Instructiuni de compilare a proiectului de backend
    - in folderul "\EventPlanner\web-api\EventPlanner" se gaseste un fisier cu extensia .sln, dublu-click pe acesta pentru a deschide solutia proiectului in Visual Studio
    - click-dreapta pe solutie si selectati comenzile "Clean Solution" si "Build Solution" 
    
    - in partea de sus a compilatorului Visual Studio, selectati Tools > NuGet Package Manager > Package Manager Console
    - in consola, pentru "Default Project" selectati optiunea "EventPlanner.Database
    - introduceti in consola comanda "update-database"

## Instructiuni de rulare a proiectului de backend 
    - click pe butonul de "run", reprezentat de triunghiul verde de sub meniul "Tools" mentionat anterior 