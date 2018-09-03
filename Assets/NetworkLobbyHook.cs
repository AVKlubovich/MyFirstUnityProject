using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;


public class NetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetupLocalPlayer car = gamePlayer.GetComponent<SetupLocalPlayer>();

        car.pName = lobby.playerName;
        car.pColor = ColorUtility.ToHtmlStringRGBA(lobby.playerColor);
    }
}
