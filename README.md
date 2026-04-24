# Stage Fright Light VR Project

This repository contains a Unity VR presentation project. The Unity project is now flattened at the repository root, so the main Unity folders are visible immediately when you open the repo.

## Table of Contents
- [Project Structure](#project-structure)
- [Setup](#setup)
- [Scene Flow](#scene-flow)
- [Adding Your Own Slides](#adding-your-own-slides)
- [Lighting Setup](#lighting-setup)
- [Main Menu Usage](#main-menu-usage)
- [End Screen](#end-screen)
- [Contributing](#contributing)

## Project Structure

- `README.md` - repository overview and setup notes
- `LICENSE` - MIT license for the project
- `Assets/` - scenes, scripts, media, and prefabs
- `Packages/` - Unity package manifest and lockfile
- `ProjectSettings/` - Unity project configuration
- `packages.config` - legacy Unity package configuration file

## Setup

1. Clone the repository and open the Unity project folder:
   ```sh
   git clone https://github.com/sananakther6642/Stage-Fright-Light-VR-Project.git
   cd Stage-Fright-Light-VR-Project
   ```

2. Open the repository root in Unity Hub.

3. Use Unity `2022.3.21f1` or a compatible 2022.3 LTS editor.

4. Let Unity restore packages from `Packages/manifest.json`.

5. Make sure the VR packages are installed. The project already references `XR Interaction Toolkit`, `XR Management`, and `OpenXR`.

## Scene Flow

The build order is intended to be:

1. `MainMenu`
2. `Room`
3. `Room End`

`MainMenu` starts the experience, `Room` contains the presentation, and `Room End` is the final scene before the application exits.

## Adding Your Own Slides

1. Prepare your slide images in a supported format such as PNG or JPG.

2. Import the images into `Assets`.

3. Create matching `UI > Image` objects for each slide if they are not already present.

4. Select the GameObject with the `Switch` script and assign your images to the `backgroundCanvas1` and `backgroundCanvas2` arrays.

5. Keep both arrays aligned and the same length so the corresponding slides stay in sync.

6. Use the left or right controller thumbstick to move between slides. In the editor, the left and right arrow keys also work.

## Lighting Setup

The `LightController` script supports two lighting modes.

### Constant Lighting

1. Select the `LightController` GameObject in the scene.

2. Assign the lights you want to control in the `lights` array.

3. Enable `constantIntensity` to keep the lights at their final intensity.

### Gradual Lighting

1. Disable `constantIntensity`.

2. Set `duration` to control how long the intensity takes to ramp up.

3. The script fades from `0` to `0.6` over the chosen duration.

## Main Menu Usage

1. Navigate the menu with the VR controller thumbstick.

2. Move up or down to change the selection between `Play` and `Quit`.

3. Pull the right controller trigger to activate the selected item.

## End Screen

1. Press `Esc` to advance to the next scene in the build order.

2. When the last scene is reached, the application quits.

3. In the Unity Editor, the final `Esc` press stops Play Mode instead of closing the editor.

## Contributing

1. Create a feature branch from the latest main branch.

2. Make your changes in the Unity project at the repository root.

3. Test the project in Unity before opening a pull request.

4. Keep scene order, script field names, README instructions, and repository layout in sync when you make changes.

---

License: MIT. If you find a mismatch between the README and the Unity project, update both together so the repository structure and the documentation stay aligned.
