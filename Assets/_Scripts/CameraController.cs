using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var xPosition = Mathf.Lerp(transform.position.x, player.position.x, 0.01f);
        var yPosition = Mathf.Lerp(transform.position.y, player.position.y, 0.01f);
        transform.position = new Vector3(xPosition, yPosition, -10.0f);
    }
}
