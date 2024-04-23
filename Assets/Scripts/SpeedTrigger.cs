using UnityEngine;
using System.Collections;
using Cinemachine;

public class SpeedTrigger : MonoBehaviour
{

    [SerializeField] private CinemachineDollyCart cart;
    [SerializeField] private CinemachineSmoothPath path;
    [SerializeField] private Move move;
    [SerializeField] private Trigger[] triggers;

    public void Update()
    {
        var max = 0f;
        var trig = triggers[0];

        foreach (Trigger trigger in triggers)
        {
            if (trigger.StartPosition > max && cart.m_Position > trigger.StartPosition)
            {
                max = trigger.StartPosition;
                trig = trigger;
            }
        }

        move.MaxSpeed = trig.Speed;
    }

    private void OnDrawGizmos()
    {
        foreach (Trigger trigger in triggers)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(path.EvaluatePosition(trigger.StartPosition), 1);
        }
    }
    
    [System.Serializable]
    public class Trigger
    {
        public float StartPosition;
        public float Speed = 1;
    }
}