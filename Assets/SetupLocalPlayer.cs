using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    // Use this for initialization
    void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        
        if (isLocalPlayer)
        {
            playerController.enabled = true;
            CameraFollow360.player = this.gameObject.transform;
        }
        else
        {
            playerController.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
