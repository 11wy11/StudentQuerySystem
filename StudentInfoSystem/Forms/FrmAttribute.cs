using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Whu_Navigation
{
    public partial class FrmAttribute : Form
    {
        private ILayer pLayer;//打开属性表的图层
        private IFeatureLayer pFeatureLayer;
        private IFeatureClass pFeatureClass;
        private ILayerFields pLayerFields;
        public FrmAttribute(ILayer pLyr)
        {
            InitializeComponent();
            pLayer = pLyr;
        }

       

        private void FrmAttribute_Load(object sender, EventArgs e)
        {
            try
            {
                string tableName;
                tableName = getValidFeatureClassName(pLayer.Name);
                this.Text = tableName + "属性表".ToString();//替换窗体名称
                pFeatureLayer = pLayer as IFeatureLayer;
                pFeatureClass = pFeatureLayer.FeatureClass;
                pLayerFields = pFeatureLayer as ILayerFields;
                DataSet ds = new DataSet("dsTest");
                DataTable dt = new DataTable(pFeatureLayer.Name);
                DataColumn dc = null;
                for (int i = 0; i < pLayerFields.FieldCount; i++)
                {
                    dc = new DataColumn(pLayerFields.get_Field(i).Name);
                    dt.Columns.Add(dc);
                    dc = null;
                }
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < pLayerFields.FieldCount; j++)
                    {
                        if (pLayerFields.FindField(pFeatureClass.ShapeFieldName) == j)
                        {
                            dr[j] = pFeatureClass.ShapeType.ToString();
                        }
                        else
                        {
                            dr[j] = pFeature.get_Value(j);
                        }
                    }
                    dt.Rows.Add(dr);
                    pFeature = pFeatureCursor.NextFeature();
                }
                dataGridView1.DataSource = dt;
            }
            catch (Exception exc)
            {
                MessageBox.Show("读取属性表失败：" + exc.Message);
                this.Dispose();
            }
        }
        public static string getValidFeatureClassName(string FCname)
        {
            int dot = FCname.IndexOf(".");
            if (dot != -1)
            {
                return FCname.Replace(".", "_");
            }
            return FCname;
        }
        /// <summary>
        /// 根据图层字段创建一个只含字段的空DataTable
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static DataTable CreateDataTableByLayer(ILayer pLayer, string tableName)
        {
            //创建一个DataTable表
            DataTable pDataTable = new DataTable(tableName);
            //取得ITable接口
            ITable pTable = pLayer as ITable;
            IField pField = null;
            DataColumn pDataColumn;
            //根据每个字段的属性建立DataColumn对象
            for (int i = 0; i < pTable.Fields.FieldCount; i++)
            {
                pField = pTable.Fields.get_Field(i);
                //新建一个DataColumn并设置其属性
                pDataColumn = new DataColumn(pField.Name);
                if (pField.Name == pTable.OIDFieldName)
                {
                    pDataColumn.Unique = true;//字段值是否唯一
                }
                //字段值是否允许为空
                pDataColumn.AllowDBNull = pField.IsNullable;
                //字段别名
                pDataColumn.Caption = pField.AliasName;
                //字段数据类型
                pDataColumn.DataType = System.Type.GetType(ParseFieldType(pField.Type));
                //字段默认值
                pDataColumn.DefaultValue = pField.DefaultValue;
                //当字段为String类型是设置字段长度
                if (pField.VarType == 8)
                {
                    pDataColumn.MaxLength = pField.Length;
                }
                //字段添加到表中
                pDataTable.Columns.Add(pDataColumn);
                pField = null;
                pDataColumn = null;
            }
            return pDataTable;
        }
        public static string ParseFieldType(esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeBlob:
                    return "System.String";
                case esriFieldType.esriFieldTypeDate:
                    return "System.DateTime";
                case esriFieldType.esriFieldTypeDouble:
                    return "System.Double";
                case esriFieldType.esriFieldTypeGeometry:
                    return "System.String";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "System.String";
                case esriFieldType.esriFieldTypeGUID:
                    return "System.String";
                case esriFieldType.esriFieldTypeInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeOID:
                    return "System.String";
                case esriFieldType.esriFieldTypeRaster:
                    return "System.String";
                case esriFieldType.esriFieldTypeSingle:
                    return "System.Single";
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeString:
                    return "System.String";
                default:
                    return "System.String";
            }
        }
    
    }
}
