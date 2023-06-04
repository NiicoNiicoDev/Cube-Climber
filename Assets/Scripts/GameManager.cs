using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public int previousLevel = 1;
    public int currentLevel = 1;

    public float currentHeight = 0;
    public float maxHeight;

    GameObject cam;

    private void Awake()
    {
        player = FindObjectOfType<SCR_CharacterMovement>().gameObject;
        cam = Camera.main.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        currentHeight = player.transform.position.y - 0.58f;

        if (currentHeight > maxHeight)
        {
            maxHeight = currentHeight;
        }

        currentLevel = Mathf.FloorToInt(player.transform.position.y / 18) + 1;

        cam.transform.position = new Vector3(cam.transform.position.x, ((currentLevel - 1) * 18) + 10, cam.transform.position.z);
    }
}
