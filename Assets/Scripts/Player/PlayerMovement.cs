using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;

    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v);
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}

