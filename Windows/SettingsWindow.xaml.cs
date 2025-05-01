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
using System.Windows.Shapes;
using CoordCalc.ClassLib;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SettingsWindow()
        {
            InitializeComponent();
            Title = "Settings";
            DataContext = this;
            tbDefault.Text = GlobalSettings.FloatPrecisionDefault.ToString();
            tbTranslationVector.Text = GlobalSettings.FloatPrecisionTranslationVector.ToString();
            tbScaleVector.Text = GlobalSettings.FloatPrecisionScaleVector.ToString();
            tbEulerAnglesDeg.Text = GlobalSettings.FloatPrecisionEulerAnglesDeg.ToString();
            tbEulerAnglesRad.Text = GlobalSettings.FloatPrecisionEulerAnglesRad.ToString();
            tbQuaternion.Text = GlobalSettings.FloatPrecisionQuaternion.ToString();
            tbMatrix.Text = GlobalSettings.FloatPrecisionMatrix.ToString();
            OnPropertyChanged(nameof(SettingsPathText));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbDefault.Text, out int defaultPrecision))
            {
                defaultPrecision = NormalizePrecision(defaultPrecision);
                GlobalSettings.FloatPrecisionDefault = defaultPrecision;
            }

            if (int.TryParse(tbTranslationVector.Text, out int translationVectorPrecision))
            {
                translationVectorPrecision = NormalizePrecision(translationVectorPrecision);
                GlobalSettings.FloatPrecisionTranslationVector = translationVectorPrecision;
            }

            if (int.TryParse(tbScaleVector.Text, out int scaleVectorPrecision))
            {
                scaleVectorPrecision = NormalizePrecision(scaleVectorPrecision);
                GlobalSettings.FloatPrecisionScaleVector = scaleVectorPrecision;
            }

            if (int.TryParse(tbEulerAnglesDeg.Text, out int eulerAnglesDegPrecision))
            {
                eulerAnglesDegPrecision = NormalizePrecision(eulerAnglesDegPrecision);
                GlobalSettings.FloatPrecisionEulerAnglesDeg = eulerAnglesDegPrecision;
            }

            if (int.TryParse(tbEulerAnglesRad.Text, out int eulerAnglesRadPrecision))
            {
                eulerAnglesRadPrecision = NormalizePrecision(eulerAnglesRadPrecision);
                GlobalSettings.FloatPrecisionEulerAnglesRad = eulerAnglesRadPrecision;
            }

            if (int.TryParse(tbQuaternion.Text, out int quaternionPrecision))
            {
                quaternionPrecision = NormalizePrecision(quaternionPrecision);
                GlobalSettings.FloatPrecisionQuaternion = quaternionPrecision;
            }

            if (int.TryParse(tbMatrix.Text, out int matrixPrecision))
            {
                matrixPrecision = NormalizePrecision(matrixPrecision);
                GlobalSettings.FloatPrecisionMatrix = matrixPrecision;
            }

            GlobalSettings.SaveSettings();
            Close();
        }

        private int NormalizePrecision(int precision)
        {
            if (precision < 0)
                return 0;
            else if (precision > 10)
                return 10;
            else
                return precision;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string SettingsPathText
        {
            get { return "Settings file: " + GlobalSettings.SettingsFilePath; }
        }
    }
}
