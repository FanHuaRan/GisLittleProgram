// ShapeAppDoc.cpp : implementation of the CShapeAppDoc class
//

#include "stdafx.h"
#include "ShapeApp.h"

#include "ShapeAppDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CShapeAppDoc

IMPLEMENT_DYNCREATE(CShapeAppDoc, CDocument)

BEGIN_MESSAGE_MAP(CShapeAppDoc, CDocument)
	//{{AFX_MSG_MAP(CShapeAppDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CShapeAppDoc construction/destruction

CShapeAppDoc::CShapeAppDoc()
{
	// TODO: add one-time construction code here

}

CShapeAppDoc::~CShapeAppDoc()
{
}

BOOL CShapeAppDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CShapeAppDoc serialization

void CShapeAppDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CShapeAppDoc diagnostics

#ifdef _DEBUG
void CShapeAppDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CShapeAppDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CShapeAppDoc commands
