NOVA BOT README
-Angular 8
-ASP.NET 2.2

#########################################################################
How to use

1 - Install bot in slack and authenticate its app.

2 - Save into configurations table the following data: Bot Token, ClientId and ClientSecret (all obtained after install the bot's app)

3 - Create a command to send a new quote, that must call the following api ( your.domain.com/api/quotes/addnewquote ). The command should look like. Being the user field optional
/<your command> <yourFrase>  [by: <user>]

4 - If you wish to be possible to send upovote and downvotes requests, you must register a new command that should send a request to: your.domain.com/api/quotes/upvotequote  or your.domain.com/api/quotes/downvotequote . The command shoud looki like:
/<down- or upvote command>  <quote vote ID>
-> The quote vote Id is shown on the same channel that the quote has been sent in a message from your bot.


###########################################################################
Missing features:

- A admin page to manager the registration of bots
- Configure the pagination html component 
- Configure the ordering by some parameter of quotes in FrontEnd (it is partially implemented )
- Improve some DB requisitions aiming to decrease the number of requests to DB.
