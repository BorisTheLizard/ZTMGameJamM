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

    //dash
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] TrailRenderer trail;
    float dashCoolDown;
    [SerializeField] float maxDashCoolDown = 1f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] dashClips;


    public float CCSpeed;
    [SerializeField] Vector3 lastPosition;

    private bool groundedPlayer;
    private float gravityValue = -100f;

    [SerializeField] GameObject effector;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Aim();
        PlayerMove();
        PlayerAnimController();


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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Time.time > dashCoolDown)
			{
                dashCoolDown = Time.time + maxDashCoolDown;
                StartCoroutine(Dash());
                audioSource.clip = dashClips[Random.Range(0, dashClips.Length)];
                audioSource.Play();
            }
		}
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
            Vector3 lookOnPoint = (new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            transform.LookAt(lookOnPoint);
            effector.transform.position = lookOnPoint;
        }
    }
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            trail.enabled = true;
            cc.Move(move.normalized * dashSpeed * Time.deltaTime);
            yield return null;
        }
        trail.enabled = false;
    }

	public void PlayerAnimController()
	{
		if (CCSpeed < 0.5f)
		{
			anim.SetBool("walk", false);
		}
		else
		{
			anim.SetBool("walk", !false);
		}
	}
}
