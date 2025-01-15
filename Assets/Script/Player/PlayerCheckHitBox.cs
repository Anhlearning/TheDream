using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckHitBox : MonoBehaviour
{
    [SerializeField] JoyStick joystick; 
    [SerializeField] private Transform checkHitBox;
    [SerializeField] private float distanceCheckHit;
    [SerializeField] private LayerMask enemyMask;
    public void CheckHitBox(Vector2 dir)
    {
        RaycastHit2D hitbox = Physics2D.Raycast(checkHitBox.position, dir, distanceCheckHit, enemyMask);
        if (hitbox)
        {
            Debug.Log(hitbox.transform.gameObject.name);
            PlayerMovement playerMovement = hitbox.transform.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.pushing();
                SpawnVFX.Instance.ActiveParticle(checkHitBox.position);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkHitBox.position,checkHitBox.position+(new Vector3(0,1,0)*distanceCheckHit));
    }

}
