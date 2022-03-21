using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{

    // Start is called before the first frame update

    public enum SpawnState { SPAWNING, WAITING, COUNTING }
    // Start is called before the first frame update
    
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject player;
    public CharacterController playerController;
    public MenuScript gameManager;
    public int maxCount = 10;
    public int count = 0;
    public float rate;
   
    public float timeBetweenWave = 5f;
    public float waveCountdown;
    public SpawnState state = SpawnState.WAITING;

    void Start()
    {
        waveCountdown = timeBetweenWave;

    }
    private void Update()
    {
        if (state == SpawnState.WAITING)
        {

        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
        if (waveCountdown <= 0)
        {   if(count <= maxCount) { 
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave());
                }
            }
        }



        IEnumerator SpawnWave()
        {
            int i = Random.Range(0, 30);
            state = SpawnState.SPAWNING;
            if (i < 29)
            {
                SpawnEnemy(enemy);
            }
            else
            {
                SpawnEnemy(enemy2);
            
            }
            
            yield return new WaitForSeconds(1f / rate);
            
            state = SpawnState.WAITING;
            yield break;
        }
        void SpawnEnemy(GameObject _enemy)
        {
            count += 1;
            Debug.Log("Spawning Enemy" + _enemy.name);
            GameObject zombie = (GameObject) Instantiate(_enemy, transform.position, transform.rotation);
            zombie.GetComponent<TargetTransform>().tartgetController = playerController;
            zombie.GetComponent<TargetTransform>().targetGameObject = player;
            zombie.GetComponent<Health>().gameManager = gameManager;


        }
    
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag == "Player")
            state = SpawnState.COUNTING;
        //Debug.Log(other.tag);
        
           
        
    }

}
