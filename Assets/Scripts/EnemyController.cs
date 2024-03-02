using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour{

    [SerializeField] private Slider _healthBar;
    [SerializeField] public GameObject[] dropItems;
    [SerializeField] public int[] dropChances;
    
    public float moveSpeed = 3000.0f;
    public float detectionRadius = 3.0f;
    public int maxHealth = 5;
    public int health = 5;
    
    private Rigidbody2D _rb;
    
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _healthBar.maxValue = maxHealth;
        _healthBar.value = health;
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
        _healthBar.value = health;
        if (health <= 0){
            foreach (var item in dropItems){
                var random = UnityEngine.Random.Range(0, 100);
                if (random < dropChances[Array.IndexOf(dropItems, item)]){
                    Instantiate(item, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().takeDamage(1);
        }
    }
}
