set -e

echo "Waiting for PostgreSQL to become available..."

until PGPASSWORD=$DB_PASSWORD psql -h "$DB_HOST" -U "$DB_USER" -d "$DB_NAME" -c '\q'; do
  >&2 echo "Postgres is unavailable - sleeping"
  sleep 1
done

>&2 echo "PostgreSQL is up - checking for migrations"

export PATH="$PATH:/root/.dotnet/tools"

dotnet ef database update --project ../Lib.Infrastructure --startup-project .

>&2 echo "Database migrations applied"

>&2 echo "Starting application..."
dotnet Lib.API.dll