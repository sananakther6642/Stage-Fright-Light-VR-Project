# Stage Fright Light VR Project

Welcome to the Stage Fright Light VR! This project allows you to create an immersive VR experience with navigable slides, a main menu accessible via mouse, and an end screen that can be exited using the keyboard. Follow the instructions below to set up and customize your project.

## Table of Contents
- [Setup](#setup)
- [Adding Your Own Slides](#adding-your-own-slides)
- [Lighting Setup](#lighting-Setup)
- [Main Menu Usage](#main-menu-usage)
- [End Screen](#end-screen)
- [Contributing](#contributing)
- [License](#license)

## Setup

1. **Clone the Repository or open the unity package**:
   ```sh
   git clone https://github.tik.uni-stuttgart.de/st157868/VRAR.git
   cd VRAR/StageFright_Light_VR/
   ```

2. **Open in Unity**:
   - Open Unity Hub.
   - Click on `Open` and navigate to the cloned repository folder.
   - Select the project to open it in Unity.

3. **Set Up VR**:
   - Ensure you have the `XR Interaction Toolkit` installed via the Unity Package Manager.
   - Configure your project for VR usage under `Project Settings` > `XR Plugin Management`.

4. **Open the Room Scene**:

   After importing the Unity package, navigate to the Scenes folder and open the Room scene.

5. **Main Menu Scene**:

   If you want to see the main menu, open the Main Menu scene in the Scenes folder. In the main menu, use the mouse to interact with the buttons.

6. **Running the Presentation**:

   While in the Room scene and the presentation is running, you can press the Escape key on your keyboard to continue to the Results screen.

7. **Results Screen**:

   In the Results screen, you can press the Escape key again to end the project.

## Adding Your Own Slides

1. **Prepare Your Slides**:
   - Create images for each of your slides.
   - Ensure the images are in a supported format (e.g., PNG, JPG).

2. **Import Slides into Unity**:
   - Drag and drop your slide images into the `Assets` folder in Unity.

3. **Create Image Objects**:
   - In the `Hierarchy` window, create a new `UI` > `Image` object for each slide.
   - Assign your slide images to the `canvasBeamer`and `canvasLaptop` components.

4. **Assign Slides to Switch Script**:
   - Select the `MainMenuManager` GameObject in the `Hierarchy`.
   - In the `Inspector`, find the `Switch` script component.
   - Add your `Image` objects to the `backgroundCanvas1` and `backgroundCanvas2` arrays.
   - Hint : Both arrays should contain similiar images

## Lighting Setup

In our Unity project, we provide two types of lighting: Constant and Gradual. Here's how you can set them up:

### Constant Lighting

1. Open the Unity project and navigate to the room scene.
2. In the Hierarchy panel on the left, select the `LightController` object.
3. In the Inspector panel on the right, you will see a checkbox labeled `Constant Light`.
4. Check this box to enable constant lighting in the scene.

### Gradual Lighting

1. If you want gradual lighting, make sure the `Constant Light` checkbox is unchecked.
2. Below the `Constant Light` checkbox, there is a text field labeled `duration`.
3. Enter the desired duration (in seconds) for the gradual lighting into this field.

## Main Menu Usage

1. **Access Main Menu**:
   - The main menu is designed to be navigated using a mouse.

2. **Interact with Menu Items**:
   - Hover over menu items to highlight them.
   - Click on "Play" to start the presentation.
   - Click on "Quit" to exit the application.

## End Screen

1. **Exit the Presentation**:
   - At any point during the presentation, press the `Esc` key on your keyboard to end the experience and return to the main menu.

## Contributing

We welcome contributions to this project! To contribute:

1. **Fork the Repository**:
   - Click on the `Fork` button at the top right of the repository page.

2. **Clone Your Fork**:
   ```sh
   git clone https://github.tik.uni-stuttgart.de/st157868/VRAR.git
   ```

3. **Create a New Branch**:
   ```sh
   git checkout -b feature/YourFeatureName
   ```

4. **Make Changes**:
   - Implement your feature or bug fix.

5. **Commit and Push**:
   ```sh
   git add .
   git commit -m "Add feature or fix"
   git push origin feature/YourFeatureName
   ```

6. **Create a Pull Request**:
   - Go to the original repository on GitHub and create a pull request from your fork.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Thank you for using the Stage Fright Light VR Project! We hope it provides an engaging and immersive experience for your presentations. If you encounter any issues or have any questions, feel free to open an issue on GitHub. Happy presenting!
