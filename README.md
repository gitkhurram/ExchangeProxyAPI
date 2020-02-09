# ECBExchangeProxy

Pre-requisites:
	- .NET Core 3.0 

Building & Running

Step 1: Clone the repository
Step 2: Run the project with following command on Windows:
		> dotnet run
Step 3: Send post request at https://localhost:5001/Exchange with following JSON content body:
		{ "dates": [...], "from_currency" : "...", "to_currency" : "..."}