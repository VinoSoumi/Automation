Step 1: Set startup project as MailAutomation\MailAutomation.API.csproj
Step 2: FYI - ApplicationDBContext included in MailAutomation.Persistence.csproj
Step 3: Apply command in package manager console - update-database
Step 4: Edit CostCenterController to change the username and password.
Step 5: Run the project



Please refer the following three route information,

1. GET /api/cost-center/do-mail-automation
   Extract expenses from mail and stored into the database
2. GET /api/cost-center/list-expenses
   Retrive all the expenses from database
3. GET /api/cost-center/list-expenses-with-mailinfo
   Retrive all the expenses with respective mail info from database
