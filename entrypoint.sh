set -e

until PGPASSWORD=1111 psql -h "postgres" -U "admin" -d "LibraryWeb" -c '\q'; do
  >&2 echo "Postgres is unavailable - sleeping"
  sleep 1
done

>&2 echo "Postgres is up - executing migrations"
dotnet ef database update

>&2 echo "Starting application"
dotnet Lib.API.dll 