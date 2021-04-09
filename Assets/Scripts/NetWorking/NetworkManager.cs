using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Assets.Scripts.NetWorking
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPref;

        [SerializeField]
        private CameraFollow camera;        

        private int myConnectionID;

        public static NetworkManager Instance;

        public int MyConnectionID { get => myConnectionID; set => myConnectionID = value; }

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);

            NetworkConfig.InitNetwork();

            NetworkConfig.ConnectToServer();
        }

        private void OnApplicationQuit()
        {
            NetworkConfig.DisconnectFromServer();
        }

        public void InstantiateNetworkPlayer(int connectionID, bool isMyPlayer)
        {
            GameObject go = Instantiate(playerPref);
            go.name = "Player " + connectionID;
            if(isMyPlayer)
            {   
                var input = go.AddComponent<InputManager>();
                camera.SetTarget(go.transform);

            }else
            {
                for(int i = 0; i < go.transform.childCount; i++)
                {
                    if(go.transform.GetChild(i).GetComponent<Light2D>() != null)
                    {
                        go.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }

            GameManager.Instance.playerList.Add(connectionID, go);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
