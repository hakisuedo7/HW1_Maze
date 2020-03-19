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

    Camera FP;
    CharacterController controller;
    GameControll gameControll;

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
        FP = GetComponentInChildren<Camera>();
        controller = GetComponentInChildren<CharacterController>();
        gameControll = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControll>();
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
    }

    void move()
    {
        bool move_forward_back = false, move_left_right = false;
        move_len = MOVE_LEN;
        move_fast = 1;
        Vector3 newPos = Vector3.zero;

        // 加速
        if (Input.GetKey(KeyCode.Space))
        {
            move_fast = MOVE_FAST;
        }

        // 重力影響
        newPos.y -= gravity * Time.deltaTime;

        // 前後移動
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            move_forward_back = true;
        }

        // 左右移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
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
    }

    void rotate()
    {
        if (m_axes == RotationAxes.MouseXandY)
        {
            float m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * OptionData.M_SensitivityX;
            m_rotationY += Input.GetAxis("Mouse Y") * OptionData.M_SensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            transform.localEulerAngles = new Vector3(0, m_rotationX, 0);
            FP.transform.localEulerAngles = new Vector3(-m_rotationY, 0, 0);
        }
        else if (m_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * OptionData.M_SensitivityX, 0);
        }
        else
        {
            m_rotationY += Input.GetAxis("Mouse Y") * OptionData.M_SensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            FP.transform.localEulerAngles = new Vector3(-m_rotationY, 0, 0);
        }
    }
}
