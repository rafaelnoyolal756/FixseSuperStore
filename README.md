This project uses DDD pattern to expose and API that stores Products information on a sqlite DB.
To execute the project first apply migrations and update database substitute the source path 'C:\Users\MyUser\source\repos2' of these commands with the correct source path of your repo to create the sqlite database settled on the appsetting.json file

dotnet ef migrations add InitialCreate -p C:\Users\MyUser\source\repos2\FixseSuperStore\eSuperStore\Inventory.Infrastructure\Inventory.Infrastructure.csproj -s C:\Users\MyUser\source\repos2\FixseSuperStore\eSuperStore\Inventory.API\Inventory.API\Inventory.API.csproj

dotnet ef database update -p C:\Users\MyUser\source\repos2\FixseSuperStore\eSuperStore\Inventory.Infrastructure\Inventory.Infrastructure.csproj -s C:\Users\MyUser\source\repos2\FixseSuperStore\eSuperStore\Inventory.API\Inventory.API\Inventory.API.csproj

Then open eSuperStore.sln with visual studio and verify Inventory.API project is the startup project, if not right click on solution and select properties then in configure startup projects select single startup project select Inventory.API, then press F5 to run the API
