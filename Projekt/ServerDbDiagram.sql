CREATE TABLE `Deamons` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Alias` varchar(255),
  `SourceArgs` varchar(255),
  `IP_Adress` int,
  `Mask` int
);

CREATE TABLE `BackupPresets` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Backup_ID` int,
  `Backup_Type` int,
  `Save_ID` int,
  `Save_Type` int,
  `DefaultSource` varchar(255),
  `TimePeriod` varchar(255),
  `Email` varchar(128)
);

CREATE TABLE `FullBackup` (
  `ID` int PRIMARY KEY AUTO_INCREMENT
);

CREATE TABLE `DiferencialBackup` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Package_Size` int,
  `Package_Count` int
);

CREATE TABLE `IncrementalBackup` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Package_Size` int,
  `Package_Count` int
);

CREATE TABLE `LocalSave` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Destination` varchar(255)
);

CREATE TABLE `NetworkSave` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Destination` varchar(255),
  `IP_Adress` int,
  `Mask` int
);

CREATE TABLE `FTP_Save` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `Destination` varchar(255),
  `Username` varchar(255),
  `Password` varchar(255),
  `IP_adress` int,
  `Mask` int
);

CREATE TABLE `Full_Record` (
  `ID` int PRIMARY KEY AUTO_INCREMENT,
  `User_ID` int,
  `BackupPreset_ID` int,
  `Time` datetime,
  `Error` varchar(512)
);

ALTER TABLE `Full_Record` ADD FOREIGN KEY (`User_ID`) REFERENCES `Deamons` (`ID`);

ALTER TABLE `LocalSave` ADD FOREIGN KEY (`ID`) REFERENCES `BackupPresets` (`Save_ID`);

ALTER TABLE `NetworkSave` ADD FOREIGN KEY (`ID`) REFERENCES `BackupPresets` (`Save_ID`);

ALTER TABLE `FTP_Save` ADD FOREIGN KEY (`ID`) REFERENCES `BackupPresets` (`Save_ID`);

ALTER TABLE `BackupPresets` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `DiferencialBackup` (`ID`);

ALTER TABLE `BackupPresets` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `IncrementalBackup` (`ID`);

ALTER TABLE `BackupPresets` ADD FOREIGN KEY (`Backup_ID`) REFERENCES `FullBackup` (`ID`);

ALTER TABLE `Full_Record` ADD FOREIGN KEY (`BackupPreset_ID`) REFERENCES `BackupPresets` (`ID`);
