using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class SetupLocalPlayer : NetworkBehaviour
{
    public Text namePrefab;
    public Text nameLabel;
    public Transform namePos;
    string textboxName = "";

    [SyncVar (hook = "OnChangeName")]
    public string playerName = "player";

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

        this._prepareName();
        this.OnChangeName(this.playerName);
    }

    private void _prepareName()
    {
        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        this.nameLabel = Instantiate(this.namePrefab, Vector3.zero, Quaternion.identity) as Text;
        this.nameLabel.transform.SetParent(canvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(this.namePos.position);
        this.nameLabel.transform.position = nameLabelPos;
    }
    
    void OnGUI()
    {
        if (isLocalPlayer)
        {
            this.textboxName = GUI.TextField(new Rect(25, 15, 100, 25), this.textboxName);
            if (GUI.Button(new Rect(130, 15, 55, 25), "Change"))
            {
                CmdChangeName(this.textboxName);
                this.textboxName = "";
                GUI.TextField(new Rect(25, 15, 100, 25), this.textboxName);
            }
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        this.playerName = newName;
        this.nameLabel.text = this.playerName;
    }

    public void OnChangeName(string newName)
    {
        this.playerName = newName;
        this.nameLabel.text = this.playerName;
    }
}
