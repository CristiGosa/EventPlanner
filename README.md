# EventPlanner

## Aplicatii recomandate pentru deschiderea si rularea proiectelor
   - Visual Studio (proiectul de backend)
   - Visual Studio Code (proiectul de frontend)
   - MySql v8.2
   - npm 10.7.0

## Instructiuni de compilare a proiectului de frontend
   - deschideti folderul "\EventPlanner\angular-app\event-planner\src\app" intr-un terminal
     ![Screenshot_3](https://github.com/CristiGosa/EventPlanner/assets/101999731/63599097-d2b3-4bc6-8bc1-6f953d786fe2)
   - inserati in acesta urmatoarele comenzi: "npm install" si apoi "ng build"

## Instructini de rulare a proiectului de frontend 
   in terminalul deschis anterior introduceti comanda "ng serve -o" pentru a deschide interfata cu utilizatorul intr-o fereastra a browser-ului pe care l-ati setat ca fiind cel default

## Instructiuni de compilare a proiectului de backend
   - in folderul "\EventPlanner\web-api\EventPlanner" se gaseste un fisier cu extensia .sln, dublu-click pe acesta pentru a deschide solutia proiectului in Visual Studio
   - click-dreapta pe solutie si selectati comenzile "Clean Solution" si "Build Solution" 
    <img width="471" alt="Untitled2" src="https://github.com/CristiGosa/EventPlanner/assets/101999731/c0004a49-32fd-4859-a157-e09f813d7290">
   - in partea de sus a compilatorului Visual Studio, selectati Tools > NuGet Package Manager > Package Manager Console
    ![Screenshot_1](https://github.com/CristiGosa/EventPlanner/assets/101999731/eead0647-9cfc-4a91-b962-1556f81ce6a7)
   - in consola, pentru "Default Project" selectati optiunea "EventPlanner.Database
   - introduceti in consola comanda "update-database"

## Instructiuni de rulare a proiectului de backend 
   - click pe butonul de "run", reprezentat de triunghiul verde de sub meniul "Tools" mentionat anterior
     
   ![Screenshot_2](https://github.com/CristiGosa/EventPlanner/assets/101999731/e4c571ec-0aa1-40e6-9a04-542e15a98dfa)

