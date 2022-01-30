using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class SetHands : MonoBehaviour
{
    public static SetHands _instance;

    private bool isTruck = false;
    public List<string> handPosStr = new List<string>();  //存储每个关键点的字符串坐标
    public List<Vector3> handPos = new List<Vector3>();   //存储每隔关键点的三维坐标

    private void Awake()
    {
        _instance = this;
    }
    private string GetDataStr()
    {
        return Connect_Client._instance.data;
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))    //开始识别的条件
        {
            isTruck = true;
        }
        if (isTruck)
        {
            SetHandPosStr(GetDataStr());
            handPos.Clear();
            for (int i = 0; i < handPosStr.Count; i++)
            {
                handPos.Add(SetHandPos(handPosStr[i]));
            }
        }
        //Debug.Log(SetDataStr());
    }
    /// <summary>
    /// 获得手部标记点的坐标字符串，并将每一个标记点的坐标字符串放入列表中
    /// </summary>
    /// <param name="str">lmList字符串</param>
    private void SetHandPosStr(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
            JArray ja = (JArray)dic["lmList"];
            handPosStr.Clear();
            for (int i = 0; i < ja.Count; i++)
            {
                handPosStr.Add(ja[i].ToString().Replace("\n", "").Replace(" ", "").Replace("\t", ""));
            }
        }
    }
    /// <summary>
    /// 获得手部标记点的世界坐标，并将每一个坐标放入列表中
    /// </summary>
    /// <param name="str">传入每一个标记点的坐标字符串</param>
    /// <returns></returns>
    private Vector3 SetHandPos(string str)
    {
        string str_1 = "";
        for (int i = 2; i < str.Length - 2; i++)
        {
            str_1 += str[i];
        }
        string[] s = str_1.Split(',');
        float X = float.Parse(s[0]);
        float Y = float.Parse(s[1]);

        return Camera.main.ScreenToWorldPoint(new Vector3(X, Y,-Camera.main.transform.position.z));
    }

}
