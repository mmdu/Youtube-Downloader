using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    // download (YouTubeVideoModel)
    public class YouTubeModel
    {
        public IEnumerable<VideoInfo> VideoInfo { get; set; }
        public string FolderPath { get; set; }    // store the folder we designated in folder Browsing 
        public string Link { get; set; }          // youtube link we tpye in   
        public string FilePath { get; set; }        // store the folder location + name of file and its extension
        public VideoInfo Video { get; set; }            // ojbect is from Youtube Extractor and contains info about the file to be download
    }

    public class YouTubeVideoModel : YouTubeModel
    {
        public  VideoDownloader VideoDownloaderType { get; set; }
        // object contain the excute methode , so wwe can download files 
        // DownloadFinished Event  we can handle when a download is finished 
        // we will use this  to enable acess to the interface after it is disabled while a file downlaods
        // DownloadProgressChange event with which updates the progress bar as a video downloads

    }

    public class YouTubeAudioModel : YouTubeModel
    {
        public AudioDownloader AudioDownloaderType { get; set; }
        // object that contian 

        // DownloadFinished Event  we can handle when a download is finished 
        // we will use this  to enable acess to the interface after it is disabled while a file downlaods
        // DownloadProgressChange event with which updates the progress bar as a video downloads
    }

}
