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