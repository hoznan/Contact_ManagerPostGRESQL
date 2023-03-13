# Contact_ManagerPostGRESQL
1)Database Creation Script
-- Database: postgres

-- DROP DATABASE IF EXISTS postgres;

CREATE DATABASE postgres
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_India.1252'
    LC_CTYPE = 'English_India.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE postgres
    IS 'default administrative connection database';

2)Table Creation Script
-- Table: public.tbl_contact

-- DROP TABLE IF EXISTS public.tbl_contact;

CREATE TABLE IF NOT EXISTS public.tbl_contact
(
    id integer NOT NULL DEFAULT 'nextval('tbl_contact_id_seq'::regclass)',
    salutation character varying(100) COLLATE pg_catalog."default" NOT NULL,
    firstname character varying(100) COLLATE pg_catalog."default" NOT NULL,
    lastname character varying(100) COLLATE pg_catalog."default" NOT NULL,
    displayname character varying(100) COLLATE pg_catalog."default" NOT NULL,
    birthdate timestamp without time zone,
    creationtimestamp timestamp without time zone NOT NULL DEFAULT 'now()',
    lastchangetimestamp timestamp without time zone NOT NULL DEFAULT 'now()',
    notifyhasbirthdaysoon boolean NOT NULL DEFAULT 'false',
    phonenumber character varying(20) COLLATE pg_catalog."default",
    email character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT tbl_contact_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.tbl_contact
    OWNER to postgres;
