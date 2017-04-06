// ShapeAppView.h : interface of the CShapeAppView class
//
/////////////////////////////////////////////////////////////////////////////
#include "stdafx.h"
#if !defined(AFX_SHAPEAPPVIEW_H__A8FE1B78_4095_48A9_8118_54A5E664F163__INCLUDED_)
#define AFX_SHAPEAPPVIEW_H__A8FE1B78_4095_48A9_8118_54A5E664F163__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CShapeAppView : public CView
{
protected: // create from serialization only
	CShapeAppView();
	DECLARE_DYNCREATE(CShapeAppView)

// Attributes
public:
	CShapeAppDoc* GetDocument();

// Operations
public:
	CString filePath; //�ļ�·��
    CPen pen;//���廭��
    int ShapeType;//shp�ļ�����
    int count;//����
    FileHead file_Head;//�ļ�ͷ
	vector<Point> points; //�㼯��
	vector<PolyLine> polylines; //�߼���
	vector<SPolygon> polygons; //�漯��
	CCommon common;//������
	FILE*   m_ShpFile_fp;       //****Shp �ļ�ָ��
	// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeAppView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	void ClearWindow();
	void DrawPoints(vector<Point> shape_points);
	void DrawPolyLine(vector<PolyLine> shape_polyline);
	void DrawPolygon(vector<SPolygon> shape_polygon);
	void ReadShape(void);
	void ReadPolygon(void);
	void ReadPolyLine(void);
	void ReadPoint(void);
	void ReadHead(CString path);
	virtual ~CShapeAppView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CShapeAppView)
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnFileOpen();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in ShapeAppView.cpp
inline CShapeAppDoc* CShapeAppView::GetDocument()
   { return (CShapeAppDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPEAPPVIEW_H__A8FE1B78_4095_48A9_8118_54A5E664F163__INCLUDED_)
