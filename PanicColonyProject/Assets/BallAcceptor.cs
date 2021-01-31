using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallAcceptor : MonoBehaviour
{
    public GameObject acceotedObject;

    public UnityEvent onAcceptBall;
    public UnityEvent onLoseBall;

    [SerializeField] private Material unactiveMat = null, activeMat = null;
    [SerializeField] private MeshRenderer meshRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.material = unactiveMat;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();

        if(projectile && !acceotedObject)
        {
            projectile.velocity = Vector3.zero;
            projectile.GetComponent<Collider>().enabled = false;
            onAcceptBall.Invoke();
            acceotedObject = projectile.gameObject;
            meshRenderer.material = activeMat;
        }
    }

    public void OnLoseBall()
    {
        onLoseBall.Invoke();

        meshRenderer.material = unactiveMat;

        acceotedObject = null;
    }
    
}
