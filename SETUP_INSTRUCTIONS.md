# Setup Instructions for Unity Multiplayer FPS

## Prerequisites
- Unity 2021 LTS or later
- Git
- Visual Studio or Rider (for C# editing)

## Step 1: Create Unity Project

1. Open Unity Hub
2. Click "New Project"
3. Select **3D (URP)** template
4. Name it "MultiplayerFPS"
5. Create the project

## Step 2: Install Mirror Networking

1. Open your project in Unity
2. Go to **Window > TextMesh Pro > Import TMP Essential Resources** (if prompted)
3. Go to **Window > Package Manager**
4. Click the **+** button and select "Add package from git URL"
5. Paste: `https://github.com/vis2k/Mirror.git?path=/Assets/Mirror`
6. Click Add and wait for installation

## Step 3: Project Folder Structure

Create these folders in `Assets/`:
```
Assets/
├── Scenes/
├── Scripts/
│   ├── Player/
│   ├── Network/
│   ├── Weapons/
│   └── UI/
├── Prefabs/
├── Materials/
└── Resources/
```

## Step 4: Add Scripts

1. Copy the scripts from this repository into `Assets/Scripts/`
2. Organize them into their respective folders (Player/, Network/, Weapons/)

## Step 5: Create Main Scene

1. Create a new scene: **Right-click in Project > Scene**
2. Name it "Main"
3. Save it to `Assets/Scenes/Main.unity`
4. Add to Build Settings: **File > Build Settings > Add Open Scenes**

## Step 6: Scene Setup

### Add Ground:
1. Right-click in Hierarchy > 3D Object > Plane
2. Scale it to (10, 1, 10)
3. Add a **Collider** component

### Add NetworkManager:
1. Create empty GameObject: **Right-click > Create Empty**
2. Name it "NetworkManager"
3. Add component: **Mirror > Network Manager**
4. Drag the PlayerController prefab to the Player Prefab slot

### Add Player Spawn Point:
1. Create empty GameObject named "SpawnPoint"
2. Position it at (0, 1, 0)
3. Tag it as "SpawnPoint"

## Step 7: Create Player Prefab

1. Create a new empty GameObject in the Hierarchy named "Player"
2. Add these components:
   - **Capsule** (3D Object) as child
   - **Rigidbody** (set mass to 1, freeze rotation)
   - **Capsule Collider**
   - **Camera** (as child, positioned at head height)
   - **NetworkIdentity** (Mirror)
   - **PlayerController.cs** script
   - **Health.cs** script
   - **Weapon.cs** script

3. Drag the Player GameObject to `Assets/Prefabs/` to create a prefab

## Step 8: Configure NetworkManager

1. Select NetworkManager in scene
2. In Inspector, set:
   - **Network Address**: localhost (for testing)
   - **Player Prefab**: Drag the Player prefab
   - **Auto Create Player**: Checked
   - **Player Spawn Method**: Random or specific spawn point

## Step 9: Build and Test

### Test Locally:
1. Click **Play** in Unity Editor (Server)
2. Build the game: **File > Build and Run**
3. Run the build as a Client
4. Test multiplayer functionality

### For Dedicated Server:
1. Build for Standalone (Windows/Mac/Linux)
2. Run with command: `game.exe -server`
3. Connect clients to server IP

## Troubleshooting

**Issue**: Mirror not found
- Solution: Ensure you installed Mirror from the Git URL correctly

**Issue**: Player doesn't spawn
- Solution: Check that Player Prefab has NetworkIdentity component

**Issue**: Movement not networked
- Solution: Ensure PlayerController inherits from NetworkBehaviour

## Next Steps

1. Customize player movement speed
2. Add more weapons
3. Implement respawn system
4. Add UI (health bar, ammo counter)
5. Deploy to dedicated server

For more info, check Mirror Documentation: https://mirror-networking.gitbook.io/
