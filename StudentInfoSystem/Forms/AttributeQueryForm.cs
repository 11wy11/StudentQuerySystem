using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace ArcEngineDemos.Forms
{
    public partial class AttributeQueryForm : Form
    {
        //地图数据 
        int m_iSelPos = -1;
        IMap pMap; 
        //选中图层 
        IFeatureLayer pFeatureLayer;
        ILayer pLayer;
        ILayerFields pLayerFields;
        IEnumLayer pEnumLayer;
        private AxMapControl mMapControl;
      
        //根据所选择的图层查询得到的特征类
        private IFeatureClass pFeatureClass = null;

        public AttributeQueryForm(AxMapControl mapControl)
        {
            InitializeComponent();
            this.mMapControl = mapControl;
            // 禁用最大化和最小化按钮
            this.MaximizeBox = false;
            //this.MinimizeBox = false;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeQueryForm_Load(object sender, EventArgs e)
        {
            //MapControl中没有图层时返回 
            if (this.mMapControl.LayerCount <= 0)
                return;
            //获取MapControl中的全部图层名称，并加入ComboBox 
            //图层 
            ILayer pLayer;
            //图层名称 
            string strLayerName;
            for (int i = 0; i < this.mMapControl.LayerCount; i++)
            {
                pLayer = this.mMapControl.get_Layer(i);
                strLayerName = pLayer.Name;
                //图层名称加入cboLayer 
                this.cboLayer.Items.Add(strLayerName);
            }
            //默认显示第一个选项 
            this.cboLayer.SelectedIndex = 0;
            //this.cboLayer.Text = cboLayer.Items[0].ToString();

        }
        /// <summary>
        /// 下拉框改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空listBoxField控件的内容
            this.listBoxField.Items.Clear();
            //获取cboLayer中选中的图层 
            pFeatureLayer = mMapControl.get_Layer(cboLayer.SelectedIndex) as IFeatureLayer;
            pFeatureClass = pFeatureLayer.FeatureClass;
            //字段名称 
            string strFldName;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                strFldName = pFeatureClass.Fields.get_Field(i).Name;
                //图层名称加入cboField 
                this.listBoxField.Items.Add(strFldName);
            }
            //默认显示第一个选项 
            this.listBoxField.SelectedIndex = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sFieldName = listBoxField.Text;
            listBoxValue.Items.Clear();
            int iFieldIndex = 0;
            IField pField = null;
            IFeatureCursor pFeatCursor = pFeatureClass.Search(null, true);
            IFeature pFeat = pFeatCursor.NextFeature();
            iFieldIndex = pFeatureClass.FindField(sFieldName);
            pField = pFeatureClass.Fields.get_Field(iFieldIndex);
            while (pFeat != null)
            {
                listBoxValue.Items.Add(pFeat.get_Value(iFieldIndex));
                pFeat = pFeatCursor.NextFeature();
            }

        }

        private void listBoxField_Click(object sender, EventArgs e)
        {
            //textBoxSql.SelectedText = listBoxField.SelectedItem.ToString() + " ";
        }

        private void listBoxValue_Click(object sender, EventArgs e)
        {
            //textBoxSql.SelectedText = listBoxValue.SelectedItem.ToString() + " ";
        }

        private void listBoxField_DoubleClick(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = listBoxField.SelectedItem.ToString() + " ";
        }

        private void listBoxValue_DoubleClick(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = listBoxValue.SelectedItem.ToString() + " ";
        }
        private void GetAttributeVale()
        {
            try
            {
                if (this.listBoxField.SelectedIndex == -1) return;

                string currentFieldName = this.listBoxField.Text;//当前字段名
                string currentLayerName = this.cboLayer.Text;
                this.pEnumLayer.Reset();
                for (this.pLayer = this.pEnumLayer.Next(); this.pLayer != null; this.pLayer = this.pEnumLayer.Next())
                {
                    if (this.pLayer.Name == currentLayerName) break;
                }
                this.pLayerFields = this.pLayer as ILayerFields;
                IField pField = this.pLayerFields.get_Field(this.pLayerFields.FindField(currentFieldName));
                esriFieldType pFieldType = pField.Type;

                //对Table中当前字段进行排序,把结果赋给Cursor
                ITable pTable = this.pLayer as ITable;
                ITableSort pTableSort = new TableSortClass();
                pTableSort.Table = pTable;
                pTableSort.Fields = currentFieldName;
                pTableSort.set_Ascending(currentFieldName, true);
                pTableSort.set_CaseSensitive(currentFieldName, true);
                pTableSort.Sort(null);//排序
                ICursor pCursor = pTableSort.Rows;
                //IRow pRow = pCursor.NextRow();
                //int nSize = 0;
                //while (pRow != null)
                //{
                //    nSize++;
                //    pRow = pCursor.NextRow();
                //}
                //DevComponents.DotNetBar.MessageBox.Show(nSize.ToString());
                //return;

                //字段统计
                IDataStatistics pDataStatistics = new DataStatisticsClass();
                pDataStatistics.Cursor = pCursor;
                pDataStatistics.Field = currentFieldName;
                System.Collections.IEnumerator pEnumeratorUniqueValues = pDataStatistics.UniqueValues;//唯一值枚举
                int uniqueValueCount = pDataStatistics.UniqueValueCount;//唯一值的个数

                this.listBoxValue.Items.Clear();
                string currentValue = null;
                pEnumeratorUniqueValues.Reset();
                if (pFieldType == esriFieldType.esriFieldTypeString)
                {
                    while (pEnumeratorUniqueValues.MoveNext())
                    {
                        currentValue = pEnumeratorUniqueValues.Current.ToString();
                        this.listBoxValue.Items.Add("'" + currentValue + "'");
                    }
                }
                else
                {
                    while (pEnumeratorUniqueValues.MoveNext())
                    {
                        currentValue = pEnumeratorUniqueValues.Current.ToString();
                        this.listBoxValue.Items.Add(currentValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region
        private void Btnequal_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "= ";

        }
        private void btnis_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "is ";

        }

        private void Btncharacter_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "' ";
            
        }
        private void btnmore_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "> ";
        }
        private void Btnless_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "<";
        }

        private void btnmoe_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = ">= ";
        }

        private void btnloe_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "<= ";
        }
        private void btnempty_Click(object sender, EventArgs e)
        {
            this.textBoxSql.Text = "";

        }
        private void btnunequal_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "!= ";
        }

        private void btnlike_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "like ";
        }

        private void btnor_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "Or";
        }

        private void btnnull_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "Null";
        }

        private void Btnnot_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "Not";
        }

        private void btnand_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "And";
        }

        private void btnin_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "In";
        }

        private void btnpercent_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "%";
        }

        private void btnbetween_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "Between";
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                mMapControl.Map.ClearSelection(); //清除上次查询结果
                IActiveView pActiveView = mMapControl.Map as IActiveView;
                //pQueryFilter的实例化 
                IQueryFilter pQueryFilter = new QueryFilterClass();
                //设置查询过滤条件 
                pQueryFilter.WhereClause = textBoxSql.Text;
                //MessageBox.Show(textBoxSql.Text);
                //查询 ,search的参数第一个为过滤条件，第二个为是否重复执行
                //IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pQueryFilter, false);
                ////获取查询到的要素 
                //IFeature pFeature = pFeatureCursor.NextFeature();
                ////判断是否获取到要素 
                IFeatureSelection pFeatureSelection = this.pFeatureLayer as IFeatureSelection;
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
                IEnumFeature pEnumFeature = mMapControl.Map.FeatureSelection as IEnumFeature;
                int nFeatureSize = 0;
                pEnumFeature.Reset();
                
                IFeature pFeature =pEnumFeature.Next();
                while (pFeature != null)
                {
                    nFeatureSize++;
                    pFeature = pEnumFeature.Next();
                }
                IEnvelope pEnvelope = new EnvelopeClass();

                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                double dScale = (this.pFeatureLayer.MinimumScale + this.pFeatureLayer.MaximumScale) / 2;
                ESRI.ArcGIS.Controls.IMapControl3 pMaps = (ESRI.ArcGIS.Controls.IMapControl3)mMapControl.Object;

                bool bRec=true;
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
                MessageBox.Show("语句有错误, 请仔细检查\n" + ex.Message);
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
            mMapControl.FlashShape(pGeometry, 4, 200, pSymbol);
        }

    }
}
