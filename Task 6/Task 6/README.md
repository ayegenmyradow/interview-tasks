﻿# Task 6 - Ханойская башня

**Цель:** Переместить пирамиду из 8 дисков с одного стержня на другой за минимальное количество ходов, следуя правилам:

1. За один раз можно переместить только один диск.
2. Нельзя помещать больший диск на меньший.

## Особенности:
- Реализация на C#.
- Используется рекурсивный алгоритм.
- Добавлены юнит-тесты на основе NUnit.
- Подсчитывается общее количество ходов.

## Минимальное количество ходов:
Для N дисков: `2^N - 1`.  
Для 8 дисков: `255`.

## Запуск тестов
```bash
dotnet test
