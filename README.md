# AIGenerative-UC2

This code is using Stripe. It is a platform to test your payments.
Within this code there are 2 separate APIs:
1.  GetBalance
Endpoint: /api/StripeBalance
Parameters: none
Type: GET

This endpoint returns balance to the stripe account.  
![Uploading image.png…]()

2.  GetTransactionBalances
Endpoint: /api/StripeBalance
Parameters: 
  Limit - int value (dafault val: 100)
  StartingAfter - string value (A cursor for use in pagination. <c>starting_after</c> is an object ID that defines your place in the list. For instance, if you make a list request and receive 100 objects, ending with <c>obj_foo</c>, your subsequent call can include <c>starting_after=obj_foo</c> in order to fetch the next page of the list.)
Type: GET

This endpoint returns list of transactions on stripe account.  

### Run code
To run app you need t have Stripe account and add Stripe:SecretKey to Secret Manager. You can do that with powershell: 
dotnet user-secrets set "Stripe:SecretKey" "your_stripe_secret_key"
