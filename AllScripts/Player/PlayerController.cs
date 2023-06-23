using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpPower;
    private CharacterController characterController;

    private Vector3 moveInput;

    [SerializeField]
    private Transform camTransformer;
    [SerializeField]
    private float mouseSensitivity;

    private bool invertX;
    private bool invertY;

    private bool canJump;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private GameObject bulletPistol,bulletSmg,bulletRifle;
    [SerializeField]
    private Transform firePos;

    [SerializeField]
    private Transform pistolFpos, cal12Fpos, rifleFpos;

    [SerializeField]
    private GameObject flashRide;

    [SerializeField]
    private Animator pst;

    [SerializeField]
    private Animator c12;

    [SerializeField]
    private Animator rfl;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FireAction();
        ActionZoom();
    }

    public void Movement()
    {
        /* moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
         moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;*/
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");
        
        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;
        }
        

        moveInput.y = yStore;
        moveInput.y += Physics.gravity.y * gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravity;
        }
        //Baby Jump

        canJump = Physics.OverlapSphere(groundCheck.position, .25f, layer).Length >0;
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
        }

        characterController.Move(moveInput * Time.deltaTime);
        


        //Camera
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if(invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if(invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        camTransformer.rotation = Quaternion.Euler(camTransformer.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        

    }

    public void FireAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camTransformer.position,camTransformer.forward,out hit,50f))
            {
                if(Vector3.Distance(camTransformer.position,hit.point) > 2f)
                {
                    firePos.LookAt(hit.point);
                }
                
            }
            else
            {
                firePos.LookAt(camTransformer.position + (camTransformer.forward * 35f));
            }

            Shoot();
            

        }
    }

    public void Shoot()
    {
        if(WeaponAndAmmoController.instance.canShoot == true)
        {
            if (WeaponAndAmmoController.instance.weaponIndex == 0)
            {
                firePos.position = pistolFpos.position;
                pst.SetTrigger("Recoil");
                Instantiate(bulletPistol, firePos.position, firePos.rotation);
                WeaponAndAmmoController.instance.PistolShoot();
                WeaponSound.instance.WeaponShoot(0);
                
                
            }

            if (WeaponAndAmmoController.instance.weaponIndex == 1)
            {
                firePos.position = cal12Fpos.position;
                
                WeaponAndAmmoController.instance.Cal12Shoot();
                StartCoroutine(Cal12());
            }

            if (WeaponAndAmmoController.instance.weaponIndex == 2)
            {
                firePos.position = rifleFpos.position;
                StartCoroutine(Rifle());
                WeaponAndAmmoController.instance.RifleShoot();
            }

           GameObject flash = Instantiate(flashRide, firePos.position, firePos.rotation);
            flash.transform.parent = firePos;

        }
        
    }


    IEnumerator Rifle()
    {
        rfl.SetTrigger("Recoil");
        WeaponSound.instance.WeaponShoot(1);
        Instantiate(bulletRifle, firePos.position, firePos.rotation);
        yield return new WaitForSeconds(.1f);
        Instantiate(bulletRifle, firePos.position, firePos.rotation);
        yield return new WaitForSeconds(.1f);
        Instantiate(bulletRifle, firePos.position, firePos.rotation);
        
    }

    IEnumerator Cal12()
    {
        WeaponSound.instance.WeaponShoot(2);
        Instantiate(bulletPistol, firePos.position, firePos.rotation);
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(5f,0f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(2f,0f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(-2f,0f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(-5f,0f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(0f,5f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(0f,-5f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(0f,-2f,0f));
        Instantiate(bulletRifle, firePos.position, firePos.rotation * Quaternion.Euler(0f,2f,0f));
        c12.SetTrigger("Recoil");
        yield return new WaitForSeconds(.4f);
        WeaponSound.instance.WeaponShoot(3);



    }


    public void ActionZoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(40f);
        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut();
        }
    }
}
