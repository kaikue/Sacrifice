# Sacrifice

For Spooktober Jam 2021: https://itch.io/jam/spooktober-jam-2021

Choose to sacrifice abilities (dash, double jump, health) to gain attack power.

Castle of Cth??????

## Credits

- Art:
	- 1-Bit Platformer Pack Some art by Kenney: https://opengameart.org/content/1-bit-platformer-pack
	- Haunted Graveyard Tileset by tamashihoshi: https://opengameart.org/content/haunted-graveyard-tileset
- Music:
	- "Approaching the Tower" composed, mixed and mastered by Viktor Kraus: https://opengameart.org/content/approaching-the-tower
	- "Dark Reign" by Shype: https://opengameart.org/content/dark-reign-chiptune-stage-music
	- "Encounter" by shiru8bit: https://opengameart.org/content/8-bit-chiptune-encounter
	- "Boss Fight" by SubspaceAudio: https://opengameart.org/content/4-chiptunes-adventure
	- "Underground Storm" by axtoncrolley: https://opengameart.org/content/underground-storm-chiptune8-bit-influence
- Fonts:
	- PixelMix by Andrew Tyler: https://www.dafont.com/pixelmix.font
	- Alagard by Hewett Tsoi: https://www.dafont.com/alagard.font
	- Gothic Pixel by SparklyDest: https://www.dafont.com/gothic-pixel.font
	- Pixeled English by kajetan: https://www.dafont.com/pixeled-english.font
	- GhastlyPixe by Cyndi Ellis: https://www.dafont.com/ghastlypixe.font

## TODO

- Character
	- Attack
		- power & knockback depends on sacrifices
	- Health
		- Dying effects, respawn at checkpoint
			- don't reload scene cause this messes with gem counts
- Camera
	- stop at level transitions
- Enemies
	- knockback on attacked
	- destroy effects
		- particles
		- drop hearts sometimes
	- behaviors
		- skeleton- turn around at edge of platform
		- ?- shoot towards player?
- Attackable walls?
	- need to break to leave altar (to test attack)
- Altar dialog
	- disable attack
	- write it
		- dependent on choices
	- jump up to touch sacrifice zone, or just walk under it to ignore
- Tutorial
	- controls for keyboard/gamepad
- Levels
	- Title
		- music: 
	- Level 1: all abilities, minor combat
		- music: Approaching the Tower
	- Altar 1: choice to sacrifice dash
		- ambient music?
		- You feel less nimble.
	- Level 2: dash or no, more combat
		- music: encounter
	- Altar 2: choice to sacrifice double jump
		- You feel heavier.
	- Level 3: dash or no, double jump or no, lots of combat
		- music: Juhani Junkala
	- Altar 3: choice to sacrifice hearts "vitality"
		- You feel more fragile.
	- Level 4: dash or no, double jump or no, cross spikes (requires health), lots of combat
		- music: inarticulate
	- Altar 5: sacrifice everything for ultimate power
		- yes- world destroyed (bad ending)
		- no- fight eldritch guy
			- music: ?
			- realistic graphics?
			- tentacles
			- spawn enemies
			- no abilities sacrificed- climb up through spikes and banish him- good ending
			- some abilities sacrificed- fight him- banish him but you die (ok ending)
- Art
	- Player
		- Attack
		- Hurt
	- Enemies
		- guy who shoots bullets
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