using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public Rigidbody bulletPrefabs;
    public GameObject cusor;
    public Transform shootPoint;    
    public LayerMask layerMask;


    void Update()
    {
        LauchProjectile();
    }
    void LauchProjectile(){
        Ray camRay= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(camRay,out hit,100f,layerMask)){
            cusor.SetActive(true);
            cusor.transform.position = hit.point + Vector3.up * 0.1f;   
            Vector3 target = hit.point;
            float time = 2.0f;
            Vector3 velocity = CaculatorVelocity(target,shootPoint.position,time);
            transform.rotation = Quaternion.LookRotation(new Vector3(hit.point .x,0,hit.point.z)); 
            if(Input.GetMouseButtonDown(0)){
                Rigidbody bullet = Instantiate(bulletPrefabs,shootPoint.position,Quaternion.identity);
                bullet.velocity = velocity;
            }   
        }
        else {
            cusor.SetActive(false);
        }
    }   
    public Vector3 CaculatorVelocity(Vector3 target,Vector3 position,float time){
        Vector3 distance = target - position;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        Vector3 result = distance.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
}
