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

The database will be a SQL database as I think the data provided can be split into a relational database and reduce the repetition of data storage

I have decided to split the data into the following tables PurchaseOrder, Customer, Supplier and Location. 

A purchase order will have one customer and one supplier.

Both a supplier and customer will have a location each. 

To recreate the database, you will need to create a new SQL database [GrainBroker] and then run the scripts located at Grainboker.Database/Tables
</p>
</details>

<details>
<summary>
Domain
</summary>
<p>
    
The domain uses Entity Framework to connect to the database. All queries to the database through EF use linq as the queries aren’t complex.
If complex queries were required, stored procedures could be considered. 

If the client would like live updates on the front end we could consider using SQL dependency
SQL Dependency would allow us to update the front end everytime a new order is inserted.

A repository pattern is used so that all data access logic is isolated from the services project. 
This would mean that the services project can be strictly business logic and also allow us to test every project in isolation.
 </p>
</details>

<details>
<summary>
Services
</summary>
<p>
    
The services project is where we will be completing most of our business logic and data transformation (as the charts may require different structures than the database table structure).

The services project could also contain the cache service which utilises Redis. This service will be used to cache large database requests e.g. get all orders
Caching this data will reduce the hit rate on the database.
We can update the cache on a timer e.g. every 15 minutes or we could use SQL dependency to update the cache whenever a new order is inserted. 
The choice of timer or on insert is dependent on if the client wants live updates.  
The caching may not be required depending on how many users are using the system. If there's only one user it would be fine to go directly to the database for every request.

While updating the cache with database updates we can also use Signal R to update the front end without the user having to refresh.
Live updates may not be required if orders are not inserted often. 
</p>
</details>

<details>
<summary>
API 
</summary>
<p>
    
The API is a simple .net 6 API, this was chosen as it's easy to set up, easy to maintain and .net 6 has a long support lifetime. 

The API has one endpoint setup for all purchase order information. 
With further investigation into what the client wants for the front end needs we can easily make more endpoints to provide more functionality.

The API currently has no authentication but we could easily add RBAC using Microsoft Identity.
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
support that vuetify provides is incomparable with their in depth documentation and discord server where they have developers on hand to help.

I will use vue-chartjs for all the charts on the dashboard. vue-chartjs is a wrapper for the popular Chart.js open source library.
Using this library will give us a fast and easy way to create all the charts that the client requires.

I will use Axios to consume the API. Axios is built into Vue and is easy to use. 

Vue also has a built in state manager so if we added authentication we could store log in details and roles easily.

Depending on the client needs, we can add signal R for live updates (This is discussed in more detail in the Services README).
</p>
</details>

<details>
<summary>
Importer 
</summary>
<p>
    
The importer is a worker service written in .net 6, this could be run when ad hoc or on a schedule using a cron job.

The importer currently just pulls from a CSV in GrainBroker.Importer/ImportData and uses the CSVHelper library to import the data.

We could also easily convert this functionality into a front end CSV uploader or manul front end input.
</p>
</details>

<details>
<summary>
Tests 
</summary>
<p>
    
The tests are written in Xunit and Moq. The test classes are split into API, Services and Domain. I have split them down so we can test all the functionality individually,

With more time I would also add full regression tests that would go from the API to the domain without using Moq.

The tests are very useful for recognising if new code affects the current code base, it would be very useful to check code coverage on pull requests with a tool such as SonarQube.
</p>
</details>
