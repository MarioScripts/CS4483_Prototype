
# Osiris Game Prototype

### Game Description
Osiris, the Egyptian god of the dead, resurrection, and afterlife, notices that some pyramids have been broken into and plundered by thieves. Angered, he resurrects you – Tutankhamun – to act out his revenge. However, Osiris is not easily pleased. He expects you to chase and destroy the thieves that are fleeing the pyramid as well as retrieve the stolen artifacts.

### Prototype Description
The game currently is a vertical slice that focuses on item generation/interaction as well as enemy spawning and attacking. There is no player death, and the momentum system for the player is implemented at a rudimentary level (player does not increase or decay in momentum, there are no boon spawners to increase momentum, enemy projectiles don't decrease momentum, etc.)

The prototype has two levels with different enemy configurations. Once the 2nd level is complete, the game will loop the 2nd level indefinitely.

Important notes:

 - The art is placeholder art which will be replaced with an Egyptian-themed art style when more work can be done. All art has been taken from the Unity asset store.
 - Currently at the end of a level, there is guaranteed to be 3 items that spawn unless there are no less than 3 items available for the player. This is in place just for the prototype to increase the likelihood of seeing all items within playing the 2 levels. Item spawning will be more scarce when in later stages of the game.
 - Ground slime on 2nd level will carry the player back. Since no death mechanics are implemented yet, when the player gets moved out of frame, he will reposition back into frame. This will be changed to the player dying in later releases.


### Game Controls
**Move:** W and S will move the player up or down to the next track, respectively.

**Attack:** Space will fire a projectile from the player. Holding down space will auto-fire.

**Use active item:** E *(Note that you do not start with an active item)*

**Pick up item:** F
