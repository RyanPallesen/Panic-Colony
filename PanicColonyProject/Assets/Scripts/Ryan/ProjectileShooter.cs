using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public int numShotsAllowedActive = 2;
    public GameObject projectilePrefab;
    public float velocityMultiplier;
    public int AimAssistRendererSteps;
    public bool shouldAimAssist;

    public List<Projectile> shotProjectiles = new List<Projectile>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotProjectiles.Count < numShotsAllowedActive)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);
            Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);// hitInfo.point.y = transform.position.y;
            Vector3 directionToShoot = alignedHitPoint - transform.position;
            directionToShoot.Normalize();

            GameObject firedProjectile = Instantiate(projectilePrefab, transform.position + directionToShoot, transform.rotation);

            Projectile firedComponent = firedProjectile.GetComponent<Projectile>();

            firedComponent.velocity = (directionToShoot * velocityMultiplier);
            shotProjectiles.Add(firedComponent);
            firedComponent.onDestroy.AddListener(() => shotProjectiles.Remove(firedComponent));
        }

        if (shouldAimAssist)
        {
            AimAssistRender();
        }

        BallReclamationTests();
    }

    public LineRenderer lineRenderer;
    public Vector3[] corners;
    public LayerMask BallReclamationLayerMask;

    public void AimAssistRender()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, 100f, BallReclamationLayerMask);
        Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);// hitInfo.point.y = transform.position.y;
        Vector3 directionToShoot = alignedHitPoint - transform.position;
        Vector3 origin = transform.position;

        corners = new Vector3[AimAssistRendererSteps + 1];

        for (int i = 0; i < AimAssistRendererSteps; i++)
        {
            corners[i] = origin;

            ray = new Ray(origin, directionToShoot);

            if (Physics.Raycast(ray, out RaycastHit hitInfoInner,100f, BallReclamationLayerMask))
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
    public void BallReclamationTests()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        BallAcceptor ballAcceptor = null;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, BallReclamationLayerMask))
        {

            Projectile relatedProjectile = hitInfo.collider.GetComponent<Projectile>();
            if (!relatedProjectile)
            {
                ballAcceptor = hitInfo.collider.GetComponent<BallAcceptor>();
                if (ballAcceptor)//hit ball acceptor
                {
                    if (ballAcceptor.acceotedObject)//acceptor is activated
                    {
                        relatedProjectile = ballAcceptor.acceotedObject.GetComponent<Projectile>();
                    }
                }
            }

            if (relatedProjectile)//we are hovering over a projectile
            {
                Vector3 direction = hitInfo.collider.transform.position - transform.position;
                float distance = Vector3.Distance(transform.position, hitInfo.collider.transform.position);

                Ray rayInner = new Ray(transform.position, direction);

                if (Physics.Raycast(rayInner, out RaycastHit HitInfoInner, distance, BallReclamationLayerMask))
                {
                    Debug.Log(HitInfoInner.collider.gameObject.name);

                    if (hitInfo.collider == HitInfoInner.collider || HitInfoInner.collider == ballAcceptor.acceotedObject.GetComponent<Collider>())//have line of sight
                    {
                        //highlight yellow
                        relatedProjectile.GetComponent<Renderer>().material.color = Color.yellow;

                        if (Input.GetMouseButtonDown(1))
                        {
                            relatedProjectile.DestroyThis();
                        }
                    }
                    else
                    {
                        //highlight red
                        relatedProjectile.GetComponent<Renderer>().material.color = Color.red;

                    }
                }

            }
        }
    }
}
