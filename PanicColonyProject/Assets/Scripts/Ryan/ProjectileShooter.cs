using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float velocityMultiplier;
    public int AimAssistRendererSteps;
    public bool shouldAimAssist;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);
            Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);// hitInfo.point.y = transform.position.y;
            Vector3 directionToShoot = alignedHitPoint - transform.position;
            directionToShoot.Normalize();

            GameObject firedProjectile = Instantiate(projectilePrefab, transform.position + directionToShoot, transform.rotation);

            firedProjectile.GetComponent<Projectile>().velocity = (directionToShoot * velocityMultiplier);
        }

        if (shouldAimAssist)
        {
            AimAssistRender();
        }
    }

    public LineRenderer lineRenderer;
    public Vector3[] corners;
    public void AimAssistRender()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);// hitInfo.point.y = transform.position.y;
        Vector3 directionToShoot = alignedHitPoint - transform.position;
        Vector3 origin = transform.position;

        corners = new Vector3[AimAssistRendererSteps + 1];

        for (int i = 0; i < AimAssistRendererSteps; i++)
        {
            corners[i] = origin;

            ray = new Ray(origin, directionToShoot);

            if (Physics.Raycast(ray, out RaycastHit hitInfoInner))
            {
                origin = hitInfoInner.point;
                directionToShoot = Vector3.Reflect(directionToShoot, hitInfoInner.normal);
            }
            else
            {
                origin = origin + directionToShoot;
            }
        }

        lineRenderer.positionCount = corners.Length - 1;
        lineRenderer.SetPositions(corners);
    }
}
