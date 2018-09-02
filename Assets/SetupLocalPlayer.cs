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
    string textBoxName = "";

    string colorBoxName = "";

    [SyncVar (hook = "OnChangeName")]
    public string playerName = "player";

    [SyncVar (hook = "OnChangeColor")]
    public string playerColor = "#ffffff";

    public override void OnStartClient()
    {
        base.OnStartClient();

        Invoke("UpdateStates", 1);
    }

    void UpdateStates()
    {
        OnChangeName(playerName);
        OnChangeColor(playerColor);
    }

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
        //determine if the object is inside the camera's viewing volume
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(this.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 &&
            screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        //if it is on screen draw its label attached to is name position
        if (onScreen)
        {
            Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(this.namePos.position);
            this.nameLabel.transform.position = nameLabelPos;
        }
        else //otherwise draw it WAY off the screen 
        {
            this.nameLabel.transform.position = new Vector3(-1000, -1000, 0);
        }
    }
    
    void OnGUI()
    {
        if (isLocalPlayer)
        {
            this.textBoxName = GUI.TextField(new Rect(25, 15, 100, 25), this.textBoxName);
            if (GUI.Button(new Rect(130, 15, 55, 25), "Change"))
            {
                CmdChangeName(this.textBoxName);
                this.textBoxName = "";
                GUI.TextField(new Rect(25, 15, 100, 25), this.textBoxName);
            }

            this.colorBoxName = GUI.TextField(new Rect(190, 15, 100, 25), this.colorBoxName);
            if (GUI.Button(new Rect(295, 15, 55, 25), "Change"))
            {
                CmdChangeColor(this.colorBoxName);
                this.colorBoxName = "";
                GUI.TextField(new Rect(190, 15, 100, 25), this.colorBoxName);
            }
        }
    }

    Color ColorFromHex(string hex)
    {
        hex = hex.Replace("0x", "");
        hex = hex.Replace("#", "");
        byte a = 255;
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color32(r, g, b, a);
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

    [Command]
    public void CmdChangeColor(string newColor)
    {
        this.OnChangeColor(newColor);
    }

    public void OnChangeColor(string newColor)
    {
        this.playerColor = newColor;
        Renderer[] rends = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in rends)
        {
            if (r.gameObject.name == "BODY" ||
                r.gameObject.name == "LEFT_MIRROR" ||
                r.gameObject.name == "RIGHT_MIRROR")
            {
                r.material.SetColor("_Color", ColorFromHex(this.playerColor));
            }
        }
    }

    public void OnDestroy()
    {
        if (this.nameLabel != null)
        {
           Destroy(this.nameLabel.gameObject);
        }
    }
}
