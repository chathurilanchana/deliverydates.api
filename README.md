# deliverydates.api
This project calculates possible delivery dates for given list of products according to specific business rules.

# How to Run it
Make sure you have .net core 3.1 installed.
Load the solution file DeliveryDates.Api.sln, select DeliveryDates.Api and run it.
Access the swagger page from http://localhost:5000/swagger
You can test it via swagger.

# Business Considerations
1. For green delivery, I consider green delivery as deliveries on Friday.
2. I have not tested all the scenarios under unit tests, byt I tried to add at least one test case for each business logic.

# Technical Considerations
1. I have used feature folder structure, to structure the project. You can locate code related to delivery dates feature in src->DeliveryDates.Api->Features->DeliveryDates
2. The mappers I added can be replaced with Automapper or similar tool.
3. In order to enforce CQRS and reduce coupling in controller and logic (I achieved this by using a service layer where handler get resolved, and adding ICommandHandler and IQueryHandler interfaces).
   we could use mediatR or similar. However CQRS is irrelavant for this application as we dont have a data source.
4. I have skipped logging part in the exception handling middleware, and instead logged to console, due to the time limitations.