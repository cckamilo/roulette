# roulette
rest api that simulates the roulette game


----endpoints dev----

swagger --> https://localhost:5001/swagger/index.html

1. new roulette
https://localhost:5001/api/v1/roulette/new

2. open roulette
https://localhost:5001/api/v1/roulette/{{id}}/open

3. new bet
https://localhost:5001/api/v1/roulette/bet
request 
{
  "id": "618744e4992473acb4eee5c9",
  "value": 37,
  "amount": "9000"
}

4. close roulette
https://localhost:5001/api/v1/roulette/{{id}}/close

5. get roulette
https://localhost:5001/api/v1/roulette
