/* 
* Custom fullscreen and Borderless window script by Martijn Dekker (Pixelstudio) 
* For questions pls contact met at martijn.pixelstudio@gmail.com 
* version 0.1 
* 
*/  

using System;  
using System.Collections;  
using System.Runtime.InteropServices;
using System.Diagnostics;  
using UnityEngine;  

public class NoWindowBorder : MonoBehaviour  
{  
	public Rect screenPosition;  

	[DllImport("user32.dll")]  
	static extern IntPtr SetWindowLong (IntPtr hwnd,int  _nIndex ,int  dwNewLong);  
	
	[DllImport("user32.dll")]
	static extern bool SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);  

	[DllImport("user32.dll")]  
	static extern IntPtr GetForegroundWindow ();  

	[DllImport("user32.dll")]
	static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

	// not used rigth now
	//const uint SWP_NOMOVE = 0x2;
	//const uint SWP_NOSIZE = 1;
	//const uint SWP_NOZORDER = 0x4;
	//const uint SWP_HIDEWINDOW = 0x0080;  

	const uint SWP_SHOWWINDOW = 0x0040;  
	const int GWL_EXSTYLE = -20;  
	const int GWL_STYLE = -16;  
	const int WS_BORDER = 1;  
	const int WS_EX_LAYERED = 0x80000;  
	public const int LWA_ALPHA = 0x2;
	public const int LWA_COLORKEY = 0x1;
	
	void Start ()  
	{  	
		SetWindowLong(GetForegroundWindow (), GWL_EXSTYLE, WS_EX_LAYERED);  
		SetWindowLong(GetForegroundWindow (), GWL_STYLE, WS_BORDER);  
		
		SetLayeredWindowAttributes(GetForegroundWindow (), 0, 0, LWA_COLORKEY);
	
		bool result = SetWindowPos (GetForegroundWindow (), 0,(int)screenPosition.x,(int)screenPosition.y, (int)screenPosition.width,(int) screenPosition.height, SWP_SHOWWINDOW);  
	
		Destroy(this.gameObject);
	}  
}
