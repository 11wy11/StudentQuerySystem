using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.SystemUI;
using Whu_Navigation;
using Whu_Navigation.Classes;
using ArcEngineDemos.Forms;
using Whu_Navigation.Forms;
namespace Whu_Navigation
{
    public partial class MainForm : Form
    {
        bool m_bDocModified = false;
        private IMapDocument m_pMapDocument = null;
        public MainForm()
        {
             
            InitializeComponent();
            
        }
        private string pMapUnits;
        private ILayer pGlobalFeatureLayer = null;    //获取当前图层
        private Pan pan = null;
        public System.Windows.Forms.ToolStripMenuItem m_LableLayer;
        /// <summary>
        /// 打开地图文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openMxd_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenMXD = new OpenFileDialog(); //可实例化类
            // Gets or sets the file dialog box title. (Inherited from FileDialog.)
            OpenMXD.Title = "打开地图"; // OpenFileDialog类的属性Title
            // Gets or sets the initial directory displayed by the file dialog box. 
            OpenMXD.InitialDirectory = "E:\\大实习";
            // Gets or sets the current file name filter string ,Save as file type
            OpenMXD.Filter = "Map Documents (*.mxd)|*.mxd";
            if (OpenMXD.ShowDialog() == DialogResult.OK) //ShowDialog是类的方法
            {
                //FileName:Gets or sets a string containing the file name selected in the file dialog box
                string MxdPath = OpenMXD.FileName;
                axMapControl1.LoadMxFile(MxdPath); //IMapControl2的方法
            }
            //RefreshcbLayer_right();

        }
        /// <summary>
        /// 打开Shape文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAddSHP_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenShpFile = new OpenFileDialog();
            OpenShpFile.Title = "打开Shape文件";
            OpenShpFile.InitialDirectory = "E:\\大实习";
            OpenShpFile.Filter = "Shape文件(*.shp)|*.shp";
            if (OpenShpFile.ShowDialog() == DialogResult.OK)
            {
                string ShapPath = OpenShpFile.FileName;
                int Position = ShapPath.LastIndexOf("\\"); //利用"\\"将文件路径分成两部分
                string FilePath = ShapPath.Substring(0, Position);
                string ShpName = ShapPath.Substring(Position + 1);
                axMapControl1.AddShapeFile(FilePath, ShpName);
            }
            //RefreshcbLayer_right();

        }
        /// <summary>
        /// 打开Shape文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAddLyr_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenLyrFile = new OpenFileDialog();
            OpenLyrFile.Title = "打开Lyr";
            OpenLyrFile.InitialDirectory = "E:\\大实习";
            OpenLyrFile.Filter = "lyr文件(*.lyr)|*.lyr";
            if (OpenLyrFile.ShowDialog() == DialogResult.OK)
            {
                string LayPath = OpenLyrFile.FileName;
                axMapControl1.AddLayerFromFile(LayPath);
            }
            m_bDocModified = true;
            //RefreshcbLayer_right();

        }
        
        /// 地图替换时将图层同时添加到axMapControl2上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                axMapControl2.Map = new MapClass();
                for (int i = 1; i <= axMapControl1.Map.LayerCount; i++)
                {
                    axMapControl2.AddLayer(axMapControl1.get_Layer(axMapControl1.Map.LayerCount - i));
                }
                //axMapControl2.Extent = axMapControl1.Extent;
                this.axMapControl2.Extent = this.axMapControl2.ActiveView.Extent;
                axMapControl2.Refresh();
            }
            #region 坐标单位替换
            esriUnits mapUnits = axMapControl1.MapUnits;
            switch (mapUnits)
            {
                case esriUnits.esriCentimeters:
                    pMapUnits = "Centimeters";
                    break;
                case esriUnits.esriDecimalDegrees:
                    pMapUnits = "Decimal Degrees";
                    break;
                case esriUnits.esriDecimeters:
                    pMapUnits = "Decimeters";
                    break;
                case esriUnits.esriFeet:
                    pMapUnits = "Feet";
                    break;
                case esriUnits.esriInches:
                    pMapUnits = "Inches";
                    break;
                case esriUnits.esriKilometers:
                    pMapUnits = "Kilometers";
                    break;
                case esriUnits.esriMeters:
                    pMapUnits = "Meters";
                    break;
                case esriUnits.esriMiles:
                    pMapUnits = "Miles";
                    break;
                case esriUnits.esriMillimeters:
                    pMapUnits = "Millimeters";
                    break;
                case esriUnits.esriNauticalMiles:
                    pMapUnits = "NauticalMiles";
                    break;
                case esriUnits.esriPoints:
                    pMapUnits = "Points";
                    break;
                case esriUnits.esriUnknownUnits:
                    pMapUnits = "Unknown";
                    break;
                case esriUnits.esriYards:
                    pMapUnits = "Yards";
                    break;
            }
            #endregion
        }
        /// <summary>
        /// 当axMapControl1的外接矩形改变时，更新鹰眼并绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            // 得到新范围 
            IEnvelope pEnv = (IEnvelope)e.newEnvelope;
            IGraphicsContainer pGra = axMapControl2.Map as IGraphicsContainer;
            IActiveView pAv = pGra as IActiveView;
            // 在绘制前，清除 axMapControl2 中的任何图形元素 
            pGra.DeleteAllElements();
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pEle = pRectangleEle as IElement;
            pEle.Geometry = pEnv;
            // 设置鹰眼图中的红线框 
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            // 产生一个线符号对象 
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 2;
            pOutline.Color = pColor;
            // 设置颜色属性 
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;
            // 设置填充符号的属性 
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGra.AddElement((IElement)pFillShapeEle, 0);
            // 刷新 
            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null); 
  
        }
      /// <summary>
      /// 鹰眼点击事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void axMapControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControl2.Map.LayerCount > 0)
            {
                if (e.button == 1)
                {
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.mapX, e.mapY);
                    axMapControl1.CenterAt(pPoint);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                else if (e.button == 2)
                {
                    IEnvelope pEnv = axMapControl2.TrackRectangle();
                    axMapControl1.Extent = pEnv;
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        private void axMapControl2_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)
            {
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                axMapControl1.CenterAt(pPoint);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
         
        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttribute Ft = new FrmAttribute(pGlobalFeatureLayer as IFeatureLayer);
            Ft.Show();
        }

        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                esriTOCControlItem pItem = new esriTOCControlItem();
                pGlobalFeatureLayer = new FeatureLayerClass();
                IBasicMap pBasicMap = new MapClass();
                object pOther = new object();
                object pIndex = new object();
                // Returns the item in the TOCControl at the specified coordinates.
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pBasicMap, ref pGlobalFeatureLayer, ref pOther, ref pIndex);
               
            }//TOCControl类的ITOCControl接口的HitTest方法
            if (e.button == 2)
            {
                TOCMenu.Show(axTOCControl1, e.x, e.y);
            }

        }
       
  
        private void SpatialAnalysis_Click(object sender, EventArgs e)
        {
           //IFeatureLayer pFeatureLayer = this.axMapControl1.get_Layer(0) as IFeatureLayer;
            ////QI至IFeatureSelection
            //IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
            ////创建过滤器
            //ISpatialFilter pSFilter = new SpatialFilterClass();
            //IGeometry geometry= axMapControl1.TrackPolygon();
            //pSFilter.Geometry=geometry;
            //pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            ////pFCursor = pCityFClass.Search(pSFilter, True);

            //Geoprocessor pGP = new Geoprocessor();    //初始化Geoprocessor  pGP.OverwriteOutput = true;                       //允许运算结果覆盖现有文件
            //ESRI.ArcGIS.AnalysisTools.Buffer pBuffer = new ESRI.ArcGIS.AnalysisTools.Buffer();  //定义Buffer工具
            //pBuffer.in_features = pGlobalFeatureLayer;
            ////输入对象，既可是IFeatureLayer对象，也可是完整文件路径如“D:\\data.shp”  
            //pBuffer.oute_class = pBuffer;
            ////输出对象，一般是包含输出文件名的完整文件路径，如“D:\\buffer.shp”
            //pBuffer.buffer_distance_or_field = "500 Meters";
            ////设置缓冲区的大小，即可是带单位的具体数值，如500 Meters ，0.1 Decimal Degrees；也可是输入图层中的某个字段，如“BufferLeng”  pBuffer.dissolve_option = “ALL”;     //融合缓冲区重叠交叉部分如果融合了边界，那么所有缓冲多边形将组合成一个几何图形。
            //pGP.Execute(pBuffer, null);
            ////执行缓冲区分析
        }

        private void 图层叠置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ILayer pLayer1 = axMapControl1.Map.get_Layer(0);
            //ILayer pLayer2 = axMapControl1.Map.get_Layer(1);
            //Geoprocessor pGP = new Geoprocessor();
            //pGP.OverwriteOutput = true;
            //Intersect pIntersect = new Intersect();
            //pIntersect.in_features = pLayer1 + ";" + pLayer2;//多个对象的输入,用分号隔开包含完整路径的文件名 ;
            //System.Object pOutputFeature = axMapControl1.Map;
            //pIntersect.out_feature_class = pOutputFeature;
            //pIntersect.join_attributes = "ALL";
            //pGP.Execute(pIntersect, null);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
             pMapUnits="UnKnown!";
             this.WindowState = FormWindowState.Maximized;
             //Image img = Image.FromFile(@"E:\课程学习\课程工程\StudentQuerySystem\Data\leftPicture.jpg");//双引号里是图片的路径 
             //pictureBox1.Image = img; 
        }

        private void FixedZoomIn_Click(object sender, EventArgs e)
        {
            FixedZoomIn fixedZoomIn = new FixedZoomIn();
            fixedZoomIn.OnCreate(this.axMapControl1.Object);
            fixedZoomIn.OnClick();

        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            ITool tool = new ControlsMapZoomInToolClass();
            ICommand command = tool as ICommand;
            command.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = tool;
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            ITool tool = new ControlsMapZoomOutToolClass();
            ICommand command = tool as ICommand;
            command.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = tool;
        }

        private void FixedZoomOut_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsMapZoomOutFixedCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            command.OnClick();
        }

        private void menuPan_Click(object sender, EventArgs e)
        {
            Pan pan = new Pan();
            pan.OnCreate(this.axMapControl1.Object);
            //this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPan;
            this.axMapControl1.CurrentTool = pan;
        }
        private AttributeQueryForm attributeQueryForm = null;
        private void menuAttributeQuery_Click(object sender, EventArgs e)
        {
            if (attributeQueryForm == null)
            {
                 attributeQueryForm = new AttributeQueryForm(axMapControl1);
            }
            attributeQueryForm.Hide();
            attributeQueryForm.Show();
        }

        private void axToolbarControl1_OnMouseMove(object sender, IToolbarControlEvents_OnMouseMoveEvent e)
        {
            int index = axToolbarControl1.HitTest(e.x, e.y, false);
            if (index != -1)
            {
                // 取得鼠标所在工具的 ToolbarItem  
                try
                {
                    IToolbarItem toolbarItem = axToolbarControl1.GetItem(index);
                    // 设置状态栏信息  
                    MessageLabel.Text = toolbarItem.Command.Message;
                }
                catch { }
            }
            else
            {
                MessageLabel.Text = " 就绪 ";
            }
        }
        /// <summary>
        /// 地图定制显示TocTool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakingMap_Click(object sender, EventArgs e)
        {
            axTOCControl1.Visible = !axTOCControl1.Visible;
            if (axTOCControl1.Visible == true)
                MessageBox.Show("右键左侧列表的相应图层可编辑哦！");
            else
                MessageBox.Show("尽情的在校园中逛逛吧！");
            //if (axTOCControl1.Visible)
            //{
            //    this.axMapControl1.Size = new System.Drawing.Size(574, 444);
            //    this.axMapControl1.Location = new System.Drawing.Point(187, 100);
            //    //this.axMapControl1.Refresh();
            //}
            //else 
            //{
            //    this.axMapControl1.Size = new System.Drawing.Size(761, 444);
            //    this.axMapControl1.Location = new System.Drawing.Point(0, 100);
            //    //this.axMapControl1.Refresh();
            //}

        }

        private void axMapControl1_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                axMapControl2.Map = new MapClass();
                for (int i = 1; i <= axMapControl1.Map.LayerCount; i++)
                {
                    axMapControl2.AddLayer(axMapControl1.get_Layer(axMapControl1.Map.LayerCount-i));
                }
                axMapControl2.Extent = axMapControl1.Extent;
                axMapControl2.Refresh();
            }
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            if (axMapControl1.DocumentFilename == null)
            {
                SaveMapAs();
            }
            if (m_bDocModified == false) return;
            try
            {
                if (axMapControl1.Map.LayerCount == 0) return;
                if (axMapControl1.DocumentFilename != null && axMapControl1.CheckMxFile(axMapControl1.DocumentFilename))
                {
                    if (m_pMapDocument != null)
                    {
                        m_pMapDocument.Close();
                        m_pMapDocument = null;
                    }
                    m_pMapDocument = new MapDocumentClass(); //实例化
                    m_pMapDocument.Open(axMapControl1.DocumentFilename, "");//必须的一步，用于将AxMapControl的实例的DocumentFileName传递给pMapDoc的
                    if (m_pMapDocument.get_IsReadOnly(m_pMapDocument.DocumentFilename))  //判断是否只读
                    {
                        MessageBox.Show("文件只读！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        m_pMapDocument.Close();
                        return;
                    }
                    m_pMapDocument.ReplaceContents((IMxdContents)axMapControl1.Map); //重置
                    IObjectCopy lip_ObjCopy = new ObjectCopyClass(); //使用Copy，避免共享引用
                    axMapControl1.Map = (IMap)lip_ObjCopy.Copy(m_pMapDocument.get_Map(0));
                    lip_ObjCopy = null;
                    m_pMapDocument.Save(m_pMapDocument.UsesRelativePaths, true);
                    axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                    this.Text = this.Text + "  -  " + axMapControl1.DocumentFilename;
                    m_bDocModified = false;

                    FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                    FileManager.m_sMxdPath = fi.GetDirectory();
                    MessageBox.Show("保存完成!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveMapAs()
        {
            if (m_bDocModified == false) return;
            try
            {
                //判断pMapDocument是否为空，
                if (axMapControl1.Map.LayerCount == 0) return;
                //获取pMapDocument对象
                IMxdContents pMxdC;
                pMxdC = axMapControl1.Map as IMxdContents;
                IMapDocument pMapDocument = new MapDocumentClass();
                //获取保存路径
                string saveURL = string.Empty;
                SaveFileDialog saveFileDialog;
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "保存地图";
                saveFileDialog.Filter = "Map Documents (*.mxd)|*.mxd";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveURL = saveFileDialog.FileName.ToString();
                }
                else
                {
                    MessageBox.Show("请选择文件路径!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                pMapDocument.New(saveURL);
                pMapDocument.ReplaceContents(pMxdC);
                pMapDocument.Save(true, true);
                this.Text = this.Text + "  -  " + saveURL;
                m_pMapDocument = pMapDocument;
                axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                m_bDocModified = false;
                FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                FileManager.m_sMxdPath = fi.GetDirectory();
                MessageBox.Show("保存完成!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private IList<ILayer> m_CopyedLayers = new List<ILayer>();   //当前拷贝的图层
        private void tsbOpenTool_Click(object sender, EventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerHourglass;
            m_pMapDocument = new MapDocumentClass();
            try
            {
                OpenFileDialog dlgOpen = new OpenFileDialog();
                dlgOpen.Title = "打开文档";
                dlgOpen.Filter = "ArcMap Document(*.mxd)|*.mxd";
                dlgOpen.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                string sFilePath = null;
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    sFilePath = dlgOpen.FileName;
                }
                else
                    return;

                if (sFilePath == "")
                {
                    MessageBox.Show("文件路径错误!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                //将数据载入pMapDocument并与map控件联系起来
                m_pMapDocument.Open(sFilePath, "");
                //IMapDocument对象中可能有多个Map对象，遍历每个map对象
                for (int i = 0; i < m_pMapDocument.MapCount; i++)
                {
                    axMapControl1.Map = m_pMapDocument.get_Map(i);
                }
                axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                this.Text = this.Text + "  -  " + axMapControl1.DocumentFilename;
                axMapControl1.Refresh();
                FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                FileManager.m_sMxdPath = fi.GetDirectory();
                axMapControl1.Extent = axMapControl1.FullExtent;
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
                //RefreshcbLayer_right();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }

        private void WY_NewLayer_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsAddDataCommandClass();
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void WY_DeleteLayer_Click(object sender, EventArgs e)
        {
            //遍历图层并断所选图层，并判断与其对应的实际图层
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (pGlobalFeatureLayer.Name == axMapControl1.get_Layer(i).Name)
                {
                    //引用deletelayer方法删除该图层
                    axMapControl1.DeleteLayer(i);
                }
            }
        }

        private void WY_LableLayer_Click(object sender, EventArgs e)
        {
            if (pGlobalFeatureLayer == null) return;
            IGeoFeatureLayer pGeoFeaturelayer = (IGeoFeatureLayer)pGlobalFeatureLayer;
            bool boolKG = pGeoFeaturelayer.DisplayAnnotation;
            m_LableLayer.Checked = !boolKG;

            if (m_LableLayer.Checked == true)
            {
                //Select Field Name from Current Layers
                FrmSelectField frm = new FrmSelectField((IFeatureLayer)pGlobalFeatureLayer, "name");
                string sFieldName = "NAME";
                if (frm.ShowDialog() == DialogResult.Cancel) return;
                sFieldName = frm.strDefFieldName;
                InitLabel(pGeoFeaturelayer, sFieldName);
                pGeoFeaturelayer.DisplayAnnotation = true;
            }
            else
            {
                pGeoFeaturelayer.DisplayAnnotation = false;
            }
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, axMapControl1.Extent);
        }
        private static void InitLabel(IGeoFeatureLayer pGeoFeaturelayer, string sFieldName)
        {
            /*IAnnotateLayerPropertiesCollection作用于一个要素图层的所有注记设置的集合，控制要素图层的一系列注记对象*/
            IAnnotateLayerPropertiesCollection pAnnoLayerPropsCollection;
            //定义标注类
            pAnnoLayerPropsCollection = pGeoFeaturelayer.AnnotationProperties;
            /*将要素图层注记集合中的所有项都移除*/
            pAnnoLayerPropsCollection.Clear();

            IBasicOverposterLayerProperties pBasicOverposterlayerProps = new BasicOverposterLayerPropertiesClass();
            switch (pGeoFeaturelayer.FeatureClass.ShapeType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    pBasicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    pBasicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPoint;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    pBasicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
                    break;
            }
            ITextSymbol pTextSymbol = new TextSymbolClass();
            stdole.IFontDisp pFont = (stdole.IFontDisp)new stdole.StdFont();
            IRgbColor pRGB = null;
            pFont.Name = "Arial";
            pFont.Size = 9;
            pFont.Bold = false;
            pFont.Italic = false;
            pFont.Underline = false;
            pTextSymbol.Font = pFont;
            if (pRGB == null)
            {
                pRGB = new RgbColorClass();
                pRGB.Red = 0;
                pRGB.Green = 0;
                pRGB.Blue = 0;
                pTextSymbol.Color = (IColor)pRGB;
            }

            ILabelEngineLayerProperties pLabelEnginelayerProps = new LabelEngineLayerPropertiesClass();
            pLabelEnginelayerProps.Expression = "[" + sFieldName + "]";
            pLabelEnginelayerProps.Symbol = pTextSymbol;
            pLabelEnginelayerProps.BasicOverposterLayerProperties = pBasicOverposterlayerProps;
            /*将一项标注属性(LayerEngineLayerProperties对象)增加到要素图层的注记集合当中*/
            /*IAnnotateLayerProperties接口用于获取和修改要素图层注记类的注记属性，定义要素图层动态注记（文本）的显示*/
            pAnnoLayerPropsCollection.Add((IAnnotateLayerProperties)pLabelEnginelayerProps);

        }
        private void WY_ZoomToLayer_Click(object sender, EventArgs e)
        {
            this.axMapControl1.Extent = pGlobalFeatureLayer.AreaOfInterest;
            this.axMapControl1.ActiveView.Refresh();
        }

        private void WY_OpenAttribute_Click(object sender, EventArgs e)
        {
            FrmAttribute Ft = new FrmAttribute(pGlobalFeatureLayer as IFeatureLayer);
            Ft.Show();
        }
        /// <summary>
        /// 复制图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WY_CopyLayer_Click(object sender, EventArgs e)
        {
            IBasicMap basicMap = null;
            object unk = null;
            object data = null;
            ILayer pLayer = null;
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            axTOCControl1.GetSelectedItem(ref itemType,
                ref basicMap, ref pLayer, ref unk, ref data);
            m_CopyedLayers.Add(pLayer);
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WY_PasteLayer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_CopyedLayers.Count; i++)
            {
                if (pGlobalFeatureLayer is IGroupLayer)
                {
                    (pGlobalFeatureLayer as IGroupLayer).Add(m_CopyedLayers[i]);
                }
                else
                {
                    axMapControl1.AddLayer(m_CopyedLayers[i], get_LayIndex(pGlobalFeatureLayer) + 1);
                }
            }
            this.axTOCControl1.Update();
            this.axTOCControl1.Refresh();
            m_CopyedLayers.Clear();
        }
        private int get_LayIndex(ILayer pLayer)
        {
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {
                if (pLayer == this.axMapControl1.get_Layer(i))
                {
                    return i;
                }
            }
            return 0;
        }
        private void WY_CutLayer_Click(object sender, EventArgs e)
        {
            //复制
            IBasicMap basicMap = null;
            object unk = null;
            object data = null;
            ILayer pLayer = null;
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            axTOCControl1.GetSelectedItem(ref itemType,
                ref basicMap, ref pLayer, ref unk, ref data);
            m_CopyedLayers.Add(pLayer);
            //删除
            //遍历图层并断所选图层，并判断与其对应的实际图层
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (pGlobalFeatureLayer.Name == axMapControl1.get_Layer(i).Name)
                {
                    //引用deletelayer方法删除该图层
                    axMapControl1.DeleteLayer(i);
                }
            }
            axMapControl1.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (axMapControl1.DocumentFilename == null)
            {
                SaveMapAs();
            }
            if (m_bDocModified == false) return;
            try
            {
                if (axMapControl1.Map.LayerCount == 0) return;
                if (axMapControl1.DocumentFilename != null && axMapControl1.CheckMxFile(axMapControl1.DocumentFilename))
                {
                    if (m_pMapDocument != null)
                    {
                        m_pMapDocument.Close();
                        m_pMapDocument = null;
                    }
                    m_pMapDocument = new MapDocumentClass(); //实例化
                    m_pMapDocument.Open(axMapControl1.DocumentFilename, "");//必须的一步，用于将AxMapControl的实例的DocumentFileName传递给pMapDoc的
                    if (m_pMapDocument.get_IsReadOnly(m_pMapDocument.DocumentFilename))  //判断是否只读
                    {
                        MessageBox.Show("文件只读！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        m_pMapDocument.Close();
                        return;
                    }
                    m_pMapDocument.ReplaceContents((IMxdContents)axMapControl1.Map); //重置
                    IObjectCopy lip_ObjCopy = new ObjectCopyClass(); //使用Copy，避免共享引用
                    axMapControl1.Map = (IMap)lip_ObjCopy.Copy(m_pMapDocument.get_Map(0));
                    lip_ObjCopy = null;
                    m_pMapDocument.Save(m_pMapDocument.UsesRelativePaths, true);
                    axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                    this.Text = this.Text + "  -  " + axMapControl1.DocumentFilename;
                    m_bDocModified = false;

                    FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                    FileManager.m_sMxdPath = fi.GetDirectory();
                    MessageBox.Show("保存完成!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            ITool tool = new ControlsMapZoomInToolClass();
            ICommand command = tool as ICommand;
            command.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = tool;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            ITool tool = new ControlsMapZoomOutToolClass();
            ICommand command = tool as ICommand;
            command.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = tool;
        }

        private void btnFullScop_Click(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
        }

        private void btnPan_Click_1(object sender, EventArgs e)
        {

            ITool tool = new ControlsMapPanToolClass();
            ICommand command = tool as ICommand;
            command.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = tool;
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            ITool tool = new ControlsMapIdentifyToolClass();
            ICommand command = tool as ICommand;
            command.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = tool;
        }

        private void InfoOk_Click(object sender, EventArgs e)
        {
            if (InfoText.Text != "")
            {
                Information info_Frm = new Information(InfoText.Text);
                info_Frm.Show();
            }
            InfoText.Text = "";
        }

        private void InfoCancel_Click(object sender, EventArgs e)
        {
            InfoText.Text = "";
        }

        private void 最短路径查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("能够为你提供从一个地方到另一个地方的最短路线规划！");
            PathSortFrm pathSortFrm = new PathSortFrm();
            pathSortFrm.Show();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ICommand  pCommand = new ControlsSelectFeaturesToolClass();
            ITool pTool = pCommand as ITool;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTool;
        }

        private void ClearSelect_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ControlsClearSelectionCommandClass();
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
            ClearDrawItems(this.axMapControl1.ActiveView);
            axMapControl1.Refresh();
        }
        public static void ClearDrawItems(IActiveView pActiveView)
        {
            if (pActiveView != null)
            {
                IGraphicsContainer pGra = pActiveView as IGraphicsContainer;
                IActiveView pAv = pGra as IActiveView;
                // clear all draw item in container
                pGra.DeleteAllElements();
            }
        }

        private void axTOCControl1_VisibleChanged(object sender, EventArgs e)
        {
            
        }
        
        private void RefreshcbLayer_right()
 
        {
            int index=0;
            
            cbLayer_right.Items.Clear();
            for(int i = 0;  i < axMapControl1.LayerCount; i++)
           {
               IFeatureLayer pFeatureLayer=axMapControl1.get_Layer(i) as IFeatureLayer;
               IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
               //bool hasNameFeature = false;
               // //只有有name字段的图层被加入待选
               //for (int j = 0; j < pFeatureClass.Fields.FieldCount; j++)
               //{
               //    if (pFeatureClass.Fields.get_Field(i).Name == "name")
               //    {
               //        hasNameFeature = true;
               //    }

               //}
               //if(hasNameFeature)
               //{
                   cbLayer_right.Items.Add(axMapControl1.get_Layer(i).Name);
                   if (axMapControl1.get_Layer(i).Name == "学生宿舍楼")
                   index = i;
               //}
               
           }          
        cbLayer_right.Text = cbLayer_right.Items[index].ToString();
   }

        private void btnRight_Click(object sender, EventArgs e)
        {
            IQueryFilter queryFilter =new  QueryFilterClass();
            //用户选择的图层
            ILayer pLayer = axMapControl1.get_Layer(cbLayer_right.SelectedIndex);
            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            try
            {
                axMapControl1.Map.ClearSelection(); //清除上次查询结果
                IActiveView pActiveView = axMapControl1.Map as IActiveView;
                //pQueryFilter的实例化 
                IQueryFilter pQueryFilter = new QueryFilterClass();
                //设置查询过滤条件 
                string textSql = "name='"+txbPlace.Text.ToString()+"'";
                //MessageBox.Show(textSql);
                pQueryFilter.WhereClause = textSql;
                //查询 ,search的参数第一个为过滤条件，第二个为是否重复执行
                //IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pQueryFilter, false);
                ////获取查询到的要素 
                //IFeature pFeature = pFeatureCursor.NextFeature();
                ////判断是否获取到要素 
                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                int iSelectedFeaturesCount = pFeatureSelection.SelectionSet.Count;
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);//执行查询
                //while (pFeature != null)
                //{
                //    mMapControl.Map.SelectFeature(pFeatureLayer, pFeature); //选择要素 
                //    mMapControl.Extent = pFeature.Shape.Envelope; //放大到要素
                //    pFeature = pFeatureCursor.NextFeature();
                //}
                //如果本次查询后，查询的结果数目没有改变，则认为本次查询没有产生新的结果
                if (pFeatureSelection.SelectionSet.Count == 0)
                {
                    MessageBox.Show("No results!");
                    return;
                }
                //pFeatureSelection.SelectionSet.Count == iSelectedFeaturesCount || 
                IEnumFeature pEnumFeature = axMapControl1.Map.FeatureSelection as IEnumFeature;
                int nFeatureSize = 0;
                pEnumFeature.Reset();

                IFeature pFeature = pEnumFeature.Next();
                while (pFeature != null)
                {
                    nFeatureSize++;
                    pFeature = pEnumFeature.Next();
                }
                IEnvelope pEnvelope = new EnvelopeClass();

                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                double dScale = (pFeatureLayer.MinimumScale + pFeatureLayer.MaximumScale) / 2;
                ESRI.ArcGIS.Controls.IMapControl3 pMaps = (ESRI.ArcGIS.Controls.IMapControl3)axMapControl1.Object;

                bool bRec = true;
                while (bRec)
                {
                    pEnumFeature.Reset();
                    pFeature = pEnumFeature.Next();
                    int i = 0;
                    while (pFeature != null)//将目标中心显示
                    {
                        pEnvelope = pFeature.Extent;
                        IPoint pCenterPt = new PointClass();
                        pCenterPt.X = (pEnvelope.XMax + pEnvelope.XMin) / 2;
                        pCenterPt.Y = (pEnvelope.YMax + pEnvelope.YMin) / 2;

                        IEnvelope pMapEntent = pActiveView.Extent;
                        pMapEntent.CenterAt(pCenterPt);
                        pActiveView.Extent = pMapEntent;

                        pMaps.MapScale = dScale;
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, pActiveView.Extent);
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, pActiveView.Extent);
                        Application.DoEvents();
                        //MapView.FlashFeature(m_MapControl, pFeature.Shape);//目标闪烁
                        FlashFeature(pFeature.Shape);
                        Application.DoEvents();
                        i++;
                        if (nFeatureSize >= 2 && i < nFeatureSize - 1)
                        {
                            this.Visible = false;
                            DialogResult pRes = MessageBox.Show("Is this feature?", "Sure", MessageBoxButtons.YesNoCancel);
                            if (pRes == DialogResult.Yes)
                            {
                                this.Visible = true;
                                return;
                            }
                            else if (pRes == DialogResult.Cancel)
                            {
                                return;
                            }
                        }
                        pFeature = pEnumFeature.Next();
                    }
                    if (nFeatureSize <= 1)
                    {
                        return;
                    }
                    DialogResult pDlgRes = MessageBox.Show("It is already last one, restart?", "Sure", MessageBoxButtons.YesNo);
                    if (pDlgRes == DialogResult.Yes)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询出错，请重新选择\n" + ex.Message);
                return;
            }
        }
        private void FlashFeature(IGeometry pGeometry)//目标闪烁
        {
            esriGeometryType pGeometryType = pGeometry.GeometryType;
            ISymbol pSymbol = null;
            IColor pColor = new RgbColorClass();
            pColor.RGB = 255;

            if (pGeometryType == esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Color = pColor;
                pSymbol = pSimpleFillSymbol as ISymbol;
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPolyline || pGeometryType == esriGeometryType.esriGeometryLine)
            {
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Color = pColor;
                pSimpleLineSymbol.Width = 2;
                pSymbol = pSimpleLineSymbol as ISymbol;
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPoint || pGeometryType == esriGeometryType.esriGeometryMultipoint)
            {
                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkerSymbol.OutlineColor = pColor;
                pSimpleMarkerSymbol.OutlineSize = 2;
                pSymbol = pSimpleMarkerSymbol as ISymbol;
            }
            axMapControl1.FlashShape(pGeometry, 4, 200, pSymbol);
        }
        private void btnClearRight_Click(object sender, EventArgs e)
        {
            txbPlace.Text = "";
        }

        private void miCreateBookmark_Click(object sender, EventArgs e)
        {
            AdmitBookmarkName frmABN = new AdmitBookmarkName(this);
            frmABN.Show();
        }
        //自定义创建书签
        public void createBookmark(string sBookmarkName)
        {
            //通过 IAOIBookmark接口创建一个变量，类型为AOIBookmark，用于保存当前地图
            IAOIBookmark aoiBookmark = new AOIBookmarkClass();
            if (aoiBookmark != null)
            {
                aoiBookmark.Location = axMapControl1.ActiveView.Extent;
                aoiBookmark.Name = sBookmarkName;
            }
            //通过IMapBookmarks接口访问当前地图，并向地图中加入新建书签
            IMapBookmarks bookmarks = axMapControl1.Map as IMapBookmarks;
            if (bookmarks != null)
            {
                bookmarks.AddBookmark(aoiBookmark);
            }
            //将新建书签名加入组合框中，用于之后调用对应书签
            cbBookmarkList.Items.Add(aoiBookmark.Name);
        }

        private void cbBookmarkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //访问地图所包含的书签，并获取书签序列
            IMapBookmarks bookmarks = axMapControl1.Map as IMapBookmarks;
            IEnumSpatialBookmark enumspatialBookmark = bookmarks.Bookmarks;
            //对地图所包含的书签进行遍历，获取与组合框所选项名称相符的书签
            enumspatialBookmark.Reset();
            ISpatialBookmark spatialBookmark = enumspatialBookmark.Next();
            while (enumspatialBookmark != null)
            {
                if (cbBookmarkList.SelectedItem.ToString() == spatialBookmark.Name)
                {
                    spatialBookmark.ZoomTo((IMap)axMapControl1.ActiveView);
                    axMapControl1.ActiveView.Refresh();
                    break;
                }
                spatialBookmark = enumspatialBookmark.Next();
            }
        }

        //IMapDocument mapDocument;
        public IFeatureWorkspace pFWorkspace;
        string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private void 添加站点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand pCommand;
            pCommand = new AddNetStopsTool();
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pCommand as ITool;
            pCommand = null;
        }

        private void 添加路障ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand pCommand;
            pCommand = new AddNetStopsTool();
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pCommand as ITool;
            pCommand = null;
        }

        private void 路径解决ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand pCommand;
            pCommand = new ShortPathSolveCommand();
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
            pCommand = null;
        }

        private void 清除路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            try
            {
                //string name =path + "\\WHU_StudentQuery.mdb";
                //string name = NetWorkAnalysClass.getPath(path) + "\\ArcEngineDemo\\WHU_StudentQuery.mdb";
                string name = NetWorkAnalysClass.getPath(path) + "\\Data\\TestNet.gdb";
                //打开工作空间
                pFWorkspace = NetWorkAnalysClass.OpenWorkspace(name) as IFeatureWorkspace;
                IGraphicsContainer pGrap = this.axMapControl1.ActiveView as IGraphicsContainer;
                pGrap.DeleteAllElements();//删除所添加的图片要素
                IFeatureClass inputFClass = pFWorkspace.OpenFeatureClass("Stops");
                //删除站点要素
                if (inputFClass.FeatureCount(null) > 0)
                {
                    ITable pTable = inputFClass as ITable;
                    pTable.DeleteSearchedRows(null);
                }
                for (int i = 0; i < axMapControl1.LayerCount; i++)//删除分析结果
                {
                    ILayer pLayer = axMapControl1.get_Layer(i);
                    if (pLayer.Name == ShortPathSolveCommand.m_NAContext.Solver.DisplayName)
                    {
                        axMapControl1.DeleteLayer(i);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.axMapControl1.Refresh();
        }

        private void SpaceSerch_Click(object sender, EventArgs e)
        {
            //ISpatialFilter spatialFilter = new SpatialFilterClass();
            //spatialFilter.Geometry = point;
            //spatialFilter.GeometryField = "shape";
            //spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            FrmSelectLayer pLayerFrm = new FrmSelectLayer();
            pLayerFrm.m_axMapControl = axMapControl1;
            
            if (pLayerFrm.ShowDialog() == DialogResult.OK)
            {
                IArray aFeatures = GetSelectedFeatures();
                if (aFeatures.Count == 0)
                {
                    MessageBox.Show("没有选中目标！");
                    return;
                }
                IFeature pSelFeature = aFeatures.get_Element(0) as IFeature;
                try
                {
                    ILayer pLayer = pLayerFrm.m_pLayer;
                    IArray pFeatures = SelectFeatureByGeometry((IFeatureLayer)pLayer, pSelFeature.ShapeCopy);
                    for (int i = 0; i < pFeatures.Count; i++)
                    {
                        IFeature pFeature = pFeatures.get_Element(i) as IFeature;
                        IRgbColor pColor = new RgbColorClass();
                        pColor.RGB = 255;
                        DrawElement(pFeature.ShapeCopy, pColor);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
        //获得当前被选择的要素
        private  IArray GetSelectedFeatures()
        {
            IArray pFeatureArray = new ArrayClass();
            ISelection pFeatureSelction = axMapControl1.Map.FeatureSelection;
            IEnumFeature pEnumFeature = pFeatureSelction as IEnumFeature;
            IEnumFeatureSetup pEnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            IFeature pFeature = pEnumFeature.Next();
            while (pFeature != null)
            {
                if (!pFeature.Shape.IsEmpty)
                {
                    pFeatureArray.Add(pFeature);
                }
                pFeature = pEnumFeature.Next();
            }
            return pFeatureArray;
        }
        /// <summary>
        /// 通过图形选择指定图层中与图形相交的要素集合
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pGeometry"></param>
        /// <returns></returns>
        private IArray SelectFeatureByGeometry(IFeatureLayer pLayer, IGeometry pGeometry)
        {
            if (pLayer == null) return null;
            IArray pFeatureArray = new ArrayClass();
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                pSpatialFilter.Geometry = pGeometry;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    if (pFeature.Shape == null)
                    {
                        pFeature = pFeatureCursor.NextFeature();
                        continue;
                    }
                    if (!pFeature.Shape.IsEmpty)
                    {
                        pFeatureArray.Add(pFeature);
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return pFeatureArray;
            }
            catch (Exception exception)
            {
                MessageBox.Show("未找到目标 " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return null;
        }
        private  void DrawElement(IGeometry pGeometry, IRgbColor pColor, bool bCleanLastDraw = false)
        {
            if (pGeometry == null || axMapControl1 == null) return;
            IElement pElement = null;
            IGraphicsContainer pGra = axMapControl1.ActiveView as IGraphicsContainer;
            IActiveView pAv = pGra as IActiveView;
            if (bCleanLastDraw)
            {
                pGra.DeleteAllElements();
            }
            if (pGeometry.Dimension == esriGeometryDimension.esriGeometry0Dimension)
            {
                IMarkerElement pMarkerElement = new MarkerElementClass();
                pElement = pMarkerElement as IElement;
                pElement.Geometry = pGeometry;

                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkerSymbol.Color = pColor;
                pSimpleMarkerSymbol.Size = 3;
                (pElement as IMarkerElement).Symbol = pSimpleMarkerSymbol;
                pGra.AddElement((IElement)pElement, 0);
            }
            else if (pGeometry.Dimension == esriGeometryDimension.esriGeometry1Dimension)
            {
                ILineElement pLineElement = new LineElementClass();
                pElement = pLineElement as IElement;
                pElement.Geometry = pGeometry;
                ILineSymbol pOutline = new SimpleLineSymbolClass();
                pOutline.Width = 1;
                pOutline.Color = pColor;
                (pElement as ILineElement).Symbol = pOutline;
                pGra.AddElement((IElement)pElement, 0);
            }
            else if (pGeometry.Dimension == esriGeometryDimension.esriGeometry2Dimension)
            {
                IPolygonElement pPolygonElement = new PolygonElementClass();
                pElement = pPolygonElement as IElement;
                pElement.Geometry = pGeometry;

                IRgbColor pOutlineColor = new RgbColorClass();
                pOutlineColor.Red = 0;
                pOutlineColor.Green = 0;
                pOutlineColor.Blue = 0;
                pOutlineColor.Transparency = 40;
                ILineSymbol pOutline = new SimpleLineSymbolClass();
                pOutline.Width = 1;
                pOutline.Color = pOutlineColor;
                IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                pFillSymbol.Color = pColor;
                pFillSymbol.Outline = pOutline;
                pFillSymbol.Color.Transparency = 60;
                IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
                pFillShapeEle.Symbol = pFillSymbol;
                pGra.AddElement((IElement)pFillShapeEle, 0);
            }
            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
    }
}
