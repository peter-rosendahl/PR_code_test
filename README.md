# PR_code_test
MVC Umbraco project based on a given test in relation to a job interview.


Project developed by Peter Rosendahl.

This project was to be developed within a week, where the tasks of the project are described in the "Development Task - Umbraco Develooper.pdf" case file attached in this repository.

Estimated timeframe spent on project development: 20 hours.


Installation guide:
1. Download the project
2. Open Microsoft SQL Server Management Studio, create a new database, and restore database with the database file in this repository
3. Open the solution in Visual Studio, go to the project's web.config file, go to <connectionStrings> and update the "umbracoDbDSN" connectionString to match with the credentials of your SQL database in step 2
4. In Visual Studio, build the solution and run the project - you should be seeing the website in function at your default browser now.
 
To test the functions in the project:
- Backend login is sent to Henrik with the instructed mail, so his team can access the backend settings.
- I have created a member in the project, whose login details are also sent to Henrik in the referenced email above.
- You can create a new member with the requested information in the forms, and the new member will be approved right away, as long as the input values are valid ;-)
  

Notes from the developer:

In the given assignment, there were a description of a user story and a visual aid for this, where the user story would return the user to the front page after successfully using the signin form, whereas the visual aid showed a response message inside the dialog. 
In this project I chose to go with the visual aid alternative, since I believe that a given message to the user will be helpful after filling out a form, rather than to be transfered to the frontpage with minor notices given to the fact, that the registration went successfully. My wishes that this decision will be understandable.

Of course, every web project is continuously upgradeable, and this project has some work areas that can be workend on from here. 
My thoughts for the next step would be to:
1. Apply smtp mail setup in web.config, which helps to send out mails regarding activation of an account and how to renew one's password.
2. When the new member clicks on a link in the received email regarding account activation or password renewal, we should work on a method validating the parameters given in the URL, in order to approve the correct member.
3. Optimizing mobile usability (the bootstrap dropdown menu pushes content down, leaving a white background at the top of the page).
4. Make full use of Umbraco in relation to images and texts.
4.1 Some default form texts comes from the Umbraco dictionary, so the rest of the standard texts can be initiated from the same place as well.
4.2 We can make the website more flexible by making the header logo and footer logo etc. editable from backend, making it possible to create season-inspired content.


Besides the above notes, I have attempted to complete all of the main tasks from the case file, including the user stories within each of them.
I hope, that this project handover and my thoughts for further coding may give you a satisfied evaluation of my profile for the job.


Best regards
Peter Rosendahl
