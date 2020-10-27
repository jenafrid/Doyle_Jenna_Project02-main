using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 15f;
    public ParticleSystem FlashFX;
    public AudioSource shoot;
    public GameObject BulletBurst;
    public Camera fpsCam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    //fire the weapon using a raycast
    void Shoot()
    {
        FlashFX.Play();
        shoot.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           // Debug.Log(hit.transform.name);
           Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(BulletBurst, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);        }
    }
}
