#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat


FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/DicewareCore.Cli/DicewareCore.Cli.csproj", "src/DicewareCore.Cli/"]
RUN dotnet restore "src/DicewareCore.Cli/DicewareCore.Cli.csproj"
COPY . .
WORKDIR "src/DicewareCore.Cli"
RUN dotnet build "DicewareCore.Cli.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DicewareCore.Cli.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DicewareCore.Cli.dll"]