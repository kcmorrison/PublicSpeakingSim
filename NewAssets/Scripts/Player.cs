using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fps;
    public GameObject cameraControls;
    public bool isSpeaker = false;
    PhotonVoiceRecorder rec;
    public GameObject instructionsText;
    public int countdownSeconds = 3;
    public int currentSeconds = 0;
    public TextMesh countdownText;
    public TextMesh timerText;
    bool isRunning = false;
    //VoiceChatNetworkProxy proxy;
    /*
    void OnConnectedToServer()
    {
        proxy = VoiceChatUtils.CreateProxy();
    }
    */    

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            fps.enabled = false;
            cameraControls.SetActive(false);
        }
        if (isSpeaker)
        {
            //fps.enabled = false;
            rec = gameObject.GetComponent<PhotonVoiceRecorder>();
            rec.Transmit = true;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        IEnumerator timer;
        IEnumerator countdown;
        if (rec != null && Input.GetKeyDown(KeyCode.S))
        {
            instructionsText.SetActive(false);
            if (!isRunning) {
                isRunning = true;
                //countdown = StartCoroutine(countdown());
                rec.Transmit = true;
                //timer = StartCoroutine(timer());
            }
            else
            {
                isRunning = false;
                //StopCoroutine(countdown);
                //StopCoroutine(timer);
            }
        }
       

    }

    IEnumerator countdown()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = countdownSeconds.ToString();
        while (countdownSeconds > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
            countdownText.text = countdownSeconds.ToString();
        }
        countdownText.gameObject.SetActive(false);
    }

    IEnumerator timer()
    {
        timerText.gameObject.SetActive(true);
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            currentSeconds++;
            if(currentSeconds%60 > 10) {
                timerText.text = (currentSeconds / 60).ToString() + ":" + (currentSeconds % 60).ToString();
            }
            else
            {
                timerText.text = (currentSeconds / 60).ToString() + ":0" + (currentSeconds % 60).ToString();
            }

        }
    }


}
