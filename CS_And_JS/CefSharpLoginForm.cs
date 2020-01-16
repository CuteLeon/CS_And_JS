﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.Enums;
using CefSharp.Event;
using CefSharp.Handler;
using CefSharp.Internals;
using CefSharp.ModelBinding;
using CefSharp.SchemeHandler;
using CefSharp.Structs;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;

/* 使用 CefSharp 作为浏览器控件，需要.NetFramework版本为 >=4.5.2 且为x86平台，
 * 需要安装 CefSharp.Winform Nuget包，
 * 貌似不能直接从Stream读取网页内容，需要使用MainWebBrowser.RegisterResourceHandler()方法为网页流绑定地址，饭后访问这个地址，
 */

namespace CS_And_JS
{
    public partial class CefSharpLoginForm : Form
    {
        ChromiumWebBrowser MainWebBrowser;

        public CefSharpLoginForm()
        {
            InitializeComponent();

            //TODO: 允许绑定JS对象
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            //TODO: 此处构造不可使用无参构造程序，否则下面会要求为IBrowser注册资源句柄；
            MainWebBrowser = new ChromiumWebBrowser("");

            //TODO: 为浏览器注册自定义流到指定的网址，并访问以显示自定义的流
            MainWebBrowser.RegisterResourceHandler("http://www.LoginForm.com", GetHTMLStream(), "text/html");
            MainWebBrowser.Load("http://www.LoginForm.com");
            //TODO: 注册JS对象并命名，JS中使用此命名调用对象方法，最后赋值为false是为了忽略方法大小写，否则只能调用小写字母开头的方法
            MainWebBrowser.RegisterJsObject("LoginHost", this, new BindingOptions() {  CamelCaseJavascriptNames = false});
            
            //禁用右键菜单
            MainWebBrowser.MenuHandler = new DisableMenuHandler();
            MainWebBrowser.Parent = this;
            MainWebBrowser.Dock = DockStyle.Fill;
        }

