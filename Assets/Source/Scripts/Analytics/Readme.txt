Analytic Asset

В данном ассете реализована отправка ряда событий в аналитические ресурсы.
Для использования необходимо создать класс AnalyticManager, в конструкторе которого
создать лист аналитик, реализующих интерфейс IAnalytic, например:

Analytic = new AnalyticManager(new List<IAnalytic>
            {
                new AppMetricaAnalytic(),
                new GameAnalyticsAnalytic()
            });

либо создать Analytic = new AnalyticManager(); с параметрами по умолчанию и добавить аналитики позже. Например:

Analytic.AddAnalytic(new GameAnalyticsAnalytic());
Analytic.AddAnalytic(new AppMetricaAnalytic());
Analytic.AddAnalytic(new YandexMetricaAnalytic());

В ассете реализована отправка данных для Yandex Metrica, в PlayerSettings -> Other Settings в Scripting Define Symbols
необходимо добавить YANDEX_GAMES и YANDEX_METRICA. Так же обязательно нужно импортировать пакет 
https://github.com/forcepusher/com.agava.yandexmetrica

В ассете реализована отправка данных для Game Analytics, в PlayerSettings -> Other Settings в Scripting Define Symbols
необходимо добавить GAME_ANALYTICS. Так же обязательно нужно импортировать и настроить Game Analytics SDK.

В ассете реализована отправка данных для Game Analytics, в PlayerSettings -> Other Settings в Scripting Define Symbols
необходимо добавить APP_METRICA (не забыть про APP_METRICA_TRACK_LOCATION_DISABLED). 
Так же обязательно нужно импортировать и настроить App Metrica SDK.

