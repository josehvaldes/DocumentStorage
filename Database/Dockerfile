#postgres images

FROM postgres
ENV POSTGRES_PASSWORD postgres
ENV POSTGRES_DB docustorage

COPY ["init-user-db.sh", "/docker-entrypoint-initdb.d/"]
COPY ["DocuStorage.sql", "/docker-entrypoint-initdb.d/"]
COPY ["DocumentContent.sql", "/docker-entrypoint-initdb.d/"]
