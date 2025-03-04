# ESCAPE GAME
 This project is a 3-level Minecraft-themed escape game where a player-controlled avatar navigates through different levels while avoiding traps, bombs, and enemies inspired by Minecraft creatures. The game includes mechanics such as lives, a timer, secret boxes, scoring, and more. The game uses Object-Oriented Programming (OOP) principles for structure, making the code modular and maintainable.

Game Overview


Level 1: Traps (Minecraft-themed)

The player starts with 3 lives. Minecraft-style traps are randomly hidden in stone blocks at the start of the game. Traps are randomly placed, totaling at least 10 traps. The player controls the avatar using the arrow keys on the keyboard. A timer starts when the game begins. The player loses a life when landing on a trap.

Level 2: Bombs

The player starts with an additional life (+1). Every 3 seconds, 10 bombs are dropped randomly across the playing field. The player controls the avatar using the arrow keys. The timer continues from where it left off after the level change. The player loses a life if standing on a bomb when it lands.

Level 3: Enemy Soldiers (Minecraft Creatures)

The player starts with an additional life (+1). Enemy soldiers inspired by Minecraft mobs (such as Creepers, Skeletons, and Zombies) randomly appear every 2 seconds on the opposite side of the playing field and move forward one block every second. The player controls the avatar using the arrow keys. The timer continues from where it left off after the level change. The player loses a life if standing on the same block as an enemy soldier. The score is calculated based on remaining health and elapsed time:
Points = remaining health * 500 + (1000 - total elapsed time (seconds))

Menu Screen:

Press ENTER to start the game.
Game keypad information: Use arrow keys to move. Press P to pause the game.

Game Features:

Track the elapsed time and calculate the score.
A secret box appears every 10 seconds with an 80% chance of providing a benefit (+1 health) or 20% chance of causing harm (-1 health).
Pause functionality with the "P" key to freeze the game (not end it).
