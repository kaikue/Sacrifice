# Sacrifice

For Spooktober Jam 2021: https://itch.io/jam/spooktober-jam-2021

Choose to sacrifice abilities (dash, double jump, health) to gain attack power.

Castle of Cth??????

## Credits

- Some art by Kenney: https://opengameart.org/content/1-bit-platformer-pack
- Fonts by devurandom: https://opengameart.org/content/new-original-grafx2-font-collection

## TODO

- Character
	- Attack
		- attacking damage sources shouldn't hurt player
		- power & knockback depends on sacrifices
	- Health
		- Player has 3 hearts, lose one for contacting enemy/spikes
		- Lose all hearts- respawn at checkpoint
	- disable abilities based on sacrifices
- Controller
	- persistent
	- keep track of sacrifices
- Camera
	- follow player on X axis only
	- screen shake
- Enemies
	- knockback on attacked
	- behaviors
		- skeleton- walk back and forth, turn around at edge of platform
		- flying eye- fly towards player
		- imp- walk towards player
		- demon- run towards player
		- ?- shoot towards player?
- Collectible gems
	- show how many you got
- Altar dialog
	- disable attack
	- fonts
	- write it
		- dependent on choices
	- jump up to touch sacrifice zone, or just walk under it to ignore
- Levels
	- Level 1: all abilities, minor combat
	- Altar 1: choice to sacrifice dash
	- Level 2: dash or no, more combat
	- Altar 2: choice to sacrifice double jump
	- Level 3: dash or no, double jump or no, lots of combat
	- Altar 3: choice to sacrifice hearts
	- Level 4: dash or no, double jump or no, cross spikes (requires health), lots of combat
	- Altar 5: sacrifice everything for ultimate power
		- yes- world destroyed (bad ending)
		- no- fight eldritch guy
			- no abilities sacrificed- climb up and banish him- good ending
			- some abilities sacrificed- fight him- banish him but you die (ok ending)
- Art
	- Player
		- Attack
	- Levels
		- Decorations
			- Window
	- Enemies
		- fix eye, skeleton head
	- Altar
- Sound
	- Music
		- Title
		- Level(s?)
		- Altar room
		- Boss
		- End?
	- Player
		- Jump
		- Double jump
		- Dash
		- Land
		- Attack
	- Enemies