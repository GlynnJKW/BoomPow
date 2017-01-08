using UnityEngine;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour {

    public delegate void JoinRoom

    [SerializeField]
    private Text roomNameText;
        

    private MatchDesc match;

    public void Setup (MatchDesc_match)
    {
       
        match = _match;

        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom ()
    {

    }
	
}
