using UnityEngine;

public class CheckAnimations : MonoBehaviour
{
    public Animator animator; // Assign the Animator component in the Inspector

    [ContextMenu("Check Jump")]
    public void CheckJumpTrigger()
    {
        // Check if the "Jump" trigger is active
        if (animator.GetBool("Jump"))
        {
            Debug.Log("Jump trigger is active!");
        }
        else
        {
            Debug.Log("Jump trigger is not active.");
        }
    }
    [ContextMenu("Check Chop")]
    public void CheckChopTrigger()
    {
        // Check if the "Jump" trigger is active
        if (animator.GetBool("Chop"))
        {
            Debug.Log("Chop trigger is active!");
        }
        else
        {
            Debug.Log("Chop trigger is not active.");
        }
    }
    [ContextMenu("Check Speed")]
    public void CheckSpeed()
    {
        // Check if the "Jump" trigger is active
        float _speed = animator.GetFloat("Speed");       
        Debug.Log("Speed  is " + _speed);
        
        
    }

    public void Update()
    {
        //CheckChopTrigger();
    }
}
