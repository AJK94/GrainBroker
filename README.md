### Grain Broker

<details>
<summary>
Technical Brief
</summary>
<p>
    
 #### User story
 A client, a grain broker, manages several grain suppliers and customers, every month
 their customer base places their grain requirements and its then the job of the grain
 broker to determine the best supplier(s) to deliver against the requirements of their
 clients. Currently this is all handled in an excel spreadsheet and largely they operate on a
 first placed first fulfilled system. They would like to move to a more scalable and resilient
 platform for client management that also provides them with a level of insights /
 analytics.
 
#### Task Summary
The purpose of this exercise is to take a standard dataset commonly used as an
exemplar for optimisation problems and turn that into a &#39;product&#39; consisting of backend,
middleware, and front end.
The singular dataset provided includes all the data candidates need to:
* Ingest the CSV and determine the data models
* Design and implement a 'database/storage layer' (relational or non-relational are
both totally acceptable as long as the candidate can justify them)
* Design, implement and test an API or decoupled middleware layer
* Design and implement a front end so that a client could better interact with this
dataset, view data trends and both active and historical data. Consideration
should be made for role-based access.

#### Outcome
It is not imperative that the candidate writes code for each stage, we would rather that
they consider, document, and design each stage and be able to communicate with
clarity around the decisions that they have or would have made. However, we would like
to see a working solution for one of the stages:
* Backend, data ingestion, database design and implementation
* Middleware, a chosen middleware layer either API or equivalent communications
solution.
* Front-end, a clean user interface that uses the middleware (where appropriate)
to demonstrate a data driven dashboard.
This brief / task highlights a candidate’s ability to take quite an informal brief and a
singular of view of data, their ability to synthesise data and turn flat data into a
considered / scalable and resilient solutions architecture and look to how we would add
further value via analytics / machine learning / statistical analysis or operational
research.


</p>
</details>

<details>
<summary>
Database 
</summary>
<p>

The database will be a SQL database as I think the data provided can be split into a relational database and reduce the 
repetition of data storage.

I have decided to split the data into the following tables Import, PurchaseOrder, Customer, Supplier and Location. 

One purchase order will have a foreign key to customer, supplier and import.
Both a supplier and customer have a foreign key to location. 

When adding more API end points to get the data in different ways, it will be important to index that tables appropriatley for 
the queries that get generated e.g. if we are querying the purchase order table by customer we should add an index on the customer Id.
Adding these indexes will greatly improve read speeds.

To recreate the database, create a new SQL database [GrainBroker] and run the scripts located at Grainboker.Database/Tables
    
The diagram below shows the database structure and the relationships between the tables.
    
![Class Diagram](http://www.plantuml.com/plantuml/png/SoWkIImgAStDuGeeBKhEI2nEzIzAIIrIqDLL22ujACZ9J2t2b74kBIx9pmNA-EGd9vPavkVX4aIOuPbRa5zK0X92COtB8JKl1MWx0000)    
    
</p>
</details>

<details>
<summary>
Importer 
</summary>
<p>
    
The importer project is a worker service written in .net 6. 

When deployed we can use a CRON job to run every month when new orders are created - this service could be run when ad hoc.
I'd recommend we look at kubernetes for deploying as it can easily scale as the system grows.

The importer currently just pulls from a CSV in GrainBroker.Importer/ImportData and uses the CSVHelper library to import the data.

I assume that this functionality will grow to more than one file a month so we could also easily convert this functionality into
a front end CSV uploader. We could also add a manaul front end input per order or look at a more automated input depending on how
orders are received.

Future development on the importer would include more validation and better error handling.

</p>
</details>

<details>
<summary>
Domain
</summary>
<p>
    
The domain project uses Entity Framework to connect to the database. All queries to the database through EF use linq which was chosen as the queries aren't very complex.
If complex queries were required, stored procedures could be considered. 

A repository pattern is used so that all data access logic is isolated from the services project. 
This allows the services project can be strictly business logic and also allows us to test every project in isolation.

Assuming that the system grows and we start adding orders more frequent we could consider adding live updates on the front end, to do this SQL dependency could be considered.
SQL Dependency detects changes on a table and allows us to act upon the change, allowing us to update the front end when a new order is inserted.

 </p>
</details>

<details>
<summary>
Services
</summary>
<p>
    
The services project will contain our business logic and data transformation.

As the system grows we could consider adding a cache service that saves frequent database requests. 
The cache service would use Redis as it's a popular open source solution for caching requests. 
Using a caching service could improve the application performance and reduce database costs, depending on the user usage of the system.
We can update the cache on a timer e.g. every 15 minutes.
We could also limit the cache to only relevent data e.g. last months worth (depending on the client needs)

Following on from the SQL dependency comments in the Domain README we could update the cache from the SQL dependancy so that it's constactly up to date. 

</p>
</details>

<details>
<summary>
API 
</summary>
<p>
    
The API is a simple .net 6 API, this was chosen as it's easy to set up, easy to maintain and .net 6 has a long support lifetime. 

The API has a few example endpoints setup for various order data.
With further investigation into what the client wants for the front end needs we can easily make more endpoints to provide more functionality.

The API currently has no authentication but we could easily add RBAC using Microsoft Identity.

If you want to test the API make sure the database is created and set the API as the startup project.
Running this project will open the Swagger which will allow the testing of the API requests

</p>
</details>

<details>
<summary>
Front End 
</summary>
<p>
    
The front end will be written in Vue.
Vue is great for small to medium size projects as it's lightweight, easy to set up and easy to learn (especially for developers who have used other javascript frameworks).

We will use the vuetify component library, this library will speed up development time and add consistency to the project. 
Vuetify also easily allows us to display data tables which will be useful for showing orders.
Vue material component library was also considered as they also provide clean and easy to use components but the 
support that vuetify provides is incomparable. The support includes in depth documentation and discord server where they have developers on hand to help.

I will use vue-chartjs for all the charts on the dashboard. vue-chartjs is a wrapper for the popular Chart.js open source library.
Using this library will give us a fast and easy way to create all the charts that the client requires.

I will use Axios to consume the API. Axios is built into Vue and is easy to use. 

Vue also has a built in state manager so we add authentication we can store login details and roles easily.

Depending on the client needs, we can add signal R for live updates (This is discussed in more detail in the Services README).

</p>
</details>

<details>
<summary>
Tests 
</summary>
<p>
    
The tests are written in Xunit and Moq. The test classes are split into API, Services and Domain. I have split them down so we can test all the functionality individually,

With more time I would also add full regression tests that would go from the API to the domain without using Moq.
It would also be useful to add load testing. Tools such as NBomber would be a good solution to use for this as it can be built in .net using their nuget package and is open source.

The tests are very useful for recognising if new code affects the current code base, it would be very useful to check code coverage on pull requests with a tool such as SonarQube.

</p>