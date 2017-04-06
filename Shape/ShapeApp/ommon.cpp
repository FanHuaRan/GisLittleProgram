// ommon.cpp: implementation of the Common class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "ShapeApp.h"
#include "ommon.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CCommon::CCommon()
{
	
}

CCommon::~CCommon()
{

}

//画点
//DEL void CCommon::DrawPoint(Point p)
//DEL {
//DEL 
//DEL }
//画线
//DEL void CCommon::DrawPolyLine(PolyLine polyline)
//DEL {
//DEL 
//DEL }
//画面
//DEL void CCommon::DrawPolygon(SPolygon polygon)
//DEL {
//DEL 
//DEL }
//void CCommon::DrawPolygon(vector<SPolygon> polygons)
//{
//
//}
//转化字节顺序
long CCommon::OnChangeByteOrder(int indata)
{
  	   char ss[8];
       char ee[8];
       long val =  long(indata);
       _ultoa( val, ss, 16);// 将十六进制的数 (val) 转到一个字符串 (ss) 中
       int i;
       int length=strlen(ss);
       if(length!=8)
       {
          for(i=0;i<8-length;i++)
                  ee[i]='0';
          for(i=0;i<length;i++)
                  ee[i+8-length]=ss[i];
           for(i=0;i<8;i++)
                  ss[i]=ee[i];
		}
       ////****** 进行倒序
       int t;
	   for(i=0;i<8;i++)
	   {
		   t=ss[i];
		   ss[i]=ss[7-i];
		   ss[7-i]=t;		
	   }
	   
	 ////******
       //****** 将存有十六进制数 (val) 的字符串 (ss) 中的十六进制数转成十进制数
       int value=0;
       for(i=0;i<8;i++)
       {
              int k;
              CString mass;
              mass=ss[i];
              if(ss[i]=='a' || 
                 ss[i]=='b' || 
                 ss[i]=='c' ||
                 ss[i]=='d' ||
                 ss[i]=='e' ||
                 ss[i]=='f')
                     k=10+ss[i]-'a';
              else
                     sscanf(mass,"%d",&k);
              value=value+int(k*pow(16,7-i));
       }
       return (value);
}

