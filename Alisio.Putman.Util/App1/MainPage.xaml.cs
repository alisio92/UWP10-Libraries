using Alisio.Putman.UtilMethods;
using Alisio.Putman.UtilMethods.Charms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        UMCharms shareCharms = null;
        DispatcherTimer timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0,0,0,0,1);
            timer.Tick += Timer_Tick;
            timer.Start();

            GetDataDevice();
            GetDayOFWeek();
            GetMethods();
            GetScrollViewer();
            GetImageBase64();
            InitShare();
        }

        private async void GetImageBase64()
        {
            Uri uri = new Uri("ms-appx:///Assets/StoreLogo.png");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            string base64 = await UMImage.ImageToBase64(file);
            txbBase64.Text = base64;
        }

        private void GetScrollViewer()
        {
            ScrollViewer scrollviewer = UMScrollViewer.GetScrollViewer(lstScrollviewer);
            if(scrollviewer != null)
                txbScrollviewer.Text = scrollviewer.ToString();
        }

        private void GetDayOFWeek()
        {
            txbDayOfWeek.Text = UMDateTime.ChangeLanguage("nl-NL", DateTime.Today.DayOfWeek);
        }

        private void GetStringFunctions()
        {
            List<string> function = UMString.ExtractFromString(txtExtract.Text, txtExtractStart.Text, txtExtractEnd.Text);
            lstResultExtract.Items.Clear();
            foreach (string item in function)
            {
                lstResultExtract.Items.Add(item);
            }
        }

        private void GetDataDevice()
        {
            txbApplicationName.Text = UMDeviceInfo.ApplicationName;
            txbApplicationVersion.Text = UMDeviceInfo.ApplicationVersion;
            txbDeviceManufacturer.Text = UMDeviceInfo.DeviceManufacturer;
            txbDeviceModel.Text = UMDeviceInfo.DeviceModel;
            txbSystemArchitecture.Text = UMDeviceInfo.SystemArchitecture;
            txbSystemFamily.Text = UMDeviceInfo.SystemFamily;
            txbSystemVersion.Text = UMDeviceInfo.SystemVersion;
        }

        private void Timer_Tick(object sender, object e)
        {
            GetDataInternet();
        }

        private void GetDataInternet()
        {
            if (UMInternet.IsInternet())
                txbInternet.Text = "Online";
            else
                txbInternet.Text = "Ofline";

            int connectionType = UMInternet.GetConnectionType();
            if (connectionType == UMInternet.WLAN)
                txbConnectionType.Text = "WLAN";
            else if (connectionType == UMInternet.WWAN)
                txbConnectionType.Text = "WWAN";
            else
                txbConnectionType.Text = "No connection";

            txbName.Text = UMInternet.GetSSID();
        }

        private async void InitShare()
        {
            RandomAccessStreamReference thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri("http://vignette3.wikia.nocookie.net/ssb/images/2/2b/Lol-face.gif/revision/latest?cb=20100823094728"));
            List<string> fileTypes = new List<string>();
            fileTypes.Add("*.txt");
            List<IStorageItem> files = new List<IStorageItem>();
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\test.txt");
            files.Add(file);
            UMShareFiles sharefiles = new UMShareFiles(fileTypes, files);
            UMShare share = new UMShare("dit is een titel", "dit is een beschrijving", "lolololol", thumbnail, thumbnail, new Uri("http://www.google.be"), sharefiles);
            shareCharms = new UMCharms(share);
        }

        //private static string GetCallForExceptionThisMethod(MethodBase methodBase, Exception e)
        //{
        //    StackTrace trace = new StackTrace(e, true);
        //    StackFrame previousFrame = null;

        //    foreach (StackFrame frame in trace.GetFrames())
        //    {
        //        if (frame.GetMethod() == methodBase)
        //        {
        //            break;
        //        }

        //        previousFrame = frame;
        //    }

        //    return previousFrame != null ? previousFrame.GetMethod().Name : null;
        //}

        private async void ExtractFromString()
        {
            List<string> function = UMString.ExtractFromString("123AB7777CD668888AB888888CD", "AB", "CD");
            for (int i = 0; i < function.Count; i++)
            {
                MessageDialog dialog = new MessageDialog("extract: " + i + " " + function[i]);
                await dialog.ShowAsync();
            }
        }

        private async void CheckIfUrlExist()
        {
            Boolean img1 = await UMRemoteUrl.RemoteFileExists(txtCheckUrl.Text);
            if (img1)
                txbCheckUrl.Text = "Url exist";
            else
                txbCheckUrl.Text = "Url doesn't exist";
        }

        private void GetMethods()
        {
            int integer = (int)UMTypeT.GetValueMethod(typeof(TestClassA), typeof(TestClassA).AssemblyQualifiedName, "GetMultiply", new object[] { 2, 3 });
            txbValueMethod.Text = integer.ToString();

            object o = UMTypeT.GetValueField(typeof(TestClassA), typeof(TestClassA).AssemblyQualifiedName, "TekstB");
            if(o != null)
                txbFieldMethod.Text = o.ToString();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            GetStringFunctions();
        }

        private void btnCheckString_Click(object sender, RoutedEventArgs e)
        {
            Boolean isEmail = UMString.CheckIfEmail(txtEmail.Text);
            if (isEmail)
                txbEmail.Text = "Is an email.";
            else
                txbEmail.Text = "Not an email.";
        }

        private void btnCheckUrl_Click(object sender, RoutedEventArgs e)
        {
            CheckIfUrlExist();
        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            shareCharms.ShowShare();
        }
    }
}
