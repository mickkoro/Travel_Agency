# Travel_Agency
Course work on the topic
"Development of a multi-user automated organization management system" 
in the discipline "Software Design"

Now it is difficult to find an area that does not use information technology. These include applications for automating and simplifying the work of employees in the enterprise.
The development of such an application for a travel agency is reflected in this course work.

Installation and launch
1. You need to download the project from github (separately in files or immediately as an archive).
2. Unzip everything into one folder, you can choose any name (but it is preferable to use English to avoid errors).
3. Run the start.bat file to use the application

## Конструирование ПО

## Участники проекта
| Имя | Роль | Telegram |
| :---: | :---: | :---: |
| Кузнецова Юлия | Архитектор, Разработчик, Работа с документами | [@yuliaKUA](https://t.me/yuliaKUA) |
| Чигасова Мария | Архитектор, Тестировщик, Работа с документами | [@marymeeeee](https://t.me/marymeeeee) |

## Описание проблемы

В условиях развития современного общества информационные технологии глубоко проникают жизнь людей. Сейчас трудно найти сферу, в которой не используются информационные технологии. В их числе приложения для автоматизации и упрощения работы сотрудников на предприятии. Разработка такого приложения для туристического бюро отображена в данной курсовой работе.

Для реализации данного проекта были использованы языки программирования SQL и C#. Наше приложение помогает упростить и структурировать работу сотрудников туристического бюро.

## Требования

Необходимо разработать многопользовательскую автоматизированную систему управления организации (Туристического бюро). В приложении должны быть реализованы интерфейс и функционал для каждой роли соответственно (Клиент, Турагент, Менеджер), а также дополнительные формы для работы с данными(обработка туров, туристических групп, продаж).

## Архитектура

Уровень 1: Системная контекстная диаграмма
![Image alt](https://github.com/mickkoro/Travel_Agency/blob/main/report/2.png)

Уровень 2: Схема контейнера
![Image alt](https://github.com/mickkoro/Travel_Agency/blob/main/report/1.png)

Схема базы данных
![Image alt](https://github.com/mickkoro/Travel_Agency/blob/main/report/0.jpg)

## Кодирование и отладка
### Нами были выбраны и использованы следующие инструменты для разработки:

1. Visual Studio Community (2017) Полнофункциональная, расширяемая и бесплатная интегрированная среда разработки для создания современных приложений Android, iOS и Windows, а также веб-приложений и облачных служб.

2. Microsoft SQL Server Management Studio 18

### В качестве СУБД была использована Microsoft SQL Server. Она была выбрана по нескольким причинам:

1. СУБД распространяется ее владельцем под «Универсальной общественной лицензией» или «General Public License» (GNU), которой «снабжаются» все open-source ПО.

2. Высокая скорость обработки данных

3. Поддержка SQL – является еще одной важной «чертой» системы. Это обеспечивает высокий уровень кроссплатформенности данных и кода, созданных с помощью SQL. Благодаря чему вы можете спокойно перенести БД в любую другую современную СУБД, также поддерживающую язык структурированных запросов. А весь сохраненный код (хранимые процедуры, триггеры и запросы) можно применять на любой из этих платформ.

4. Система привилегий – позволяет наделять каждую учетную запись сервера правами на осуществление определенных действий с данными. Причем не только на уровне сервера, БД, но и на уровне отдельных таблиц.

5. Хэширование паролей – обеспечивает высокий уровень «противовзломности». Именно поэтому в SQL восстановить пароль root очень сложно. Так что лучше не забывать его.

### Для разработки данного приложения был использован язык программирования C# 

(платформа .NET Framework 4.6.1) и Windows Forms — интерфейс программирования приложений (API), отвечающий за графический интерфейс пользователя и являющийся частью Microsoft.NET Framework. Данный интерфейс упрощает доступ к элементам интерфейса Microsoft Windows за счет создания обёртки для существующего Win32 API в управляемом коде. потому, что в нем имеются удобные для реализации компоненты, а также множество учебных пособий для изучения языка.

Для каждого вида пользователей приложения реализован персональный функционал с различными правами доступа.

## Тестирование

Во время разработки приложения были протестированы его работоспособность и корректное выполнение всех функций. Файлы с тестами находятся в папке «UnitTestProject».

Тестирование проводилось на двух устройствах. Конфигурация устройств отличалась незначительно, операционная система: Windows 10. Единственная проблема, которая возникала – подключение к серверу SQL (все неполадки были устранены и относились они именно к Microsoft SQL Management Studio, а не к нашему приложению). На обоих устройствах приложение работало исправно и никаких ошибок не возникало. Все функции также прошли тестирование.

| №| Действие | Ожидаемый результат | Статус|
| :---: | :---: | :---: | :---: |
|1 |Пользователь ввел верный логин и пароль |Пользователь перешел на страницу, соответствующую его роли (менеджер, турагент, клиент)| ✔️|
|2| Пользователь ввел неверный логин/пароль |Выводится сообщение об ошибке: «Пользователя с таким логином и паролем не существует. Попробуйте еще раз.»| ✔️|
|3| Клиент нажимает на кнопку «Показать все туры» |Выводится таблица с информацией о турах| ✔️|
|4| Клиент нажимает на кнопку «Показать самые популярные туры»| Выводится таблица с турами, которые покупают больше всего с сортировкой по временам года| ✔️|
|5| Клиент нажимает кнопку «Показать мои группы и туры» |Выводится таблица с информацией о приобретенных турах и группах, где клиент состоит| ✔️|
|6| Клиент нажимает кнопку «Купить»| Выбранный клиентом тур добавляется в таблицу с заказами и в таблицу с приобретенными клиентом турами| ✔️|
|7| Клиент переходит в личный кабинет |Идет переход в следующую форму| ✔️|
|8 |Клиент пытается поменять логин на тот, который уже имеется в базе данных| Выводится сообщение «Пользователь с таким логином уже существует»| ✔️|
|9 |Клиент пытается поменять логин на тот, которого нет в базе данных |Логин успешно изменяется| ✔️|
|10 |Клиент пытается поменять пароль, но неверно вводит старый пароль| Выводится сообщение: «Старый пароль введен неверно!»| ✔️|
|11| Клиент пытается поменять пароль, но старый и новый пароль совпадают |Выводится сообщение: «Новый пароль не может быть как старый»| ✔️|
|12 |Клиент оставил поле «Старый пароль» пустым |Выводится сообщение: «Введите старый пароль»| ✔️|
|13 |Клиент все вводит правильно| Пароль успешно изменяется |✔️|
|14| Клиент нажимает кнопку выйти |Клиент успешно выходит из своего аккаунта| ✔️|

Остальные тесты можно посмотреть в UnitTestProject

Таким образом, этап тестирования показал правильную и безошибочную работу нашего клиентского приложения. Все функции выдавали ожидаемый результат. Поэтому можно сделать вывод, что этап тестирования прошел успешно.

## Установка и запуск

1. Необходимо скачать проект с github (отдельно файлами или сразу архивом).

2. Разархивировать все в одну папку, название можно выбрать любое (но предпочтительнее использовать английский язык, чтобы не возникало ошибок).

3. Запускаем файл start.bat для использования приложения
