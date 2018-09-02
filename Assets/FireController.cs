using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class FireController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;



    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * 2000);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 50;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 4);
    }
}
