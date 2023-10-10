using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get ; private set; }
    public float duraction;
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }
    public void Enable()
    {
        Enable(this.duraction);
    }
    public virtual void Enable(float duraction)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duraction);
    }
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
