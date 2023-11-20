# iSun.WeatherForecast
To run the project set startup projects iSun.UI and iSun.WeatherForecast.API
Update Connection string in appsettings.json in API project
To add migrations and update database please view iSun.WeatherForecast.Infrastructure -> HowToAddMigrationAndUpdateDatabase.txt file
For this solution I used onion architecture where inner most layer is Core, outer most is UI.
Backend part uses application and infrastructure layers. If any additional business logic would need to be applied it would be implemented in Infrastructure services.
API is built in such way so that any type of UI could consume it.
API Program.cs has global exception handler to handle exeption in a centralized way and each type of exception is logged
