using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector3 offset;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
