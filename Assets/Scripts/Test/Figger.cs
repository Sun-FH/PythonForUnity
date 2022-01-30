using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figger : MonoBehaviour
{
    private float dis = 0;
    public Transform cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SetHands._instance.handPos.Count != 0 && Input.GetKey(KeyCode.K))
        {
            dis = Vector3.Distance(SetHands._instance.handPos[4], SetHands._instance.handPos[8]);
            Debug.Log(dis);
            cube.transform.localScale = Vector3.one * dis / 5;
        }
        
    }
}
