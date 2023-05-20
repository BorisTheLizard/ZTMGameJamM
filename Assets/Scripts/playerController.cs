using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 move;
    public Vector3 playerVelocity;
    private Camera mainCamera;
    private CharacterController cc;
    [SerializeField] Animator anim;


    public float CCSpeed;
    [SerializeField] Vector3 lastPosition;
    public bool isRunning = false;

    private bool groundedPlayer;
    private float gravityValue = -100f;

    public Vector3 playerDir;
    public float SpeedChangeRate = 10.0f;
    public bool grabbingSnow = false;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Aim();
        PlayerMove();
        //PlayerAnimController();

        CCSpeed = Mathf.Lerp(CCSpeed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
    }

    public void PlayerMove()
    {

        groundedPlayer = cc.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		playerVelocity.y += gravityValue * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
        cc.Move(move * Time.deltaTime * moveSpeed);
    }

    private void Aim()
	{
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, color: Color.red);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

/*    public void PlayerAnimController()
	{
		if (CCSpeed < 0.5f)
		{
            anim.SetBool("walk", false);
		}
		else
		{
            anim.SetBool("walk", !false);
        }
		if (grabbingSnow)
		{
            anim.SetBool("grabSnow",true);
		}
		else
		{
            anim.SetBool("grabSnow", !true);
        }
    }*/
}
