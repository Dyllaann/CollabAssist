FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY ./publish/CollabAssist.API App/

WORKDIR /App

ENTRYPOINT ["dotnet", "CollabAssist.API.dll"]