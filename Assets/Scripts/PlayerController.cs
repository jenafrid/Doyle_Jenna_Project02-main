using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
//using System.Security.Policy;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using System.Security.AccessControl;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    public float dashSpeed = 15f;
    public float walkSpeed = 7f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;
    public GameObject enemyBullet;
    public GameObject FirstPersonPlayer;
    public AudioSource dmgSound;
   // public GameObject HealthPickup;
   // public AudioSource HealthSound;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] ParticleSystem explosion = null;

    Vector3 velocity;
    bool isGrounded;

    void start()
    {
        //sets health at beginning of game
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

       // if (Input.GetKeyDown(KeyCode.J))
       //{
       //     TakeDamage(20f);
       // }
        if (Input.GetKey(KeyCode.E))
        {
            speed = dashSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
        if(currentHealth <= 0)
        {
            //delayHelper.DelayAction(this, Respawn, 2f);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        dmgSound.Play();

        healthBar.SetHealth(currentHealth);
    }

    //collision with bullet objects
    void OnTriggerEnter(Collider other)
    {
        //detect if it's player ship
        enemyBullet EnemyBullet
            = other.gameObject.GetComponent<enemyBullet>();
        //if we found something valid, continue
        if (enemyBullet != null)
        {
            //do sometthing
            explosion.transform.position = EnemyBullet.transform.position;
            EnemyBullet.kill();
            TakeDamage(20f);
            hazardExplode();
        }
    }

    public void Respawn()
    {
        FirstPersonPlayer.gameObject.SetActive(true);
        FirstPersonPlayer.transform.position = new Vector3(0, 0, 0);
        //Debug.Log("Something Happened");
    }
    public void hazardExplode()
    {
        explosion.Play();
    }
}
