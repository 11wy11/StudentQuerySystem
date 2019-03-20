using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Whu_Navigation
{
    /// <summary>
    /// Summary description for AddDataTool.
    /// </summary>
    [Guid("4285954c-2db1-4094-ac66-c8ae7cce4616")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Whu_Navigation.自定义工具栏.AddDataTool")]
    public sealed class AddDateTool : BaseTool
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

        //private IHookHelper m_hookHelper = null;
        private IHookHelper m_hookHelper = new HookHelperClass();
        public AddDateTool()
        {
            //
            // TODO: Define values for the public properties
            //
            //base.m_category = ""; //localizable text 
            //base.m_caption = "";  //localizable text 
            //base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            //base.m_toolTip = "";  //localizable text
            //base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")         
            base.m_category = "CustomCommands";
            base.m_caption = "添加日期";
            base.m_message = "在页面布局中增加一个日期元素";
            base.m_toolTip = "添加日期";
            base.m_name = "CustomCommands_Add Date";
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
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
        }
        public override bool Enabled
        {
            get
            {
                // 设置使能属性
                if (m_hookHelper.ActiveView != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add AddDataTool.OnClick implementation
             
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDataTool.OnMouseDown implementation
            base.OnMouseDown(Button, Shift, X, Y);

            // 获取活动视图
            IActiveView activeView = m_hookHelper.ActiveView;

            // 创建新的文本元素
            ITextElement textElement = new TextElementClass();

            // 创建文本符号
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Size = 25;

            // 设置文本元素属性
            textElement.Symbol = textSymbol;
            textElement.Text = DateTime.Now.ToShortDateString();

            // 对IElementQI
            IElement element = (IElement)textElement;

            // 创建页点
            IPoint point = new PointClass();
            point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

            // 设置元素图形
            element.Geometry = point;

            // 增加元素到图形绘制容器
            activeView.GraphicsContainer.AddElement(element, 0);

            // 刷新图形
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDataTool.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDataTool.OnMouseUp implementation
        }
        #endregion
    }
}
