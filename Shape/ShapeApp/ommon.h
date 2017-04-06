// ommon.h: interface for the Common class.
//
//////////////////////////////////////////////////////////////////////
#include "stdafx.h"
#if !defined(AFX_OMMON_H__562D29E2_7164_4BB6_81BC_296E00BA421C__INCLUDED_)
#define AFX_OMMON_H__562D29E2_7164_4BB6_81BC_296E00BA421C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

struct FileHead
{
	   int FileCode;
       int Unused;
       int FileLength;
       int Version;
       int ShapeType;
       double Xmin;
       double Ymin;
       double Xmax;
       double Ymax;
       double Zmin;
       double Zmax;
       double Mmin;
       double Mmax;
};
struct Point
{
	double X;
	double Y;
	//���캯��
	Point::Point(double x,double y)
	{
		X=x;
		Y=y;
	}
	Point::Point()
	{
	
	}
};
struct PolyLine
{
double   Box[4];              // ��ǰ��״Ŀ������귶Χ
int     NumParts;     // ��ǰ��Ŀ�������������߶εĸ���
int     NumPoints;   // ��ǰ��Ŀ���������Ķ������
int		*Parts;      // ÿ�����߶εĵ�һ��������� Points ��λ��
Point  *Points;             // ��¼��������������
};

struct SPolygon
{
	double   Box[4];              // ��ǰ��״Ŀ������귶Χ
	int     NumParts;     // ��ǰ��Ŀ�����������ӻ��ĸ���
    int     NumPoints;   // ���ɵ�ǰ��״Ŀ������ж���ĸ���
	int  *Parts;              // ÿ���ӻ��ĵ�һ��������� Points ��λ��
	Point  *Points;              // ��¼��������������
};

class CCommon  
{
public:
	long OnChangeByteOrder (int indata);
//	void DrawPolygon(vector<SPolygon> polygons);
	CCommon();
	virtual ~CCommon();

};


#endif // !defined(AFX_OMMON_H__562D29E2_7164_4BB6_81BC_296E00BA421C__INCLUDED_)
