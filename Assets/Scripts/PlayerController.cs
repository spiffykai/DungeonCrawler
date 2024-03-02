using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject attackObject;
    
    public GameObject weaponPivot;
    
    public float playerSpeed = 4.0f;
    public int currentDamage = 1;
    public int health = 10;
    public int maxHealth = 10;
    public int coins = 0;
    
    private Rigidbody2D _rb;
    private Vector2 _attackDirection;
    
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _attackDirection = Vector2.up;
        
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    
    void Update(){
        //get attack direction based on input
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            _Attack(new Vector2(0,1));
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            _Attack(new Vector2(0,-1));
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            _Attack(new Vector2(-1,0));
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            _Attack(new Vector2(1, 0));
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

    public void takeDamage(int damage, Vector2 direction){
        health -= damage;
        healthBar.value = health;
        _rb.AddForce(direction * 1000);
    }

    private void _Attack(Vector2 direction){
        //attackObject.GetComponent<Animator>().SetTrigger("attack");
        attackObject.transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        attackObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        attackObject.GetComponent<Animator>().Play("player_attack", -1, 0f);
        
        RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, new Vector2(1, 1), 0, direction, .8f, LayerMask.GetMask("Enemies"));
        foreach (var hitObject in hit){
            if (hitObject.collider != null){
                if (hitObject.collider.CompareTag("Enemy")){
                    hitObject.collider.GetComponent<EnemyController>().TakeDamage(currentDamage);
                    hitObject.collider.GetComponent<Rigidbody2D>().AddForce(direction * 1000);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("HeartPickup")){
            health += 2;
            if (health > maxHealth){
                health = maxHealth;
            }
            healthBar.value = health;
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("CoinPickup")){
            coins++;
            Destroy(other.gameObject);
        }
    }
}
