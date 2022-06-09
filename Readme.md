# Smartwyre Developer Test Instructions

Solutions is constitued by 4 Projects

Smartwyre.API
Smartwyre.DeveloperTest
Smartwyre.DeveloperTest.Runner
Smartwyre.DeveloperTest.Tests

Beforew you run te API:

Before anything, please seed your local database. Open Smartwyre.DeveloperTest.Runner and execute the following commands on your Package manager Console:

add-migrations "v1"
update-database

(*)You might need to update your connection string. It's using te default connection (  private const string connectionString = "Server=localhost\\SQLEXPRESS;Initial Catalog=Smartwyre;Trusted_Connection=True;";)

For the API:

Given an account with balance of 10000m:

Request:
{
  "creditorAccountNumber": "123456",
  "debtorAccountNumber": "123456",
  "amount": 2,
  "paymentDate": "2022-06-09T15:30:30.740Z",
  "paymentScheme": 0
}

Response:
{
  "errors": [],
  "statusCode": 200,
  "value": {
    "success": true
  },
  "isSuccessful": true
}