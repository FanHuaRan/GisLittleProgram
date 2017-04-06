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
	//构造函数
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
double   Box[4];              // 当前线状目标的坐标范围
int     NumParts;     // 当前线目标所包含的子线段的个数
int     NumPoints;   // 当前线目标所包含的顶点个数
int		*Parts;      // 每个子线段的第一个坐标点在 Points 的位置
Point  *Points;             // 记录所有坐标点的数组
};

struct SPolygon
{
	double   Box[4];              // 当前面状目标的坐标范围
	int     NumParts;     // 当前面目标所包含的子环的个数
    int     NumPoints;   // 构成当前面状目标的所有顶点的个数
	int  *Parts;              // 每个子环的第一个坐标点在 Points 的位置
	Point  *Points;              // 记录所有坐标点的数组
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
