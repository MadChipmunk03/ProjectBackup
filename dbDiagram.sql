CREATE TABLE `Users` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Name` varchar(255),
  `IP_Adress` varchar(255)
);

CREATE TABLE `Backups` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Backup_ID` int,
  `Backup_Type` int
);

CREATE TABLE `FullBackup` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Time` datetime
);

CREATE TABLE `DiferencialBackup` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Time` datetime,
  `Package_Size` int,
  `Package_Count` int
);

CREATE TABLE `IncrementalBackup` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Time` datetime,
  `Package_Size` int,
  `Package_Count` int
);

CREATE TABLE `Saves` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Save_ID` int,
  `Save_Type` int
);

CREATE TABLE `LocalSave` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Source` varchar(255),
  `Destination` varchar(255)
);

CREATE TABLE `NetworkSave` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Source` varchar(255),
  `Destination` varchar(255),
  `IP_Adress` varchar(255)
);

CREATE TABLE `FTP_Save` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `Source` varchar(255),
  `Destination` varchar(255),
  `Username` varchar(255),
  `Password` varchar(255),
  `IP_adress` varchar(255)
);

CREATE TABLE `Full_Record` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `User_ID` int,
  `Backup_ID` int,
  `Save_ID` int,
  `Time` datetime
);

ALTER TABLE `Full_Record` ADD FOREIGN KEY (`User_ID`) REFERENCES `Users` (`id`);

ALTER TABLE `LocalSave` ADD FOREIGN KEY (`id`) REFERENCES `Saves` (`Save_ID`);

ALTER TABLE `NetworkSave` ADD FOREIGN KEY (`id`) REFERENCES `Saves` (`Save_ID`);

ALTER TABLE `FTP_Save` ADD FOREIGN KEY (`id`) REFERENCES `Saves` (`Save_ID`);

ALTER TABLE `Saves` ADD FOREIGN KEY (`id`) REFERENCES `Full_Record` (`Save_ID`);

ALTER TABLE `Backups` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `DiferencialBackup` (`id`);

ALTER TABLE `Backups` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `IncrementalBackup` (`id`);

ALTER TABLE `Backups` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `FullBackup` (`id`);

ALTER TABLE `Full_Record` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `Backups` (`id`);
