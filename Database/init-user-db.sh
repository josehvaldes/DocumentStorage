#init-user-db.sh
#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE USER docker;
	DROP DATABASE ID EXISTS docustorage;
	DROP DATABASE ID EXISTS docucontent;
	CREATE DATABASE docustorage;
	CREATE DATABASE docucontent;
	GRANT ALL PRIVILEGES ON DATABASE docustorage TO docker;
	GRANT ALL PRIVILEGES ON DATABASE docucontent TO docker;
EOSQL