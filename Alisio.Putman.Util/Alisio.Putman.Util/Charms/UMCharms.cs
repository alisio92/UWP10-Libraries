using Alisio.Putman.UtilMethods.ErrorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Alisio.Putman.UtilMethods.Charms
{
    public class UMCharms
    {
        UMShare Share = null;
        private static UMErrorHandler ErrorMessage { get; set; }

        public static String GetErrorMessage()
        {
            if (ErrorMessage != null)
                return ErrorMessage.Error;

            return null;
        }

        public UMCharms(UMShare share)
        {
            this.Share = share;
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.ShareImageHandler);
        }

        private void ShareImageHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            DataRequestDeferral deferral = request.GetDeferral();

            try
            {
                if (Share.Thumbnail != null)
                    request.Data.Properties.Thumbnail = Share.Thumbnail;
                if (Share.Image != null)
                    request.Data.SetBitmap(Share.Image);
                if (Share.Title != null)
                    request.Data.Properties.Title = Share.Title;
                if (Share.Description != null)
                    request.Data.Properties.Description = Share.Description;
                if (Share.Text != null)
                    request.Data.SetText(Share.Text);
                if (Share.Link != null)
                    request.Data.SetWebLink(Share.Link);
                if (Share.Files != null)
                {
                    if(Share.Files.FileTypes != null)
                        for (int i = 0; i < Share.Files.FileTypes.Count; i++)
                            request.Data.Properties.FileTypes.Add(Share.Files.FileTypes[i]);

                    if (Share.Files.Files != null)
                        if (Share.Files.Files.Count > 0)
                            request.Data.SetStorageItems(Share.Files.Files);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = new UMErrorHandler();
                ErrorMessage.Error = e.Message;
            }
            finally
            {
                deferral.Complete();
            }
        }

        public void ShowShare()
        {
            DataTransferManager.ShowShareUI();
        }
    }
}
