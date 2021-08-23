# InsuranceQuoteRating
## How to run it?
InsuranceQuoteRating was created using Visual Studio 2019, so the recomendation is to run it using that. 

Please ensure that the InsureQuoteRating is the startup project:
- Right click on the project
- Select Set as Startup Project

Now, when you run by doing click on the green play button (at the top-center of the window) your browser will be open with the following link: https://localhost:44364/api/rating
On this moment you are ready to test using postman.

## Postman testing 

On Postman, you need to create a new Post request with these details:
- URL: https://localhost:44364/api/rating/premium
- Header: disable Content-type default header and create a new one: Content-type=application/json
- Use this example in the body of the request:
```
{
    "revenue": 6000000,
    "state": "TX",
    "business": "Programmer"
}
```

Now, you are for test by clicking send.

## What is next?
- Security: we have multiple options, but for example we can configure to process bearer token add a middleware to ensure it is valid (this is option - can be handle by a Azure API Management) and contains the corresponding claims for the especific endpoints (authorization should take place here). 
- More unit testing: We can continue adding testing around the service and the repos. For example: possible exceptions, bad parameters and some scenarios of expected results.
- More integration testing: Would be better to create an automation testing framework using a library that make easier the API testing (for example https://restsharp.dev/). On this framework we can ensure that the API is returning the correct HTTPS statuses code, messages and correct premium ratings.
- Enable Cors with name policy and middleware, to ensure that we will get request only from expected external origins.
