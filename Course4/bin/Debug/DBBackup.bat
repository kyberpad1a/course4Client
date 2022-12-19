SET PGPASSWORD=3785

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_dump.exe --host 192.168.73.225 --port 5432 --username postgres --format custom --blobs --verbose --file "C:\1234.backup" "Course4"


