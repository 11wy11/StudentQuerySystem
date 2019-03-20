using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Whu_Navigation
{
    class FileManager
    {
        public static string m_sMxdPath = "";
        //e.g. c:\temp
        public static string GetMxdPath()
        {
            return m_sMxdPath;
        }
        //e.g. c:\temp\WSData
        public static string GetWorkspacePath()
        {
            string sPath = m_sMxdPath + "\\Data";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            return sPath;
        }
    }
    class FileInfo
    {
        string sFullName;//全路径名c:\temp\abc.exe
        string sDirectory;//路径c:\temp
        string sPathFileName;//路径文件名c:\temp\abc(无后缀)
        string sFileName;//文件名abc.exe
        string sFileNameNoExt;//文件名abc
        string sExt;//后缀 .exe(含有点)
        string sRoot;//跟目录c:\\
        public FileInfo(string strFullName)
        {
            sFullName = strFullName;
            //string s = Path.GetFullPath(sFullName);//c:\temp\abc.exe
            sDirectory = Path.GetDirectoryName(strFullName);//c:\temp
            sExt = Path.GetExtension(strFullName);//.exe
            sFileName = Path.GetFileName(strFullName);//abc.exe
            sFileNameNoExt = Path.GetFileNameWithoutExtension(strFullName);//abc
            sRoot = Path.GetPathRoot(strFullName);//c:\\  
            sPathFileName = sDirectory + "\\" + sFileNameNoExt;//路径文件名c:\temp\abc(无后缀)
        }
        public string GetFullName()//c:\temp\abc.exe
        {
            return sFullName;
        }
        public string GetDirectory()//c:\temp
        {
            return sDirectory;
        }
        public string GetPathFileName()//路径文件名c:\temp\abc(无后缀)
        {
            return sPathFileName;
        }
        public string GetFileName()//abc.exe
        {
            return sFileName;
        }
        public string GetFileNameNoExt()//abc
        {
            return sFileNameNoExt;
        }
        public string GetExt()//.ext
        {
            return sExt;
        }
        public string GetRoot()//c:\
        {
            return sRoot;
        }
    }
   
}
