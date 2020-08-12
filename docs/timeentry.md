Project Overview
---
AgencyPro is a backend to a business management platform that supports multiple business models.

Terms
---

<dl>
  <dt>Person</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization Person</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Contractor</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization Contractor</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Recruiter</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization Recruiter</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Marketer</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization Marketer</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Project Manager</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization Project Manager</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Account Manager</dt>
  <dd>A physical user of the system, belongs to one more more roles</dd>
<dt>Organization Account Manager</dt>
  <dd>An account manager who belongs to an organization</dd>
</dl>

Company role

Person 
: User of the platform 

OrganizationPerson 
: Person in context of an organization

Contractor 
: A type of freelancer that tracks time and works on projects.

Recruiter
: a type of freelancer that recruits contractors

Marketer
: a type of freelancer that generates leads

Initial Setup
---
1) open solution in VS 2017 Community Edition or higher
2) set startup projects to include the projects you want to test.  You will want to include at a bare minimum `AgencyPro.Identity.Api`


Creating a new Migration
---
1) open `Package Manager Console`
2) setup startup project to `AgencyPro.Data`
3) run `add-migration [Migration Name]`
4) run `update-database` to update your local database


Running Migrations
---
1) open `Package Manager Console`
2) setup startup project to `AgencyPro.Data`
3) run `update-database` to update your local database
