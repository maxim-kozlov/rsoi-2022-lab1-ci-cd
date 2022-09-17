FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

WORKDIR /app

COPY PersonsGatewayPublish/ .

# EXPOSE 8080

ENTRYPOINT ["dotnet", "Persons.Gateway.dll"]