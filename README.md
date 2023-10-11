# unity-racing-game
Vilgot Ottestig
The game used the agreed upon Unity version 2022.3.9f1.

You start the game in the scene GameScene (the only scene).

The game has two players, red and green car, and is always local multiplayer. Red car uses WASD while Green car uses arrow keys. You accelerate with W or Up-key, turn left/right with A/D or Left/Right-keys, and reverse/brake with S or Down-key. You pause/unpause with ESC or P.

You start playing the game by choosing one of the 3 levels in the main menu. The level will then load and the race will immediately start. The first car to complete the required number of laps wins. There are two power-ups that can appear. The red-orange one gives a speed boost by increasing the acceleration force and the blue one gives an Ice Curse that makes a car have less friction. The effects dissappear after a predetermined amount of time. Power-ups spawn at a random position on the track after a random amount of time when there are less than 2 power-ups on the track.

Asset source(s):
I use the white 4x4 car sprite from https://assetstore.unity.com/packages/2d/characters/2d-urban-cars-89754 for my cars. For everything else I use two pngs I made myself using Paint3D and default unity shape sprites.

Code Source(s)/Tutorial(s):
I didn't follow any tutorials or look at example code for any of the features I wanted in this game but I did watch https://www.youtube.com/watch?v=dtv7mjj_iog while working on the project. Most of the methods in the video I was already using but the one thing I changed in my code because of the video was that I added TryGetComponent in relevant places, following the video's advice.

Vilgot Ottestig