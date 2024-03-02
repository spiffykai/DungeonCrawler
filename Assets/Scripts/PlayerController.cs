using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public GameObject weaponPivot;
    
    public float playerSpeed = 4.0f;
    public int currentDamage = 1;
    public int health = 5;
    
    private Rigidbody2D _rb;
    public Vector2 _attackDirection;
    
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _attackDirection = Vector2.up;
    }
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            _Attack();
        }
    }
    
    void FixedUpdate(){
        //Get axis input and move player using RigidBody2D
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector2(moveHorizontal, moveVertical);
        movement = movement.normalized;
        _rb.AddForce(Time.deltaTime * playerSpeed * movement);
        
        
        //kinda bad solution, works for now
        if (moveHorizontal > 0){
            _attackDirection = Vector2.right;
            weaponPivot.transform.position = new Vector2(transform.position.x + .5f, transform.position.y);
            weaponPivot.transform.localScale = new Vector3(1, 1, 1);
        } 
        
        if (moveHorizontal < 0){
            _attackDirection = Vector2.left;
            weaponPivot.transform.position = new Vector2(transform.position.x - .5f, transform.position.y);
            weaponPivot.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (moveVertical > 0){
            _attackDirection = Vector2.up;
            weaponPivot.transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
            weaponPivot.transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (moveVertical < 0){
            _attackDirection = Vector2.down;
            weaponPivot.transform.position = new Vector2(transform.position.x, transform.position.y - .5f);
            weaponPivot.transform.localScale = new Vector3(1, -1, 1);
        }
    }

    public void takeDamage(int damage){
        health -= damage;
        _rb.AddForce(-_attackDirection * 1000);
    }

    private void _Attack(){
        RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, new Vector2(1, 1), 0, _attackDirection, .8f, LayerMask.GetMask("Default"));
        foreach (var hitObject in hit){
            if (hitObject.collider != null){
                if (hitObject.collider.CompareTag("Enemy")){
                    hitObject.collider.GetComponent<EnemyController>().TakeDamage(currentDamage);
                    hitObject.collider.GetComponent<Rigidbody2D>().AddForce(_attackDirection * 1000);
                }
            }
        }
    }
}
