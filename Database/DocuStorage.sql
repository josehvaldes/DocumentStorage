/**
Document Storage Demo
Author: Jose Valdes
*/
CREATE TABLE users(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	username VARCHAR(50) UNIQUE NOT NULL,
	password VARCHAR(50) NOT NULL,
	role int not null,
	PRIMARY KEY (id)
);

CREATE TABLE documents(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	name varchar(50) not null,
	category varchar(50) not null,
	description varchar(50),
	created_on TIMESTAMP NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE groups(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	name varchar(50) not null,
	PRIMARY KEY (id)
);



CREATE TABLE user_groups (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	user_id int NULL,
	group_id int NULL,
	PRIMARY KEY (id),
	CONSTRAINT fk_group 
		FOREIGN KEY(group_id)
			REFERENCES GROUPS(id) ON DELETE CASCADE,
	CONSTRAINT fk_user 
		FOREIGN KEY(user_id)
			REFERENCES users(id) ON DELETE CASCADE
);

CREATE INDEX user_group_user_idx ON user_groups(user_id);
CREATE INDEX user_group_group_idx ON user_groups(group_id);

CREATE TABLE group_documents (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	group_id int NULL,
	document_id int NULL,
	PRIMARY KEY (id),
	CONSTRAINT fk_group 
		FOREIGN KEY(group_id)
			REFERENCES GROUPS(id) ON DELETE CASCADE,
	CONSTRAINT fk_document 
		FOREIGN KEY(document_id)
			REFERENCES documents(id)  ON DELETE CASCADE
);

CREATE INDEX group_doc_gidx ON group_documents(group_id);
CREATE INDEX group_doc_didx ON group_documents(document_id);		


CREATE TABLE user_documents (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	user_id int NULL,
	document_id int NULL,
	PRIMARY KEY (id),
	CONSTRAINT fk_user 
		FOREIGN KEY(user_id)
			REFERENCES users(id) ON DELETE CASCADE,
	CONSTRAINT fk_document 
		FOREIGN KEY(document_id)
			REFERENCES documents(id) ON DELETE CASCADE
);

CREATE INDEX user_doc_idx ON user_documents(user_id);

CREATE INDEX user_doc_didx ON user_documents(document_id);		


CREATE OR REPLACE FUNCTION get_all_users()
RETURNS SETOF users
AS $$
SELECT * FROM users;
$$
LANGUAGE SQL;

CREATE OR REPLACE FUNCTION get_user(uid int)
RETURNS SETOF users
AS $$
SELECT * FROM users WHERE id = uid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION get_user(uname varchar(50), pswd varchar(50))
RETURNS SETOF users
AS $$
SELECT * FROM users where username = uname AND password = pswd;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION get_document(uid int)
RETURNS SETOF documents
AS $$
SELECT id, name, category, description, created_on from documents where id = uid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION get_documents()
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	created_on TIMESTAMP
)
AS $$
SELECT id, name, category, description, created_on from documents;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION get_groups()
RETURNS SETOF groups
AS $$
SELECT id, name FROM groups;
$$
LANGUAGE SQL;



CREATE OR REPLACE FUNCTION get_groups_by_user(uid int)
RETURNS SETOF GROUPS
AS $$
	SELECT g FROM groups g INNER JOIN user_groups u 
	ON g.id = u.group_id 
	WHERE u.user_id = uid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION get_documents_by_user_id(uid int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	created_on TIMESTAMP
)
as $$
SELECT d.id, d.name, d.category, d.description, d.created_on from documents d inner join user_documents ud 
ON d.id = ud.document_id
WHERE ud.user_id = uid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION get_documents_by_group_id(gid int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	created_on TIMESTAMP
)
as $$
SELECT d.id, d.name, d.category, d.description, d.created_on from documents d inner join group_documents gd 
ON d.id = gd.document_id
where gd.group_id = gid;
$$
LANGUAGE SQL;



CREATE OR REPLACE FUNCTION get_docs_in_groups_by_user_id(uid int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	created_on TIMESTAMP,
	groupname varchar(50)
)
as $$
SELECT d.id, d.name , d.category, d.description, d.created_on, g.name
FROM documents d inner join group_documents gd on d.id = gd.document_id  
INNER JOIN user_groups ug on gd.group_id = ug.group_id 
INNER JOIN GROUPS g on g.id = ug.group_id 
where ug.user_id = uid;
$$
LANGUAGE SQL;



CREATE OR REPLACE FUNCTION get_all_docs_by_user_id(uid int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	created_on TIMESTAMP,
	source varchar(50),
	source_type varchar(50)
)
as $$

SELECT d.id, d.name , d.category, d.description, d.created_on, u.username as source, 'USER' as source_type
FROM documents d INNER JOIN user_documents ud on d.id = ud.document_id 
INNER JOIN users u on ud.user_id = u.id 
WHERE ud.user_id = uid
UNION ALL
SELECT d.id, d."name" , d.category, d.description, d.created_on, g.name as source, 'GROUP' as source_type
FROM documents d inner join group_documents gd on d.id = gd.document_id  
INNER JOIN user_groups ug on gd.group_id = ug.group_id 
INNER JOIN GROUPS g on g.id = ug.group_id 
WHERE ug.user_id = uid 

$$
LANGUAGE SQL;

CREATE OR REPLACE FUNCTION assign_groups_to_user(uid INT, groups INT[])
RETURNS void 
as $$
BEGIN
	DELETE FROM user_groups where user_id = uid;
	INSERT INTO user_groups (user_id, group_id) 
	SELECT uid, UNNEST(groups) ON CONFLICT DO NOTHING;
END
$$
LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION assign_documents_to_user(uid INT, documents INT[])
RETURNS void 
as $$
BEGIN
	DELETE FROM user_documents WHERE user_id = uid;
	INSERT INTO user_documents (user_id, document_id) 
	SELECT uid, UNNEST (documents) ON CONFLICT DO NOTHING;
END
$$
LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION assign_documents_to_group(gid int, documents int[])
RETURNS void 
as $$
BEGIN
	DELETE FROM group_documents WHERE group_id = gid;
	INSERT INTO group_documents (group_id, document_id) 
	SELECT gid, UNNEST (documents) ON CONFLICT DO NOTHING;
END
$$
LANGUAGE plpgsql;
