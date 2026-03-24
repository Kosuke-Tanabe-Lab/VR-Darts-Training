# VR Soft-Tip Dart-Throwing Performance

This repository contains a VR dart simulation system developed as part of a research study on motor performance and throwing accuracy. The system is specifically designed to replicate the conditions of **soft-tip dart throwing** within a virtual environment.

## Overview
The simulation was developed using the Unity game engine (Unity Technologies, San Francisco, CA, USA) and is optimized for the **Meta Quest 2** VR headset and its accompanying Touch controllers. 

### Key Features:
* **Realistic Environment:** The virtual space replicates real-world assessment conditions, with the center of the dartboard positioned **173 cm** above the floor and a horizontal throwing distance of **244 cm**.
* **Physics-Based Simulation:** Dart flight trajectories are calculated using Unity’s built-in physics engine to ensure a realistic representation of throwing mechanics.
* **Data Accuracy:** Unlike real-world soft-tip darts where bounce-outs can occur, this system is programmed to ensure all throws land on the board for consistent data collection.
* **Performance Feedback:** After each throw, the distance from the impact point to the center of the board is displayed in-headset (in centimeters, to one decimal place).

---

## Controls
Participants use the Touch controller in their dominant hand to interact with the system.

| Action | Button | Description |
| :--- | :--- | :--- |
| **Grasp Dart** | **Press & Hold B Button** | Grasp the virtual dart to prepare for a throw. |
| **Release Dart** | **Release B Button** | Release the button in coordination with the throwing motion to launch the dart. |
| **Feedback** | **Automatic** | The distance from the center is displayed on-screen immediately after impact. |
| **Reset System** | **Press & Hold A Button** | Resets the impact position and clears the display for the next throw. |

---

## Installation Guide (for Peer Reviewers)
To verify the system or replicate the experiment, follow these steps to install the application on a Meta Quest 2 headset:

1. **Enable Developer Mode:** Ensure your Meta account is registered as a developer and "Developer Mode" is enabled on your Quest 2 via the Meta Horizon mobile app.
2. **Download SideQuest:** Install the [SideQuest Advanced Installer](https://sidequestvr.com/setup-howto) on your PC or Mac.
3. **Download the APK:** Navigate to the **[Releases]** section of this repository and download the latest `.apk` file.
4. **Install via SideQuest:** Connect your Quest 2 to your PC via USB, open SideQuest, and use the **"Install APK file from folder"** button to transfer the file to your headset.
5. **Launch the App:** On your Quest 2, go to the **App Library**, select the search/filter bar, and navigate to **"Unknown Sources"**. Locate and launch **VR-Darts-Training**.
