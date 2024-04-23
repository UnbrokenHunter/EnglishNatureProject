using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Events;
using System.ComponentModel;

public class TextTrigger : MonoBehaviour
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
                trigger.Text.gameObject.SetActive(true);
                trigger.Triggered = true;
            }


            if (cart.m_Position >= trigger.EndPosition && trigger.DoDestroy)
            {
                if (trigger.Text.gameObject.activeInHierarchy)
                    trigger.Text.gameObject.SetActive(false);
            }

            if (cart.m_Position <= trigger.EndPosition && trigger.DoDestroy && trigger.Triggered) {
                if (!trigger.Text.gameObject.activeInHierarchy)
                {
                    trigger.Triggered = false;
                    trigger.Text.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Trigger trigger in triggers)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(path.EvaluatePosition(trigger.StartPosition), 1);

            if (trigger.DoDestroy)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(path.EvaluatePosition(trigger.EndPosition), 1);
            }
        }
    }
    
    [System.Serializable]
    public class Trigger
    {
        public string Name;
        public float StartPosition;
        public FadeIn Text;
        public bool Triggered = false;
        
        [Space]

        public bool DoDestroy = false;
        public float EndPosition;

    }
}