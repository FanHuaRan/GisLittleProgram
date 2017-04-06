// ShapeAppView.cpp : implementation of the CShapeAppView class
//

#include "stdafx.h"
#include "ShapeApp.h"
#include "ShapeAppDoc.h"
#include "ShapeAppView.h"

#include<iostream>  
#include<algorithm>  
#include<cstdio>  
#include<string.h>  
#include<set>  

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CShapeAppView

IMPLEMENT_DYNCREATE(CShapeAppView, CView)

BEGIN_MESSAGE_MAP(CShapeAppView, CView)
	//{{AFX_MSG_MAP(CShapeAppView)
	ON_WM_RBUTTONDOWN()
	ON_COMMAND(ID_FILE_OPEN, OnFileOpen)
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CShapeAppView construction/destruction

CShapeAppView::CShapeAppView()
{
	// TODO: add construction code here
	//Point p(12.5,13.5);
	// vector<Point> v;
	// v.push_back(p);
	// myset.insert(p);
	pen.CreatePen(PS_SOLID,2,RGB(255,0,0));
}

CShapeAppView::~CShapeAppView()
{
}

BOOL CShapeAppView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CShapeAppView drawing

void CShapeAppView::OnDraw(CDC* pDC)
{
	CShapeAppDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: add draw code for native data here
}

/////////////////////////////////////////////////////////////////////////////
// CShapeAppView printing

BOOL CShapeAppView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CShapeAppView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CShapeAppView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

/////////////////////////////////////////////////////////////////////////////
// CShapeAppView diagnostics

#ifdef _DEBUG
void CShapeAppView::AssertValid() const
{
	CView::AssertValid();
}

void CShapeAppView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CShapeAppDoc* CShapeAppView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CShapeAppDoc)));
	return (CShapeAppDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CShapeAppView message handlers

void CShapeAppView::OnRButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	
	CView::OnRButtonDown(nFlags, point);
}

//读取文件头
void CShapeAppView::ReadHead(CString path)
{
		// 打开坐标文件
       if((m_ShpFile_fp=fopen(path,"rb"))==NULL)
       {
              return;
       }
       // 读取坐标文件头的内容开始
       fread(&file_Head.FileCode,sizeof(int), 1,m_ShpFile_fp);
       file_Head.FileCode = common.OnChangeByteOrder(file_Head.FileCode);
       for(int i=0;i<5;i++)
      fread(&file_Head.Unused,sizeof(int), 1,m_ShpFile_fp);

      fread(&file_Head.FileLength,sizeof(int), 1,m_ShpFile_fp);
      file_Head.FileLength= common.OnChangeByteOrder(file_Head.FileLength);

       fread(&file_Head.Version,      sizeof(int),   1,m_ShpFile_fp);
       fread(&file_Head.ShapeType,    sizeof(int),   1,m_ShpFile_fp);
       fread(&file_Head.Xmin,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Ymin,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Xmax,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Ymax,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Zmin,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Zmax,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Mmin,         sizeof(double),1,m_ShpFile_fp);
       fread(&file_Head.Mmax,         sizeof(double),1,m_ShpFile_fp);
       // 读取坐标文件头的内容结束
}

//读取点文件
void CShapeAppView::ReadPoint()
{
	 // 读取点状目标的实体信息
       int RecordNumber;
       int ContentLength;
       int num=0;
       while((fread(&RecordNumber, sizeof(int),1,m_ShpFile_fp)!=0))
       {
			  num++;
              fread(&ContentLength,sizeof(int),1,m_ShpFile_fp);
              RecordNumber=common.OnChangeByteOrder(RecordNumber);
              ContentLength=common.OnChangeByteOrder(ContentLength);
              int shapeType;
              double x;
			  double y;
              fread(&shapeType, sizeof(int),1,m_ShpFile_fp);
              fread(&x, sizeof(double),1,m_ShpFile_fp);
              fread(&y, sizeof(double),1,m_ShpFile_fp);
			  //添加到点集合
			  Point point(x,y);
			  points.push_back(point);
		}
	   //关闭文件
	   fclose(m_ShpFile_fp);
}

