# Todo Project

This example was created to illustrate several scenarios encounter in mobile application development.  These include:
* Authentication
* Realtime communication
* Offline Sync

The project conists of:
* Aspnet Core 2.0 WebApi that generates and Authenticates JWT (bearer token).  Included in an extension to enable Swagger to pass the token during testing.  The database is an embedded Sqlite middleware implementation for quick development. SignlR is also enabled for duplex communication
* Mobile App demonstrates offline sync and SignalR in order to refresh datastore.  JWT token is used in the header of the HttpClient.
* WebClient is an Angular 5 frontend


