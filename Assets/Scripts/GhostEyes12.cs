using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes12 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }  
    public Movement movement { get; private set; }  
    // Start is called before the first frame update
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

     private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            spriteRenderer.sprite = right;
        }
    }
}
