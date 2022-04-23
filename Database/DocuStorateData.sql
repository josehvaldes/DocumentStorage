
/**
Document Storage Demo
Author: Jose Valdes
*/
INSERT INTO users(username, password, role) VALUES('root','root123',0);
INSERT INTO users(username, password, role) VALUES('test','123',1);
INSERT INTO groups(name) VALUES('Basic');

INSERT INTO user_groups (user_id, group_id) VALUES(1,1);

INSERT INTO documents (name, category, description, created_on,content) VALUES('Test.txt','txt','default text', Now(),'Demo Test');

INSERT INTO user_documents (user_id, document_id) VALUES (1,1);
INSERT INTO group_documents (group_id, document_id) VALUES (1,1);
