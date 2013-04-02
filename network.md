Networking
============

Player _current
List<Player> _players

Server
--------

	while(true)
	{
		foreach(Player p in _players)
		{
			> verify if p is still connected to the server. If not, react consequently
			> get p's current position and direction, sync with the local instance of the player
			> sync the map with p's new direction and position
			> send the new version of the map to the player
		}
	}