//读取线文件
void CShapeAppView::ReadPolyLine()
{
	 // 读取线状目标的实体信息
       int RecordNumber;
       int ContentLength;
       int num   =0;
       while((fread(&RecordNumber,    sizeof(int),1,m_ShpFile_fp)!=0))
		{
			  PolyLine p_Line;
              fread(&ContentLength,sizeof(int),1,m_ShpFile_fp);
              RecordNumber=common.OnChangeByteOrder(RecordNumber);
              ContentLength=common.OnChangeByteOrder (ContentLength);
              int shapeType;
			  int i;
              fread(&shapeType,    sizeof(int),   1,m_ShpFile_fp);   
			  // 读 Box
              for(i=0;i<4;i++)
                     fread(&p_Line.Box[i], sizeof(double),1,m_ShpFile_fp);   

              fread(&p_Line.NumParts,sizeof(int), 1,m_ShpFile_fp);	// 读 NumParts
              fread(&p_Line.NumPoints,sizeof(int), 1,m_ShpFile_fp);   // 读 Parts 

              p_Line.Parts = new int[p_Line.NumParts];
			  p_Line.Points=new Point[p_Line.NumPoints];

              for(i=0;i<p_Line.NumParts;i++)
				fread(&p_Line.Parts[i],sizeof(int),1,m_ShpFile_fp);
              for(i=0;i<p_Line.NumPoints;i++)
              {
				  Point point;
                  fread(&point.X, sizeof(double),1,m_ShpFile_fp);
                  fread(&point.Y, sizeof(double),1,m_ShpFile_fp);
				  p_Line.Points[i]=point;
              }
			  //存储改线
             polylines.push_back(p_Line);
       }
	   //关闭文件
	   fclose(m_ShpFile_fp);
}

//读取面文件
void CShapeAppView::ReadPolygon()
{
	  // 读取面状目标的实体信息
       int RecordNumber;
       int ContentLength;
       int num   =0;
       while((fread(&RecordNumber,sizeof(int),1,m_ShpFile_fp)!=0))
		{
			  SPolygon p_Gon;
              fread(&ContentLength,sizeof(int),1,m_ShpFile_fp);
              RecordNumber=common.OnChangeByteOrder(RecordNumber);
              ContentLength=common.OnChangeByteOrder (ContentLength);
              int shapeType;
			  int i;
              fread(&shapeType,sizeof(int),   1,m_ShpFile_fp);   
			  // 读 Box
              for(i=0;i<4;i++)
                     fread(&p_Gon.Box[i], sizeof(double),1,m_ShpFile_fp);   

              fread(&p_Gon.NumParts,sizeof(int), 1,m_ShpFile_fp);	// 读 NumParts
              fread(&p_Gon.NumPoints,sizeof(int), 1,m_ShpFile_fp);   // 读 Parts 

              p_Gon.Parts = new int[p_Gon.NumParts];
			  p_Gon.Points=new Point[p_Gon.NumPoints];

              for(i=0;i<p_Gon.NumParts;i++)
				fread(&p_Gon.Parts[i],sizeof(int),1,m_ShpFile_fp);
              for(i=0;i<p_Gon.NumPoints;i++)
              {
				  Point point;
                  fread(&point.X, sizeof(double),1,m_ShpFile_fp);
                  fread(&point.Y, sizeof(double),1,m_ShpFile_fp);
				  p_Gon.Points[i]=point;
              }
			  //存储改线
             polygons.push_back(p_Gon);
       }
	   //关闭文件
	   fclose(m_ShpFile_fp);
}

