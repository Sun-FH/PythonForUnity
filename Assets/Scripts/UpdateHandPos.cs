using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHandPos : MonoBehaviour
{
    private List<GameObject> point = new List<GameObject>();
    private List<LineRenderer> lineR = new List<LineRenderer>();
    private List<LineRenderer> lineR_2 = new List<LineRenderer>();

    private void Start()
    {
        for (int i = 0; i < 21; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.SetParent(this.transform);
            go.GetComponent<SphereCollider>().enabled = false;
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.Add(go);
        }
        for (int i = 0; i < 20; i++)
        {
            GameObject go = new GameObject(i.ToString());
            go.transform.SetParent(this.transform.Find("handLine"));
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lineR.Add(lr);
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject go = new GameObject(i.ToString());
            go.transform.SetParent(this.transform.Find("handLine"));
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lineR_2.Add(lr);
        }
    }

    private void Update()
    {
        UpdataPos();
    }

    private void UpdataPos()
    {
        if (SetHands._instance.handPos.Count != 0)
        {
            for (int i = 0; i < SetHands._instance.handPos.Count; i++)
            {
                point[i].transform.position = SetHands._instance.handPos[i];
            }
            DrawHandLine();
        }
    }

    private void DrawHandLine()
    {
        for (int i = 0; i < lineR.Count; i++)
        {
            if (i%4 != 0)
            {
                lineR[i].SetPosition(0, point[i].transform.localPosition);
                lineR[i].SetPosition(1, point[i + 1].transform.localPosition);
            }
            else
            {
                if (i == 0)
                {
                    lineR[i].SetPosition(0, point[i].transform.localPosition);
                    lineR[i].SetPosition(1, point[i + 1].transform.localPosition);
                }
                else
                {
                    lineR[i].SetPosition(0, point[0].transform.localPosition);
                    lineR[i].SetPosition(1, point[i + 1].transform.localPosition);
                }
            }
        }
        lineR_2[0].SetPosition(0, point[5].transform.localPosition);
        lineR_2[0].SetPosition(1, point[9].transform.localPosition);

        lineR_2[1].SetPosition(0, point[9].transform.localPosition);
        lineR_2[1].SetPosition(1, point[13].transform.localPosition);

        lineR_2[2].SetPosition(0, point[13].transform.localPosition);
        lineR_2[2].SetPosition(1, point[17].transform.localPosition);
    }
}
