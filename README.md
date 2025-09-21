====PLANNING====
break down task
1. Bird movement
   - Adjustable Jump force/Gravity
   - Have bird state if alive or dead
   - rotate up if jump, down if fall
   - gavity and jump work only if game start
2. Pipe movement
   - Adjustable speed/spawn rate/gap size
   - start spawning only if game start and stop immedietly after bird dead
3. Score
   - maybe store in .txt so it not disappear after close (not sure how, will decide later)
4. GameManager
   - check if game is starting or not
   - every adjustable data can be adjust from here

Decide to use PrimeTween to help with bird rotating/UI animation because it's in allow list and similar to DOTween(Less time to learn)

== Devlog ==
- make it exist first, scrub it later
  
[Add] Gameloop
- I never use listener before. but I saw it has been use a lot while doing my internship so I want to learn and used too.
- Decide to use it when GameState change so it easy to handle and more efficiency than make player check if game is start or not every time in Update().
- Now pipe spawn/player movement can be toggle without need to directly link GameManager in hirechy.
- Another thing I never use before but learn from my internship, input system. decide to use to in case to handle fute mobile port and reduce using Update() to check if there os any input or not.
- Yes I want to do anything to reduce using Update() for this project.
  
[Add] Score system
- I found peoples talking about PlayerPref on unity discussion webboard. after looking into it, this is better than my first idea about saving it seperate as .txt since the only thing I want to save is just high score and nothing else.
  
[Add] art asset
- grab a random art stuff form Kenny. Why player is fish? because fish is cool!
- list of asset taken :
https://www.kenney.nl/assets/fish-pack
https://www.kenney.nl/assets/background-elements-redux
https://www.kenney.nl/assets/simplified-platformer-pack
https://toffeecraft.itch.io/ui-user-interface-pack-pastel-dream
https://www.dafont.com/minecraft.font

[Add] Game Difficulty config
- in GameManager, can now config game difficulty (Change pipe spawn rate, spawn range and move speed).
- trigger when player score hit target of each difficulty.
- can easily add or remove difficulty level.

[Add] sound effect and fade scene
- took me around 1-2 hour to get use to PrimeTween, But I able to make screen fade to black while changing menu.
- I want it to have only script using to play sound so it easier than setting up audiosource to every gameobject that have sound. I seperate SoundManager to it sole script for this purpose.  
- I also want to make soundsystem better but since it not priority and time limit, this is what I can come up. (if have time, I want to change it to better enum system or maybe dictionary that match audionumber and name for other script to call?)
- sound credit! https://brainzplayz.itch.io/retro-sounds-32-bit

[Update] refactor
- rearrange UI code, currently it's too hard-code for changing one scene(to open menu, set this false, set that true) so I write new method to have all the UI close and make it open just what scene I needed.
- also decide to move UI to seperate script from GameMAnager in case more UI add to this project in future? it will be easier to edit
- adjust build setting so it not jumpscare with unadjustable full screen after open.
- Still have time to make fish jumping with tween!

== End of Devlog ==