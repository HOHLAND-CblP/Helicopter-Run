using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelicopterControler : MonoBehaviour
{
    public Rigidbody2D Rig;

    GameObject Cam;

    public bool isCanPainted;
    public SpriteRenderer[] HelSprite;
    public string FileName;

    public int speedVertical;//in fly
    int TempSpeedHorizontal;
    public int speedHorizontal;
    public float HorizontalAxis;
    public float VerticalAxis;
    public int HorizontalSpeedLimit;
    public int VerticalSpeedLimit;
    public float speedAngle;
    float TempSpeedAngle;
    Quaternion rot;

    public bool Grounded1;//on earth
    public bool Grounded2;
    public Transform GroundCheck1;
    public Transform GroundCheck2;
    public LayerMask WhatIsGrounded;
    public float GroundCircle;

    public Text VerticaSpeedText;   //Text
    public Text HorizontalSpeedText;

    public GameObject TNT;
    public Vector2 TNTPosition;

    public bool CompPlay;

    private void Awake()
    {
        Rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Cam = Camera.main.gameObject;
        FileName = Application.persistentDataPath + FileName;
        if (isCanPainted)
            Cam.GetComponent<SaveControler>().LoadHelCol(FileName, gameObject);

        TNT = null;
        
        Grounded1 = Physics2D.OverlapCircle(GroundCheck1.position, GroundCircle, WhatIsGrounded);
        Grounded2 = Physics2D.OverlapCircle(GroundCheck2.position, GroundCircle, WhatIsGrounded);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (TNT != null)
            {
                TNT.GetComponent<DynamiteCollisionCheck>().OnBoard = false;
                TNT.transform.SetParent(null);
                TNT.AddComponent<Rigidbody2D>();
                TNT.GetComponent<BoxCollider2D>().enabled = true;
                TNT = null;
                StartCoroutine(TimeToOnTrigger());
            }

        if (Mathf.Abs(Rig.velocity.x) > 3)
            Cam.GetComponent<Camera>().orthographicSize = 5 + 3 * (Mathf.Abs(Rig.velocity.x) - 3)/2;
        else 
            Cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Cam.GetComponent<Camera>().orthographicSize, 5, 0.1f);


        if (Input.GetKeyDown(KeyCode.Escape))
            CompPlay = !CompPlay;
    }

    void FixedUpdate()
    {
        Grounded1 = Physics2D.OverlapCircle(GroundCheck1.position, GroundCircle, WhatIsGrounded); //Check touch earth
        Grounded2 = Physics2D.OverlapCircle(GroundCheck2.position, GroundCircle, WhatIsGrounded);

#if UNITY_STANDALONE_WIN
        HorizontalAxis = Input.GetAxis("Horizontal");  //Axis
        VerticalAxis = Input.GetAxis("Vertical");
        TempSpeedAngle = speedAngle;
#endif

#if UNITY_ANDROID
        TempSpeedAngle = speedAngle * 2 / 3;
#endif

        if (Grounded1 || Grounded2)     //if on eatrh
        {
            if (Mathf.Abs(Rig.velocity.y)>=3)
            {
                Damage();
            }
            Rig.velocity = new Vector2(0, Rig.velocity.y);
            TempSpeedHorizontal = 0 * speedHorizontal;
        }
        else
        {
            TempSpeedHorizontal = speedHorizontal;
        }
        
        rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -15 * HorizontalAxis);   //Rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, TempSpeedAngle * Time.deltaTime);
        
        Rig.AddForce(Vector2.up * VerticalAxis * speedVertical);   //Vertical speeed              
        Rig.AddForce(Vector2.right * HorizontalAxis * TempSpeedHorizontal);//Horizontal speed

        Rig.velocity = new Vector2(Mathf.Clamp(Rig.velocity.x, -HorizontalSpeedLimit, HorizontalSpeedLimit), Mathf.Clamp(Rig.velocity.y, -VerticalSpeedLimit, VerticalSpeedLimit));   //speed limit

        VerticaSpeedText.text = Mathf.Abs(Rig.velocity.y).ToString("0.00");    //Speed text
        HorizontalSpeedText.text = (Mathf.Abs(Rig.velocity.x)*5).ToString("0.0");
        if (Mathf.Abs(Rig.velocity.y) >= 3)
            VerticaSpeedText.color = Color.red;
        else
            VerticaSpeedText.color = Color.black;

        if (Mathf.Abs(Rig.velocity.x) > 0)  //Air brake
            Rig.AddForce(-Vector2.right * Rig.velocity.x * 0.2f);
    }

    public void Damage()
    {
        Cam.GetComponent<GameControler>().Damage();
    }


    IEnumerator TimeToOnTrigger()
    {
        yield return new WaitForSeconds(1);
        transform.GetComponent<BoxCollider2D>().enabled = true;
    }




    public void CargoDischarge()
    {
        if (TNT != null)
        {
            TNT.GetComponent<DynamiteCollisionCheck>().OnBoard = false;
            TNT.transform.SetParent(null);
            TNT.AddComponent<Rigidbody2D>();
            TNT.GetComponent<BoxCollider2D>().enabled = true;
            TNT = null;
            StartCoroutine(TimeToOnTrigger());
        }
    }

    public void LoadColor(List<Save.HelColors> save)
    {
        for (int i = 0; i < HelSprite.Length; i++)
            HelSprite[i].color = new Color(save[i].col.x, save[i].col.y, save[i].col.z, save[i].col.w);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TNT")
        {
            TNT = collision.gameObject;
            TNT.transform.SetParent(transform);
            Destroy(TNT.GetComponent<Rigidbody2D>());
            TNT.transform.localPosition = new Vector2(TNTPosition.x, TNTPosition.y - TNT.GetComponent<DynamiteCollisionCheck>().Height);
            TNT.transform.rotation = transform.rotation;
            GetComponent<BoxCollider2D>().enabled = false;
            if(PlayerPrefs.GetInt("Difficult")==0)
            {
                TNT.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
} 