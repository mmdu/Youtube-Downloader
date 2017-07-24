using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        private object folderBrowserDiaglod1;

        public frmYTDownloader()
        {
            InitializeComponent();
            cboFileType.SelectedIndex = 0; // set video as first choice 
            // line below gets path to mydocuments
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // set the path of browser dialog box 
            folderBrowserDialog1.SelectedPath = folder;
        }


        private void btnDownloadFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtDownloadFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

            Tuple<bool, string> isLinkGood = ValidateLink();
            MessageBox.Show("Is it good link ?" + isLinkGood.Item1 + "Link is : " + isLinkGood.Item2);



            //// Our test youtube link ************************************************************************          
            ////string link = "https://www.youtube.com/watch?v=xjo1FCW9_kM&list=RDpHdm3K2qEzU&index=22";
            //string link = "https://www.youtube.com/watch?v=pv-6rweZR_s";


            ///*
            // * Get the available video formats.
            // * We'll work with them in the video and audio download examples.
            // */
            //IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);


            ////*Select the first .mp4 video with 360p resolution

            //VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            ///*
            // * If the video has a decrypted signature, decipher it
            // */
            //if (video.RequiresDecryption)
            //{
            //    DownloadUrlResolver.DecryptDownloadUrl(video);
            //}

            ///*
            // * Create the video downloader.
            // * The first argument is the video to download.
            // * The second argument is the path to save the video file.
            // */
            //var videoDownloader = new VideoDownloader(video,
            //    Path.Combine(txtDownloadFolder.Text, video.Title + video.VideoExtension));

            //// Register the ProgressChanged event and print the current progress
            //// videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            ///*
            // * Execute the video downloader.
            // * For GUI applications note, that this method runs synchronously.
            // */
            //videoDownloader.Execute();

            //*******************************************************************************
            //******************************************************************************

            //           // Our test youtube link
            //           string link = "https://www.youtube.com/watch?v=pHdm3K2qEzU&list=RDpHdm3K2qEzU";

            //           /*
            //            * Get the available video formats.
            //            * We'll work with them in the video and audio download examples.
            //            */
            //           IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            //           /*
            //* We want the first extractable video with the highest audio quality.
            //*/
            //           VideoInfo video = videoInfos
            //               .Where(info => info.CanExtractAudio)
            //               .OrderByDescending(info => info.AudioBitrate)
            //               .FirstOrDefault();

            //           /*
            //            * If the video has a decrypted signature, decipher it
            //            */
            //           if (video.RequiresDecryption)
            //           {
            //               DownloadUrlResolver.DecryptDownloadUrl(video);
            //           }

            //           /*
            //            * Create the audio downloader.
            //            * The first argument is the video where the audio should be extracted from.
            //            * The second argument is the path to save the audio file.
            //            */
            //           var audioDownloader = new AudioDownloader(video, Path.Combine(txtDownloadFolder.Text, video.Title + video.AudioExtension));

            //           // Register the progress events. We treat the download progress as 85% of the progress and the extraction progress only as 15% of the progress,
            //           // because the download will take much longer than the audio extraction.
            //           //audioDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage * 0.85);
            //           //audioDownloader.AudioExtractionProgressChanged += (sender, args) => Console.WriteLine(85 + args.ProgressPercentage * 0.15);

            //           /*
            //            * Execute the audio downloader.
            //            * For GUI applications note, that this method runs synchronously.
            //            */
            //           audioDownloader.Execute();
            //       }

            //       private void frmYTDownloader_Load(object sender, EventArgs e)
            //       {

            //       }

            //       private void txtLink_TextChanged(object sender, EventArgs e)
            //       {

            //       }

            //       private void cboFileType_SelectedIndexChanged(object sender, EventArgs e)
            //       {

            //       }

            //       private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
            //       {

            //       }

            // Our test youtube link

        }

        private Tuple<bool, string> ValidateLink()
        {
            string normalURL;
            if(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text,out normalURL))
            {
                return Tuple.Create(true, normalURL); // return true and acutla link if succesfull

            }
            else
            {
                MessageBox.Show("Please enter a valid youtube link ");
                return Tuple.Create(false, "");
            }
        }
    }
}
