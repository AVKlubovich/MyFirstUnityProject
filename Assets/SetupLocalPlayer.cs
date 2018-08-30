using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    // Use this for initialization
    void Start ()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        playerController.enabled = isLocalPlayer;
    }

    // Update is called once per frame
    void Update ()
    {
    }
}
