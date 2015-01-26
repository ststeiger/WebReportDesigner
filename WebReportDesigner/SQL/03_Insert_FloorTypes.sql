--
-- PostgreSQL database dump
--

-- Dumped from database version 9.0.4
-- Dumped by pg_dump version 9.0.4
-- Started on 2014-04-08 13:22:31

SET statement_timeout = 0;
SET client_encoding = 'WIN1252';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

SET search_path = public, pg_catalog;

DELETE FROM floortype; 
INSERT INTO floortype (floortype_uid, floortype_code, floortype_short_de, floortype_short_fr, floortype_short_it, floortype_short_en, floortype_long_de, floortype_long_fr, floortype_long_it, floortype_long_en, floortype_sort, floortype_multiplicatorno, floortype_mez_sort) VALUES ('da3f7d56-02d4-4ba1-be6b-262c8f8c3983', 'SG', 'SG', 'SM', 'MPB', 'SM', 'Unter-Zwischengeschoss', 'Sous-mezzanine', 'Mezzo piano basso', 'Sub mezzanine', 60, 1, 3);
INSERT INTO floortype (floortype_uid, floortype_code, floortype_short_de, floortype_short_fr, floortype_short_it, floortype_short_en, floortype_long_de, floortype_long_fr, floortype_long_it, floortype_long_en, floortype_sort, floortype_multiplicatorno, floortype_mez_sort) VALUES ('5baf44cc-4d1c-4ea6-91d5-6ac7b4f3a915', 'OG', 'OG', 'SU', 'PS', 'UF', 'Obergeschoss', 'Étage supérieur', 'Piano superiore', 'Upper floor', 20, -1, 2);
INSERT INTO floortype (floortype_uid, floortype_code, floortype_short_de, floortype_short_fr, floortype_short_it, floortype_short_en, floortype_long_de, floortype_long_fr, floortype_long_it, floortype_long_en, floortype_sort, floortype_multiplicatorno, floortype_mez_sort) VALUES ('5c10aff2-5152-4248-a895-6ddc577bb425', 'UG', 'UG', 'SS', 'PST', 'BA', 'Untergeschoss', 'Sous-sol', 'Sotterraneo', 'Basement', 60, 1, 2);
INSERT INTO floortype (floortype_uid, floortype_code, floortype_short_de, floortype_short_fr, floortype_short_it, floortype_short_en, floortype_long_de, floortype_long_fr, floortype_long_it, floortype_long_en, floortype_sort, floortype_multiplicatorno, floortype_mez_sort) VALUES ('80277bef-9d8a-40c7-8dcd-aeebc34b85ac', 'ZG', 'ZG', 'MS', 'MPS', 'ME', 'Ober-Zwischengeschoss', 'Mezzanine supérieur', 'Mezzo piano superiore', 'Mezzanine', 20, -1, 1);
INSERT INTO floortype (floortype_uid, floortype_code, floortype_short_de, floortype_short_fr, floortype_short_it, floortype_short_en, floortype_long_de, floortype_long_fr, floortype_long_it, floortype_long_en, floortype_sort, floortype_multiplicatorno, floortype_mez_sort) VALUES ('621b8f64-ff08-4ad9-af19-d39a2463be79', 'EG', 'EG', 'RC', 'PT', 'GF', 'Erdgeschoss', 'Rez-de-chaussée', 'Pianoterra', 'Ground floor', 40, -1, 2);
INSERT INTO floortype (floortype_uid, floortype_code, floortype_short_de, floortype_short_fr, floortype_short_it, floortype_short_en, floortype_long_de, floortype_long_fr, floortype_long_it, floortype_long_en, floortype_sort, floortype_multiplicatorno, floortype_mez_sort) VALUES ('88e9061a-3883-45a7-86f1-e46e87009136', 'DG', 'DG', 'DG', 'DG', 'DG', 'Dachgeschoss', 'Auvent', 'Attico', 'Roof', 50, 1, 1);
