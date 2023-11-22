# Переконайтеся, що ви вказали правильний шлях до вашого .sln або .csproj файлу

# Очистити попередню збірку
dotnet clean UI/UI.csproj

# Відновлення залежностей NuGet
dotnet restore UI/UI.csproj

# Збірка проекту в релізному режимі
dotnet build UI/UI.csproj --configuration Release

# Публікація проекту в папку для розгортання
dotnet publish UI/UI.csproj --configuration Release --output ./publish
