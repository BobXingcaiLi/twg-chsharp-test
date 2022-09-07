# twg-chsharp-test

Finished:
- Used a sln to manage multiple projects
- Extracted the business logic to service layer
- Used a log service to wrap the SQL operation (DB access could be refected to DB context layer further in the future. This way the log service would look nicer). 
- Added Automapper to make the data display as expected
- Used DI for the required services
- Used Middleware to log the general request (Only knew S for search, not sure what for price, guess P)

Not finished:
- Unit test / integration test. No time to do.  But it would be using moq and fluentassert to do some assertions.
- Exception middleware not done. No exception handle either.
