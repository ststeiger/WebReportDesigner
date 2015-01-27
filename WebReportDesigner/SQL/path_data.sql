

-- Table: t_paths

-- DROP TABLE t_paths;

CREATE TABLE t_paths
(
  path_id bigint NOT NULL,
  real_path_id bigint NOT NULL,
  name character varying(255),
  typ character varying(20),
  parent_path_id bigint,
  CONSTRAINT pk_t_paths PRIMARY KEY (path_id)
);



--
-- PostgreSQL database dump
--

-- Dumped from database version 9.3.5
-- Dumped by pg_dump version 9.3.5
-- Started on 2015-01-27 08:31:07 CET

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

SET search_path = public, pg_catalog;

--
-- TOC entry 1934 (class 0 OID 24682)
-- Dependencies: 170
-- Data for Name: t_paths; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (3, 3, 'Folder 2', 'Folder', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (2, 2, 'Folder 1', 'Folder', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (4, 4, 'Folder 3', 'Folder', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (5, 5, 'File 3.1', 'File', 4);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (6, 6, 'File 3.2', 'File', 4);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (8, 4, 'SymLink 1', 'SYML', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (9, 9, 'Folder 4', 'Folder', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (10, 10, 'File 0.1', 'File', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (11, 11, 'File 0.2', 'File', 1);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (1, 1, 'root', 'Folder', 0);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (7, 7, 'Folder 3.3', 'Folder', 4);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (12, 12, 'Folder 3.3.1', 'Folder', 7);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (13, 13, 'File 3.3.1.1', 'File', 12);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (14, 14, 'File 4.1', 'File', 9);
INSERT INTO t_paths (path_id, real_path_id, name, typ, parent_path_id) VALUES (15, 15, 'File 4.2', 'File', 9);


-- Completed on 2015-01-27 08:31:08 CET

--
-- PostgreSQL database dump complete
--

