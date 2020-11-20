using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform arrow;

    public Transform northPoint;
    public Transform target;
    public Transform player;

    private Quaternion targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(arrow.up);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetDirection();
    }

    void UpdateTargetDirection()
    {
        Vector3 target_trunc = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 player_trunc = new Vector3(player.position.x, 0f, player.position.z);
        Vector3 direction = player_trunc - target_trunc;

        targetDirection = Quaternion.LookRotation(direction);

        targetDirection.z = -targetDirection.y;
        targetDirection.x = 0;
        targetDirection.y = 0;

        arrow.localRotation = targetDirection * Quaternion.Euler(-Vector3.up);
    }
}
