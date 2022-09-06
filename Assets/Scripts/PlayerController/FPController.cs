using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPController : MonoBehaviour
{
    public Transform fpCamera;

    public RobotControll drone;
    //public float movespeed = 5f;
    CharacterController controller;

    [Header("Gravity and Jump Settings")]
    public float velocityY;
    public bool isGround;
    public Transform groundDetect;
    public float groundDistance;
    public float gravityScale;
    public float jumpHeight;
    public LayerMask groundLayer;

    public Vector3 HorizontalVelocity;

    GunControll gunController;

    bool recorderMode = false;

    public static FPController instance;

    public void SetMove(Vector3 a)
    {
       controller.Move(a);
    }

    

    void Awake()
    {
        instance = this;

        
    }


    void jumpAndGravity()
    {
        isGround = Physics.CheckSphere(groundDetect.position, groundDistance, groundLayer);
        if(!isGround)
        {
            if(groundtimer >= 0f)
            {
                groundtimer += Time.deltaTime;
                if(groundtimer > grounddelay)
                    groundtimer = -1f;
            }
            
        }
        else
            groundtimer = 0f;
        
        if(isGround && velocityY <0)
        {
            velocityY = -2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (isGround || groundtimer>0))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
        }
        if(!isGround)
            velocityY += gravityScale * Time.deltaTime;
    }

    public float maxspeed = 5f;

    public float grounddelay = 0.1f;
    float groundtimer = 0f;

    void moveControll()
    {
        float forward = Input.GetAxis("Vertical");
        float Rightward = Input.GetAxis("Horizontal");
        Vector3 dir = fpCamera.transform.forward;
        //Debug.Log(dir);
        dir.y = 0;
        dir = dir * forward + Vector3.Cross(Vector3.up, dir) * Rightward;
        //Vector3 dir = transform.right * Rightward + transform.forward * forward;
        dir.Normalize();
        if(dir.magnitude > 0f)
            drone.setDirection(dir);
        dir=dir * maxspeed;
        /*if(!isGround)
        {
            if(Vector3.Angle(HorizontalVelocity, dir)>100)
                dir = Vector3.zero;
        }
        else
            HorizontalVelocity = dir;*/
        dir += velocityY * Vector3.up;
        controller.Move(dir * Time.deltaTime);

    }

    public LayerMask cameraCollideLayer;

    void updateCameraDistance()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0f, fpCamera.localPosition.y, 0f), - fpCamera.transform.forward, out hit, ResourceManager.maxCamDistance, cameraCollideLayer);
        float distance = hit.distance>0 ? -hit.distance : -ResourceManager.maxCamDistance;
        fpCamera.localPosition = new Vector3(0, fpCamera.localPosition.y, distance);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        /* GameObject spawn = GameObject.Find("SPAWN");
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        GetComponent<Recorder>().setRecord(); */
    }

    

    void resetPlayer()
    {

        StartCoroutine(resetPlayerIE());
    }

    public void recorderModeActivate(bool activated)
    {
        if(recorderMode == activated)
            return;
        recorderMode = activated;
        if(activated)
        {
            transform.position = RespawnPoint.RESPAWN.position;
            transform.rotation = RespawnPoint.RESPAWN.rotation;
            GetComponent<Recorder>().setRecord();
        }
        else
        {
            GetComponent<Recorder>().setRecord();
            DollControl[] dolls = FindObjectsOfType<DollControl>();
            foreach (var doll in dolls)
            {
                Destroy(doll.gameObject);
            }
        }
    }

    IEnumerator resetPlayerIE()
    {
        drone.gameObject.SetActive(false);
        ResourceManager.RManager.setPublicSpark(transform.position, Vector3.up, 0,1);
        GetComponent<Recorder>().setRecord();
        yield return new WaitForSeconds(.5f);
        GameObject spawn = GameObject.Find("SPAWN");
        transform.position = RespawnPoint.RESPAWN.position;
        transform.rotation = RespawnPoint.RESPAWN.rotation;
        drone.gameObject.SetActive(true);
        DollControl[] dolls = FindObjectsOfType<DollControl>();
        foreach(var doll in dolls)
        {
            doll.reset(true);
        }
        GetComponent<Recorder>().setRecord();
    }

    void Update()
    {
        moveControll();
        //tapCheck();
        jumpAndGravity();
        updateCameraDistance();
        
        

        if(recorderMode && Input.GetKeyDown(KeyCode.R))
        {
            /* SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); */
            resetPlayer();
        }
    }

    //public GameObject currentObject;
    //public LayerMask tapLayer;

    /* void tapCheck()
    {
        bool isTap = Physics.CheckSphere(groundDetect.position, groundDistance, tapLayer);
        if(isTap)
        {
            if(currentObject == null)
                return;
            currentObject.GetComponent<tap>().evokeTap();
            currentObject.GetComponent<tap>().controlled = true;
        }
        else
        {
            if(currentObject != null)
            {
                currentObject.GetComponent<tap>().disableTap();
                currentObject.GetComponent<tap>().controlled = false;
                currentObject = null;
            }
            
        }
    } */

    //DyeRecorder currentRecord = null;
    /* DyableObject currentCube = null;
    bool recording = false;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "tap" && currentObject != hit.gameObject)
        {
            //Debug.Log("get tap " + hit.gameObject.name);
            currentObject = hit.gameObject;
        }

        if(hit.gameObject.layer == LayerMask.NameToLayer("dyable"))
        {
            DyableObject tmp = hit.gameObject.GetComponent<DyableObject>();
            if(!recording)
            {
                currentCube = tmp;
                recording = true;
            }
            if(currentCube != tmp)
            {
                currentCube.setConnection(tmp);
                currentCube = tmp;
            }
        }
        else
        {
            if(recording)
            {
                currentCube = null;
                recording = false;
            }
        }
        if(hit.gameObject.layer == LayerMask.NameToLayer("dyable") && Vector3.Angle(Vector3.up, hit.normal) < 45 )
        {
            DyableObject tmp = hit.gameObject.GetComponent<DyableObject>();
            if(currentCube != tmp)
            {
                currentCube = tmp;
                Debug.Log(tmp.color_code);
                if(tmp.color_code == 1){
                    //currentCube.getColor(gunController.gunColor);
                    maxspeed = 8f;
                }
                else
                    maxspeed = 4f;
            }
        }


    } */

}
