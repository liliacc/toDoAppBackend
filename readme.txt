rm -rf bin; dotnet publish -r linux-arm -c Release
scp -C bin/Release/netcoreapp2.0/linux-arm/publish/toDoAppBackend.dll host:toDoAppBackend/publish/
sudo service kestrel-toDoAppBackend restart