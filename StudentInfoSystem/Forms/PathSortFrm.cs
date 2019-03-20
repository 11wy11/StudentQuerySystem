using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Whu_Navigation.Classes;

namespace Whu_Navigation.Forms
{
    public partial class PathSortFrm : Form
    {
        IMapDocument mapDocument;
        public IFeatureWorkspace pFWorkspace;
        string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public PathSortFrm()
        {

            InitializeComponent();
        }
        private void PathOpenMxd_Click(object sender, EventArgs e)
        {
            mapDocument = new MapDocumentClass();
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog;
                openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "打开图层文件";
                openFileDialog.Filter = "map documents(*.mxd)|*.mxd";
                openFileDialog.ShowDialog();
                string filePath = openFileDialog.FileName;
                mapDocument.Open(filePath, "");
                for (int i = 0; i < mapDocument.MapCount; i++)
                {
                    axMapControl1.Map = mapDocument.get_Map(i);
                }
                axMapControl1.Refresh();
                path = System.IO.Path.GetDirectoryName(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载失败" + ex.ToString());
            }
        }
        private void AddNetPoint_Click(object sender, EventArgs e)
        {
            ICommand pCommand;
            pCommand = new AddNetStopsTool();
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pCommand as ITool;
            pCommand = null;
        }

        private void PathSolution_Click(object sender, EventArgs e)
        {
            ICommand pCommand;
            pCommand = new ShortPathSolveCommand();
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
            pCommand = null;
        }

        private void ClearNet_Click(object sender, EventArgs e)
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

        private void PathSortFrm_Load(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.FullExtent;
        }

    }
}
