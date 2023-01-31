# SALES 
SalesReport, Net Core, DDD, Generic Repository, XUnit

**File to be used**
Sales Data - data for importing. Please test at least on 1000000 sales records.
[Sales File To be imported](http://eforexcel.com/wp/downloads-18-sample-csv-files-data-sets-for-testing-sales/)

http://eforexcel.com/wp/downloads-18-sample-csv-files-data-sets-for-testing-sales/ 
 

***
**1. SPA with a basic interface.**

It is preferable to use AngularJS / Angular.

The user interface should be able to handle the following:

  - Import sales records

    - Upload a file from UI

  - Show imported records with pagination inside a grid

    - Sort records in grid by order date

    - Filter imported records in grid by country

  - Modifying imported sales records:

    - Edit a sales record (you can choose one):

      - either inline within the grid itself or

      - via a pop-up form which will display the relevant order details

    - Delete imported sales record one by one or in bulk

  - Aggregate the sales records to display the number of orders and total profit by country
and year

    - User should be able to see how many orders have been sold in the specific
country for a specific year

    - User should be able to see what profit has been earned in the specific country for
a specific year


**NEW FEATURES - NOT DONE YET**
These optional tasks are for candidates who would like to demonstrate their skills further:

  -  User authorisation

     - The only authorised user can import sales records

  - Show progress bar with percentage for long operations

  - Handle uploading several files from UI:

    - If a record already exists in the Storage, it should be updated in the storage

    - If the record does not exist in the Storage, the new record should be added to the
storage

**END NEW FEATURES**

****

**2. Web API used by SPA.**

.NetCore 3.1

It should communicate with Storage to retrieve / update imported data.
***
**3. Storage used by Web API.**

Using SQL Server 2017 Express.
***



