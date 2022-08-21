FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Play.Catelog.Service/Play.Catelog.Service.csproj", "Play.Catelog.Service/"]
COPY ["Caltalog.Contacts/Caltalog.Contacts.csproj", "Caltalog.Contacts/"]
RUN dotnet restore "Play.Catelog.Service/Play.Catelog.Service.csproj"


# COPY . .
# WORKDIR "/src/Play.Catelog.Service"
# RUN dotnet build "Play.Catelog.Service.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "Play.Catelog.Service.csproj" -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app 
# COPY --from=publish /app/out .
# ENTRYPOINT ["dotnet", "Play.Catelog.Service.dll"]