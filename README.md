# openroom-on-dapr


```sh
cd  ~/src/dotnet/openroom-on-dapr/; git remote update && git reset --hard @{u}; time git pull; cd ~/src/dotnet/openroom-on-dapr/OpenroomDapr/; time dotnet build; cd  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Server; time rm -r  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Server/bin/; time rm -r  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Server/obj/; time rm -r  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Client/bin/; time rm -r  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Client/obj/; time rm -r  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Shared/bin/; time rm -r  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Shared/obj/; time dotnet publish --configuration Release --os linux --self-contained true --verbosity detailed; time cp  ~/src/dotnet/openroom-on-dapr-openroomdapr-appconfig.json  ~/src/dotnet/openroom-on-dapr/OpenroomDapr/Server/bin/Release/net7.0/linux-x64/publish/appsettings.json; cd /home/kushal/src/dotnet/openroom-on-dapr/OpenroomDapr/Server/bin/Release/net7.0/linux-x64/publish/ && ./OpenroomDapr.Server --urls "https://0.0.0.0:7278;http://0.0.0.0:5220";
```

