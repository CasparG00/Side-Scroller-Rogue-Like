using System;

public class Player : Singleton<Player>
{
    public static event Action<bool> grounded;

    private void OnEnable()
    {
        Instance = this;
    }

    public void InvokeGroundedAction(bool _isGrounded)
    {
        grounded?.Invoke(!_isGrounded);
    }
}
