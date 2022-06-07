// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

// Общие правила подавления предупреждений для всех проектов
// Подключается во все проекты автоматически

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage(
        "Design", "CA1000:Do not declare static members on generic types",
        Justification = "В правиле описана какая-то ерунда, все с этим ок, никаких проблем", Scope = "module")]

[assembly:
    SuppressMessage(
        "Naming", "CA1716:Identifiers should not match keywords",
        Justification = "Вполне терпимые сложности, ничего страшного", Scope = "module")]

[assembly:
    SuppressMessage(
        "Globalization", "CA1303:Do not pass literals as localized parameters",
        Justification = "Пока не доросли до мультиязычности", Scope = "module")]

[assembly:
    SuppressMessage(
        "Naming", "CA1720:Identifier contains type name",
        Justification = "Срабатывет на безобидных obj, там, где это нормально. Сообщество обсуждает.",
        Scope = "module")]

[assembly:
    SuppressMessage(
        "Performance", "CA1819:Properties should not return arrays",
        Justification = "Фигня, описанных проблем с нормальными разработчиками не бывает", Scope = "module")]

[assembly:
    SuppressMessage(
        "Usage", "CA2234:Pass system uri objects instead of strings",
        Justification = "Довели до маразма, очень неудобно." +
                        " Добавили внутри библиотеки возможность использования Uri - и достаточно," +
                        " а снаружи (в точках вызова) - можно уже и просто строку подавать", Scope = "module")]

[assembly:
    SuppressMessage(
        "Style", "IDE0022:Use expression body for methods",
        Justification = "Не стоит жестко требовать, особенно для методов, которые не просто возвращают значение",
        Scope = "module")]

[assembly:
    SuppressMessage(
        "Style", "IDE0063:Use simple 'using' statement", Justification = "Иногда хочется показать scope using'а явно",
        Scope = "module")]