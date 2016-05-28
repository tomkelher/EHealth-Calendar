----
-- phpLiteAdmin database dump (https://bitbucket.org/phpliteadmin/public)
-- phpLiteAdmin version: 1.9.6
-- Exported: 3:28pm on May 22, 2016 (CEST)
-- database file: .\db_agenda_app
----
BEGIN TRANSACTION;

----
-- Table structure for events
----
CREATE TABLE 'events' ('id' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'titel' TEXT NOT NULL, 'omschrijving' TEXT, 'locatie' TEXT, 'van' DATETIME NOT NULL, 'tot' DATETIME NOT NULL);

----
-- Data dump for events, a total of 1 rows
----
INSERT INTO "events" ("id","titel","omschrijving","locatie","van","tot") VALUES ('1','titel','omsch','loc','20/02/2016','30/03/2016');
COMMIT;
