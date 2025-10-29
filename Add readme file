# TodoApp (.NET 8 / Angular 19)
Zadanie rekrutacyjne

## 🛠️ Wymagane narzędzia

Przed uruchomieniem upewnij się, że masz zainstalowane:

* **SDK .NET 8.0**
* **Node.js 18+** (razem z **npm 10+**)
* **Angular CLI 18+** (jeśli nie masz: `npm install -g @angular/cli@18`)
* **PostgreSQL** (działająca lokalna instancja)
* **Narzędzie EF Core CLI**:

```bash
dotnet tool install --global dotnet-ef --version 9.0.10
```

## 🚀 Uruchomienie Backendu

Backend znajduje się w folderze `TodoApp/TodoApp.API`.

### 1. Konfiguracja bazy danych

1.  Upewnij się, że serwer PostgreSQL działa.
2.  Stwórz ręcznie nową, pustą bazę danych (np. o nazwie `todo_db`).
3.  Otwórz plik `TodoApp/TodoApp.API/appsettings.json`.
4.  Zaktualizuj `ConnectionStrings` swoimi danymi (Host, Database, Username, Password).

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=postgres"
    }
    ```

### 2. Migracje bazy danych

Otwórz terminal w folderze `TodoApp/TodoApp.API` i wykonaj komendę, aby utworzyć tabele w bazie danych:

```bash
dotnet ef database update
```

### 3. Start API
W folderze `TodoApp/TodoApp.API` wykonaj polecenia
```bash
dotnet restore
dotnet watch run --urls="http://localhost:5069"
```

---

## 🖥️ Uruchomienie Frontendu

Frontend znajduje się w folderze `TodoApp/TodoApp.Frontend`.

### 1. Konfiguracja adresu API

1.  Otwórz plik `TodoApp/TodoApp.Frontend/src/environments/environment.ts`.
2.  Upewnij się, że `apiUrl` wskazuje na poprawny adres URL Twojego backendu (ten z kroku 3. backendu).

    ```typescript
    export const environment = {
      apiUrl: 'http://localhost:5069/api'
    }
    ```

### 2. Instalacja zależności

Otwórz terminal w folderze `TodoApp/TodoApp.Frontend` i uruchom:

```bash
npm install
```

### 3. Start frontendu
W tym samym folderze (`TodoApp/TodoApp.Frontend`) uruchom serwer
```bash
ng serve -o
```
