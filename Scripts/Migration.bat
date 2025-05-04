@echo off
chcp 65001 > nul
cls

echo Проверка dotnet ef...
dotnet ef --version || (
    echo Ошибка: dotnet ef не установлен. Установите через: 
    echo dotnet tool install --global dotnet-ef
    pause
    exit /b 1
)

echo Удаление старой БД (если существует)...
dotnet ef database drop --force || (
    echo База не найдена или ошибка удаления. Продолжаем...
)

echo Создание миграции InitialCreate...
dotnet ef migrations add InitialCreate

if %errorlevel% neq 0 (
    echo Ошибка! Проверьте:
    echo 1. Вы в папке с .csproj?
    echo 2. Установлен ли Microsoft.EntityFrameworkCore.Design?
    echo 3. Нет ли ошибок в коде моделей?
    pause
    exit /b 1
)

echo Обновление БД...
dotnet ef database update

if %errorlevel% neq 0 (
    echo Ошибка при обновлении БД!
    pause
    exit /b 1
)

echo Миграция и БД готовы.
pause