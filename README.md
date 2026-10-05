# Clipper

Небольшой десктопный помощник в духе старого Microsoft Clippy для Windows 10/11.

Проект написан на C# + WPF и задуман как живой персонаж поверх рабочего стола: он реагирует на активные приложения, периодически моргает, засыпает при бездействии, просыпается при вводе, умеет показывать случайные реплики и сворачиваться в системный трей.

## Возможности

- Always-on-top окно без рамки
- Перетаскивание мышкой
- Comic Sans MS в репликах
- Случайные фразы
- Реакции на активные приложения
- Отдельные реакции для:
  - Visual Studio
  - Unity
  - Tor Browser
  - Telegram
  - Notepad++
  - FL Studio
  - Git / Git Bash
  - qBittorrent
  - PowerShell
  - Windows Terminal
  - cmd
  - VPN-клиентов
  - WireGuard
  - AmneziaVPN
  - Happ
  - и других приложений
- Cooldown между репликами
- Idle-анимация
- Моргание
- Сон после бездействия
- Пробуждение при движении мыши или нажатии клавиш
- Контекстное меню по ПКМ
- Пункт "Что я сейчас вижу?"
- Возврат в правый нижний угол
- Сворачивание в системный трей
- Иконка приложения и трея
- Self-contained публикация в один `.exe`

## Пример поведения

Visual Studio:

> Похоже, ты пытаешься создать баг.

Unity:

> Ты сохранил сцену?

Tor Browser:

> Опять за закладкой собрался, пёс?

VPN:

> Обычный человек нажимает «подключиться». Ты строишь транспортную сеть.

qBittorrent:

> Архивы научных трудов качаем?

## Структура проекта

```text
clipper
├─ Assets
│  ├─ clipper_original.png
│  ├─ clipper_blink.png
│  ├─ clipper_sleep.png
│  └─ clippy.ico
│
├─ Data
│  └─ ReactionLibrary.cs
│
├─ Models
│  └─ AppReaction.cs
│
├─ Services
│  ├─ ActiveWindowService.cs
│  ├─ AnimationService.cs
│  ├─ AppReactionService.cs
│  ├─ BlinkService.cs
│  ├─ ClippyStateService.cs
│  ├─ ImageService.cs
│  ├─ ReactionCooldownService.cs
│  ├─ SleepService.cs
│  └─ TrayService.cs
│
├─ App.xaml
├─ App.xaml.cs
├─ MainWindow.xaml
├─ MainWindow.xaml.cs
└─ clipper.csproj
