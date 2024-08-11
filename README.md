# CMPG-323-Project-2-38082918

## What is a RESTful API

 Representational State Transfer Application Programming Interface is a widely adopted architectural style for designing networked applications. RESTful APIs allow different software systems to communicate over the internet using standard protocols, most commonly HTTP. These APIs are essential in modern software development due to their simplicity, scalability, and flexibility. The primary purpose of a RESTful API is to enable communication between clients (like web or mobile applications) and servers in a standardized way. RESTful APIs allow clients to perform operations such as creating, reading, updating, or deleting resources on a server. These operations correspond to the HTTP methods: POST, GET, PUT/PATCH, and DELETE.


## **NWUTechTrends RESTful API**

The API designed for the NWUTechTrends project 2 is a RESTful service that supports the management of clients, processes, projects, and job telemetries. This API enables NWU Tech Trends to track time and cost savings from automation processes across different clients and projects. The API is crucial for facilitating efficient data management, reporting, and client interaction within the system.

## Purpose of our API

The primary purpose of the API is to manage data related to clients, processes, projects, and job telemetries. It allows users to:

**_Manage Clients_**: The API provides endpoints to create, retrieve, update, and delete client information. This is essential for maintaining accurate records of all clients who are benefiting from the automation services offered.

**_Track Processes_**: The API includes functionality to handle processes associated with various projects. This helps in organizing and tracking specific operations within a project.

**_Project Management_**: The API allows the creation, retrieval, updating, and deletion of project details. This feature supports efficient project management by ensuring all relevant data is accessible and up-to-date.

**_Job Telemetry Management_**: The API tracks job telemetries, including human time saved and cost savings from automation. This is a critical feature that enables the calculation of ROI (Return on Investment) and efficiency gains from automation.


## Important Considerations
Several important considerations have been incorporated into the design and implementation of this API:

**_Error Handling and Validation_**: The API includes robust error handling mechanisms, such as returning NotFound when a resource is not available and BadRequest for invalid input data. These ensure that the API is resilient and user-friendly.

**_Data Integrity_**: Methods like ClientExists, JobTelemetryExists, and ProcessExists ensure that data operations are only performed on valid and existing records, preventing errors and maintaining the integrity of the database.

**_Custom Functionality_**: The API provides custom endpoints like GetSavings and GetSavingsByClient, which go beyond basic CRUD operations. These endpoints offer specific business logic necessary for tracking and reporting the effectiveness of automation efforts.

**_Security Considerations_**: Authentication and Authorization is applied at the controller level (or action level) to enforce security by requiring that the user making the request is authenticated. This ensures that only users with valid credentials can access specific API endpoints. The [Authorize] attribute, when properly implemented, is a critical component of the NWUTechTrends API’s security architecture. It ensures that only authenticated and authorized users can access or modify resources, thereby protecting the API from unauthorized access, data breaches, and other security threats. This attribute plays a pivotal role in maintaining the confidentiality, integrity, and availability of the data managed by the API, and it is essential for aligning with industry standards and regulatory requirements.
