using clipper.Models;

namespace clipper.Data
{
    public static class ReactionLibrary
    {
        public static readonly string[] GenericPhrases =
        {
            "Похоже, ты пытаешься работать.",
            "А точно надо это делать?",
            "Я бы уже всё удалил.",
            "Похоже, пора сохранить проект.",
            "Я всё вижу.",
            "Это выглядит подозрительно.",
            "Может, лучше сделать бэкап?",
            "Ты уверен, что эта кнопка безопасна?",
            "Я бы на твоём месте нажал Ctrl+S.",
            "Похоже, ты снова что-то сломал.",
            "Не переживай. Я тоже не понимаю, что происходит."
        };

        public static readonly AppReaction[] AppReactions =
        {
            new()
            {
                Name = "Tor Browser",

                // Tor использует firefox.exe,
                // поэтому по имени процесса его отличить трудно.
                ProcessNames = new[] { "firefox" },

                PathKeywords = new[]
                {
                    "Tor Browser"
                },

                Phrases = new[]
                {
                    "Опять за закладкой собрался, пёс?",
                    "Tor открыт. Ну всё, ушёл в подполье.",
                    "Очень анонимно. Очень подозрительно.",
                    "Похоже, обычного интернета тебе снова стало мало.",
                    "Я ничего не видел."
                },

                Probability = 0.75
            },

            new()
            {
                Name = "Visual Studio",
                ProcessNames = new[] { "devenv" },

                Phrases = new[]
                {
                    "Опять Visual Studio? Ну всё, вечер потерян.",
                    "Не забудь Ctrl+S. Я серьёзно.",
                    "Похоже, ты пытаешься создать баг.",
                    "А оно точно компилируется?",
                    "Я бы сначала сделал бэкап."
                }
            },

            new()
            {
                Name = "Unity",
                ProcessNames = new[] { "Unity" },

                Phrases = new[]
                {
                    "MissingReferenceException уже был?",
                    "Похоже, ты опять забыл назначить объект в Inspector.",
                    "У тебя FPS упал или мне кажется?",
                    "Не трогай Rigidbody. Оно и так работает.",
                    "Ты сохранил сцену?"
                }
            },

            new()
            {
                Name = "Notepad++",
                ProcessNames = new[] { "notepad++" },

                Phrases = new[]
                {
                    "Notepad++ открыт. Значит, сейчас будет конфиг.",
                    "Похоже, ты опять правишь JSON руками.",
                    "Главное — не сохранить UTF-8 как ANSI.",
                    "Сейчас будет поиск лишней запятой."
                }
            },

            new()
            {
                Name = "FL Studio",

                ProcessNames = new[]
                {
                    "FL64",
                    "FL"
                },

                Phrases = new[]
                {
                    "FL Studio открыт. Работа официально закончилась.",
                    "Снова 48 дорожек и одна бочка?",
                    "Ты опять крутишь один синт уже сорок минут?",
                    "Этот снейр ты уже час двигаешь на три миллисекунды.",
                    "Сейчас будет export_final_final_REAL.wav?"
                }
            },

            new()
            {
                Name = "qBittorrent",
                ProcessNames = new[] { "qbittorrent" },

                Phrases = new[]
                {
                    "qBittorrent открыт. Архивы научных трудов качаем?",
                    "Скорость хорошая. Содержание я не видел.",
                    "Раздача идёт. Репутация растёт."
                }
            },

            new()
            {
                Name = "Telegram",
                ProcessNames = new[] { "Telegram" },

                Phrases = new[]
                {
                    "Опять Telegram. Работа закончилась?",
                    "Сейчас будет «я на пять минут».",
                    "Надеюсь, это рабочий чат."
                }
            },

            new()
            {
                Name = "Git Bash",
                ProcessNames = new[]
                {
                    "bash",
                    "mintty"
                },

                TitleKeywords = new[]
                {
                    "MINGW",
                    "Git Bash"
                },

                Phrases = new[]
                {
                    "Git Bash. Сейчас будет git status и тревога.",
                    "Главное — не force push в main.",
                    "Похоже, ты опять забыл, в какой ветке находишься.",
                    "git commit -m \"fix final final 2\"?"
                }
            },

            new()
            {
                Name = "GitHub Desktop",
                ProcessNames = new[]
                {
                    "GitHubDesktop"
                },

                Phrases = new[]
                {
                    "GitHub Desktop открыт. Пушим баги в облако?",
                    "Похоже, пора сделать commit.",
                    "Ты точно ту ветку выбрал?"
                }
            },

            new()
            {
                Name = "PowerShell",
                ProcessNames = new[]
                {
                    "powershell",
                    "pwsh"
                },

                Phrases = new[]
                {
                    "PowerShell открыт. Сейчас либо починим, либо добьём.",
                    "Перед Enter ещё можно передумать.",
                    "Похоже, ты опять запускаешь команду из интернета.",
                    "Красный текст уже появился?"
                }
            },

            new()
            {
                Name = "Windows Terminal",
                ProcessNames = new[]
                {
                    "WindowsTerminal"
                },

                Phrases = new[]
                {
                    "Терминал открыт. Сейчас будет команда из интернета.",
                    "Ты её хотя бы прочитал перед Enter?",
                    "Похоже, начинается системное администрирование.",
                    "sudo здесь не поможет. Это Windows."
                }
            },

            new()
            {
                Name = "Command Prompt",
                ProcessNames = new[]
                {
                    "cmd"
                },

                Phrases = new[]
                {
                    "cmd.exe. Олдскул.",
                    "Пахнет ipconfig и отчаянием.",
                    "Главное — не format C:.",
                    "ping 8.8.8.8 — великий диагностический ритуал."
                }
            },

            new()
            {
                Name = "Happ",

                ProcessNames = new[]
                {
                    "Happ",
                    "happ"
                },

                Phrases = new[]
                {
                    "Happ открыт. Опять строим тоннель в свободный интернет?",
                    "VPN включён. Теперь ты технически везде и нигде.",
                    "Похоже, интернету снова нужен костыль."
                },

                Probability = 0.5
            },

            new()
            {
                Name = "AmneziaVPN",

                ProcessNames = new[]
                {
                    "AmneziaVPN",
                    "Amnezia"
                },

                Phrases = new[]
                {
                    "Амнезия включена. Ничего не помню.",
                    "VPN включён. География временно отменяется.",
                    "Ты снова делаешь вид, что находишься не здесь."
                },

                Probability = 0.5
            },

            new()
            {
                Name = "WireGuard",

                ProcessNames = new[]
                {
                    "wireguard"
                },

                Phrases = new[]
                {
                    "WireGuard открыт. Быстро, тихо, подозрительно.",
                    "Пакеты ушли в подполье.",
                    "Туннель поднят. Теперь главное не уронить."
                },

                Probability = 0.5
            }
        };

        public static readonly string[] VpnKeywords =
        {
            "VPN",
            "WireGuard",
            "Amnezia",
            "Happ",
            "OpenVPN",
            "WARP",
            "Hiddify",
            "VLESS",
            "Xray",
            "v2ray"
        };

        public static readonly string[] VpnPhrases =
        {
            "Похоже, ты опять прокладываешь туннель через пол-интернета.",
            "VPN включён. География теперь условность.",
            "Трафик ушёл в подполье.",
            "Интернет пошёл обходным путём.",
            "Пакеты выехали в неизвестном направлении.",
            "Похоже, у нас снова дипломатический конфликт с провайдером.",
            "Туннель поднят. Главное теперь ничего не трогать.",
            "Обычный человек нажимает «подключиться». Ты строишь транспортную сеть."
        };
    }
}