using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGuideController : MonoBehaviour
{
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
}
