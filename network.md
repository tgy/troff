# Networking

## PACKETS

#### SYNCING

	10 bytes sent
	* [1] BYTE -> player's number
	* [2] BYTE -> player's direction
	* [3-6] SHORT -> player's X position
	* [7-10] SHORT -> player's Y position

#### LOBBY JOINING

	Just the string of the player who joins
	
## LOBBY CREATION

	{ LobbyState context }
	* create the players list
	* start listening on the chosen port
	* add all the clients who are sending a string
	* start a PlayState when button pressed
