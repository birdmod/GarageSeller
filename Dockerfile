# Help from https://stackoverflow.com/a/48599532

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
# EXPOSE 80
# ENV ASPNETCORE_URLS=http://+:80
