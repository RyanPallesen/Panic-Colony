using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float velocityMultiplier;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject firedProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);
            Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);// hitInfo.point.y = transform.position.y;
            Vector3 directionToShoot = alignedHitPoint - transform.position;
            directionToShoot.Normalize();
            firedProjectile.GetComponent<Projectile>().velocity = (directionToShoot * velocityMultiplier);

        }
    }
}
