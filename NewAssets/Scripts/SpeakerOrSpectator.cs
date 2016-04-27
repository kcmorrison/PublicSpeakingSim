using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

public class SpeakerOrSpectator : NetworkManager {

    public static bool speakerSpawned = false;
    public GameObject playerPrefabSpecial;
    public GameObject spectatorPrefab;
    public GameObject mainCamera;
    public GameObject speakerSpawn;
    public Transform[] spectatorSpawn;
    public NetworkManagerHUD managerHub;
    byte numMatches = 1;
    Dictionary<string, byte> matches = new Dictionary<string, byte>();




    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //mainCamera.SetActive(false);
        GameObject player;
        
            if (!speakerSpawned)
            {
                player = (GameObject)GameObject.Instantiate(playerPrefabSpecial, speakerSpawn.transform.position, speakerSpawn.transform.rotation);
                player.GetComponent<Player>().isSpeaker = true;
                //Debug.Log("Spawning speaker");
                speakerSpawned = true;
                PhotonVoiceRecorder rec = player.GetComponent<PhotonVoiceRecorder>();
                rec.Transmit = true;

                PhotonVoiceNetwork.Client.DebugEchoMode = true;
             }
            else
            {
                int i = 0;
                player = (GameObject)GameObject.Instantiate(spectatorPrefab, spectatorSpawn[i].position, spectatorSpawn[i].transform.rotation);
            //Debug.Log("Speaker already spawned");
            //PhotonVoiceRecorder.Transmit = false;
            }
         NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);


    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

    }
    /*
    public override void OnMatchCreate(CreateMatchResponse matchInfo)
    {
        if (matchInfo.success)
        {
            matches[NetworkManager.matchName] = numMatches;
            PhotonVoiceNetwork.GlobalAudioGroup = matches[NetworkManager.matchName];
            numMatches++;
            StartHost(new MatchInfo(matchInfo));
        }
    }
    */
    // Use this for initialization
    void Start() {
        #if UNITY_5_3
                                PhotonNetwork.ConnectUsingSettings(string.Format("1.{0}", UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));
        #else
                PhotonNetwork.ConnectUsingSettings(string.Format("1.{0}", Application.loadedLevel));
        #endif

        PhotonVoiceNetwork.Connect();


        PhotonVoiceNetwork.Client.GlobalAudioGroup = 0;
        PhotonVoiceNetwork.Client.DebugEchoMode = true;

    }

    public void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        PhotonNetwork.Disconnect();
        PhotonVoiceNetwork.Disconnect();
    }

    public void OnJoinedRoom()
    {
        Debug.Log("joined room");
        managerHub.enabled = true;
    }

        // Update is called once per frame
    void Update () {

    }
}
