Requirements to Run:
.NET8.0

Account information:
Login for Umbraco: 
Email: Admin@Admin.com
Password: Gv:v*d+kS[


-----------------------------------------

Umbraco Technical Test

-----------------------------------------
Time to complete: 8h

Prior to starting your test, please create a branch (include your name in branch name, i.e.
“/dev/john-smith”) from master to push it back to remote - this marks the beginning of your
challenge.
Objectives: You are required to complete as much as possible in the time given. We want to see some good coding practices in use. Give some thought to how you would improve your solution for discussion after.

-----------------------------------------

User story: 

As a web-editor, I want to be able to collect user data directly from the page. Custom grid module is a desired approach that can be reused across pages on the site.

Prerequisites:
 - Create new Umbraco project.
 - Create a page in Umbraco that will utilise "Grid layout" property type. Allow standard grid elements such as "Rich Text Editor" or "Image" so content displayed on the grid does not look empty). 

Functional requirements:
 - Design and create a new grid editor module that will allow users to input and submit their data:
   - Name
   - Email
   - DOB (date of birth)
 - All above field must validate using appropriate validation rules (name cannot be empty, email must match regex, etc.) preferably both client and server side. 
 - User submitted data from the form must be stored as text files in the desired folder location within website.
 - Use front-end js/css frameworks of your choice to style it up and provide better user experience. 

Mare sure, you supply adequate instructions around how to build and run both front-end (i.e. node version, grunt/gulp tasks, Umbraco backoffice login) and back-end sides of your technical solution.

 ----------------------------------------- 
 
 Testable criteria:
  - Good use of SOLID 
  - Bonus marks for unit tests


