# Project: 'Musicians Searcher'
### Architecture: Microservices
### The app contains 3 microservices:

### Identity Service:

- Handles authentication and policy based authorization by using identityserver4
- Provides user management functionality by using microsoft identity

Service architecture: Clean architecture

Database:  Postgres

### Musicians Service:

- Provides functionality to search and filter other musicians based on their skill sets and musics tastes

Database: MongoDB

Service architecture: Clean architecture + CQRS

### Chat Service:

- Allows users to communicate with each other by creating chat rooms and sending messages
  
Database: MS SQL Server

Service architecture: N-Layer
