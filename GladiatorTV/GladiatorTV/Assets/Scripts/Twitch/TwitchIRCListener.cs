using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
public class TwitchIRCListener : MonoBehaviour {


    private TwitchIRC IRC;
    public TwitchChatController chatController;
    // Use this for initialization
    void Start () {
        IRC = this.GetComponent<TwitchIRC>();
        //IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
    }

    void OnChatMsgRecieved(string msg)
    {

        //parse from buffer.
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
        string user = msg.Substring(1, msg.IndexOf('!') - 1);

        Debug.Log(msgString);

        chatController.TranslateMessage(msgString);
        
    }
}
