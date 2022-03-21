using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimatorController : MonoBehaviour
{
    public Animator animator;
    public string state = "idle" ;
    public float moveSpeed = 1;
    [SerializeField] private float _waitTime = 5f;
   


    // Start is called before the first frame update
    private void Awake()
    {
        if (!animator) { gameObject.GetComponent<Animator>(); }
    }
    void Start()
    {
        
    }
    public void setMoveSpeed(float speed)
    {
        animator.SetFloat("moveSpeed", speed);
    }
    
    // Update is called once per frame

    public void Dead()
    {
        state = "dead";
        StartCoroutine(SelfDestruct());

    }
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }
    void Update()
    {
        //setMoveSpeed(moveSpeed);
        switch (state)
        {
            case "attack":
                animator.SetTrigger("attack");
                break;
            case "dead":
                animator.SetTrigger("dead");
                break;
        }
    }
}