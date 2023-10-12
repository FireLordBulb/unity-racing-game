DON'T GRADE THIS BRANCH. GRADE MASTER






# unity-racing-game
Vilgot Ottestig

The game uses the agreed upon Unity version 2022.3.9f1.

You start the game in the scene GameScene (the only scene).

The game has two players, Red and Green car, and is always local multiplayer. Red car uses WASD while Green car uses arrow keys. You accelerate with W or Up-key, turn left/right with A/D or Left/Right-keys, and reverse/brake with S or Down-key. You pause/unpause with ESC or P.

You start playing the game by choosing one of the 3 levels in the main menu. The level will then load and the race will immediately start. The first car to complete the required number of laps wins. There are two power-ups that can appear. The red-orange one gives a speed boost by increasing the acceleration force and the blue one gives an Ice Curse that makes a car have less friction. The effects dissappear after a predetermined amount of time. Power-ups spawn at a random position on the track after a random amount of time when there are less than 2 power-ups on the track.

The cars work by having a Rigidbody2D and using AddRelativeForce and AddTorque on the Rigidbody2D when the relevant keys are pressed. The (linear) drag of the Rigidbody2Ds is updated every frame to be higher when the car's velocity is more perpendicular with it's rotation. This makes the cars feel less like they're pucks on ice and more like they have wheels with a direction. The finish line uses math to calculate which direction a car enters it/exits it from, to add/subtract a lap depending on that direction, so no checkpoints are needed to ensure that cars have to drive around the whole track for laps to count. Power-ups use a List of the positions of the track sections to determine where they can spawn. Everything is in one scene, so instead of a SceneManager loading and unloading scenes I have a LoadingManager loading and unloading Prefabs.
  
Asset source(s):
I use (recolorings of) the white 4x4 car sprite from https://assetstore.unity.com/packages/2d/characters/2d-urban-cars-89754 for my cars. For everything else I use two pngs I made myself using Paint3D and default unity shape sprites.

Code Source(s)/Tutorial(s):
I didn't follow any tutorials or look at example code for any of the features I wanted in this game but I did watch https://www.youtube.com/watch?v=dtv7mjj_iog while working on the project. Most of the methods in the video I was already using but the one thing I changed in my code because of the video was that I added TryGetComponent in relevant places, following the video's advice.

Vilgot Ottestig