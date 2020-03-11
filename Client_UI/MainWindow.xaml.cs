using FTN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Client_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<string> types = null;
        private TestGDA testGDA = new TestGDA();
        private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();
        private ModelCode modelCode;

        public MainWindow()
        {
            InitializeComponent();
            InitializeModelCodes();
            Reset_ComboBox_Selection();
            ComboBox_Types.IsEnabled = false;
            ComboBox_GID.IsEnabled = false;
            ComboBox_AssocProps.IsEnabled = false;
        }
        #region RadioButton_Methods
        private void RadioButton_GV_Checked(object sender, RoutedEventArgs e)
        {
            ComboBox_Types.IsEnabled = true;
            ComboBox_GID.IsEnabled = true;
            ComboBox_AssocProps.IsEnabled = false;
            ListBox_Properties.ItemsSource = null;
            Reset_ComboBox_Selection();
        }

        private void RadioButton_GRV_Checked(object sender, RoutedEventArgs e)
        {
            ComboBox_Types.IsEnabled = true;
            ComboBox_GID.IsEnabled = true;
            ComboBox_AssocProps.IsEnabled = true;
            ListBox_Properties.ItemsSource = null;
            Reset_ComboBox_Selection();
        }

        private void RadioButton_GEV_Checked(object sender, RoutedEventArgs e)
        {
            ComboBox_Types.IsEnabled = true;
            ComboBox_GID.IsEnabled = false;
            ComboBox_AssocProps.IsEnabled = false;
            ListBox_Properties.ItemsSource = null;
            Reset_ComboBox_Selection();
        }
        #endregion RadioButton_Methods
        private void ComboBox_Types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Types.SelectedItem != null)
            {
                List<ModelCode> properties = new List<ModelCode>();
                if (ComboBox_Types.SelectedItem.ToString().Equals("PLENSEQIMPENDANCE"))
                {
                    modelCode = ModelCode.PERLENSEQIMP;
                }
                else
                {
                    modelCode = (ModelCode)Enum.Parse(typeof(ModelCode), ComboBox_Types.SelectedItem.ToString());
                }

                if (RadioButton_GV.IsChecked == true)
                {
                    List<ResourceDescription> rds = testGDA.GetExtentValues(modelCode);
                    List<long> gids = new List<long>();
                    for (int i = 0; i < rds.Count; i++)
                    {
                        gids.Add(rds[i].Id);
                    }
                    ComboBox_GID.ItemsSource = gids;

                    properties = modelResourcesDesc.GetAllPropertyIds(modelCode);
                    ListBox_Properties.ItemsSource = properties;
                }
                else if (RadioButton_GRV.IsChecked == true)
                {
                    List<ResourceDescription> rds = testGDA.GetExtentValues(modelCode);
                    List<long> gids = new List<long>();
                    for (int i = 0; i < rds.Count; i++)
                    {
                        gids.Add(rds[i].Id);
                    }
                    ComboBox_GID.ItemsSource = gids;

                    foreach (Property p in rds[0].Properties)
                    {
                        if (p.Type == PropertyType.Reference || p.Type == PropertyType.ReferenceVector)
                            properties.Add(p.Id);
                    }
                    ComboBox_AssocProps.ItemsSource = properties;
                }
                else if (RadioButton_GEV.IsChecked == true)
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(modelCode);
                    ListBox_Properties.ItemsSource = properties;
                }
            }
        }

        private void ComboBox_AssocProps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_AssocProps.SelectedItem != null)
            {
                string selected = ComboBox_AssocProps.SelectedItem.ToString();
                List<ModelCode> properties = new List<ModelCode>();

                if (selected.Contains("CONNODE_TERMINALS"))
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(ModelCode.TERMINAL);
                }

                if (selected.Contains("TERMINAL_CONNODE"))
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(ModelCode.CONNODE);
                }
                if (selected.Contains("TERMINAL_CONDEQ"))
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(ModelCode.CONDEQ);
                }

                if (selected.Contains("CONDEQ_TERMINALS"))
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(ModelCode.TERMINAL);
                }

                if (selected.Contains("ACLINESEG_PERLENIMP"))
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(ModelCode.PERLENIMP);
                }

                if (selected.Contains("PERLENIMP_ACLINESEGS"))
                {
                    properties = modelResourcesDesc.GetAllPropertyIds(ModelCode.ACLINESEG);
                }

                ListBox_Properties.ItemsSource = properties;
            }
        }

        private void InitializeModelCodes()
        {
            types = new BindingList<string>();
            types.Add(DMSType.ACLINESEG.ToString());
            types.Add(DMSType.CONNODE.ToString());
            types.Add(DMSType.DCLINESEG.ToString());
            types.Add(DMSType.PLENSEQIMPENDANCE.ToString());
            types.Add(DMSType.SERCOMPENSATOR.ToString());
            types.Add(DMSType.TERMINAL.ToString());

            ComboBox_Types.ItemsSource = types;
        }

        private void Reset_ComboBox_Selection()
        {
            ComboBox_Types.SelectedItem = null;
            ComboBox_GID.SelectedItem = null;
            ComboBox_AssocProps.SelectedItem = null;
        }

        private string GetPropertyValue(Property property)
        {
            switch (property.Type)
            {
                case PropertyType.String:
                    return property.AsString();
                case PropertyType.DateTime:
                    return property.AsDateTime().ToString();
                case PropertyType.Int32:
                    return property.AsInt().ToString();
                case PropertyType.Int64:
                    return property.AsLong().ToString();
                case PropertyType.Bool:
                    return property.AsBool().ToString();
                case PropertyType.Float:
                    return property.AsFloat().ToString();
                case PropertyType.Enum:
                    EnumDescs enumDescs = new EnumDescs();
                    return enumDescs.GetStringFromEnum(property.Id, property.AsEnum());
                case PropertyType.Reference:
                    return property.AsReference().ToString();
                case PropertyType.ReferenceVector:
                    List<long> ids = property.AsReferences();
                    string retPropValue = "";
                    foreach (long id in ids)
                    {
                        retPropValue += id.ToString() + ", ";
                    }
                    return retPropValue;
            }
            return "Type not defined!";
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset_ComboBox_Selection();
            RadioButton_GEV.IsChecked = false;
            RadioButton_GV.IsChecked = false;
            RadioButton_GRV.IsChecked = false;
            ListBox_Properties.ItemsSource = null;
            RichTextBox_Result.Document.Blocks.Clear();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure?", "Exit Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_GV.IsChecked == true)
            {
                //GetValues
                ResourceDescription rd = null;
                string text = String.Empty;

                if (ComboBox_GID.SelectedItem != null && ComboBox_Types.SelectedItem != null && ListBox_Properties.SelectedItems != null)
                {
                    if (!ComboBox_GID.SelectedItem.ToString().Trim().Equals(String.Empty) && !ComboBox_Types.SelectedItem.ToString().Trim().Equals(String.Empty) && ListBox_Properties.SelectedItems.Count > 0)
                    {
                        rd = testGDA.GetValues(long.Parse(ComboBox_GID.SelectedItem.ToString()));

                        text += "PROPERTY VALUES FOR:\nType: " + ComboBox_Types.SelectedItem.ToString() + "   GID: " + ComboBox_GID.SelectedItem.ToString() + "\n\n";
                        foreach (Property p in rd.Properties)
                        {
                            if (ListBox_Properties.SelectedItems.Contains(p.Id))
                            {
                                string propValue = GetPropertyValue(p);
                                text += p.Id + ":   " + propValue + "\n";
                            }
                        }
                        RichTextBox_Result.Document.Blocks.Clear();
                        RichTextBox_Result.Document.Blocks.Add(new Paragraph(new Run(text)));
                    }
                    else
                    {
                        MessageBox.Show("Please select type, one GID and at leat one property for specified type.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select type, one GID and at leat one property for specified type.");
                }
            }
            else if (RadioButton_GRV.IsChecked == true)
            {
                //GetRelatedValues
                if (ComboBox_Types.SelectedItem != null && ComboBox_GID.SelectedItem != null
                    && ListBox_Properties.SelectedItems != null && ComboBox_AssocProps.SelectedItem != null)
                {
                    if (!ComboBox_Types.SelectedItem.ToString().Trim().Equals(String.Empty) && !ComboBox_GID.SelectedItem.ToString().Trim().Equals(String.Empty)
                         && ListBox_Properties.SelectedItems.Count > 0 && !ComboBox_AssocProps.SelectedItem.ToString().Trim().Equals(String.Empty))
                    {
                        Association asoc = new Association();
                        ModelCode mcProp;
                        ModelCode type;
                        ModelCodeHelper.GetModelCodeFromString(ComboBox_AssocProps.SelectedItem.ToString(), out mcProp);

                        List<ModelCode> properties = new List<ModelCode>(ListBox_Properties.SelectedItems.Count);
                        foreach (object obj in ListBox_Properties.SelectedItems)
                        {
                            properties.Add((ModelCode)obj);
                        }

                        ResourceDescription resourse = testGDA.GetValues((long)ComboBox_GID.SelectedItem);
                        List<long> gids = new List<long>();

                        for (int i = 0; i < resourse.Properties.Count; i++)
                        {
                            if (resourse.Properties[i].Id == mcProp)
                            {
                                gids = resourse.Properties[i].PropertyValue.LongValues;
                                gids.Add(resourse.Properties[i].PropertyValue.LongValue);
                                break;
                            }
                        }
                        type = modelResourcesDesc.GetModelCodeFromId(gids[0]);
                        asoc.PropertyId = mcProp;
                        asoc.Type = type;

                        List<ResourceDescription> rds = testGDA.GetRelatedValues(long.Parse(ComboBox_GID.SelectedItem.ToString()), asoc, properties);
                        string text = "";

                        foreach (ResourceDescription rd in rds)
                        {
                            text += "PROPERTY VALUES FOR:\nType: " + ComboBox_Types.SelectedItem.ToString() + "   GID: " + rd.Id + "\n\n";
                            foreach (Property p in rd.Properties)
                            {
                                string propValue = GetPropertyValue(p);
                                text += p.Id + ":   " + propValue + "\n";
                            }
                            text += "\n";
                        }
                        RichTextBox_Result.Document.Blocks.Clear();
                        RichTextBox_Result.Document.Blocks.Add(new Paragraph(new Run(text)));
                    }
                    else
                    {
                        MessageBox.Show("Please select type, one GID, one associated property for object of selected type and at leat one property for associated type.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select type, one GID, one associated property for object of selected type and at leat one property for associated type.");
                }
            }
            else if (RadioButton_GEV.IsChecked == true)
            {
                //GetExtentValues
                if (ComboBox_Types.SelectedItem != null && ListBox_Properties.SelectedItems != null)
                {
                    if (!ComboBox_Types.SelectedItem.ToString().Trim().Equals(String.Empty) && ListBox_Properties.SelectedItems.Count > 0)
                    {
                        List<ResourceDescription> rds = testGDA.GetExtentValues(modelCode);
                        string text = String.Empty;

                        foreach (ResourceDescription rd in rds)
                        {
                            text += "PROPERTY VALUES FOR:\nType: " + ComboBox_Types.SelectedItem.ToString() + "   GID: " + rd.Id + "\n\n";
                            foreach (Property p in rd.Properties)
                            {
                                if (ListBox_Properties.SelectedItems.Contains(p.Id))
                                {
                                    string propValue = GetPropertyValue(p);
                                    text += p.Id + ":   " + propValue + "\n";
                                }

                            }
                            text += "---------------------------------------------------------------------------------------\n";
                        }
                        RichTextBox_Result.Document.Blocks.Clear();
                        RichTextBox_Result.Document.Blocks.Add(new Paragraph(new Run(text)));
                    }
                    else
                    {
                        MessageBox.Show("Please select type and at leat one property for specified type.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select type and at leat one property for specified type.");
                }
            }
        }
    }
}
