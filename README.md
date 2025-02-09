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

## Other

- *optional* - Add json viewer extension in chrome https://chromewebstore.google.com/detail/json-viewer/gbmdgpbipfallnflgajpaliibnhdgobh