        private MemoryStream GetHTMLStream()
        {
            #region 网页内容
            string HTMLContent = @"
﻿<!DOCTYPE html>
<html >
  <head>
    <meta charset=""UTF-8"">
    <title>Cute Leon.</title>
    
    <style>
      body{
        background: url(data:image/jpeg;base64,/9j/4QpqRXhpZgAATU0AKgAAAAgADAEAAAMAAAABAbQAAAEBAAMAAAABAy8AAAECAAMAAAADAAAAngEGAAMAAAABAAIAAAESAAMAAAABAAEAAAEVAAMAAAABAAMAAAEaAAUAAAABAAAApAEbAAUAAAABAAAArAEoAAMAAAABAAIAAAExAAIAAAAdAAAAtAEyAAIAAAAUAAAA0YdpAAQAAAABAAAA6AAAASAACAAIAAgACvyAAAAnEAAK/IAAACcQQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKQAyMDE4OjA3OjAxIDE3OjU5OjA3AAAAAAAEkAAABwAAAAQwMjIxoAEAAwAAAAH//wAAoAIABAAAAAEAAACgoAMABAAAAAEAAAErAAAAAAAAAAYBAwADAAAAAQAGAAABGgAFAAAAAQAAAW4BGwAFAAAAAQAAAXYBKAADAAAAAQACAAACAQAEAAAAAQAAAX4CAgAEAAAAAQAACOQAAAAAAAAASAAAAAEAAABIAAAAAf/Y/+0ADEFkb2JlX0NNAAL/7gAOQWRvYmUAZIAAAAAB/9sAhAAMCAgICQgMCQkMEQsKCxEVDwwMDxUYExMVExMYEQwMDAwMDBEMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMAQ0LCw0ODRAODhAUDg4OFBQODg4OFBEMDAwMDBERDAwMDAwMEQwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCACgAFYDASIAAhEBAxEB/90ABAAG/8QBPwAAAQUBAQEBAQEAAAAAAAAAAwABAgQFBgcICQoLAQABBQEBAQEBAQAAAAAAAAABAAIDBAUGBwgJCgsQAAEEAQMCBAIFBwYIBQMMMwEAAhEDBCESMQVBUWETInGBMgYUkaGxQiMkFVLBYjM0coLRQwclklPw4fFjczUWorKDJkSTVGRFwqN0NhfSVeJl8rOEw9N14/NGJ5SkhbSVxNTk9KW1xdXl9VZmdoaWprbG1ub2N0dXZ3eHl6e3x9fn9xEAAgIBAgQEAwQFBgcHBgU1AQACEQMhMRIEQVFhcSITBTKBkRShsUIjwVLR8DMkYuFygpJDUxVjczTxJQYWorKDByY1wtJEk1SjF2RFVTZ0ZeLys4TD03Xj80aUpIW0lcTU5PSltcXV5fVWZnaGlqa2xtbm9ic3R1dnd4eXp7fH/9oADAMBAAIRAxEAPwD0lJJYf1i68zALcGo215du14tYzRjJ3bmmz22eo5npv2f4P1P0ldqqk0LOzYjEyPCHcSVbp2czPxGZLBtJ9trBw2wfzjAfzmf6N3+jVlJBBBoqSSSSUpJJJJSkkkklKSSSSU//0PSSAQQRIOhB4IKpZVFGVm0Y2acY4hqLMbHewtuOQ39I6zFyG2N9jMVjvUppq9X8/wBT01dQTXGczJeQW01ObW2Ndzj+kO6P3NighqeGrB6MsjQuyK7I+n9N6f02o4/T6vQpbDPT3OcAWDhvqE/vf21aTm1lh9v0mhu/+0Pbr+emQnGpEfkmJsDyUkkkmpUoW3NqaXO0De7jtb/nqaDlVteayRucHe1kTJHun+s1ybkJECRuExAJ1U3JO8tsa1sDd7Xbjzt+hARgZAI4Oo7flWX0+p1jWmxxJbbpZJcdfc5gd/W/f/8APy1edUgJxMoz3ia/lSiYkCUdiskkknIf/9H0lJJJVWdSSSSSlJKrldUw8QlljnOsbtmutpe4F382Hx7a/U/M9RyLj5VGTWH1OBJaHOZILmzw2zY5+1yNFVGrSob7mU2MdZowwA6fzp+hH730XoiRDXCHAOHgQCPxTZAkaGj9uyhXVpPONh1llNr7S94tMgE7jwGtZX7vUd+ZtVprrXXEgAY4bpIIe589tfbW1n8j3qe1u4P2jeBAfA3AHsHfSSS9ZlKUpXdbDh2/xk+kRAA77+KkkkkUP//S9JSSSVVnWe8MbuIJEiY1IH70D6W1NZbVTWbbXhlbdXPJ0/1d+apEgalQsxMPILW5TRc9ri5jdQJ8dNrXf106MbQTQcq/AGTijPwHbcm5xdWbg4ML3O9N9ljK99zatm5/0/8Atv8AwehhilhtpqYGNY4EGZfYC0N+03DazY59rbq2fv1U+rX+jsVixtbjt2w1mgEaafR/stS8k6ZiBw1r1PiiOvq11+ylJJJKNcpJJJJSkkkklP8A/9P0lJJJVWdSR17n4dj8UkklKSSSSUpVuoDIsxbMfGa511zSzc07dgd7XWepLdrv6n0P5xWU7XbZ0DgeR8ERV6o16OJXn9Tqtx8TMNVJbZXXbaGu1A/Mccj2fpWs/nqfU/62teq6q9pONbXb2DmuD2gxPu2O/tKfUMGnPxX0WBsWCC4gOMfyT/V/PVfp/ScfplFgq+nbtDnARo0uc1n/AIJapJQ/BaJXTFmE9mM+puVd61pD3ZJcXO3jb9Ct0sqp9n8xX+Z/wv6VJWklHa6n/9T0lJJM6NpkSBrHPCqs7IAzESeY7woWV2ta1xMuaSZ1HPbaFVbn0B+Q2t25mE14y62te+0WbWZFOzaP0u/HL37K/wB+uutGZvu/SFx2WNY8EgtcCR7mbX+9v9S36CinLijUQeI7Uf8ACtQIvcfy9LOtzny8tLAdA13On5ymkkpIihV34+aipI8JJIqX0LGtjvJB81Gv217I2jcSB4D/AM6TpJxkTfitEQK8FJJJJq5//9X0lJJP59lVZ2FdTa621hznBogOc47v84bU7Wta0NaIA+f4lOkgAB0USSpJJKY1iY1hFS4DySA2IjnzE8D3NTSR9Jpb7tuumv8AJ/eQrb85xb9kbSXm2v1X2uIYKZ/TMq9OXOyWs3+i136P/SJMvzLMq8WtqOGdjsOysu9QDbF/2uuxrW1/pfbR6X/XFNLGAPJYJEnbdKkkkoV6kkkklP8A/9bsq87reRVcxuOzHvotNW7cALHR7a6vV9RrXfyt1n/nxD6VZkO6tZ9sususFbmVB5gMILXWs9BsV7nM/wAJ/IWrdQ+0sc54eWODh6jQ6CP8JWPbssVNnQ6nbnZF9z7N5dW9jzW5sne33sO572OP9T/g1ETCtGWjp+LpJJ0yiXKSSSSUo686/HVKBMxqeT3SSSUpCvy8ejSx43mNtQI3uJ+gyuv6Tnv/ADEVCbiUNutua2H5EerOoJboHtafoPRFXqg30aH27rPrf0VpZ/ogJj+S7I9T22bv0f8AN/8AW0lp/pN+8P0PLdoj7/pJKS8f8gii/wD/1/SUkklVZ1JnvYxu57g1shsnQS4hjB/ae7anVTqmZgYeI+7qDXvx2GuW1tc90vdsZFdM2u937qMYmRodUGQGp0DbSVOvqmLlXDCreMbqFoc9lbvfLGOa26yv/BvczerzmGshpO6Roe5jxRMCBfRAkDp1YpJJJq5SSSSSlJJJJKf/0PSUkklVZ1JtoL2uPLZHeNRG1/7zE6SING0EWKa1VVbcUWVYbK8kEltRdAlrpG6/Zv2P+m3c3/raPfdc91JbUS3U2Q5uhj6PLd38lSSRMyt9sdypJJJNXqSSSSUpJJJJT//Z/+0R9lBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAPHAFaAAMbJUccAgAAAgAAADhCSU0EJQAAAAAAEM3P+n2ox74JBXB2rq8Fw044QklNBDoAAAAAANcAAAAQAAAAAQAAAAAAC3ByaW50T3V0cHV0AAAABQAAAABQc3RTYm9vbAEAAAAASW50ZWVudW0AAAAASW50ZQAAAABJbWcgAAAAD3ByaW50U2l4dGVlbkJpdGJvb2wAAAAAC3ByaW50ZXJOYW1lVEVYVAAAAAEAAAAAAA9wcmludFByb29mU2V0dXBPYmpjAAAABWghaDeLvn9uAAAAAAAKcHJvb2ZTZXR1cAAAAAEAAAAAQmx0bmVudW0AAAAMYnVpbHRpblByb29mAAAACXByb29mQ01ZSwA4QklNBDsAAAAAAi0AAAAQAAAAAQAAAAAAEnByaW50T3V0cHV0T3B0aW9ucwAAABcAAAAAQ3B0bmJvb2wAAAAAAENsYnJib29sAAAAAABSZ3NNYm9vbAAAAAAAQ3JuQ2Jvb2wAAAAAAENudENib29sAAAAAABMYmxzYm9vbAAAAAAATmd0dmJvb2wAAAAAAEVtbERib29sAAAAAABJbnRyYm9vbAAAAAAAQmNrZ09iamMAAAABAAAAAAAAUkdCQwAAAAMAAAAAUmQgIGRvdWJAb+AAAAAAAAAAAABHcm4gZG91YkBv4AAAAAAAAAAAAEJsICBkb3ViQG/gAAAAAAAAAAAAQnJkVFVudEYjUmx0AAAAAAAAAAAAAAAAQmxkIFVudEYjUmx0AAAAAAAAAAAAAAAAUnNsdFVudEYjUHhsQFIAAAAAAAAAAAAKdmVjdG9yRGF0YWJvb2wBAAAAAFBnUHNlbnVtAAAAAFBnUHMAAAAAUGdQQwAAAABMZWZ0VW50RiNSbHQAAAAAAAAAAAAAAABUb3AgVW50RiNSbHQAAAAAAAAAAAAAAABTY2wgVW50RiNQcmNAWQAAAAAAAAAAABBjcm9wV2hlblByaW50aW5nYm9vbAAAAAAOY3JvcFJlY3RCb3R0b21sb25nAAAAAAAAAAxjcm9wUmVjdExlZnRsb25nAAAAAAAAAA1jcm9wUmVjdFJpZ2h0bG9uZwAAAAAAAAALY3JvcFJlY3RUb3Bsb25nAAAAAAA4QklNA+0AAAAAABAASAAAAAEAAQBIAAAAAQABOEJJTQQmAAAAAAAOAAAAAAAAAAAAAD+AAAA4QklNBA0AAAAAAAQAAAAeOEJJTQQZAAAAAAAEAAAAHjhCSU0D8wAAAAAACQAAAAAAAAAAAQA4QklNJxAAAAAAAAoAAQAAAAAAAAABOEJJTQP1AAAAAABIAC9mZgABAGxmZgAGAAAAAAABAC9mZgABAKGZmgAGAAAAAAABADIAAAABAFoAAAAGAAAAAAABADUAAAABAC0AAAAGAAAAAAABOEJJTQP4AAAAAABwAAD/////////////////////////////A+gAAAAA/////////////////////////////wPoAAAAAP////////////////////////////8D6AAAAAD/////////////////////////////A+gAADhCSU0ECAAAAAAAEAAAAAEAAAJAAAACQAAAAAA4QklNBB4AAAAAAAQAAAAAOEJJTQQaAAAAAANPAAAABgAAAAAAAAAAAAABKwAAAKAAAAANADUALQAxADMAMAA1ADAARwAzADMANAAzADkAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAKAAAAErAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAEAAAAAAABudWxsAAAAAgAAAAZib3VuZHNPYmpjAAAAAQAAAAAAAFJjdDEAAAAEAAAAAFRvcCBsb25nAAAAAAAAAABMZWZ0bG9uZwAAAAAAAAAAQnRvbWxvbmcAAAErAAAAAFJnaHRsb25nAAAAoAAAAAZzbGljZXNWbExzAAAAAU9iamMAAAABAAAAAAAFc2xpY2UAAAASAAAAB3NsaWNlSURsb25nAAAAAAAAAAdncm91cElEbG9uZwAAAAAAAAAGb3JpZ2luZW51bQAAAAxFU2xpY2VPcmlnaW4AAAANYXV0b0dlbmVyYXRlZAAAAABUeXBlZW51bQAAAApFU2xpY2VUeXBlAAAAAEltZyAAAAAGYm91bmRzT2JqYwAAAAEAAAAAAABSY3QxAAAABAAAAABUb3AgbG9uZwAAAAAAAAAATGVmdGxvbmcAAAAAAAAAAEJ0b21sb25nAAABKwAAAABSZ2h0bG9uZwAAAKAAAAADdXJsVEVYVAAAAAEAAAAAAABudWxsVEVYVAAAAAEAAAAAAABNc2dlVEVYVAAAAAEAAAAAAAZhbHRUYWdURVhU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aABvAHAAAAASAEEAZABvAGIAZQAgAFAAaABvAHQAbwBzAGgAbwBwACAAQwBDAAAAAQA4QklNBAYAAAAAAAcABAAAAAEBAP/hDblodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTQyIDc5LjE2MDkyNCwgMjAxNy8wNy8xMy0wMTowNjozOSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDozOThkOWFkZi04OGY1LTRmNDktOGRjYi00ZTYwZWM5ZjlkMGYiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NGYyNmU5YWYtZGU5Zi1lZTQ1LWIzODgtMzk0YjRiMjRiZDE2IiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9IjkzNzYwODM0MDZCOTlENTRCOEE1QThDRDM5RDc0MjVEIiBkYzpmb3JtYXQ9ImltYWdlL2pwZWciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSIiIHhtcDpDcmVhdGVEYXRlPSIyMDE1LTA3LTI3VDEwOjQyOjE2KzA4OjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAxOC0wNy0wMVQxNzo1OTowNyswODowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAxOC0wNy0wMVQxNzo1OTowNyswODowMCI+IDx4bXBNTTpIaXN0b3J5PiA8cmRmOlNlcT4gPHJkZjpsaSBzdEV2dDphY3Rpb249InNhdmVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjE3MGEzMWY2LWE4MTctNjc0Ni1hMDM0LWZkMTM5YzI0ZmQ3OSIgc3RFdnQ6d2hlbj0iMjAxOC0wNy0wMVQxNzo1OTowNyswODowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6NGYyNmU5YWYtZGU5Zi1lZTQ1LWIzODgtMzk0YjRiMjRiZDE2IiBzdEV2dDp3aGVuPSIyMDE4LTA3LTAxVDE3OjU5OjA3KzA4OjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDwvcmRmOlNlcT4gPC94bXBNTTpIaXN0b3J5PiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8P3hwYWNrZXQgZW5kPSJ3Ij8+/+4ADkFkb2JlAGQAAAAA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)
      }
      h1{
        font-size: 40px;
        font-family:""Microsoft YaHei"";
        color: #fff;
        text-shadow: 0px 0px 50px #fa6c9f;
      }
      /* 按钮文本区域样式 */
      span {
        font:bold;
        font-family:""Microsoft YaHei"";
        display: inline-block;
        padding: 18px 60px;
        border-radius: 50em;
        position: relative;
        z-index: 2;
        border: 5px solid #fff;
      }
      /* 按钮填充和阴影样式 */
      .bg-gradient span,
      .bg-gradient:before {
        background: #fa6c9f;
        background: -webkit-linear-gradient(left, #fa6c9f 0%, #ffe140 80%, #ffe140 100%);
        background: linear-gradient(to right, #fa6c9f 0%, #ffe140 80%, #ffe140 100%);
      }
      .wrapper {
        position: relative;
        text-align: center;
      }
      
      .fancy-button {
        display: inline-block;
        margin: 30px;
        font-family: 'Montserrat', Helvetica, Arial, sans-serif;
        font-size: 30px;
        letter-spacing: 0.03em;
        text-transform: uppercase;
        color: #fff;
        position: relative;
      }
      
      .fancy-button:before {
        content: '';
        display: inline-block;
        height: 100px;
        width: 120%;
        position: absolute;
        bottom: -5px;
        left: -10%;
        right: 30px;
        z-index: -1;
        opacity: 0.5;
        bottom: -7px;
        -webkit-filter: blur(20px) brightness(1);
                filter: blur(20px) brightness(1);
        -webkit-transform-style: preserve-3d;
                transform-style: preserve-3d;
        -webkit-transition: all 0.3s ease-out;
        transition: all 0.3s ease-out;
      }
      
      .fancy-button:hover:before {
        bottom: -7px;
        opacity: 1;
        -webkit-filter: blur(30px);
                filter: blur(30px);
      }

      input{
	    transition:all 0.30s ease-in-out;
	    -webkit-transition: all 0.30s ease-in-out;
	    -moz-transition: all 0.30s ease-in-out;
	    height:30px;
        font-size:15px;
	    border:#35a5e5 1px solid;
	    border-radius:3px;
	    outline:none;
      }
      input:focus{
	    box-shadow:0 0 25px rgba(81, 203, 238, 1);
	    -webkit-box-shadow:0 0 25px rgba(81, 203, 238, 1);
	    -moz-box-shadow:0 0 25px rgba(81, 203, 238, 1);
      }
    </style>
  </head>

  <body>
    <div class=""wrapper"" style=""top: 20%;"">
      <h1>欢迎访问 Cute Leon.</h1>
      <input type=""text"" placeholder=""用户名"" id=""UserNameTextBox""/>
      <input type=""password"" placeholder=""密码"" id=""PasswordTextBox""/>
    </div>
    <div class=""wrapper"" style=""top: 40%;"">
      <a onclick=""CheckLogin();"" class=""fancy-button bg-gradient""><span id=""ButtonLabel"">   Welcome !   </span></a>
    </div>
  </body>

  <script type=""text/javascript"">
    function CheckLogin()
    {
        var UserName=document.getElementById(""UserNameTextBox"").value;
        var Password=document.getElementById(""PasswordTextBox"").value;
        LoginHost.CheckLogin(UserName, Password);
    }
    function LoginSuccessfully(message)
    {
        document.getElementById(""ButtonLabel"").innerText=message;
        window.setTimeout(GotoLeonNote, 1000); 
        }
        function GotoLeonNote()
        {
            document.getElementById(""ButtonLabel"").innerText=""正在跳转..."";
            window.location.href=""http://118.25.131.65:1304"";
        }
  </script>
</html>";
            #endregion

            return new MemoryStream(Encoding.UTF8.GetBytes(HTMLContent));
        }

        //TODO : 被 JS 调用的方法必须为 Public ;
        public void CheckLogin(string UserName, string Password)
        {
            if (UserName == "123" && Password == "456")
            {
                //TODO : Client 调用 Browser 代码；
                MainWebBrowser.ExecuteScriptAsync("LoginSuccessfully", new String[] { "登录成功，欢迎访问！" });
                /* 或使用 EvaluateScriptAsync 方法可以获取JS返回结果；
                var task = MainWebBrowser.EvaluateScriptAsync("LoginSuccessfully", "123");
                => task.Result
                 */
            }
            else
            {
                MessageBox.Show("用户名或密码输入错误，请重新输入！");
            }
        }

    }

    /// <summary>
    /// 禁用菜单事件
    /// </summary>DisableMenuHandler
    public class  : IContextMenuHandler
    {
        void CefSharp.IContextMenuHandler.OnBeforeContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model) => model.Clear();
        bool CefSharp.IContextMenuHandler.OnContextMenuCommand(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.CefMenuCommand commandId, CefSharp.CefEventFlags eventFlags) => false;
        void CefSharp.IContextMenuHandler.OnContextMenuDismissed(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame) {}
        bool CefSharp.IContextMenuHandler.RunContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model, CefSharp.IRunContextMenuCallback callback) => false;
    }
}
