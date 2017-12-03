using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoSingleton<GameLogic>
{
    private readonly List<IUpdateable> _updateableObjects = new List<IUpdateable>();

    protected GameLogic() { }

    public void RegisterUpdateableObject(IUpdateable obj)
    {
        if (!_updateableObjects.Contains(obj))
        {
            _updateableObjects.Add(obj);
        }
    }

    public void DeregisterUpdateableObject(IUpdateable obj)
    {
        if (_updateableObjects.Contains(obj))
        {
            _updateableObjects.Remove(obj);
        }
    }

    private void Update()
    {
        var dt = Time.deltaTime;
        foreach (var updateableObject in _updateableObjects)
        {
            updateableObject.OnUpdate(dt);
        }
    }
}
