SET PGPASSWORD=3785

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_restore.exe --host 192.168.212.225 --port 5432 --username postgres --dbname "Course4Restore" --verbose "C:\aaa.backup"
