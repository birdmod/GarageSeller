# # Help from https://stackoverflow.com/a/48599532
# FROM microsoft/dotnet:sdk AS build-env
# WORKDIR /app
# WORKDIR /src

# COPY . ./

# WORKDIR /src/GarageSeller.Api.Models
# RUN dotnet build -c Release -o /app

# WORKDIR /src/GarageSeller.DbContext
# RUN dotnet build -c Release -o /app

# WORKDIR /src/GarageSeller.DbContext.Interfaces
# RUN dotnet build -c Release -o /app

# WORKDIR /src/GarageSeller.Models
# RUN dotnet build -c Release -o /app

# WORKDIR /src/GarageSeller.SampleApi
# RUN dotnet build -c Release -o /app

# FROM build-env
# RUN dotnet publish -c Release -o /app

# FROM microsoft/dotnet:aspnetcore-runtime
# WORKDIR /app
# COPY --from=build-env /app .
# ENTRYPOINT ["dotnet", "GarageSeller.SampleApi.dll"]



# version avec pdb en plus car le build est debug pour les projets intermediaires
FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
WORKDIR /src

COPY . ./

WORKDIR /src/GarageSeller.SampleApi
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "GarageSeller.SampleApi.dll"]
# EXPOSE 5000
# ENV ASPNETCORE_URLS=http://+:5000