using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subject 4 - The main movement
public class Player : MonoBehaviour {

    private BoxCollider2D _rb;
    private RaycastHit2D _hit;
    private Vector3 _move;
    private float _speed;
    private float _x;
    private float _y;
    public Animator _an;

    void Start(){
        _rb = gameObject.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate(){
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        _an.SetFloat("Horizontal", movement.x);
        _an.SetFloat("Vertical", movement.y);
        _an.SetFloat("Magnitude", movement.magnitude);

        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");
        
        _move = new Vector3(_x,_y,0);
        if(_move.x > 0.1f) transform.localScale = Vector3.one;
        else if(_move.x < 0.1f) transform.localScale = new Vector3(-1,1,1);
    
        _hit = Physics2D.BoxCast(transform.position, _rb.size, 0, new Vector2(0, _move.y), Mathf.Abs(_move.y * Time.deltaTime), LayerMask.GetMask("Enemy/NPC","Blocking"));
        if(_hit.collider == null){
            transform.Translate(0,_move.y * Time.deltaTime, 0);
        }
        _hit = Physics2D.BoxCast(transform.position, _rb.size, 0, new Vector2(_move.x, 0), Mathf.Abs(_move.x * Time.deltaTime), LayerMask.GetMask("Enemy/NPC","Blocking"));
        if(_hit.collider == null){
            transform.Translate(_move.x * Time.deltaTime, 0, 0);
        }
    }
}
