using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Platformer;
using Unity.VisualScripting;
using UnityEngine;
 
public class Projectile : MonoBehaviour
{
    public InputReader inputReader;
    public Transform banana;
    public Transform Jamo;
    public Transform target;
    public Rigidbody projectile;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment = 10;
    public float flightTime = 1f;
    private bool isThrowing=false;    
    private bool thorw=false;
    private bool firstThrow=false;
    private Camera cam;
    
    private int thorwCount=0;
    private void OnEnable() {
        inputReader.Throw +=()=>Throw(); 
        inputReader.MouseLeftClick +=MouseLeftClickEvent;   
    }
    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment + 1;
    }
    private void OnDisable() {
        inputReader.Throw -=()=>Throw(); 
        inputReader.MouseLeftClick -=MouseLeftClickEvent;  
    }   
    private void MouseLeftClickEvent(){
        if(isThrowing){
            thorw=true;
        }
    }
    private void Throw(){
        isThrowing=true;
        if(isThrowing && thorwCount > 0){
            Jamo.gameObject.SetActive(false);
            banana.gameObject.SetActive(true); 
            Vector3 targetPos=target.position; 
            target.gameObject.SetActive(false);
            
            transform.DOMove(targetPos, 0.5f).SetEase(Ease.InOutExpo).OnComplete(()=>{
                Jamo.gameObject.SetActive(true);
                banana.gameObject.SetActive(false);  
                thorwCount=0;
                isThrowing=false;
            });
        }
    }
    void Update()
    {
        if (isThrowing && thorwCount == 0)  {
            LaunchProjectile();
        }
    }
 
    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 1000f, layer))
        {
            lineVisual.enabled=true;
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
 
            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, flightTime);

            Visualize(vo, cursor.transform.position); 
 
            transform.rotation = Quaternion.LookRotation(new Vector3(vo.x, 0f, vo.z));  
 
            if (thorw && !firstThrow)
            {
                Rigidbody obj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                obj.AddForce(vo, ForceMode.Impulse); 
                target=obj.transform;   
                cursor.SetActive(false);
                lineVisual.enabled=false;   
                thorw=false; 
                isThrowing=false;
                firstThrow=true;
                thorwCount++;   
            }
        }
    }
    void Visualize(Vector3 vo, Vector3 finalPos)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, (i / (float)lineSegment) * flightTime);
            lineVisual.SetPosition(i, pos);
        }
 
        lineVisual.SetPosition(lineSegment, finalPos);
    }
 
    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;
 
        float sY = distance.y;
        float sXz = distanceXz.magnitude;
 
        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);
 
        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;
 
        return result;
    }
 
    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;
 
        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;
 
        result.y = sY;
 
        return result;
    }
}