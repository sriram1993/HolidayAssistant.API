#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 8081

FROM microsoft/dotnet:2.2-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["HolidayAssistant.Flights/HolidayAssistant.Flights.csproj", "HolidayAssistant.Flights/"]
RUN dotnet restore "HolidayAssistant.Flights/HolidayAssistant.Flights.csproj"
COPY . .
WORKDIR "/src/HolidayAssistant.Flights"
RUN dotnet build "HolidayAssistant.Flights.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HolidayAssistant.Flights.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HolidayAssistant.Flights.dll"]