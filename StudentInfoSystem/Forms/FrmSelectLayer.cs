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

namespace Whu_Navigation
{
    public partial class FrmSelectLayer : Form
    {
        public AxMapControl m_axMapControl = null;
        public ILayer m_pLayer = null;
        string sLayerName = "";
        public FrmSelectLayer()
        {
            InitializeComponent();
        }

        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sLayerName = cbLayer.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string sLayerName = cbLayer.SelectedItem.ToString();
            IEnumLayer pLayers = m_axMapControl.Map.get_Layers();
    
            ILayer pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer != null && pLayer.Name == sLayerName)
                {
                    m_pLayer = pLayer;
                }
                pLayer = pLayers.Next();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FrmSelectLayer_Load(object sender, EventArgs e)
        {
            IEnumLayer players = m_axMapControl.Map.get_Layers();
            ILayer plyr = players.Next();
            while (plyr != null)
            {
                if (plyr is IFeatureLayer)
                {
                    cbLayer.Items.Add(plyr.Name);
                }
                plyr = players.Next();
            }
            if (cbLayer.Items.Count > 0)
            {
                cbLayer.SelectedIndex = 0;
            }
        }
    }
}
