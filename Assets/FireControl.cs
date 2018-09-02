using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class FireControl : NetworkBehaviour {

	public GameObject bulletPrefab;
	public GameObject bulletSpawn;

	// Update is called once per frame
	void Update () 
	{
		if(!isLocalPlayer) return;

		if(Input.GetKeyDown("space"))
		{
			CmdShoot();
		}	
	}

	[Command]
	void CmdShoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward*2000);
		NetworkServer.Spawn(bullet);
		Destroy(bullet,1.0f);
	}

}
