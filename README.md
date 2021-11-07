# roulette
rest api that simulates the roulette game

---> El proyecto postman se encuentra en la raiz de la solucion para importar.

----endpoints dev----

swagger --> https://test-roulette.azurewebsites.net/swagger/index.html

1. new roulette
https://test-roulette.azurewebsites.net/api/v1/roulette/new

2. open roulette
https://test-roulette.azurewebsites.net/api/v1/roulette/{{id}}/open

3. new bet
https://test-roulette.azurewebsites.net/api/v1/roulette/bet
request 
{
  "id": "618744e4992473acb4eee5c9",
  "value": 37,
  "amount": 9000
}

4. close roulette
https://test-roulette.azurewebsites.net/api/v1/roulette/{{id}}/close

5. get roulette
https://test-roulette.azurewebsites.net/api/v1/roulette
