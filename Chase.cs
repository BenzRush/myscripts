using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public bool isdead;
    public float ChasingSpeed;
    ObjectDestroyer hit;
    public int ChasingDistance;
    public float ChasingMagnitude;
    public AudioSource EnemyFootsteps;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        

}

    // Update is called once per frame
    public void Update()
    {
       
        
            if (Vector3.Distance(player.position, this.transform.position) < ChasingDistance)
            {
                
                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), ChasingSpeed);

                anim.SetBool("isIdle", false);

                if (direction.magnitude > ChasingMagnitude)
                {
                    
                    this.transform.Translate(0, 0, 0.02f);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                    


            }
                else
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isWalking", false);
                    


            }
            }
            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", false);

            }
        }
    

    public void isDeath()
    {
        
        hit.transform.GetComponent<Animator>().SetBool("isDeath", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
        



    }

    }

