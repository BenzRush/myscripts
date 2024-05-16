using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 4f;

    [SerializeField]
    Transform _camera;

    public Rigidbody _rb;
    public float _jumpSpeed = 5;
    public bool _isGrounded;

    Vector3 _forward, _right;
    
    // Start is called before the first frame update
    void Start()
    {
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90f, 0)) * _forward;
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = _right * _moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = _forward * _moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            Move();

        _camera.position = transform.position + new Vector3(-2f, 7, -5);

        float jump = Input.GetAxis("Jump");
        if(jump != 0 && _isGrounded == true)
        {
            _rb.AddForce(Vector3.up * jump * _jumpSpeed, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    private void OnCollissionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }
}
