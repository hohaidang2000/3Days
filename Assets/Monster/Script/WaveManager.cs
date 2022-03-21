using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    public int count = 0;
    public int winCount = 50;
    public bool win = false;

    public void Count()
    {
        count += 1;
        if(count == winCount){
            win = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
