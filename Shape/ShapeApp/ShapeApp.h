// ShapeApp.h : main header file for the SHAPEAPP application
//

#if !defined(AFX_SHAPEAPP_H__307953CC_6CFF_47F7_BE9F_5FB44D31ABC4__INCLUDED_)
#define AFX_SHAPEAPP_H__307953CC_6CFF_47F7_BE9F_5FB44D31ABC4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CShapeAppApp:
// See ShapeApp.cpp for the implementation of this class
//

class CShapeAppApp : public CWinApp
{
public:
	CShapeAppApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeAppApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CShapeAppApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPEAPP_H__307953CC_6CFF_47F7_BE9F_5FB44D31ABC4__INCLUDED_)
