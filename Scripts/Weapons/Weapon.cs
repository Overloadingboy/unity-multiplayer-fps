using Mirror;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float range = 100f;
    [SerializeField] private int ammo = 30;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    private float lastFireTime = 0f;
    private Camera mainCamera;

    private void Start()
    {
        if (isLocalPlayer)
        {
            mainCamera = GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time - lastFireTime < fireRate || ammo <= 0) return;

        lastFireTime = Time.time;
        ammo--;

        CmdFire(mainCamera.transform.position, mainCamera.transform.forward);
    }

    [Command]
    private void CmdFire(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            if (hit.collider.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }

        RpcOnFire(origin, direction);
    }

    [ClientRpc]
    private void RpcOnFire(Vector3 origin, Vector3 direction)
    {
        Debug.Log("Weapon fired!");
    }

    public int GetAmmo()
    {
        return ammo;
    }

    [Command]
    public void CmdReload(int reloadAmount)
    {
        ammo += reloadAmount;
    }
}
