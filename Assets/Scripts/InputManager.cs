using UnityEngine;
using System.Collections;
using Assets.Scripts.NetWorking;

public class InputManager : MonoBehaviour
{
    //[SerializeField]
    //private keys pressKey;

    //public keys PressKey { get => pressKey; set => pressKey = value; }

    //[SerializeField]
    //private keys pressSecondKey;

    //public keys PressSecondKey { get => pressSecondKey; set => pressSecondKey = value; }

    //public enum keys
    //{
    //    None,
    //    W,
    //    A,
    //    S,
    //    D
    //}

    private Rigidbody2D myRB;

    private float speed = 200f;

    private float currentVelocity;
    
    // Use this for initialization
    void Start()
    {
        myRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SendLocation();       
    }

    public void FixedUpdate()
    { 

        if (myRB == null) myRB = gameObject.GetComponent<Rigidbody2D>();

        Vector2 input = Vector2.zero;

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude != 0)
        {
            

            Vector2 inputDir = input.normalized;

            float rotation = Mathf.Atan2(inputDir.x, -inputDir.y) * Mathf.Rad2Deg - 90;

            myRB.rotation = Mathf.SmoothDampAngle(myRB.rotation, rotation, ref currentVelocity, 0.5f);

            myRB.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        }
        else
            myRB.velocity = Vector2.zero;

    }


    private void SendLocation()
    {
        Vector2 locationNetwork = transform.position;
        Vector3 rotationNetwork = transform.eulerAngles;

        NetworkSend.SendNetworkPosition(new System.Numerics.Vector2(locationNetwork.x, locationNetwork.y), new System.Numerics.Vector3(rotationNetwork.x, rotationNetwork.y, rotationNetwork.z));
    }

    //private void CheckInput()
    //{
    //    GetKeyCode();

    //    if (pressKey == pressSecondKey) pressSecondKey = keys.None;



    //    NetworkSend.SendKeyInput(pressKey, pressSecondKey);

    //}

    //private void GetKeyCode()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        pressKey = keys.W;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.W))
    //    {
    //        pressKey = keys.None;
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        if (pressKey != keys.None)
    //        {
    //            pressSecondKey = keys.A;
    //        }
    //        else
    //            pressKey = keys.A;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        if (pressSecondKey == keys.A) pressSecondKey = keys.None;
    //        else
    //            pressKey = keys.None;
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        if (pressKey != keys.None)
    //        {
    //            pressSecondKey = keys.S;
    //        }
    //        else
    //            pressKey = keys.S;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.S))
    //    {
    //        if (pressSecondKey == keys.S) pressSecondKey = keys.None;
    //        else
    //            pressKey = keys.None;
    //    }

    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        if (pressSecondKey != keys.None && pressSecondKey != keys.D)
    //        {
    //            pressKey = pressSecondKey;
    //            pressSecondKey = keys.D;
    //        }
    //        else
    //            if (pressKey == keys.None) pressKey = keys.D;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.D))
    //    {
    //        if (pressSecondKey == keys.D) pressSecondKey = keys.None;
    //        if (pressKey == keys.D) pressKey = keys.None;
    //    }
    //}
}
