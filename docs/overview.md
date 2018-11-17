---
title: Music Game Overview
date: 2018-11-17 03:16:55
---

# Assets Overview
...

--------------------------
# Scene
## Main menu
- Log in
- Sign up
## Help
Contain instructions for how to play the game
## Settings
Delay setting and other settings
## Song list
Select a specific song to play the game
## SampleScene
Play-game scene
## Score
Show score

-------------------------
# Scripts
## Activator.cs
...
## GameController.cs
Controls the high level rhythm game logic. Universal gameplay values are configured on this component, including Note Speed, LaneController​ specification, Lead In Time, and more.
The Event ID of the KoreographyTrack​to source for Note Objects is also specified here.
## GameManager.cs
Hit evaluation: Miss, Good, Great and Perfect.
Voice controller
## HPSlider.cs
Slider play mode
## Lane.cs
Lane controller.
Manages the logic for an individual Note Lane.
Lane-specific values are configured on this component, including Color, Target Visuals, the Keyboard K​eyCode​to use for input, and the T​extPayloads​to match KoreographyEvents​against.
## Login_and_reg.cs
Login and sign up in "Main menu" scene
## MusicManager.cs
(None)
## Note.cs
Drop-down notes controller
## Scores.cs
Update scores
