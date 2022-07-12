#The following Dockerfile composes a solution containing multiple .NET projects.

# Use ASP.NET 3.1 image as the base.
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]

# This refers to a multi-project .NET Core solution.
# Create /src virtual directory, and copy the following project files into its subdirectories.
# Run 'dotnet' build on the copied projects, and place DLLs in /app/build
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["example.Api/Example.Api.csproj", "Example.Api/"]
COPY ["Example.Integration/Example.Integration.csproj", "Example.Integration/"]
COPY ["Example.Common/Example.Common.csproj", "Example.Common/"]
RUN dotnet restore "Example.Api/Example.Api.csproj"
COPY . .
WORKDIR "/src/Example.Api"
RUN dotnet build "Example.Api.csproj" -c Release -o /app/build

# Copy the DLLs from the /app/build directory to /app/publish.
FROM build AS publish
RUN dotnet publish "Example.Api.csproj" -c Release -o /app/publish

# Finally we run the compiled project, using the 'dotnet run' terminal command, and Example.Api.dll
# as the entry point.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Api.dll"]
RUN dotnet run
