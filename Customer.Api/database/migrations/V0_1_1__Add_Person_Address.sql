------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------
ALTER TABLE public.person ADD address_id int4 NULL;
ALTER TABLE public.person ADD CONSTRAINT person_address_id_fk FOREIGN KEY (address_id) REFERENCES public.address(id);
