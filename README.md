# iSun.WeatherForecast

- To run the project, set startup projects iSun.UI and iSun.WeatherForecast.API.
- Update the connection string in the appsettings.json file in the API project.
- To add migrations and update the database, please view `iSun.WeatherForecast.Infrastructure -> HowToAddMigrationAndUpdateDatabase.txt` file.

For this solution, I used the onion architecture where the innermost layer is Core, and the outermost is UI. The backend part uses application and infrastructure layers. Any additional business logic needed is implemented in infrastructure services.

The API is built in such a way that any type of UI could consume it. The APIs project Program.cs have a global exception handler (for backend only) to handle exceptions in a centralized way, and each type of exception is logged with serilog to a file. Currently, it is ignored in git, but after running a project new file should be generated.
