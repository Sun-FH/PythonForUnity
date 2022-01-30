using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class SetHands : MonoBehaviour
{
    public static SetHands _instance;

    private bool isTruck = false;
    public List<string> handPosStr = new List<string>();  //�洢ÿ���ؼ�����ַ�������
    public List<Vector3> handPos = new List<Vector3>();   //�洢ÿ���ؼ������ά����

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
        if (Input.GetMouseButton(1))    //��ʼʶ�������
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
    /// ����ֲ���ǵ�������ַ���������ÿһ����ǵ�������ַ��������б���
    /// </summary>
    /// <param name="str">lmList�ַ���</param>
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
    /// ����ֲ���ǵ���������꣬����ÿһ����������б���
    /// </summary>
    /// <param name="str">����ÿһ����ǵ�������ַ���</param>
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
