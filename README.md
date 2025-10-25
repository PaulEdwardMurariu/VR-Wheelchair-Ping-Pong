# 🏓 VR Wheelchair Ping Pong

**VR Wheelchair Ping Pong** is an accessibility-focused VR ping pong game created for wheelchair users.  
Built in **Unity (2022.3.31f1)** with **SteamVR/Oculus** support, the project emphasizes **realistic physics**, **adaptive AI opponents**, and **sub-10 ms motion tracking** for smooth, immersive gameplay.  

This repository combines all documentation, images, and the full Unity source code for the 2024-2025 senior design project.

---

## 🎯 Project Overview
The goal of this project was to design a **fully playable, accessible VR ping pong experience** for users in wheelchairs—providing intuitive motion controls, accurate ball-paddle interaction, and a user-friendly interface optimized for VR comfort.  

The game includes adaptive AI difficulty, realistic sound effects, and performance tuning for 60 + FPS.

---

## 👥 Team 3 — 2024 / 2025 Senior Design
| Member | Role |
|--------|------|
| **Paul Edward Murariu** | Software Engineer – AI Logic, SteamVR/Oculus Integration |
| **Luca Irimie** | Lead Developer – Gameplay Mechanics, Environment Design |
| **Sebastian Kuka** | Risk Management, Testing, Documentation |
| **Leia Sayed** | UI / UX Design, Accessibility Features |

Supervised by **Professor Bruce Maxim**  
Department of Computer and Information Science, University of Michigan-Dearborn  

---

## 🧩 Technologies Used
- **Unity 2022.3.31f1**
- **C#**, **ShaderLab**, **HLSL**
- **SteamVR / Oculus SDKs**
- **Blender 3D** (for assets)
- **Git + GitHub** (version control)
- **Google Docs / Slides / Sheets** (documentation)

---

## 📁 Repository Structure

VR-Wheelchair-Ping-Pong/
│
├── Project Plan and RMMM Presentation/
├── Requirements Document (full SRS) Presentation/
├── Software Quality Assurance Presentation/
├── VR Wheelchair Ping Pong – Use Case Presentation/
│
├── Alpha Demo Screenshots/ ← Images from early build
│
├── VRWCPP_SenDesign/ ← Full Unity source + executable
│ ├── Assets/
│ ├── Packages/
│ ├── ProjectSettings/
│ ├── .gitignore
│ ├── README.md
│ └── (Unity scenes, prefabs, builds, etc.)
│
└── README.md

The `VRWCPP_SenDesign/` folder contains the complete Unity project and build files, integrated from  
[LucaIri/VRWCPP_SenDesign](https://github.com/LucaIri/VRWCPP_SenDesign) via Git subtree.

---

## 🧠 Key Features
- 🏓 Realistic ball-paddle physics and collision detection  
- 🧍‍♂️ Wheelchair-accessible motion system with smooth tracking  
- 🤖 Adaptive AI opponent (difficulty scaling)  
- 🎮 Intuitive menu navigation and controller input mapping  
- ⚙️ Optimized for 60 + FPS on Oculus / SteamVR  
- 🔉 Immersive sound and environment feedback  

---

## 🧭 Quick Start — Opening in Unity
1. Clone this repository:  
   ```bash
   git clone https://github.com/PaulEdwardMurariu/VR-Wheelchair-Ping-Pong.git
