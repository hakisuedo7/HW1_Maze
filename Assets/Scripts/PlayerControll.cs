using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    const float MOVE_LEN = 4f;
    const float gravity = 9.8f;
    const float MOVE_FAST = 1.5f;
    float move_len;
    float move_fast;
    bool isPlay;
    bool Cam;

    GameObject FPObj, TPObj;
    Camera FP, TP;
    CharacterController controller;
    GameControll gameControll;

    Animator animator;
    AudioSource footStep;

    enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }
    RotationAxes m_axes = RotationAxes.MouseXandY;

    // 水平方向
    public float m_minimumX;
    public float m_maximumX;
    // 垂直方向
    public float m_minimumY;
    public float m_maximumY;

    float m_rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        FPObj = this.transform.GetChild(2).gameObject;
        TPObj = this.transform.GetChild(3).gameObject;
        FP = FPObj.GetComponent<Camera>();
        TP = TPObj.GetComponent<Camera>();
        Cam = false;

        controller = GetComponentInChildren<CharacterController>();
        gameControll = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControll>();
        animator = GetComponentInChildren<Animator>();
        footStep = GetComponentInChildren<AudioSource>();
        isPlay = false;
        CloseAllAudioListen();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameControll.ISPAUSE && !gameControll.ISGAMEOVER)
        {
            // 移動
            move();
            // 轉動視角
            rotate();
        }

        // 切換視角
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Cam = !Cam;
        }
        if (!Cam)
        {
            FPObj.SetActive(true);
            TPObj.SetActive(false);
            CloseAllAudioListen();
            FPObj.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            FPObj.SetActive(false);
            TPObj.SetActive(true);
            CloseAllAudioListen();
            TPObj.GetComponent<AudioListener>().enabled = true;
        }
    }
    void CloseAllAudioListen()
    {
        FPObj.GetComponent<AudioListener>().enabled = false;
        TPObj.GetComponent<AudioListener>().enabled = false;
    }

    void move()
    {
        bool move = false, run = false;
        bool move_forward_back = false, move_left_right = false;
        move_len = MOVE_LEN;
        move_fast = 1;
        Vector3 newPos = Vector3.zero;

        // 加速
        if (Input.GetKey(KeyCode.Space))
        {
            run = true;
            move_fast = MOVE_FAST;
        }

        // 重力影響
        newPos.y -= gravity * Time.deltaTime;

        // 前後移動
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            move = true;
            move_forward_back = true;
        }

        // 左右移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            move = true;
            move_left_right = true;
        }

        // 斜向移動判斷
        if(move_forward_back && move_left_right)
        {
            move_len /= Mathf.Sqrt(MOVE_LEN);
        }

        // 移動
        if (Input.GetKey(KeyCode.W))
            newPos.z +=  move_len * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            newPos.z -= move_len * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            newPos.x -= move_len * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            newPos.x += move_len * Time.deltaTime;

        newPos *= move_fast;
        controller.Move(transform.TransformDirection(newPos));

        // 動畫控制
        animator.SetBool("idle", false);
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        
        if (move && run)
        {
            if (!isPlay)
            {
                footStep.Play();
                isPlay = true;
            }
            footStep.pitch = 1f;
            animator.SetBool("run", true);
        }
        else if (move)
        {
            if (!isPlay)
            {
                footStep.Play();
                isPlay = true;
            }
            footStep.pitch = 0.5f;
            animator.SetBool("walk", true);
        }
        else
        {
            isPlay = false;
            footStep.Stop();
            animator.SetBool("idle", true);
        }
    }

    void rotate()
    {
        Camera currentCam = Cam ? TP : FP;
        if (m_axes == RotationAxes.MouseXandY)
        {
            float m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * OptionData.M_SensitivityX;
            m_rotationY += Input.GetAxis("Mouse Y") * OptionData.M_SensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            transform.localEulerAngles = new Vector3(0, m_rotationX, 0);
            currentCam.transform.localEulerAngles = new Vector3(-m_rotationY, 0, 0);
        }
        else if (m_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * OptionData.M_SensitivityX, 0);
        }
        else
        {
            m_rotationY += Input.GetAxis("Mouse Y") * OptionData.M_SensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            currentCam.transform.localEulerAngles = new Vector3(-m_rotationY, 0, 0);
        }
    }
}
