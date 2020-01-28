# Data Entry Form

[![Build Status](https://dev.azure.com/ShashwatiShradha/Data%20Entry%20Form/_apis/build/status/FourFront-Senior-Design.frontend?branchName=development)](https://dev.azure.com/ShashwatiShradha/Data%20Entry%20Form/_build/latest?definitionId=1&branchName=development)

Displays a record from the database.

### Prerequisites
* Microsoft Access Database Manager 2010 (x64 bit) https://www.microsoft.com/en-US/download/details.aspx?id=13255

### Loading the Database
1. Open the Data Entry Form application (double-click on the icon). 
2. Enter the path to the section by
    - using the “Browse” button
    - pasting the path into the input field. This field may be pre-filled with the last path that was successfully loaded.
3. Next, hit the “Load Data” button. This may take a few seconds depending on the size of the database.
    - If successful,  the message “Successfully uploaded ___ records from the Database” will be displayed.
    - Otherwise, it displays “Invalid Path. Try Again.” Check that a database exists in the entered location.
4. Next, hit the “Review” button, which will open the main input form.

### Saving the Record
* "Save & Go to Menu" Button will save any changes and return to the Data Entry page (to load a section path). However, be aware that if you make changes to the current record, and then close the window using the ‘X’ in the upper right corner, the changes will not be saved. A warning will pop up to remind the user that they may lose changes.
* All data is saved back to the database when the record is switched to another record (either forward or backward). 
* When the last record in a section is reached, the recommended way to quit the program is to click the “Save & Go to Menu” button. This will make sure that the last record is saved correctly.
* The database is not locked by the input form - the only interaction with the database occurs when data is transferred to and from the database when switching between records.
