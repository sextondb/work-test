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
- [ ] Create a login screen for a user
- [ ] Create a dashboard with a list of contact records
- [ ] Allow the list of contact records to be paged
- [ ] Allow the list of contact records to be scrolled
- [ ] Allow adding a contact record
- [ ] Allow editing a contact record
- [ ] Allow deleting a contact record
- [ ] Performance testing to validate MVP is capable of handling the required traffic

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