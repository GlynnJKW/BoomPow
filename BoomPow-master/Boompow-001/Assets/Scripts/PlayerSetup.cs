using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }
        //Camera sceneCamera;

        void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        } //else {
          // sceneCamera = Camera.main;
          //if (sceneCamera != null) { 
          // sceneCamera.gameObject.SetActive(false);
          //}
          //}
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }
    //void OnDisable () {
    // if (sceneCamera != null) {
    // sceneCamera.gameObject.SetActive(true);
//}
//}
 



}


