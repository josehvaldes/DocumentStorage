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
	createdon TIMESTAMP NOT NULL,
	content bytea,
	PRIMARY KEY (id)
);

CREATE TABLE groups(
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	name varchar(50) not null,
	PRIMARY KEY (id)
);



CREATE TABLE usergroups (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	user_id int NULL,
	group_id int NULL,
	PRIMARY KEY (id),
	constraint fk_group 
		foreign KEY(group_id)
			references groups(id) ON DELETE CASCADE,
	constraint fk_user 
		foreign KEY(user_id)
			references users(id) ON DELETE CASCADE
);


CREATE TABLE groupDocuments (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	group_id int NULL,
	document_id int NULL,
	PRIMARY KEY (id),
	constraint fk_group 
		foreign KEY(group_id)
			references groups(id) ON DELETE CASCADE,
	constraint fk_document 
		foreign KEY(document_id)
			references documents(id)  ON DELETE CASCADE
);


CREATE TABLE userDocuments (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	user_id int NULL,
	document_id int NULL,
	PRIMARY KEY (id),
	constraint fk_user 
		foreign KEY(user_id)
			references users(id) ON DELETE CASCADE,
	constraint fk_document 
		foreign KEY(document_id)
			references documents(id) ON DELETE CASCADE
);

CREATE OR REPLACE FUNCTION GetAllUsers()
RETURNS SETOF users
AS $$
SELECT * FROM users;
$$
LANGUAGE SQL;

CREATE OR REPLACE FUNCTION GetUser(uid int)
RETURNS SETOF users
AS $$
SELECT * FROM users WHERE id = uid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION GetUser(uname varchar(50), pswd varchar(50))
RETURNS SETOF users
AS $$
SELECT * FROM users where username = uname AND password = pswd;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION GetDocument(uid int)
RETURNS SETOF documents
AS $$
SELECT id, name, category, description, createdon, content from documents where id = uid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION GetDocuments()
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	createdon TIMESTAMP
)
AS $$
SELECT id, name, category, description, createdon from documents;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION GetGroups()
RETURNS SETOF groups
AS $$
SELECT id, name FROM groups;
$$
LANGUAGE SQL;



CREATE OR REPLACE FUNCTION GetGroupsbyUser(userid int)
RETURNS SETOF GROUPS
AS $$
	SELECT g FROM groups g INNER JOIN usergroups u 
	on g.id = u.group_id 
	where u.user_id =userid;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION GetDocumentsByUserId(userId int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	createdon TIMESTAMP
)
as $$
SELECT d.id, d.name, d.category, d.description, d.createdon from documents d inner join userdocuments ud 
ON d.id = ud.document_id
WHERE ud.user_id = userId;
$$
LANGUAGE SQL;


CREATE OR REPLACE FUNCTION GetDocumentsByGroupId(groupId int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	createdon TIMESTAMP
)
as $$
SELECT d.id, d.name, d.category, d.description, d.createdon from documents d inner join groupdocuments gd 
ON d.id = gd.document_id
where gd.group_id = groupId;
$$
LANGUAGE SQL;



CREATE OR REPLACE FUNCTION getdocsingroupsbyuserid(userId int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	createdon TIMESTAMP,
	groupname varchar(50)
)
as $$
SELECT d.id, d."name" , d.category, d.description, d.createdon, g.name
FROM documents d inner join groupdocuments gd on d.id = gd.document_id  
INNER JOIN usergroups ug on gd.group_id = ug.group_id 
INNER JOIN GROUPS g on g.id = ug.group_id 
where ug.user_id = userId;
$$
LANGUAGE SQL;



CREATE OR REPLACE FUNCTION getalldocsbyuserid(userId int)
RETURNS TABLE (
	id int,
	name varchar(50),
	category varchar(50),
	description varchar(50),
	createdon TIMESTAMP,
	source varchar(50),
	sourcetype varchar(50)
)
as $$

SELECT d.id, d."name" , d.category, d.description, d.createdon, u.username as source, 'USER' as sourcetype
FROM documents d INNER JOIN userdocuments ud on d.id = ud.document_id 
INNER JOIN users u on ud.user_id = u.id 
WHERE ud.user_id = userId
UNION ALL
SELECT d.id, d."name" , d.category, d.description, d.createdon, g.name as source, 'GROUP' as sourcetype
FROM documents d inner join groupdocuments gd on d.id = gd.document_id  
INNER JOIN usergroups ug on gd.group_id = ug.group_id 
INNER JOIN GROUPS g on g.id = ug.group_id 
WHERE ug.user_id = userId 

$$
LANGUAGE SQL;




CREATE OR REPLACE FUNCTION AssignGroupsToUser(userid INT, groups INT[])
RETURNS void 
as $$
DECLARE                                                     
  id INT;
BEGIN
	DELETE FROM usergroups where user_id = userid;
	FOREACH id IN ARRAY groups
	LOOP
		INSERT INTO usergroups (user_id, group_id) values(userid,id);
	END LOOP;
END
$$
LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION ASSIGNDOCUMENTSTOUSER(USERID INT, DOCUMENTS INT[])
RETURNS void 
as $$
DECLARE                                                     
  id INT;
BEGIN
	DELETE FROM userdocuments WHERE user_id = userid;
	foreach id in array documents
	LOOP
		INSERT INTO userdocuments (user_id, document_id) values(userid,id);
	END LOOP;
END
$$
LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION AssignDocumentsToGroup(groupid int, documents int[])
RETURNS void 
as $$
DECLARE                                                     
  id INT;
BEGIN
	DELETE FROM groupdocuments WHERE group_id = groupid;
	FOREACH id in array documents
	LOOP
		INSERT INTO groupdocuments (group_id, document_id) values(groupid,id);
	END LOOP;
END
$$
LANGUAGE plpgsql;
