using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class pointTransform : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float AimDistance = 8;
    [SerializeField] Rig rig;
    [SerializeField] string State = "IDLE";
    [SerializeField] Animator anim;
    [SerializeField] GameObject effector;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);

        if (State == "IDLE")
        {
            if (rig.weight != 0)
            {
                rig.weight -= 0.3f * Time.deltaTime;
            }         
            
            if (distance < AimDistance)
            {
                State = "AIM";
            }
        }

        else if (State == "AIM")
        {
			if (rig.weight != 1)
			{
                rig.weight += 0.3f * Time.deltaTime;
			}

			if (rig.weight > 0.1f)
			{
                effector.transform.position = Vector3.Lerp(transform.position, Player.position, 3f * Time.deltaTime);
            }

            if (distance > AimDistance)
            {
                State = "IDLE";
            }
        }

    }
}
