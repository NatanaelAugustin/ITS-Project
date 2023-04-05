# Project Readme: ITS_Project
This is a console-based Incident Tracking System (ITS) project created with C# .NET Core. It is a simple ITS tool that allows users to create, track and update cases. The system uses Microsoft's Entity Framework Core to interact with a database and provide basic CRUD (Create, Read, Update, Delete) functionalities.

## Getting Started
To run this project, you will need to have the following tools installed on your machine:

Visual Studio - The IDE used to develop this project.
.NET Core SDK - The .NET Core SDK used to build and run the project.

## Installation
1. Clone the project repository: git clone https://github.com//ITS_Project.git
2. Navigate to the project directory: cd ITS_Project
3. Open the project in Visual Studio
4. Open Package Manager Console and run the following command: Update-Database
5. Build and run the project

## Usage
After building and running the project, you will be presented with a console interface with several menu options:

1. Create a Case: You can create a new case by selecting option 1 from the main menu. You will be prompted to enter the user's details and case details. If the user already exists, you can simply enter their email address and the system will retrieve their details.
2. Update Case Status: To update the status of a case, select option 2 from the main menu. You will be prompted to enter the case ID and the new status ID. If you do not know the status ID, the system will display a list of all available statuses to choose from.
3. Create a Comment: To create a comment for a case, select option 3 from the main menu. You will be prompted to enter the case ID and the comment. The comment must not exceed 1000 characters.
4. Remove a Case: To remove a case, select option 4 from the main menu. You will be prompted to enter the case ID and confirm the deletion.
Display All Cases: To display all cases in the system, select option 5 from the main menu. The system will display a table of all cases with their respective details.
5. Quit: To exit the program, select option 6 from the main menu.

## Technologies
C# .NET
Entity Framework Core
Microsoft IdentityModel.Tokens
This project was created by Natanael Augustin as a project for a course - DataLagring whilst studying to become a web developer at EC-Utbildning Gothenburg.
