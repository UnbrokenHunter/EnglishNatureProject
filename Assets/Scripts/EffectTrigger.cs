using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Events;

public class EffectTrigger : MonoBehaviour
{

    [SerializeField] private CinemachineDollyCart cart;
    [SerializeField] private CinemachineSmoothPath path;
    [SerializeField] private Trigger[] triggers;

    public void Update()
    {
        foreach (Trigger trigger in triggers)
        {
            if (cart.m_Position >= trigger.StartPosition && !trigger.Triggered)
            {
                trigger.Event.Invoke();
                trigger.Triggered = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Trigger trigger in triggers)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(path.EvaluatePosition(trigger.StartPosition), 1);
        }
    }
    
    [System.Serializable]
    public class Trigger
    {
        public string Name;
        public float StartPosition;
        public UnityEvent Event;
        public bool Triggered = false;
    }
}