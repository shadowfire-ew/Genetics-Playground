using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("")]
    [SerializeField] Rigidbody phys;
    [Header("Controll")]
    [SerializeField] float jumpForce;
    [SerializeField] float moveForce;
    [SerializeField] float lookAngle;
    [SerializeField] float maxLookAngle;
    [SerializeField] float turnAngle;
    [SerializeField] int actionInterval = 100;
    [SerializeField] bool playerControlled = false;
    [Header("Cam")]
    [SerializeField] Camera bodyCam;

    public Camera BodyCam { get { return bodyCam; } }
    static Camera currentActiveBodyCam = null;
    static CharacterController currentlyControlled = null;
    bool canJump = false;
    int counter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        if (playerControlled)
        {
            TakeControll();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerControlled)
        {
            if (Input.GetKeyDown(KeyCode.Space)) DoJump();
            if (Input.GetKeyDown(KeyCode.W)) LookUp();
            if (Input.GetKeyDown(KeyCode.S)) LookDown();
            if (Input.GetKeyDown(KeyCode.A)) TurnLeft();
            if (Input.GetKeyDown(KeyCode.D)) TurnRight();
        }
        else
        {
            counter++;
            if (counter >= actionInterval)
            {
                counter = 0;
                int action = Random.Range(0, 4);
                DoAction(action);
            }
        }
    }
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            canJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
            canJump = false;
    }


    [ContextMenu("Do/JumpForward")]
    public void DoJump()
    {
        if (canJump)
        {
            phys.AddForce(new Vector3(moveForce, jumpForce, 0));
            canJump= false;
        }
    }

    [ContextMenu("Do/TurnLeft")]
    public void TurnLeft()
    {
        transform.Rotate(0, -turnAngle, 0, Space.World);
    }

    [ContextMenu("Do/TurnRight")]
    public void TurnRight()
    {
        transform.Rotate(0,turnAngle,0, Space.World);
    }

    [ContextMenu("Do/LookUp")]
    public void LookUp()
    {
        if (transform.eulerAngles.z < maxLookAngle)
            transform.Rotate(0, 0, lookAngle);
    }

    [ContextMenu("Do/LookDown")]
    public void LookDown()
    {
        if (transform.eulerAngles.z > 0)
        {
            transform.Rotate(0, 0, -lookAngle);
            if (transform.eulerAngles.z > 180)
            {
                Vector3 newEul = transform.eulerAngles;
                newEul.z = 0;
                transform.eulerAngles = newEul;
            }
        }
    }

    public void DoAction(int action)
    {
        switch(action)
        {
            case 0:
                //DoNothing
                break;
            case 1:
                DoJump();
                break;
            case 2:
                TurnLeft();
                break;
            case 3:
                TurnRight();
                break;
        }
    }

    [ContextMenu("Cam/Enable")]
    public void EnableBodyCam()
    {
        if (currentActiveBodyCam != bodyCam)
        {
            if (currentActiveBodyCam != null)
                currentActiveBodyCam.gameObject.SetActive(false);
            bodyCam.gameObject.SetActive(true);
            currentActiveBodyCam = bodyCam;
        }
    }

    [ContextMenu("Cam/Disable")]
    public void DisableBodyCam()
    {
        if (currentActiveBodyCam == bodyCam)
        {
            bodyCam.gameObject.SetActive(false);
            currentActiveBodyCam = null;
        }
    }

    [ContextMenu("Controll/Take")]
    public void TakeControll()
    {
        if (currentlyControlled != this)
        {
            if (currentlyControlled != null)
                currentlyControlled.YeildControll();
            currentlyControlled = this;
            playerControlled = true;
            EnableBodyCam();
        }
    }

    [ContextMenu("Controll/Yield")]
    public void YeildControll()
    {
        playerControlled = false;
        currentlyControlled = null;
        DisableBodyCam();
    }
}
