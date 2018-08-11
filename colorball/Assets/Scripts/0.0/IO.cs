using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class IO  {
	//总次数
	//public  int totaltime=0;
	//打中次数
	//public  int hittime=0;
	//玩家名
	public  string name ="player_y";

	public int allhit;
	public int alltimes;
	public float allangle;
	//命中率
	private float hitrate;
	//偏差率
	private float deviation;

	//存活时间
	//public float t;

	private  string allstr;

	public void creatfile()
	{
		FileStream fs = new FileStream("/Users/yangfan/Desktop/数据统计.txt", FileMode.Create);
		fs.Close();
		//StringBuilder sb = new StringBuilder();//声明一个可变字符串
		//for (int i = 0; i<10; i++)
		//{
		//	//循环给字符串拼接字符
		//	sb.Append(i*100 + "|"+"\n");
		//}
		//sb.Append (2000);
		//写文件 文件名为save.text
		//这里的FileMode.create是创建这个文件,如果文件名存在则覆盖重新创建
		//存储时时二进制,所以这里需要把我们的字符串转成二进制
		//byte[] bytes = new UTF8Encoding().GetBytes(sb.ToString());
		//fs.Write(bytes, 0, bytes.Length);
		//每次读取文件后都要记得关闭文件
	}

	//读取
	public  void writetofile(string mode,float dist)
	{
		hitrate = (float)allhit / alltimes *100;
		hitrate=Math.mathfn (hitrate, 3);
		deviation = allangle / (alltimes * 180) * 100;
		deviation=Math.mathfn (deviation, 3);

		//allstr="Mode: Random mixing model"+"\t"+"Player: "+name +"\t"+ "Recently "+totaltime+" times hit: "+hittime+",hit rate: "+hitrate+" total time: "+t+"\n";
		allstr="Mode: "+mode+"\t"+"  Player: "+name +"\t"+"Distance :"+dist+
			"\t"+ "hitrate: "+"\t"+hitrate+"\t"+ "deviation: "+"\t"+deviation+"\t"+"\n";
		File.AppendAllText ("/Users/yangfan/Desktop/数据统计.txt",allstr.ToString(), Encoding.Default);

		//FileMode.Open打开路径下的save.text文件
		//FileStream fs = new FileStream("/Users/yangfan/Desktop/save.txt", FileMode.Open,FileAccess.ReadWrite);
		//StringBuilder sb = new StringBuilder();
		//sb.Append ("123456");
		//byte[] bytess = new UTF8Encoding ().GetBytes (sb.ToString());
		//fs.Write(bytess,0,bytess.Length);
		//将读取到的二进制转换成字符串
		//string s = new UTF8Encoding().GetString(bytes);
		//将字符串按照'|'进行分割得到字符串数组
		//string[] itemIds = s.Split('|');
		//for (int i = 0; i < itemIds.Length; i++)
		//{
			//Debug.Log(itemIds[i]);
		//}
	}
	public void Newtime(){
		File.AppendAllText ("/Users/yangfan/Desktop/数据统计.txt", "next attempt:\n");
	}
}
