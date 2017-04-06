// ShapeAppDoc.h : interface of the CShapeAppDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_SHAPEAPPDOC_H__FD462F5E_6217_4C47_BF85_5BE165FDBC6C__INCLUDED_)
#define AFX_SHAPEAPPDOC_H__FD462F5E_6217_4C47_BF85_5BE165FDBC6C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CShapeAppDoc : public CDocument
{
protected: // create from serialization only
	CShapeAppDoc();
	DECLARE_DYNCREATE(CShapeAppDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeAppDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CShapeAppDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CShapeAppDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPEAPPDOC_H__FD462F5E_6217_4C47_BF85_5BE165FDBC6C__INCLUDED_)
