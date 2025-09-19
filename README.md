====PLANNING====
break down task
1. Bird movement
   - Adjustable Jump force/Gravity
   - Have bird state if alive or dead
   - rotate up if jump, down if fall
   - gavity and jump work only if gmae start
2. Pipe movement
   - Adjustable speed/spawn rate/gap size
   - start spawning only if game start and stop immedietly after bird dead
3. Score
   - maybe store in .txt so it not disappear after close (not sure how, will decide later)
4. GameManager
   - check if game is starting or not
   - every adjustable data can be adjust from here

Decide to use PrimeTween to help with bird rotating/UI animation because it's in allow list and similar to DOTween(Less time to learn)
