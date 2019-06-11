# CSTDirectoryApp

  I downloaded the project on a separate machine and didn't have any problems running the application. I didn’t include dependencies in the repository, so I’d make sure Nuget Restore is on in 
Tools -> Options -> NuGet Package Manager -> General if they don’t automatically download in Visual Studio. 

  The app is built using WebApi, Angularjs, Entity framework, and the LocalDB database. The database is dropped and created on each run of the application. It does not include any test data.

  The application consists of two forms, one for adding a new person to the directory and one for searching 
the directory based on given criteria. The form to add requires the user input values for the first name, last name, and address fields. It will give a notification when a person has been successfully added. The form to search takes in values for the first name, last name, and address fields and searches the directory based on any combination of the three given. A notification is given when no records could be found with the given search criteria. When it returns records, a table will show next to the search form. The records can be sorted by first name, last name, or full address by clicking the column header names.
