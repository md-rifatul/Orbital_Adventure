using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    public float yOffset = 1f;

    // Update is called once per frame
    private void Update()
    {
        transform.position=new Vector3(player.position.x+3f,player.position.y+3f,transform.position.z);
        
    }
}
