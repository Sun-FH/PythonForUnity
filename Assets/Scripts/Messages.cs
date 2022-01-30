using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;


public class Messages
{
    private byte[] data = new byte[1024];     //��ȡ�������ݻ�浽����
    private int startIndex = 0;  //�ӵڼ�λ��ʼ��Ҳ���ڴ��˶��ٸ�����
    private string str = "";    //��ȡ������������
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
    /// ����statIndex
    /// </summary>
    /// <param name="count"></param>
    public void AddCound(int count)
    {
        startIndex += count;
    }

    /// <summary>
    /// ����/��ȡ����
    /// </summary>
    public void ReadMessage()
    {
        while (true)
        {
            if (startIndex <= 4) return;        //���ݲ�����
            int count = BitConverter.ToInt32(data, 0);   //���ܵ������ݵĳ���
            if (startIndex - 4 >= count)
            {
                str = Encoding.UTF8.GetString(data, 4, count);  //�ӵ��ĸ�������ʼ��ȡ����ȡcount������һ�����������ݣ�
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
