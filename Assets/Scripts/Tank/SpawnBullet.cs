using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bullets;
    [SerializeField, Range(10f, 10000f)] private float _force = 500f;
    [SerializeField] private float _fireSpellCooldown = 2f;

    private float _fireSpellStart = 0f;
    

    public void Shoot()
    {
        var bullet = (GameObject)Instantiate(_bullet);
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;
        bullet.transform.parent = _bullets.transform;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _force, ForceMode.Impulse);
    }
    private void Update()
    {
        Shooting();
    }
    private void Shooting()
    {
        if (Time.time > _fireSpellStart + _fireSpellCooldown && Input.GetButton("Fire1"))
        {
            _fireSpellStart = Time.time;
            Shoot();
        }
    }

}
