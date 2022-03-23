diagram of server database: https://dbdiagram.io/d/6209356b85022f4ee586b6db

---
Checkpoint 5:
-Implementovat API  s napojením do databáze, security netřeba


Checkpoint 4:
 - udělat deamon z dialogu na page
 - info o proběhnutí u configu a deamona
 - udělat Angula verzi (jen staticky do komponent)
 - opravit chyby

---

## Zadání

- Komerční projekt
- Ústní zadání

- Cíl: Implementovat zálohovací systém
- Zálohování celé firmy
- Různé doby zálohování
- Pouze soubory
- Vždy z lokálního disku

#### Místo zálohy: 
  - a) lokální disk
  - b) síť
  - c) FTP
- Více různých složek na zálohování
- Více možných míst záloh zároveň (stejné či rozdílné)
- Formát: 
  - a) plain text
  - b) .zip

#### Zálohy: 
  - a) Full backup
  - b) Diferenciální backup (rozdíl oproti Full backup) (už teď vím že to bude pain :| )
  - c) Inkrementální záloha (rozdíl oproti poslednímu backupu) 
- Kolníkova poznámka o použití času změny souboru - Dobrý nápad 👍
- Konfigurace rentence záloh - počet uchovaných záloh
- U diferenciální a ikrementální zálohy je třeba uvést dva parametry konfigurace: velikost a počet balíčků
- Jak často? libovolně - nastavení v konfiguraci
- Výroba konfigurací (templatů) zálohování, bude se přiřazovat stanicím
- deamon, a server
- reporty -> PC, druh chyby
- V nastavený čas se zašle e-mailem správci report

#### 3 Aplikace: 
  - a) klient (deamon) - konzolová aplikace, no UI
  - b) server komunikuje s klientem - webové rozhraní, API, konigurace v databázi
  - c) administrační aplikace konfiguruje server

