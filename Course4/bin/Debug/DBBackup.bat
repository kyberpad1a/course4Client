SET PGPASSWORD=3785

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_dump.exe --host 192.168.212.225 --port 5432 --username postgres --format custom --blobs --verbose --file "C:\aaa.backup" "Course4"


