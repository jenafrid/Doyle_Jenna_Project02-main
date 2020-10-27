using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    [SerializeField] public GameObject explosionEffect;
    [SerializeField] private float speed;
   private float finalSpeed;
   public GameObject player;
   public Vector3 currentPlayerPos;
   private Vector3 direction;
   private Transform selfTransform;

    private void Start()
    {
        player = GameObject.Find("FirstPersonPlayer");
        if (player)
            currentPlayerPos = player.transform.position;
        finalSpeed = speed * Time.deltaTime;
        selfTransform = GetComponent<Transform>();
        direction = (currentPlayerPos - selfTransform.position).normalized;
    }

   private void Update()
    {
        transform.LookAt(currentPlayerPos);
        selfTransform.position += direction * speed * Time.deltaTime;
        if (transform.position == currentPlayerPos)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void kill()
    {
        //UnityEngine.Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }
}
