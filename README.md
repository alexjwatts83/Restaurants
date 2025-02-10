# Restaurants

## EF Migrations

`dotnet ef migrations add InitialCreate -s Restaurants.API -p Restaurants.Infrastructure`

`dotnet ef database update -s Restaurants.API -p Restaurants.Infrastructure`

`dotnet ef migrations remove -s Restaurants.API -p Restaurants.Infrastructure`

`dotnet ef database drop -s Restaurants.API -p Restaurants.Infrastructure`

## Seq

Links

- https://hovermind.com/serilog/logging-to-sink.html

To view the actual events in seq

- http://localhost:81

## Azure

### Web App

Nice option is to view advanced settings

- Navigate to web service
- Under Development Tools --> Advanced tool. Click on the go button to bring you to a new web page

### Problem

*Basic authentication is disabled* when trying to **Download publish profile**

### fix

- Under settings | Configuration | "General settings", Ensure that both SCM Basic Auth Publishing and FTP Basic Auth Publishing are "ON". --> Save
- Navigate back to "Overview" --> STOP web app.
- (Publishing Profile should be come available now)
- Restart your application




## Other

- *optional* - Add json viewer extension in chrome https://chromewebstore.google.com/detail/json-viewer/gbmdgpbipfallnflgajpaliibnhdgobh

