using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody _rigidbody;
    [SerializeField, Range(1f, 20f)] private float _explosionPower = 10f;
    [SerializeField, Range(1f, 20f)] private float _explosionRadius = 10F;

    [SerializeField] private GameObject _explosion;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        Quaternion q = transform.rotation;
        var explosion = (GameObject)Instantiate(_explosion,explosionPos, q);
        Destroy(explosion, 2f);

        KnockBack();
        Destroy(gameObject, .1f);
    }

    private void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach(Collider collider in colliders)
        {   
            if(collider.transform.tag != "Ground")
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                rb.AddExplosionForce(_explosionPower, transform.position, _explosionRadius);
            }
            
        }
    }
}
