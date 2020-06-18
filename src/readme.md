# Original Instructions

## Product/Project Requirements (initial MVP)
- A user should be able to interact with a scrollable and pageable list of their business contacts, as well as adding, editing, and deleting their contacts
- Each business contact record contains basic information such as email, name and address.
- The application should access a simple database through a RESTful service.
- The UI is undefined.
- The application must support a minimum of 20,000 users daily with each user having roughly ~1,000 contact records.
- The application is an MVP, but the data cannot be lost.

## Technical Requirements
- A modern javaScript UI framework of choice (Vue, React, Angular, etc)
- An HTTP-based API layer (.Net Core 3.1, doesn't need to be SSL for the purposes of this test)
- A database backend (candidate's discretion, but should reflect a production-based solution)
  - Use YOUR preferred method for DB communication (e.g. DB/SQL client, an ORM, etc.)

# Ubiquitous Language
- Basic information: Commonly used information about a business contact. In this application, this is defined as email, name and address.
- Business contact: A person or entity that a User has, or wants, to do business with.
- Business contact record: A group of information, owned by a user, that represents information about a business contact.
- Contact record: see Business contact record.
- Contacts: see Business contact.
- Record: see Business contact record.
- User: A user is a website user. They own business contact records.

# MVP Feature Backlog (Funcitional requirements)
- [x] Create a login screen for a user
- [x] Create a dashboard with a list of contact records
- [x] Allow the list of contact records to be paged
- [x] Allow the list of contact records to be scrolled
- [ ] Allow adding a contact record
- [ ] Allow editing a contact record
- [ ] Allow deleting a contact record
- [x] Performance testing to validate MVP is capable of handling the required traffic

# MVP Non-functional Requirements
- HTTP based API layer (interoperability, maintainability)
- Modern javaScript UI framework (interoperability, maintainability, usability)
- 20,000 users daily with ~1,000 contact records each (scalability, capacity, performance)
- data cannot be lost (data integrity, data retention)

# Decision Log
- Integrating a user identity system that is actually secure, and is able to be set up without putting secrets in source control, does not seem reasonable given the constraints on this project.  As such, the project will assume that user separation is required, but that integration into an existing (or new) user identity system would be done at a later time.
- The project will assume that there is a single address for each business contact record, given that the initial MVP does not give any hints that this is otherwise (even though in the real world multiple addresses are common)
- The project will assume that 'simple database' means "as simple as reasonble" as long as the goals for performance and scalability are met.
- Addresses are often modelled as separate database entities, however this project will use the concept of 'table splitting'. As there is an exact 1-to-1 relationship between Business Contact Records and Addresses and choosing this direction simplifies the data access code.  Choosing otherwise at this stage implies YAGNI and more complex code.
- SQL Server 2019 express has be the chosen database system.
- Dapper has been chosen as the data access method of choice.  This is primarily due to conversations with Matthew about the use of EF as a default.
- For the sake of simplicity (and honestly time), the project will not implement common features such as creation/modification time tracking, soft deletes, etc.
- native server compression will be used for responses, rather than the slower and more resource intensive .net core middleware. This means that responses won't be compressed when running in development.
- The clustered index on the records table will be a composite key of the fields (UserId, Id) in that order.  This is an optimization to limit the impact of list operations inside of multi-tenant databases.  Under this scheme, all records for a particular user are located together in consecutive pages.  This reduces the impact of physical reads when serving list operations, as it reduces the likelyhood that multiple users will have row data in a single page, which would increase the raw number of pages needed to be read in order to fulfil a query.
  - For example, if an average row is 512 bytes in size, then a page can hold ~16 records.  Under this scheme, the likelyhood of each of those ~16 records belonging to one user is very high.  Under a schema where the clustered index is solely on the id, and with a user count in the 20k+ range, then the likelyhood of a page containing more than one record is extremely low. Thus for a user with 1000 records, those records are located on ~63 physical pages, vs being located on ~1000 physical pages.
  - Note that the tradeoff is that in some cases, page splits will happen when rows are inserted into nearly full pages.  However, this is mitigated by having appropriate fill factors and doing continuing online index reorganization, which are practices that should be done on a regular basis anyways.
- The project will use Next.js (based on React) and Material UI as the modern UI framework
  - The primary developers are most familiar with react, have never worked with Next.js before, and wish to highlight the quality of work when working within a new technology with no experience and limited time to prepare.
- Some features (primarily updating records) were omitted due to a lack of time.  The API does support the actions, but hooks for the app were not completed.

# Informal Performance Analysis

## Assumptions
average user spends 5 minutes on a call with a business contact
average user spends 30 seconds when a contact cannot be contacted
average user calls each user on their contact list in order
average user spends 4 hours in the application, out of a normal 8 hour work day
users are located evenly over the 4 US time zones.
average user makes contact with 1 out of 5 contact
average updates per successful contact call is 1 in 10
average user creates 10 contacts per day
a page of results from the paginator is 100 records
20,000 active users per day implies some number of inactive users, whose data is still present in the system.  100,000 total users was used for testing.

## Napkin calculations

For one page of contacts, there are 20 successful phone calls, over about 100 minutes.
For each successful call, there are 2.1 requests on average.  One request to get the customer details, one reques to go back to the list page, and 1/10th chance to do an update request.

Given the above, successful phone calls average about 42 requests per 100 minutes or 0.007 req/s per user.
For 20,000 users, this translates to approxiamately 140 req/s for the successful call workflows

In addition to the above, we can estimate that in the worst case scenario for the call each contact on a page workflow, when no phone calls are successful, the user will navigate through a page of results in 50 minutes (2 contacts per minute).
This is about 0.0003 req/s.  For 20,000 users that is 67 req/s.

In addition to the above, we can estimate some amount of random activity for each user, which represents when a user might just flip through the system looking for some information.
Given no other information, we will guestimate that the users will navigate all of their records at a rate of 1% of users per day.
This would give a result of 200 users doing 10 requests over a period of 10 seconds.  For a total of 2000 requests over the day, which is di minimus when compared to other traffic.

Even though the users will only spend 4 hours out of a work day, for performance estimates, we can provide a fudge factor that represents users using the full traffic for the entire day.  This also makes the math a little easier.

Totalling the above we have (140 + 67) = 207 req/s

This does assume uniform usage over the day, however, no other information about application usage was provided.  

## Results

Informal testing shows that API and database will likely meet performance goals once deployed into a proper production environment.  JMeter testing was done, and resulted in a througput of about 650 req/s under a load of 10,000 concurrent users (given that most users will use the application for half the day).  This testing was done with the load tester, API and database all running on the same system (core i3-8350k @ 4Ghz).  Presumably the performance would increase once the load testing tools were not running on the same system, as there was a clear fight for CPU resources. 