using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public static int catchCount;

    public Animator m_Animator;
    public Rigidbody rb;
    public PhotonView pv;

    [Range(0, 10f)]
    public float f_MoveSpeed;

    [Range(0, 10f)]
    public float f_RunSpeed;

    [Range(0, 20f)]
    public float f_JumpPower;

    [Range(0, 100f)]
    public float f_RotateSpeed;

    public GameObject obj_Rotate_Horizontal;
    public GameObject obj_Rotate_Vertical;
    public GameObject obj_Body;
    public GameObject obj_Cam_First, obj_Cam_Quarter;

    float jumpForce;
    
    private bool _isJump;
    private bool _isGround;
    private bool _isSitting;
    private bool _isDalgona;

    public bool _startarea;

    private void Awake()
    {
        pv = this.GetComponent<PhotonView>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _startarea = false;
        if (pv.IsMine)
        {
            obj_Cam_First.SetActive(false);
            obj_Cam_Quarter.SetActive(true);
            this.gameObject.name += "(LocalPlayer)";
        }
        else
        {
            obj_Cam_First.SetActive(false);
            obj_Cam_Quarter.SetActive(false);
            this.gameObject.name += "(OtherPlayer)";
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (pv.IsMine && !DalgonaAtiveManager.isDalgona)
        {
            float pos_x = Input.GetAxis("Horizontal");
            float pos_z = Input.GetAxis("Vertical");


            //달리기 ON&OFF
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_Animator.SetBool("Run", true);
            }
            else
            {
                m_Animator.SetBool("Run", false);
            }

            //걷기 ON&OFF 및 캐릭터 이동
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //Debug.Log(new Vector2(pos_x, pos_z));
                if (pos_x > 0)
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 45f, 0f);
                        //transform.Rotate(new Vector3(0f, 45f, 0f));
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 135f, 0f);
                        //transform.Rotate(new Vector3(0f, 135f, 0f));
                    }
                    else
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        //transform.Rotate(new Vector3(0f, 90f, 0f));
                    }
                }
                else if (pos_x < 0)
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, -45f, 0f);
                        //transform.Rotate(new Vector3(0f, -45f, 0f));
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, -135f, 0f);
                        //transform.Rotate(new Vector3(0f, -135f, 0f));
                    }
                    else
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        //transform.Rotate(new Vector3(0f, 270f, 0f));
                    }
                }
                else
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        //transform.Rotate(new Vector3(0f, 0f, 0f));
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        //transform.Rotate(new Vector3(0f, 45f, 0f));
                    }
                }
                //시소타기
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //시소타는 액션
                }



                m_Animator.SetBool("Walk", true);
                if (m_Animator.GetBool("Run"))
                {
                    transform.Translate(new Vector3(pos_x, 0, pos_z) * Time.deltaTime * f_MoveSpeed * f_RunSpeed);
                }
                else
                {
                    //transform.position += new Vector3(pos_x, 0, pos_z) * Time.deltaTime * f_MoveSpeed;
                    transform.Translate(new Vector3(pos_x, 0, pos_z) * Time.deltaTime * f_MoveSpeed);
                }
            }
            else
            {
                m_Animator.SetBool("Walk", false);
            }
            //점프하기
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isGround && !_isJump) // 캐릭터가 땅 위에 있는지 확인합니다.
                {
                    m_Animator.SetTrigger("Jump");
                    _isJump = true;
                    _isGround = false;
                    jumpForce = f_JumpPower * Time.deltaTime;
                    rb.AddForce(Vector3.up * f_JumpPower, ForceMode.Impulse);
                    Debug.Log("점프 실행됨");
                }
            }
            if (Input.GetMouseButton(1))
            {
                float rot_x = Input.GetAxis("Mouse Y");
                float rot_y = Input.GetAxis("Mouse X");
                //obj_Rotate_Horizontal.transform.eulerAngles += new Vector3(0, rot_y, 0) * f_RotateSpeed;
                transform.eulerAngles += new Vector3(0, rot_y, 0) * f_RotateSpeed;
            }
            rb.angularVelocity = Vector3.zero;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJump = false;
            _isGround = true;
            
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerManager>().Pstatus == PlayerManager.status._hideseek)
            {
                if (this.gameObject.GetComponent<PlayerManager>().Pjob == PlayerManager.job.polic && other.gameObject.GetComponent<PlayerManager>().Pjob == PlayerManager.job.theif)
                {
                    other.gameObject.GetComponent<PlayerManager>().Pjob = PlayerManager.job.none;
                    catchCount++;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seesaw"))
        {
            Debug.Log("시소 접촉");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dalgona") && !DalgonaAtiveManager.isDalgona && this.gameObject.GetComponent<PlayerManager>().Pjob == PlayerManager.job.none)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<DalgonaAtiveManager>().LoadSceneAdditive();
            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
            _isGround = false;

        }
    }

    IEnumerator Roca(Vector3 before, Vector3 after, float settime)
    {
        float timer = 0;
        while (timer < settime)
        {
            timer += Time.deltaTime;
            Vector3 vec = Vector3.Lerp(before, after, timer / settime);
            obj_Body.transform.localEulerAngles = vec;
            yield return null;
        }

    }
}