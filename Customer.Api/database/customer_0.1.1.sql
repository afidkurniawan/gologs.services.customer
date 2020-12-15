------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------
ALTER TABLE public.person ADD address_id int4 NULL;
ALTER TABLE public.person ADD CONSTRAINT person_address_id_fk FOREIGN KEY (address_id) REFERENCES public.address(id);

------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------
INSERT INTO public.schema_migration
(version_major, version_minor, version_patch, app_version, up_script, down_script, applied)
VALUES (0, 1, 1, '0.1.0', '', '', CURRENT_TIMESTAMP);

UPDATE schema_migration
SET    up_script =
'
------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------
ALTER TABLE public.person ADD address_id int4 NULL;
ALTER TABLE public.person ADD CONSTRAINT person_address_id_fk FOREIGN KEY (address_id) REFERENCES public.address(id);

------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------
INSERT INTO public.schema_migration
(version_major, version_minor, version_patch, app_version, up_script, down_script, applied)
VALUES (0, 1, 1, ''0.1.0'', '', '', CURRENT_TIMESTAMP);

'
WHERE db_version = '0.1.1';

UPDATE schema_migration
SET    down_script =
'
------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------
ALTER TABLE public.person DROP CONSTRAINT person_address_id_fk;
ALTER TABLE public.person DROP COLUMN address_id;

------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------
DELETE FROM public.schema_migration
WHERE db_version = ''0.1.1'';

'
WHERE db_version = '0.1.1';
