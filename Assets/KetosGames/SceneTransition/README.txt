Scene Transition

This it to be used to smoothly transition between scenes with a nice loading screen in between.

---------------------------------
VR Mode
---------------------------------
VR mode is specifically for Virtual Reality support. It will make sure that the canvas
is placed in wold space and always in front of the camera. For best results, use the
Loading Scene or a solid color Loading UI when VR Mode is on (See next section).
If you do not need VR support, it's better to leave this off.

---------------------------------
Two options for a loading screen
---------------------------------
1. A Loading Scene
2. A Loading UI

---------------------------------
A Loading Scene
---------------------------------
To use this mode, select the SceneLoader Prefab and check the 'Use Scene For Loading Screen' checkbox.
During load, a scene will be used to show the loading screen.
This is usful if you want to show 3D objects during loading.
In order for this to work, the Loading scene will need to be added to the Build Settings.
Feel free to modify the Loading scene to your liking.

---------------------------------
A Loading UI
---------------------------------
To use this mode, select the SceneLoader Prefab and uncheck the 'Use Scene For Loading Screen' checkbox.
A Loading UI will keep the current scene loaded until the next scene is ready.
This has the added benefit that the new scene can begin loading immediatly during the first fade out.
No need to Loading scene to the Build Settings as it will not be used in this mode.

---------------------------------
Loading a Scene
---------------------------------
To load a scene, simply call SceneLoader.LoadScene('NameOfSceneToGoTo');
The SceneLoader will automatically be added and the transition will take place.

---------------------------------
SceneLoader Prefab Settings
---------------------------------
Use Scene For Loading Screen - When checked, use the Loading scene as the Loading screen (instead of the Loading UI).
Loading Scene Name - The name of the Loading scene to load.
Fade In Loading Screen - When checked,  fade in the loading screen.
Fade Out Loading Screen - When checked, fade out the loading screen.
Fade Seconds - The number of seconds to show the loading screen after fade in. Set it to 0 to go to the new scene as soon as it's ready.
Fade Color - The color to use in the fade animation.

---------------------------------
Tips
---------------------------------
1. If you would like to only show the loading screen when it's needed, lower the 'Minumum Loading Screen Sceonds' to 0.
2. If you would like the game to start with a fade in, simply add the SceneLoader prefab (Assets/Resources/Ketos Games/Scene Transition/SceneLoader) to the first scene.
3. If you would like to see it in action, add Scene 1 and Scene 2 to the Build Settings, start one of the scenes and try it out.
4. If you don't really want a loading screen at all, set 'Minumum Loading Screen Sceonds' to 0, set 'Use Scene For Loading Screen' to unchecked, 
set the background of the Loading Sceen in the Scene Loader Prefab to match the Fade Color and remove all the UI elements under the Loading Screen 
in the SceneLoader Prefab.

---------------------------------
The essential components included
---------------------------------
Assets/KetosGames/SceneTransition/Scenes/Loading         (If using the Loading Scene
Assets/KetosGames/SceneTransition/Scripts/LoadingText    (If used in the Loading Screen)
Assets/KetosGames/SceneTransition/Scripts/LoadingSpinner (If used in the Loading Screen)
Assets/KetosGames/SceneTransition/Scripts/SceneLoader
Assets/KetosGames/SceneTransition/Images/LoadingStar     (If used in the Loading Screen)
Assets/Resources/KetosGames/SceneTransition/SceneLoader