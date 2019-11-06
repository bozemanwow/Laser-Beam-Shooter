using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    [SerializeField]
    private Camera aimCamera;
    [SerializeField]
    [Range(0,20)]
    private float speed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Quaternion targetRotation;
    [SerializeField]
    Vector3 relativePosition;
    [SerializeField]
    private LayerMask hitMask;
    [SerializeField]
    [Range(0,1000)]
    private float range;
    [SerializeField]
    private float timer;
    [SerializeField]
    private LineRenderer laserBeam;
    // Start is called before the first frame update
    void Start()
    {
        if (aimCamera == null)
            aimCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FireGun();
        SetTargetPosition();
        RotateArm();
    }
    public void SetTargetPosition()
    {
        Vector3 aimPoint = RayAimFromCamera();
        target.position = new Vector3(aimPoint.x, transform.position.y, aimPoint.z);
    }
    public Vector3 RayAimFromCamera()
    {
        Ray ray = aimCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, hitMask)) 
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public void RotateArm()
    {
        relativePosition =  target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation, Time.deltaTime * speed);
    }

    void FireGun()
    {

       if(Input.GetButtonDown("Fire1"))
        {
            Invoke("ShootLaser", timer);
        }
       if(Input.GetButtonUp("Fire1"))
        {
            Invoke("EndLaser", timer);
        }

    }

    void ShootLaser()
    {
        laserBeam.enabled = (true);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        laserBeam.SetPosition(0, transform.position);
        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            if (hit.collider.gameObject.tag == "Enemies")
            {
                hit.collider.SendMessage("Click", SendMessageOptions.DontRequireReceiver);
                laserBeam.SetPosition(1, hit.point);
            }
            else
                laserBeam.SetPosition(1, (transform.forward.normalized * 300) + transform.position);


        }
        else
        {
            laserBeam.SetPosition(1, (transform.forward.normalized * 300) + transform.position);
        }
    }
    void EndLaser()
    {
        laserBeam.enabled = false;
      
    }

}
