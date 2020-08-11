IdeaFortune
===

- [Bios](docs/bios.md)
- [Roadmap](docs/readme.md)
- [Pricing Model](docs/misc/pricing.md)
- [Actors](docs/misc/actors.md)

## Development Standards
- [C# Standards](docs/standards/c-sharp.md)
- [TS Standards](docs/standards/ts.md)
- [EF Standards](docs/standards/ef.md)
- [Security Standards](docs/standards/sec.md)
- [Design Standards](docs/standards/des.md)
- [Testing Standards](docs/standards/test.md)
----
Initial Setup
---
1) open solution in VS 2017 Community Edition or higher
2) set startup projects to include the projects you want to test.
   - You will be setting multiple *.API* projects to startup at once.

Postman
---
You should have access to companionship repositiory for postman tests.  

Running Migrations
---
1) open `Package Manager Console`
2) setup startup project to `Backend/IdeaFortune.Data`
2) run `add-migration [Migration Name]`
3) double check to make sure the migration is correct
4) run `update-database` to update your local database

