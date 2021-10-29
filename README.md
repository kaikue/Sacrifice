# Sacrifice

For Spooktober Jam 2021: https://itch.io/jam/spooktober-jam-2021

Choose to sacrifice abilities (dash, double jump, health) to gain attack power.

Castle of Cth??????

## Credits

- Art:
	- 1-Bit Platformer Pack by Kenney: https://opengameart.org/content/1-bit-platformer-pack
	- Haunted Graveyard Tileset by tamashihoshi: https://opengameart.org/content/haunted-graveyard-tileset
	- Electrical Disintegration Animation by superjoe: https://opengameart.org/content/electrical-disintegration-animation
	- Nighthawking 2 Battlers (Kraken Tentacle) by benamas: https://opengameart.org/content/nighthawking-2-battlers
	- Castle 2D by Alucard: https://opengameart.org/content/castle-2d
- Sound:
	- Effects generated with sfxr: https://www.drpetter.se/project_sfxr.html
	- Electric by D4XX: https://freesound.org/people/D4XX/sounds/567269/
- Music:
	- "Approaching the Tower" composed, mixed and mastered by Viktor Kraus: https://opengameart.org/content/approaching-the-tower
	- "Dread Castle" composed by Scott Elliott: https://opengameart.org/content/dread-castle
	- "Dark Reign" by Shype: https://opengameart.org/content/dark-reign-chiptune-stage-music
	- "Encounter" by shiru8bit: https://opengameart.org/content/8-bit-chiptune-encounter
	- "Boss Fight" by SubspaceAudio: https://opengameart.org/content/4-chiptunes-adventure
	- "Underground Storm" by axtoncrolley: https://opengameart.org/content/underground-storm-chiptune8-bit-influence
	- "Demon Lord" by Scrabbit: https://opengameart.org/content/demon-lord
- Fonts:
	- PixelMix by Andrew Tyler: https://www.dafont.com/pixelmix.font
	- Alagard by Hewett Tsoi: https://www.dafont.com/alagard.font
	- Gothic Pixel by SparklyDest: https://www.dafont.com/gothic-pixel.font
	- Pixeled English by kajetan: https://www.dafont.com/pixeled-english.font
	- GhastlyPixe by Cyndi Ellis: https://www.dafont.com/ghastlypixe.font

## TODO

- Attack
	- power & knockback depends on sacrifices
		- test
	- screen shake more when hitting enemy at higher levels?
	- different sounds for higher level attacks?
	- bigger AoE at higher levels?
- Sound
	- Music
		- Title
		- Endings?
- Level design
	- Multiple scenes per level to act as checkpoints
	- Short ceilings so you can't jump over enemies
	- Attackable walls take multiple hits- can't just run past enemies since they'll get you from behind
	- Dash over spikes
	- 4 flying eyes room
	- Introduce demon as miniboss first
	- Opportunities to use saved abilities to skip enemy fights
	- Decorations- occasional eyes & tentacles
- Altar
	- show button prompt in dialog corner
	- write dialog for rooms 2, 3, 4
		- dependent on choices
- Tutorial
	- controls for keyboard/gamepad
- Boss fight
	- have to attack tentacles
	- spawn waves of enemies
- Endings
	- stop music (fade?)
	- restart button
	- bad
		- fade to white in altar
	- ok
		- castle destroyed?
	- good
		- ?
- Enemies
	- faster+tougher flying eye?
	- one that shoots towards player?
	- big shambler? (can't jump over)
- Levels
	- Title
		- castle
		- music: 
	- Level 1: all abilities, minor combat
		- music: Approaching the Tower
	- Altar 1: choice to sacrifice dash
		- music: dread castle
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
		- yes- bad ending
		- no- fight eldritch guy
			- music: demon lord
			- no abilities sacrificed- climb up through spikes and banish him- good ending
			- some abilities sacrificed- fight him- banish him but you die (ok ending)