void CShapeAppView::ReadShape()
{
	points.clear();
	polygons.clear();
	polylines.clear();
	ReadHead(filePath);
	switch(file_Head.ShapeType)
	{
	case 1:
		ReadPoint();
		DrawPoints(points);
		break;
	case 3:
		ReadPolyLine();
		DrawPolyLine(polylines);
		break;
	case 5:
		ReadPolygon();
		DrawPolygon(polygons);
		break;
	default:
		break;
	}
}



//画集合点
void CShapeAppView::DrawPoints(vector<Point> shape_points)
{
	CClientDC dc(this);
	dc.SelectObject(pen);
	//集合遍历
	for(vector<Point>::iterator it  = shape_points.begin(); it != shape_points.end(); it++)  
         {  
            int x=(int)5*it->X;
			int y=(int)5*it->Y;
			dc.SetPixel(x,y,RGB(0,0,255));
        }  
//	dc.DeleteDC();
}
//画集合线
void CShapeAppView::DrawPolyLine(vector<PolyLine> shape_polyline)
{
	CClientDC dc(this);
	dc.SelectObject(pen);
	//集合遍历
	for(vector<PolyLine>::iterator it  = shape_polyline.begin(); it != shape_polyline.end(); it++)  
         {  
           	for (int i = 0; i < it->NumParts; i++)
                        {
                            int startpoint=0;
                            int endpoint=0;
                            CPoint  *point = NULL;
                            if (i == it->NumParts-1)
                            {
                                startpoint = it->Parts[i];
                                endpoint = it->NumPoints;
                            }
                            else
                            {
                                startpoint = it->Parts[i];
                                endpoint = it->Parts[i + 1];
                            }
                            point = new CPoint[endpoint - startpoint];
                            for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                            {
                                Point ps = it->Points[j];
                                point[k].x =(int)5*ps.X;
                                point[k].y =500-(int)5*ps.Y;
                            }
                            dc.Polyline(point,endpoint - startpoint);
							delete []point;
                        }
        }  

}
//画集合面
void CShapeAppView::DrawPolygon(vector<SPolygon> shape_polygon)
{
	CClientDC dc(this);
	dc.SelectObject(pen);
	//集合遍历
	for(vector<SPolygon>::iterator it  = shape_polygon.begin(); it != shape_polygon.end(); it++)  
         {   
				for (int i = 0; i < it->NumParts; i++)
                        {
                            int startpoint=0;
                            int endpoint=0;
                            CPoint  *point = NULL;
                            if (i == it->NumParts-1)
                            {
                                startpoint = it->Parts[i];
                                endpoint = it->NumPoints;
                            }
                            else
                            {
                                startpoint = it->Parts[i];
                                endpoint = it->Parts[i + 1];
                            }
                            point = new CPoint[endpoint - startpoint];
                            for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                            {
                                Point ps = it->Points[j];
                                point[k].x =(int)7*ps.X;
                                point[k].y =500-(int)7*ps.Y;
                            }
                            dc.Polygon(point,endpoint - startpoint);
							delete []point;
                        }

        }  

}

void CShapeAppView::OnFileOpen() 
{
	// TODO: Add your command handler code here
	 // TODO: 在此添加控件通知处理程序代码
    CFileDialog dlg(TRUE, //TRUE为OPEN对话框，FALSE为SAVE AS对话框
        NULL, 
        NULL,
        OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
        (LPCTSTR)_TEXT("shape Files (*.shp)|*.shp|All Files (*.*)|*.*||"),
        NULL);
    if(dlg.DoModal()==IDOK)
    {
		ClearWindow();
        filePath=dlg.GetPathName(); //文件名保存
		ReadShape();
    }
    else
    {
         return;
    }
}

void CShapeAppView::ClearWindow()
{
	CRect rect;
	GetClientRect(&rect);
	int width=rect.Width();
	int height=rect.Height();
	CDC *pDC=GetDC();
	CPen pen(PS_SOLID,1,RGB(255,255,255));
	CBrush brush(RGB(255,255,255));
	pDC->SelectObject(pen);
	pDC->FillRect(CRect(0,0,width,height),&brush);
}
