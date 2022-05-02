#postgres images

FROM postgres
ENV POSTGRES_PASSWORD postgres
ENV POSTGRES_DB docustorage

COPY ["DocuStorage.sql", "/docker-entrypoint-initdb.d/"]
COPY ["DocuContent.sql", "/docker-entrypoint-initdb.d/"]
