using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGuideController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, Time.deltaTime * 30f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 30f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.localScale.z < 2)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
                    transform.localScale.z + (1 * Time.deltaTime));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.localScale.z > 0.1f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
                    transform.localScale.z - (1 * Time.deltaTime));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f);
            }
        }
    }
    /*
    // References
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _spawnPoint;
    // Trajectory line smoothness/length
    [SerializeField] private int length = 10;

    //public GameObject ball;

    private Vector3[] measurement;
    private LineRenderer aimGuide;

    private void Start()
    {
        //initialize segments
        measurement = new Vector3[length];
        // get line renderer component and set length
        aimGuide = GetComponent<LineRenderer>();
        aimGuide.positionCount = length;
    }
    private void Update()
    {
        // set starting position of line renderer
        //Vector3 startPos = ball.position; // !! need to get ref of balls position from ballcontroller.cs
        //measurement[0] = startPos;
        //LineRenderer.SetPosition(0, startPos);
    }
    */

}
