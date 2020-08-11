Initial Setup
---
1) open solution in VS 2017 Community Edition or higher
2) set startup projects to include the projects you want to test.
   - You will be setting multiple *.API* projects to startup at once.

Running Migrations
---
1) open `Package Manager Console`
2) setup startup project to `AgencyPro.Data`
2) run `add-migration [Migration Name]`
3) double check to make sure the migration is correct
4) run `update-database` to update your local database

