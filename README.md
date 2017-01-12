# Student Enrollment App

A Technical coding exam that creates/updates a Class and Student

###To run the project:###

Attach the database StudentEnrollDb from ~\StudentEnrollmentApp\Sea.Core\Database folder. Change the connection string from Web.config of Sea.Web project. Change the data source to the source of the local machine where you attach the database. Change the user id and password. Build and Run the project. Make sure that the Sea.Web is the start up project.

###To test the project:###

Click the Test from the Menu tools and click Run then Run All Tests

###To access the Rest API endpoint###

http://localhost:portnumber/OData/ClassApi?$expand=Students

