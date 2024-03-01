using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{

    public float moveSpeed = 3000.0f;
    public float detectionRadius = 3.0f;
    public int health = 5;
    
    private Rigidbody2D _rb;
    
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
    }

    
    void Update(){
        
    }

    void FixedUpdate(){
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRadius, Vector2.right, 0.0f, LayerMask.GetMask("Player"));
        if (hit.collider != null){
            if (hit.collider.gameObject.CompareTag("Player")){
                Vector2 direction = (hit.collider.transform.position - transform.position).normalized;
                _rb.AddForce(Time.deltaTime * moveSpeed * direction);
            }
        }
        
    }

    public void TakeDamage(int damage){
        health--;
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
