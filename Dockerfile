FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY out/ App/

WORKDIR /App

ENTRYPOINT ["dotnet", "CollabAssist.API.dll"]