using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Whu_Navigation.Classes
{
    /// <summary>
    /// Summary description for OpenTool.
    /// </summary>
    [Guid("d0c8d138-1022-4ff2-9995-0769a237c40b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Whu_Navigation.Classes.OpenTool")]
    public sealed class OpenTool : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IMapControl3 pMapControl;
        public OpenTool()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "打开地图文档"; //localizable text 
            base.m_caption = "打开地图文档";  //localizable text 
            base.m_message = "打开地图";  //localizable text
            base.m_toolTip = "打开地图文档";  //localizable text
            base.m_name = "OpenTool";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                //string bitmapResourceName = GetType().Name + ".bmp";
                string bitmapResourceName = "OpenTool.bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                //base.m_bitmap = new System.Drawing.Bitmap(Application.StartupPath + @"/icon/OpenTool.bmp");  
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
            if (hook is IToolbarControl)
            {
                IToolbarControl toolbarControl = (IToolbarControl)hook;
                pMapControl = (IMapControl3)toolbarControl.Buddy;
            }
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add OpenTool.OnClick implementation
            OpenFileDialog OpenMXD = new OpenFileDialog(); //可实例化类
            OpenMXD.Title = "打开地图"; // OpenFileDialog类的属性Title
            OpenMXD.Multiselect = false;
            OpenMXD.Filter = "Map Documents (*.mxd)|*.mxd";
            if (OpenMXD.ShowDialog() == DialogResult.OK) //ShowDialog是类的方法
            {
                string docName = OpenMXD.FileName;
                IMapDocument pMapDoc = new MapDocumentClass();
                if (pMapDoc.get_IsPresent(docName) && !pMapDoc.get_IsPasswordProtected(docName))
                {
                    pMapControl.LoadMxFile(OpenMXD.FileName, null, null);
                    pMapControl.ActiveView.Refresh();
                    pMapDoc.Close();
                }
            } 
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.FocusMap == null)
                    return false;
                return true;
            }
        }  
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add OpenTool.OnMouseDown implementation
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add OpenTool.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add OpenTool.OnMouseUp implementation
        }
        #endregion
    }
}
