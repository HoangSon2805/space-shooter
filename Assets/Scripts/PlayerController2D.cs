using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControllers : MonoBehaviour
{
    public float moveSpeed = 8f;

    [Header("Movement boundaries")]
    public Vector2 minBounds = new Vector2(-3f, -4.5f);
    public Vector2 maxBounds = new Vector2(3f, 4.5f);
    
    // Update is called once per frame
    void Update()
    {   
        // Read input (WASD or Arraw keys)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Calculate movement vector
        Vector3 move = new Vector3(h, v, 0f).normalized;

        // Apply movement
        transform.position += move * moveSpeed * Time.deltaTime;

        // Clamp player position inside screen bounds
        ClampToBounds();

    }

    void ClampToBounds() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }
}
