# TODO


## Animation to do
- strategy from bomberman for queueing
- When get hit by enemy: falls to ground and stays there.
- Life transform position to character. Life gets disabled.
- Character gets up and keeps moving right.
- When character falls down and stays down when there are no more lives, reset whole game.

##
- **Playtesting**!!!

- camera zoom out?

## Grading
- look at rubric
- get rid of unused/commented out code
- resolve all TODOs

## For build
- Whatever target works but probably WebGL
- Application.Quit();


## Game overall
- Enemies spawn and lerp toward player
- (important) **key controls displayed**
- win condition
- lose condition
- animations for both of those


- Ben idea- make more varied environments with tileset, and have player move automatically after defeating enemies
	- e.g. player climbs up rocks, approaches enemy, stops for QTE with sliders, kill or die animation, climbs down, next enemy etc.
	- could just walk off screen for screen transitions, but also consider one scrolling scene with sliders at constant position
	- sliders should only pop up during QTEs

## Animation
- Logic should be similar to that in PlayerController script
- Main character
- Game over
- Lose heart

## Sound
- Use a different synth preset for bg
	- or make original patch
- sine wave or triangle wave for oscillator?
	- or make original patch
- Or make actual good bg music and have the slowdown mechanic for quicktime events (using pitch)

## Textures
- (important) **center slider**
- target: closed eye w/ eyelashes and then different color when open
	- same size as regular eyes
- glowing particle effect around target eyes
- ray from eyes toward enemy
- enemies
- different slider shapes etc
- QTE bar/circle? (don't need this because measured by enemy)
- parallax scrolling bg
- particle effects around sliders
- different sized eyes on sliders
- light source- would be quick to implement and look cool


## Various issues
- Slider move speed is tied to frame rate- ask Benno about this