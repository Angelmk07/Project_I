using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    private List<PoisonReceiver> receivers = new List<PoisonReceiver>();
    private int damage = 3;
    public void ApplyPoisonTo(PoisonReceiver receiver)
    {
        receiver.AddStack(damage);
        receivers.Add(receiver);
    }
    public void RemoveReceiver(PoisonReceiver receiver)
    {
        receivers.Remove(receiver);
    }
    public void TakeAll()
    {
        if (receivers.Count != 0)
        {
            for (int i = 0; i < receivers.Count - 1; i++)
            {
                receivers[i].StackExplode();
                RemoveReceiver(receivers[i]);
            }
        }

    }
    public void TeleportToInfected()
    {
        if (receivers.Count != 0)
        {
            PoisonReceiver Chosen = receivers[Random.Range(0, receivers.Count)];
            if(Chosen != null)
            {
                gameObject.transform.position = Chosen.transform.position;
                Chosen.StackExplode();
                RemoveReceiver(Chosen);
            }

        }

    }

}