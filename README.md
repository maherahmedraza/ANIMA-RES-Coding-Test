# ANIMA-RES-Coding-Test
This is a Unity project that implements seamless scene transition with camera control and object interaction. The app consists of three scenes. The first scene shows a timeline that allows users to navigate between scenes and displays the current scene. The second scene has three rotating spheres, and the user can select one of them to transition to the third scene. The third scene displays the selected sphere and additional objects that fade in, and a button allows restarting at the first scene.

## Installation

1. Clone or download the project
2. Open the project in Unity 2021.3.24f1 or later
3. Load the first scene, `Scene-1`, in the `Scenes` folder
4. Press the Play button in Unity to run the app

## Usage

### Timeline

The timeline is visible at all times, and it allows the user to navigate between the three scenes. The current scene is displayed as selected, and the other scenes are displayed as unselected. To navigate between scenes, click on the corresponding button.

### Camera Control

The user can rotate the camera around the scene and zoom in and out. To rotate the camera, click and drag the left mouse button. To zoom in and out, scroll the mouse wheel.

### Object Interaction

Each element in the scene is rotatable by the user. To rotate an object, click and drag the left mouse button on the object.

### Scene-1

The first scene displays a title that fades in.

### Scene-2

The second scene displays three spheres that rotate around each other. The user can select any of the spheres by clicking on them. When a sphere is pressed, a transition starts, and the remaining spheres fade out. The selected sphere moves to the center of the third scene.

### Scene-3

The third scene displays the selected sphere and additional objects that fade in. A button allows restarting at the first scene.

## Credits

This project uses the following assets:

- DOTween (https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- TextMeshPro (https://assetstore.unity.com/packages/essentials/beta-projects/textmesh-pro-84126)

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.