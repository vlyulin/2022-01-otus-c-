# OTUS C# Developer 2022-01

Content:
* [Student](#Student)
* [Module hw01 Подключение БД к проекту](#Module-hw01-db)

# Student
`
Student: Vadim Lyulin

## Module hw01 Подключение БД к проекту <a name="Module-hw01-db"></a>
> Цель: научиться создавать базу данных с таблицами, а также писать скрипты наполнения таблиц данными. 
> А так же создавать приложение, способное получать данные из базы и добавлять новые.

1. Создан проект hw01. Путь vlyulin_c#_homeworks\hw01\hw01

2. В VS2022 добавлен Nuget package source (сразу почему-то не было), где расположена библиотека npg для работы с PostgreSQL.
Ссылка: https://api.nuget.org/v3/index.json

![](hw01/imgs/nuget-package-source.png)

3. Добавлена библиотека  PostgreSQL/Npgsql provider for Entity Framework Core.

![](hw01/imgs/manage-nuget-packages.png)
![](hw01/imgs/install-npgsql-ef-package.png)

4. Добавлена библиотека Microsoft.EntityFrameworkCore

5. База данных установлена на heroku

![](hw01/imgs/heroku-db.png)

8. Создано приложение с реализацией отношения many-to-many между сущностями Instructor и Course.

9. Создана миграция "Add-Migration InitialCreate" в Package Manager Console (PMC)
Меню: View > Other Windows > Package Manager Console
```
Install-Package Microsoft.EntityFrameworkCore.Tools
Add-Migration InitialCreate
```
Получаем ошибку
```
PM> Add-Migration Initial
Build started...
Build succeeded.
Your startup project 'hw01' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.
```
Добавлена библиотека EntityFrameworkCore.Design
После этого инициализация прошла нормально
```
PM> Add-Migration Initial
Build started...
Build succeeded.
The Entity Framework tools version '6.0.2' is older than that of the runtime '7.0.0-preview.1.22076.6'. Update the tools for the latest features and bug fixes. See https://aka.ms/AAc1fbw for more information.
To undo this action, use Remove-Migration.
PM> 
```

10. После генерации файла миграции для создания базы данных выполнена команда:
```
Update-Database
```

11. Создан скрипт создания таблиц для приложения hw01\DbScripts\create-tables.sql 

12. Создан скрипт наполнения таблиц hw01\DbScripts\dml.sql

13. Результат работы приложения

![](hw01/imgs/program-output-result.png)
