diagram of server database: https://dbdiagram.io/d/61fc2a8385022f4ee5362d85

## Projekt ZPR
---

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

