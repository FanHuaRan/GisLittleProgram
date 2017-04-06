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

//����
//DEL void CCommon::DrawPoint(Point p)
//DEL {
//DEL 
//DEL }
//����
//DEL void CCommon::DrawPolyLine(PolyLine polyline)
//DEL {
//DEL 
//DEL }
//����
//DEL void CCommon::DrawPolygon(SPolygon polygon)
//DEL {
//DEL 
//DEL }
//void CCommon::DrawPolygon(vector<SPolygon> polygons)
//{
//
//}
//ת���ֽ�˳��
long CCommon::OnChangeByteOrder(int indata)
{
  	   char ss[8];
       char ee[8];
       long val =  long(indata);
       _ultoa( val, ss, 16);// ��ʮ�����Ƶ��� (val) ת��һ���ַ��� (ss) ��
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
       ////****** ���е���
       int t;
	   for(i=0;i<8;i++)
	   {
		   t=ss[i];
		   ss[i]=ss[7-i];
		   ss[7-i]=t;		
	   }
	   
	 ////******
       //****** ������ʮ�������� (val) ���ַ��� (ss) �е�ʮ��������ת��ʮ������
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

