CREATE TABLE document_contents (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	ref_id int unique not null,
	content bytea,
	PRIMARY KEY (id)
);

CREATE INDEX document_idx ON document_contents(ref_id);

CREATE OR REPLACE FUNCTION get_document_content(did int)
RETURNS TABLE(content bytea)
AS $$
SELECT content FROM document_contents WHERE ref_id = did;
$$
LANGUAGE SQL;
