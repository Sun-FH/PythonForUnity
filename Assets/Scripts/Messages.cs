using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;


public class Messages
{
    private byte[] data = new byte[1024];     //读取到的数据会存到这里
    private int startIndex = 0;  //从第几位开始存也等于存了多少个数据
    private string str = "";    //读取到的最终数据
    public int RemainSize
    {
        get { return (data.Length - startIndex); }
    }
    public byte[] Data
    {
        get { return data; }
    }
    public int StartIndex
    {
        get { return startIndex; }
    }
    public string GetData
    {
        get { return str; }
    }
    /// <summary>
    /// 更新statIndex
    /// </summary>
    /// <param name="count"></param>
    public void AddCound(int count)
    {
        startIndex += count;
    }

    /// <summary>
    /// 解析/读取数据
    /// </summary>
    public void ReadMessage()
    {
        while (true)
        {
            if (startIndex <= 4) return;        //数据不完整
            int count = BitConverter.ToInt32(data, 0);   //接受到的数据的长度
            if (startIndex - 4 >= count)
            {
                str = Encoding.UTF8.GetString(data, 4, count);  //从第四个索引开始读取，读取count个（读一条完整的数据）
                //Debug.Log("data:" + str);
                
                Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
        
        
    }

}
