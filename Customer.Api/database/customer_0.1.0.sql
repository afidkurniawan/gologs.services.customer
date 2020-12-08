------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- public.company_role definition

-- Drop table

-- DROP TABLE public.company_role;

CREATE TABLE public.company_role (
	id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	role_name varchar(32) NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT company_role_pk PRIMARY KEY (id)
);


-- public.company_type definition

-- Drop table

-- DROP TABLE public.company_type;

CREATE TABLE public.company_type (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	type_name varchar(32) NOT NULL,
    is_tenant_type bool NOT NULL DEFAULT false,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT company_type_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX company_type_type_name_key ON public.company_type USING btree (type_name) WHERE (rowstatus = 0);


-- public."module" definition

-- Drop table

-- DROP TABLE public."module";

CREATE TABLE public."module" (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	module_name varchar(16) NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT module_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX module_module_name_key ON public.module USING btree (module_name) WHERE (rowstatus = 0);


-- public.province definition

-- Drop table

-- DROP TABLE public.province;

CREATE TABLE public.province (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	province_name varchar(25) NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT province_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX province_province_name_key ON public.province USING btree (province_name) WHERE (rowstatus = 0);


-- public.city definition

-- Drop table

-- DROP TABLE public.city;

CREATE TABLE public.city (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	city_name varchar(85) NOT NULL,
	dial_code smallint NOT NULL,
	province_id int NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT city_pk PRIMARY KEY (id),
	CONSTRAINT city_province_id_fk FOREIGN KEY (province_id) REFERENCES province(id)
);
CREATE UNIQUE INDEX city_dial_code_key ON public.city USING btree (dial_code) WHERE (rowstatus = 0);


-- public.company definition

-- Drop table

-- DROP TABLE public.company;

CREATE TABLE public.company (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	company_name varchar(64) NOT NULL,
	company_type_id int NOT NULL,
	broker_company_id int NULL,
	npwp bpchar(15) NOT NULL,
	nib bpchar(13) NULL,
	nik bpchar(8) NULL,
	npppjk bpchar(6) NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT company_pk PRIMARY KEY (id),
	CONSTRAINT company_company_type_id_fk FOREIGN KEY (company_type_id) REFERENCES company_type(id),
    CONSTRAINT company_broker_company_id_fk FOREIGN KEY (broker_company_id) REFERENCES company(id)
);
CREATE UNIQUE INDEX company_npwp_key ON public.company USING btree (npwp) WHERE (rowstatus = 0);


-- public.contract definition

-- Drop table

-- DROP TABLE public.contract;

CREATE TABLE public.contract (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	contract_no varchar(15) NOT NULL,
	start_date date NOT NULL,
	end_date date NOT NULL,
	is_void bool NOT NULL DEFAULT false,
	company_id int NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT contract_pk PRIMARY KEY (id),
	CONSTRAINT contract_company_id_fk FOREIGN KEY (company_id) REFERENCES company(id)
);
CREATE UNIQUE INDEX contract_contract_no_key ON public.contract USING btree (contract_no) WHERE (rowstatus = 0);


-- public.contract_module definition

-- Drop table

-- DROP TABLE public.contract_module;

CREATE TABLE public.contract_module (
	contract_id int NOT NULL,
	module_id int NOT NULL,
	rowstatus smallint NOT NULL DEFAULT 0,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT contract_module_pk PRIMARY KEY (contract_id, module_id),
	CONSTRAINT contract_module_contract_id_fk FOREIGN KEY (contract_id) REFERENCES contract(id),
	CONSTRAINT contract_module_module_id_fk FOREIGN KEY (module_id) REFERENCES module(id)
);


-- public.district definition

-- Drop table

-- DROP TABLE public.district;

CREATE TABLE public.district (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	district_name varchar(64) NOT NULL,
	city_id int NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT district_pk PRIMARY KEY (id),
	CONSTRAINT district_city_id_fk FOREIGN KEY (city_id) REFERENCES city(id)
);
CREATE UNIQUE INDEX district_district_name_key ON public.district USING btree (district_name) WHERE (rowstatus = 0);


-- public.person definition

-- Drop table

-- DROP TABLE public.person;

CREATE TABLE public.person (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	email varchar(256) NOT NULL,
	firstname varchar(64) NOT NULL,
	lastname varchar(64) NULL,
	npwp bpchar(15) NULL,
	company_id int NULL,
	company_role_id smallint NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT person_pk PRIMARY KEY (id),
	CONSTRAINT person_company_id_fk FOREIGN KEY (company_id) REFERENCES company(id),
	CONSTRAINT person_company_role_id_fk FOREIGN KEY (company_role_id) REFERENCES company_role(id)
);
CREATE UNIQUE INDEX person_npwp_key ON public.person USING btree (npwp) WHERE (rowstatus = 0 AND npwp IS NOT NULL);
CREATE UNIQUE INDEX person_email_key ON public.person USING btree (email) WHERE (rowstatus = 0);


-- public.subdistrict definition

-- Drop table

-- DROP TABLE public.subdistrict;

CREATE TABLE public.subdistrict (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	subdistrict_name varchar(64) NOT NULL,
	postal_code smallint NOT NULL,
	district_id int NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT subdistrict_pk PRIMARY KEY (id),
	CONSTRAINT subdistrict_district_id_fk FOREIGN KEY (district_id) REFERENCES district(id)
);
CREATE UNIQUE INDEX subdistrict_postal_code_key ON public.subdistrict USING btree (postal_code) WHERE (rowstatus = 0);


-- public.address definition

-- Drop table

-- DROP TABLE public.address;

CREATE TABLE public.address (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus smallint NOT NULL DEFAULT 0,
	address_name varchar(32) NOT NULL,
	street varchar(256) NOT NULL,
	phone varchar(16) NULL,
	notes varchar(256) NULL,
	subdistrict_id int NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT address_pk PRIMARY KEY (id),
	CONSTRAINT address_subdistrict_id_fk FOREIGN KEY (subdistrict_id) REFERENCES subdistrict(id)
);
CREATE UNIQUE INDEX address_address_name_key ON public.address USING btree (address_name) WHERE (rowstatus = 0);


-- public.company_address definition

-- Drop table

-- DROP TABLE public.company_address;

CREATE TABLE public.company_address (
	company_id int NOT NULL,
	address_id int NOT NULL,
	rowstatus smallint NOT NULL DEFAULT 0,
	is_primary bool NOT NULL DEFAULT true,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT company_address_pk PRIMARY KEY (company_id, address_id),
	CONSTRAINT company_address_address_id_fk FOREIGN KEY (address_id) REFERENCES address(id),
	CONSTRAINT company_address_company_id_fk FOREIGN KEY (company_id) REFERENCES company(id)
);
CREATE UNIQUE INDEX company_address_is_primary_key ON public.company_address USING btree (is_primary) WHERE (rowstatus = 0);


-- public.tenant definition

-- Drop table

-- DROP TABLE public.tenant;

CREATE TABLE public.tenant (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    company_id int NOT NULL,
    tenant_name varchar(128) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT tenant_pk PRIMARY KEY (id),
    CONSTRAINT tenant_company_id_fk FOREIGN KEY (id) REFERENCES public.company(id)
);
CREATE UNIQUE INDEX tenant_name_key ON public.tenant USING btree (tenant_name) WHERE (rowstatus = 0);


-- public.schema_migration definition

-- Drop table

-- DROP TABLE public.schema_migration;

CREATE TABLE public.schema_migration (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    version_major int2 NOT NULL,
    version_minor int2 NOT NULL,
    version_patch int2 NOT NULL,
    db_version varchar(17) GENERATED ALWAYS AS (CAST(version_major AS varchar(5)) || '.' || CAST(version_minor AS varchar(5)) || '.' || CAST(version_patch AS varchar(5))) STORED,
    app_version varchar NOT NULL,
    up_script text NOT NULL,
    down_script text NOT NULL,
    applied timestamp NOT NULL,
    CONSTRAINT schema_migration_pk PRIMARY KEY (id)
);

------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------

INSERT INTO public.company_type
(rowstatus, type_name, created, creator, is_tenant_type)
VALUES (0, 'Go-Logs', CURRENT_TIMESTAMP, 'DEVELOPER', FALSE);

INSERT INTO public.company
(rowstatus,company_name,company_type_id,npwp,nib,created,creator)
VALUES (0, 'PT. GoLogs Aplikasi Indonesia', 1, '000000000000000', '0000000000000', CURRENT_TIMESTAMP, 'ANONYMOUS');

INSERT INTO public.schema_migration
(version_major, version_minor, version_patch, app_version, up_script, down_script, applied)
VALUES (0, 1, 0, '0.1.0', '', '', CURRENT_TIMESTAMP);


UPDATE schema_migration
SET    up_script =
'
------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- public.company_role definition

-- Drop table

-- DROP TABLE public.company_role;

CREATE TABLE public.company_role (
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    role_name varchar(32) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT company_role_pk PRIMARY KEY (id)
);


-- public.company_type definition

-- Drop table

-- DROP TABLE public.company_type;

CREATE TABLE public.company_type (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    type_name varchar(32) NOT NULL,
    is_tenant_type bool NOT NULL DEFAULT false,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT company_type_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX company_type_type_name_key ON public.company_type USING btree (type_name) WHERE (rowstatus = 0);


-- public."module" definition

-- Drop table

-- DROP TABLE public."module";

CREATE TABLE public."module" (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    module_name varchar(16) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT module_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX module_module_name_key ON public.module USING btree (module_name) WHERE (rowstatus = 0);


-- public.province definition

-- Drop table

-- DROP TABLE public.province;

CREATE TABLE public.province (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    province_name varchar(25) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT province_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX province_province_name_key ON public.province USING btree (province_name) WHERE (rowstatus = 0);


-- public.city definition

-- Drop table

-- DROP TABLE public.city;

CREATE TABLE public.city (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    city_name varchar(85) NOT NULL,
    dial_code smallint NOT NULL,
    province_id int NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT city_pk PRIMARY KEY (id),
    CONSTRAINT city_province_id_fk FOREIGN KEY (province_id) REFERENCES province(id)
);
CREATE UNIQUE INDEX city_dial_code_key ON public.city USING btree (dial_code) WHERE (rowstatus = 0);


-- public.company definition

-- Drop table

-- DROP TABLE public.company;

CREATE TABLE public.company (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    company_name varchar(64) NOT NULL,
    company_type_id int NOT NULL,
    broker_company_id int NULL,
    npwp bpchar(15) NOT NULL,
    nib bpchar(13) NULL,
    nik bpchar(8) NULL,
    npppjk bpchar(6) NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT company_pk PRIMARY KEY (id),
    CONSTRAINT company_company_type_id_fk FOREIGN KEY (company_type_id) REFERENCES company_type(id),
    CONSTRAINT company_broker_company_id_fk FOREIGN KEY (broker_company_id) REFERENCES company(id)
);
CREATE UNIQUE INDEX company_npwp_key ON public.company USING btree (npwp) WHERE (rowstatus = 0);


-- public.contract definition

-- Drop table

-- DROP TABLE public.contract;

CREATE TABLE public.contract (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    contract_no varchar(15) NOT NULL,
    start_date date NOT NULL,
    end_date date NOT NULL,
    is_void bool NOT NULL DEFAULT false,
    company_id int NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT contract_pk PRIMARY KEY (id),
    CONSTRAINT contract_company_id_fk FOREIGN KEY (company_id) REFERENCES company(id)
);
CREATE UNIQUE INDEX contract_contract_no_key ON public.contract USING btree (contract_no) WHERE (rowstatus = 0);


-- public.contract_module definition

-- Drop table

-- DROP TABLE public.contract_module;

CREATE TABLE public.contract_module (
    contract_id int NOT NULL,
    module_id int NOT NULL,
    rowstatus smallint NOT NULL DEFAULT 0,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT contract_module_pk PRIMARY KEY (contract_id, module_id),
    CONSTRAINT contract_module_contract_id_fk FOREIGN KEY (contract_id) REFERENCES contract(id),
    CONSTRAINT contract_module_module_id_fk FOREIGN KEY (module_id) REFERENCES module(id)
);


-- public.district definition

-- Drop table

-- DROP TABLE public.district;

CREATE TABLE public.district (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    district_name varchar(64) NOT NULL,
    city_id int NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT district_pk PRIMARY KEY (id),
    CONSTRAINT district_city_id_fk FOREIGN KEY (city_id) REFERENCES city(id)
);
CREATE UNIQUE INDEX district_district_name_key ON public.district USING btree (district_name) WHERE (rowstatus = 0);


-- public.person definition

-- Drop table

-- DROP TABLE public.person;

CREATE TABLE public.person (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    email varchar(256) NOT NULL,
    firstname varchar(64) NOT NULL,
    lastname varchar(64) NULL,
    npwp bpchar(15) NULL,
    company_id int NULL,
    company_role_id smallint NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT person_pk PRIMARY KEY (id),
    CONSTRAINT person_company_id_fk FOREIGN KEY (company_id) REFERENCES company(id),
    CONSTRAINT person_company_role_id_fk FOREIGN KEY (company_role_id) REFERENCES company_role(id)
);
CREATE UNIQUE INDEX person_npwp_key ON public.person USING btree (npwp) WHERE (rowstatus = 0 AND npwp IS NOT NULL);
CREATE UNIQUE INDEX person_email_key ON public.person USING btree (email) WHERE (rowstatus = 0);


-- public.subdistrict definition

-- Drop table

-- DROP TABLE public.subdistrict;

CREATE TABLE public.subdistrict (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    subdistrict_name varchar(64) NOT NULL,
    postal_code smallint NOT NULL,
    district_id int NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT subdistrict_pk PRIMARY KEY (id),
    CONSTRAINT subdistrict_district_id_fk FOREIGN KEY (district_id) REFERENCES district(id)
);
CREATE UNIQUE INDEX subdistrict_postal_code_key ON public.subdistrict USING btree (postal_code) WHERE (rowstatus = 0);


-- public.address definition

-- Drop table

-- DROP TABLE public.address;

CREATE TABLE public.address (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    address_name varchar(32) NOT NULL,
    street varchar(256) NOT NULL,
    phone varchar(16) NULL,
    notes varchar(256) NULL,
    subdistrict_id int NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT address_pk PRIMARY KEY (id),
    CONSTRAINT address_subdistrict_id_fk FOREIGN KEY (subdistrict_id) REFERENCES subdistrict(id)
);
CREATE UNIQUE INDEX address_address_name_key ON public.address USING btree (address_name) WHERE (rowstatus = 0);


-- public.company_address definition

-- Drop table

-- DROP TABLE public.company_address;

CREATE TABLE public.company_address (
    company_id int NOT NULL,
    address_id int NOT NULL,
    rowstatus smallint NOT NULL DEFAULT 0,
    is_primary bool NOT NULL DEFAULT true,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT company_address_pk PRIMARY KEY (company_id, address_id),
    CONSTRAINT company_address_address_id_fk FOREIGN KEY (address_id) REFERENCES address(id),
    CONSTRAINT company_address_company_id_fk FOREIGN KEY (company_id) REFERENCES company(id)
);
CREATE UNIQUE INDEX company_address_is_primary_key ON public.company_address USING btree (is_primary) WHERE (rowstatus = 0);


-- public.tenant definition

-- Drop table

-- DROP TABLE public.tenant;

CREATE TABLE public.tenant (
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    company_id int NOT NULL,
    tenant_name varchar(128) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64) NULL,
    CONSTRAINT tenant_pk PRIMARY KEY (id),
    CONSTRAINT tenant_company_id_fk FOREIGN KEY (id) REFERENCES public.company(id)
);
CREATE UNIQUE INDEX tenant_name_key ON public.tenant USING btree (tenant_name) WHERE (rowstatus = 0);


-- public.schema_migration definition

-- Drop table

-- DROP TABLE public.schema_migration;

CREATE TABLE public.schema_migration (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    version_major int2 NOT NULL,
    version_minor int2 NOT NULL,
    version_patch int2 NOT NULL,
    db_version varchar(17) GENERATED ALWAYS AS (CAST(version_major AS varchar(5)) || ''.'' || CAST(version_minor AS varchar(5)) || ''.'' || CAST(version_patch AS varchar(5))) STORED,
    app_version varchar NOT NULL,
    up_script text NOT NULL,
    down_script text NOT NULL,
    applied timestamp NOT NULL,
    CONSTRAINT schema_migration_pk PRIMARY KEY (id)
);

------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------

INSERT INTO public.company_type
(rowstatus, type_name, created, creator, is_tenant_type)
VALUES (0, ''Go-Logs'', CURRENT_TIMESTAMP, ''DEVELOPER'', FALSE);

INSERT INTO public.company
(rowstatus,company_name,company_type_id,npwp,nib,created,creator)
VALUES (0, ''PT. GoLogs Aplikasi Indonesia'', 1, ''000000000000000'', ''0000000000000'', CURRENT_TIMESTAMP, ''ANONYMOUS'');

INSERT INTO public.schema_migration
(version_major, version_minor, version_patch, app_version, up_script, down_script, applied)
VALUES (0, 1, 0, ''0.1.0'', '''', '''', CURRENT_TIMESTAMP);

'
WHERE db_version = '0.1.0';
