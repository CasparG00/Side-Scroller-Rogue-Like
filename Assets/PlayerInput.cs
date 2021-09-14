using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action jump;
    public static event Action<float> input;

    private void Update()
    {
        var isJumping = Input.GetButton("Jump");
        if (isJumping)
        {
            jump?.Invoke();
        }
        
        input?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}
