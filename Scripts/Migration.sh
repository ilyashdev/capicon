#!/bin/bash
set -e

echo -e "Проверка dotnet ef..."
dotnet ef --version || {
    echo -e "Ошибка: dotnet ef не установлен. Установите через:\n  dotnet tool install --global dotnet-ef"
    exit 1
}

echo -e "Удаление старой БД (если существует)..."
dotnet ef database drop --force || {
    echo "База не найдена или ошибка удаления. Продолжаем..."
}

echo -e "Создание миграции InitialCreate..."
dotnet ef migrations add InitialCreate || {
    echo -e "Ошибка! Проверьте:\n  1. Вы в папке с .csproj?\n  2. Установлен ли Microsoft.EntityFrameworkCore.Design?\n  3. Нет ли ошибок в моделях?"
    exit 1
}

echo -e "Обновление БД..."
dotnet ef database update || {
    echo "Ошибка при обновлении БД!"
    exit 1
}

echo -e "Миграция и БД готовы."