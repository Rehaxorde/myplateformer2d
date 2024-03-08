using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Moving_Platform : Platform
{
    //graphism
    private List<Vector2> checkPointPos = new List<Vector2>();

    [SerializeField]
    private Color textColor = Color.white;
    [SerializeField]
    private Vector2 textOffset;
    [SerializeField]
    private int textSize = 3;

    //moves
    [SerializeField]
    private bool isPingPong;
    [SerializeField] 
    private float startDelay;
    [SerializeField]
    private float endDelay;
    [SerializeField]
    private float checkPointDelay;

    private int nextCheckPoint;
    public int speed = 5;
    private int sens = 1;



    public new void Start()
    {
        base.Start();
        checkPointPos.Add(transform.position);
        for (int i = 0; i< transform.childCount;i++ ) 
        {
            checkPointPos.Add(transform.GetChild(i).position);
        }
        StartCoroutine(move(/*speed * Time.deltaTime*/));

    }

    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator move(/*float step*/)
    {
        //float step = speed * Time.deltaTime;
        float step = speed * 0.01f;

        Vector3 target = checkPointPos[nextCheckPoint];
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        if (transform.position == target)
        {
            yield return new WaitForSeconds(checkPointDelay);

            if(nextCheckPoint == 0)
            {
                yield return new WaitForSeconds(startDelay);
            }

            if (nextCheckPoint == checkPointPos.Count -1)
            {
                yield return new WaitForSeconds(endDelay);
            }

            if (nextCheckPoint < checkPointPos.Count - 1)
            {
                nextCheckPoint += sens;
            }
            else
            {
                if (isPingPong)
                {
                    sens = -sens;
                }
                else
                {
                    nextCheckPoint = 0;
                }
            }
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(move());
    }
    private void OnDrawGizmos()
    {
        GUI.color = textColor;
        GUIStyle labelStyle = GUI.skin.label;
        labelStyle.fontSize = textSize;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        for (int i = 0; i < transform.childCount; i++)
        {
            Handles.Label((Vector2)transform.GetChild(i).position + textOffset,i.ToString());
            if (i < transform.childCount - 1)
            {
                Handles.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position, 2);

            }
            else if(!isPingPong)
            {
                Handles.DrawLine(transform.GetChild(i).position, transform.GetChild(0).position, 2);
            }
        }
    }
}
