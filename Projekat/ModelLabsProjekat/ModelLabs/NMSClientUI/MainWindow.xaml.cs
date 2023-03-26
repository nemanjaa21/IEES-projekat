using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace NMSClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<long> allIds; //GIDs
        List<ModelCode> modelCodes, idobjmodel;
        TestGda gda;
        ModelResourcesDesc modelResources;
        public MainWindow()
        {
            InitializeAll();
            InitializeComponent();
        }

        void InitializeAll()
        {
            try
            {
                gda = new TestGda();
                allIds = gda.TestGetExtentValuesAllTypes();
                modelResources = new ModelResourcesDesc();
                modelCodes = new List<ModelCode>();
                foreach (DMSType item in Enum.GetValues(typeof(DMSType)))
                {
                    if (item != DMSType.MASK_TYPE)
                        modelCodes.Add(modelResources.GetModelCodeFromType(item));
                }
                idobjmodel = new List<ModelCode>() { ModelCode.IDOBJ_ALIASNAME, ModelCode.IDOBJ_MRID, ModelCode.IDOBJ_NAME, ModelCode.IDOBJ_GID };
            }
            catch (Exception e)
            {
                CommonTrace.WriteTrace(true, e.Message);
            }
        }

        private void cbValues_Initialized(object sender, EventArgs e)
        {
            cbValues.ItemsSource = allIds;
        }

        private void cbValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbValues.ItemsSource = modelResources.GetAllPropertyIdsForEntityId((long)cbValues.SelectedItem);
            lbValues.UnselectAll();
        }

        private void btValues_Click(object sender, RoutedEventArgs e)
        {
            if (cbValues.SelectedItem == null || lbValues.SelectedItems.Count == 0)
                return;

            output.Text = gda.GetValues((long)cbValues.SelectedItem, lbValues.SelectedItems.OfType<ModelCode>().ToList());
        }

        private void cbExtend_Initialized(object sender, EventArgs e)
        {
            cbExtend.ItemsSource = modelCodes;
        }

        private void cbExtend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbExtend.ItemsSource = modelResources.GetAllPropertyIds((ModelCode)cbExtend.SelectedItem);
            lbExtend.UnselectAll();
        }

        private void btExtend_Click(object sender, RoutedEventArgs e)
        {
            if (lbExtend.SelectedItems.Count == 0)
                return;

            output.Text = gda.GetExtentValues((ModelCode)cbExtend.SelectedItem, lbExtend.SelectedItems.OfType<ModelCode>().ToList());
        }

        private void cbRelGID_Initialized(object sender, EventArgs e)
        {
            cbRelGID.ItemsSource = allIds;
        }

        private void cbRelGID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ModelCode> ret = new List<ModelCode>();
            foreach (var item in modelResources.GetAllPropertyIdsForEntityId((long)cbRelGID.SelectedItem))
            {
                long s = ((long)item) & ((long)ModelCodeMask.MASK_ATTRIBUTE_TYPE);
                if (s == 0x9 || s == 0x19)
                    ret.Add(item);
            }

            cbRelProp.ItemsSource = ret;
        }

        private void cbRelFilter_Initialized(object sender, EventArgs e)
        {
            List<string> filters = new List<string>();
            filters.Add("No filter");
            modelCodes.ForEach(t => filters.Add(t.ToString()));
            cbRelFilter.ItemsSource = filters;
        }

        private void cbRelFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRelFilter.SelectedItem.ToString() == "No filter")
            {
                lbRel.ItemsSource = idobjmodel;
                lbRel.UnselectAll();
                return;
            }

            lbRel.ItemsSource = modelResources.GetAllPropertyIds(
                modelResources.GetModelCodeFromModelCodeName(cbRelFilter.SelectedItem.ToString()));
            lbRel.UnselectAll();
        }

        private void btRel_Click(object sender, RoutedEventArgs e)
        {
            if (cbRelGID.SelectedItem == null || cbRelProp.SelectedItem == null || lbRel.SelectedItems.Count == 0)
                return;

            Association association = new Association((ModelCode)cbRelProp.SelectedItem);
            if (cbRelFilter.SelectedItem.ToString() != "No filter")
                association.Type = modelResources.GetModelCodeFromModelCodeName(cbRelFilter.SelectedItem.ToString());

            output.Text = gda.GetRelatedValues((long)cbRelGID.SelectedItem, lbRel.SelectedItems.OfType<ModelCode>().ToList(),
                   association);
        }
    }
}
