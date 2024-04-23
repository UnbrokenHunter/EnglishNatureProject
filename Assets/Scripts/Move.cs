using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart cart;
    [SerializeField] private CinemachineSmoothPath path;

    [SerializeField] private int CurPos = 0;
    private int dir = 1;

    public float MaxSpeed = 1f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.6f;
    [SerializeField] private Trigger[] triggers;

    [SerializeField] private float simSpeed = 1f;

    [SerializeField] private float windMult = 1f;
    [SerializeField] private float windAccel = 1f;
    private AudioSource source;

    private void Start()
    {
        Time.timeScale = simSpeed;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        source.volume = Mathf.Lerp(source.volume, cart.m_Speed * windMult, windAccel * Time.deltaTime);

        if ((triggers[CurPos].Position > cart.m_Position && dir == 1) ||
            (triggers[CurPos].Position < cart.m_Position && dir == -1))
        {
            cart.m_Speed = Mathf.Lerp(cart.m_Speed, MaxSpeed * dir, acceleration * Time.deltaTime);

        }
        else
        {
            cart.m_Speed = Mathf.Lerp(cart.m_Speed, 0, deceleration * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CurPos + 1 < triggers.Length)
                {
                    dir = 1;
                    CurPos++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (CurPos > 0)
                {
                    dir = -1;
                    CurPos--;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        foreach (Trigger trigger in triggers)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(path.EvaluatePosition(trigger.Position), 1);
        }
    }

    [System.Serializable]
    public class Trigger
    {
        [Range(0, 67)] public float Position;
    }

}
