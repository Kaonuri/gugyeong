using UnityEngine;

public abstract class UpdateableMonoBehaviour : MonoBehaviour, IUpdateable
{
    public virtual void OnUpdate(float dt) { }

    protected void Start()
    {
        GameLogic.Instance.RegisterUpdateableObject(this);
        Initialize();
    }

    protected void OnDestroy()
    {
        GameLogic.Instance.DeregisterUpdateableObject(this);
    }

    protected virtual void Initialize()
    {
        
    }
}
