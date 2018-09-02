using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    Animator animator;
    
    [SyncVar (hook = "OnChangeAnimation")]
    public string animState = "idle";

    void OnChangeAnimation(string aS)
    {
        id (isLocalPlayer)
        {
            return;
        }

        UpdateAnimationState(aS);
    }

    [Command]
    public void CmdChangeAnimState(string aS)
    {
        UpdateAnimationState(aS);
    }

    void UpdateAnimationState(string aS)
    {
        if (animState = aS)
        {
            return;
        }

        animState = aS;
        if (animState == "idle")
        {
            animator.SetBool("Idling", true);
        }
        else if (animState == "run")
        {
            animator.SetBool("Idling", false);
        }
        else if (animState == "attack")
        {
            animator.setTrigger("Attacking");
        }
    }

    void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Idling", true);

        if (isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
            CameraFollow360.player = this.gameObject.transform;
        }
        else
        {
            GetComponent<PlayerController>().enabled = false;
        }
    }
}
