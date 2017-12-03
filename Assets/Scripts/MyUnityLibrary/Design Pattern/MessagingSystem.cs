using System.Collections.Generic;
using UnityEngine;

public class MessagingSystem : MonoSingleton<MessagingSystem>
{
    protected MessagingSystem() {}

    public delegate bool MessageHandlerDelegate(BaseMessage message);

    private Dictionary<string, List<MessageHandlerDelegate>> _listenerDict = new Dictionary<string, List<MessageHandlerDelegate>>();

    public bool AttacthListener(System.Type type, MessageHandlerDelegate handler)
    {
        if (type == null)
        {
            Debug.Log("MessagingSystem: AttachListener failed due to no message type specified");
            return false;
        }

        string msgName = type.Name;
        if (!_listenerDict.ContainsKey(msgName))
        {
            _listenerDict.Add(msgName, new List<MessageHandlerDelegate>());
        }

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgName];
        if (listenerList.Contains(handler))
        {
            return false;
        }
        listenerList.Add(handler);
        return true;
    }

    private Queue<BaseMessage> _messageQeue = new Queue<BaseMessage>();

    public bool QueueMessage(BaseMessage msg)
    {
        if (!_listenerDict.ContainsKey(msg.Name))
        {
            return false;
        }

        _messageQeue.Enqueue(msg);
        return true;
    }


    private float maxQueueProcessingTime = 0.16667f;

    void Update()
    {
        float timer = 0.0f;
        while (_messageQeue.Count > 0)
        {
            if (maxQueueProcessingTime > 0.0f)
            {
                if(timer > maxQueueProcessingTime)
                    return;
            }

            BaseMessage msg = _messageQeue.Dequeue();
            if(!TriggerMessage(msg))
                Debug.LogError(string.Format("Error when processing message: {0}", msg.Name));

            if (maxQueueProcessingTime > 0.0f)
                timer += Time.deltaTime;
        }
    }

    public bool TriggerMessage(BaseMessage msg)
    {
        string msgName = msg.Name;
        if (!_listenerDict.ContainsKey(msgName))
        {
            Debug.LogError("MessagingSystem: Message \"" + msgName + "\" has no listeners!");
            return false;
        }

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgName];

        for (int i = 0; i < listenerList.Count; i++)
        {
            if (listenerList[i](msg))
                return true;
        }

        return true;
    }
}
