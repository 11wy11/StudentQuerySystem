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

namespace Whu_Navigation.Forms
{
    public partial class FrmSelectField : Form
    {
        private IFeatureLayer m_pFeatureLayer = null;
        private string m_strDefFieldName = null;
        public string strDefFieldName
        {
            get { return m_strDefFieldName; }
            set { m_strDefFieldName = value; }
        }
        public FrmSelectField(IFeatureLayer pFeatureLayer, string strDefFieldName)
        {
            m_strDefFieldName = strDefFieldName;
            m_pFeatureLayer = pFeatureLayer;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void FrmSelectField_Load(object sender, EventArgs e)
        {
            cbField.Items.Clear();
            if (m_pFeatureLayer == null || m_pFeatureLayer.FeatureClass == null) return;
            IFeatureClass pFeatureClass = m_pFeatureLayer.FeatureClass;
               
            ILayerFields pLayerFields = m_pFeatureLayer as ILayerFields;
            for (int k = 0; k < pLayerFields.FieldCount; k++)
            {
                IField pField = pLayerFields.get_Field(k);
                string sFieldName = pField.Name;
                if (sFieldName.Equals("FID", StringComparison.OrdinalIgnoreCase) || sFieldName.Equals("OBJECTID", StringComparison.OrdinalIgnoreCase)
                || sFieldName.Equals("SHAPE", StringComparison.OrdinalIgnoreCase) || sFieldName.Equals("SHAPE_Length", StringComparison.OrdinalIgnoreCase)
                || sFieldName.Equals("SHAPE_Area", StringComparison.OrdinalIgnoreCase)) continue;
                cbField.Items.Add(pField.Name);
            }
            if (cbField.Items.Count > 0)
            {
                cbField.SelectedIndex = 0;
            }
            cbField.SelectedIndex = cbField.FindString(m_strDefFieldName);
            if (cbField.SelectedIndex != -1)
            {
                cbField.SelectedIndex = 0;
            } 
        }
    }
}
