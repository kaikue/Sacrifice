# Sacrifice

For Spooktober Jam 2021: https://itch.io/jam/spooktober-jam-2021

Choose to sacrifice abilities (dash, double jump, health) to gain attack power.

Castle of Cth??????

## Credits

- Art:
	- 1-Bit Platformer Pack Some art by Kenney: https://opengameart.org/content/1-bit-platformer-pack
	- Haunted Graveyard Tileset by tamashihoshi: https://opengameart.org/content/haunted-graveyard-tileset
	- Electrical Disintegration Animation by superjoe: https://opengameart.org/content/electrical-disintegration-animation
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

- Attack
	- animation (player & attack)
	- power & knockback depends on sacrifices
		- test
	- different sounds for higher level attacks?
	- bigger AoE at higher levels?
- Sound
	- Music
		- Title
		- Altar room
		- Boss
		- Endings?
	- Player
		- Jump
		- Double jump
		- Dash
		- Land
		- Attack
		- Hurt
		- Die (in killed prefab)
	- Enemies
		- Hurt
		- Die
- Level design
	- Multiple scenes per level to act as checkpoints
	- Short ceilings so you can't jump over enemies
	- Attackable walls take multiple hits- can't just run past enemies since they'll get you from behind
	- Dash over spikes
	- 4 flying eyes room
	- Introduce demon as miniboss first
	- Opportunities to use saved abilities to skip enemy fights
	- Decorations- occasional eyes
- Altar
	- wiggling tentacles on ceiling
	- more activate effects
		- screen flash
		- particles
		- sound
	- show button prompt in dialog corner
	- write it
		- dependent on choices
- Tutorial
	- controls for keyboard/gamepad
- Enemies
	- faster+tougher flying eye?
	- one that shoots towards player?
	- big shambler? (can't jump over)
- Levels
	- Title
		- music: 
	- Level 1: all abilities, minor combat
		- music: Approaching the Tower
	- Altar 1: choice to sacrifice dash
		- ambient music?
	- Level 2: dash or no, more combat
		- music: encounter
	- Altar 2: choice to sacrifice double jump
	- Level 3: dash or no, double jump or no, lots of combat
		- music: Juhani Junkala
	- Altar 3: choice to sacrifice hearts "vitality"
		- reload hearts GUI (test)
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