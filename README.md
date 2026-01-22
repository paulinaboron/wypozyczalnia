
# Wypożyczalnia Sprzętu do Sportów Zimowych

System rezerwacji przeznaczony do kompleksowej obsługi wypożyczalni nart, snowboardów oraz akcesoriów narciarskich. Aplikacja została stworzona w języku C# z wykorzystaniem technologii WPF (GUI) w ramach projektu zaliczeniowego z przedmiotu Programowanie Obiektowe.

## Kluczowe Funkcjonalności

Zarządzanie asortymentem - dodawanie i przeglądanie nart, snowboardów oraz butów z uwzględnieniem parametrów technicznych takich jak rozmiar, cena i rok produkcji.


Rejestracja osób korzystających z usług wraz z ich danymi kontaktowymi.


Możliwość rezerwowania konkretnego sprzętu w określonym przedziale czasowym.


Obsługa  wydania sprzętu oraz proces przyjmowania zwrotów z automatycznym naliczaniem kosztów.


Zapis i odczyt stanu wypożyczalni do plików XML.

## Architektura Systemu
Program opiera się na mechanizmach programowania obiektowego:


Abstrakcja: Klasy bazowe Osoba i SprzetNarciarski definiują wspólne fundamenty dla klientów i wyposażenia.


Polimorfizm: Metoda ObliczKoszt() jest nadpisywana w klasach pochodnych, aby uwzględnić różne progi zniżek dla dzieci (Narty – 20%, Snowboard – 15%, Buty – 10%).


Walidacja: System wykorzystuje autorskie wyjątki (np. NiepoprawnyPeselException, NiepoprawnaRezerwacjaException) do ochrony przed wprowadzeniem błędnych danych.

## Autorki projektu

- Oliwia Gawlik
- Kamila Bal
- Paulina Boroń
- Weronika Czyż

