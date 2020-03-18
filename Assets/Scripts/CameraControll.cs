using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            findPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            findPlayer();
        transform.position = new Vector3(player.transform.position.x, 100, player.transform.position.z);
    }

    void findPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
