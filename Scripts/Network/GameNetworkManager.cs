using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    public static GameNetworkManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("[SERVER] Server started successfully");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("[CLIENT] Connected to server");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        Transform spawnPoint = GetStartPosition();
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        Debug.Log("[SERVER] Player spawned for connection: " + conn.connectionId);
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("[CLIENT] Disconnected from server");
    }
}
