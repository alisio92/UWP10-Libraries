# Util library
## DataDevice
```
String applicationName = UMDeviceInfo.ApplicationName;
String applicationVersion = UMDeviceInfo.ApplicationVersion;
String deviceManufacturer = UMDeviceInfo.DeviceManufacturer;
String deviceManufacturer = UMDeviceInfo.DeviceModel;
String systemArchitecture = UMDeviceInfo.SystemArchitecture;
String systemFamily = UMDeviceInfo.SystemFamily;
String systemVersion = UMDeviceInfo.SystemVersion;
```

## Internet
```
String onlineOfline = "";
if (UMInternet.IsInternet())
	onlineOfline = "Online";
else
        onlineOfline = "Ofline";

int connectionType = UMInternet.GetConnectionType();
String connectionType = "";
if (connectionType == UMInternet.WLAN)
	connectionType = "WLAN";
else if (connectionType == UMInternet.WWAN)
        connectionType = "WWAN";
else
        connectionType = "No connection";

String ssid = UMInternet.GetSSID();
```

## String functions
```
List<string> extractedItems = UMString.ExtractFromString("{source}", "{start value}", "{end value}");
Boolean isEmail = UMString.CheckIfEmail("{source}");
```

## DateTime
```
String day = UMDateTime.ChangeLanguage("nl-NL", DateTime.Today.DayOfWeek);
```

## Remote url
```
Boolean imgExist = await UMRemoteUrl.CheckIfRemoteFileExistsAsync("{url}");
```

## Generic type
```
int integer = (int)UMTypeT.GetValueMethod(typeof("{class}"), typeof("{class}").AssemblyQualifiedName, "{name method}", new object[] { 2, 3 });
object o = UMTypeT.GetValueField(typeof("{class}"), typeof("{class}").AssemblyQualifiedName, "{name property or field}");
```

## ScrollViewer
```
ScrollViewer scrollviewer = UMScrollViewer.GetScrollViewer("{dependencyObject}");
```

## charms
```
RandomAccessStreamReference thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri("{path}"));

List<string> fileTypes = new List<string>();
fileTypes.Add("*.txt");

List<IStorageItem> files = new List<IStorageItem>();
StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("{path}");
files.Add(file);

UMShareFiles sharefiles = new UMShareFiles(fileTypes, files);

UMShare share = new UMShare("{title}", "{description}", "{text}", thumbnail, thumbnail, new Uri("http://www.google.be"), sharefiles);

UMCharms shareCharms = new UMCharms(share);
```