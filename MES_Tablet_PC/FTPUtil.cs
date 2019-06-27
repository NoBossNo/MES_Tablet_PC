using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Util
{
    public class FTPUtil {
        string ftpServerIP;

        /// <summary>
        /// FTP服务器IP
        /// </summary>
        public string FtpServerIP {
            get { return ftpServerIP; }
            set { ftpServerIP = value; }
        }
        string ftpRemotePath;

        /// <summary>
        /// 指定FTP连接成功后的当前目录, 如果不指定即默认为根目录
        /// </summary>
        public string FtpRemotePath {
            get { return ftpRemotePath; }
            set { ftpRemotePath = value; }
        }
        string ftpUserID;
        /// <summary>
        /// 用户名
        /// </summary>
        public string FtpUserID {
            get { return ftpUserID; }
            set { ftpUserID = value; }
        }
        string ftpPassword;

        /// <summary>
        /// 密码
        /// </summary>
        public string FtpPassword {
            get { return ftpPassword; }
            set { ftpPassword = value; }
        }
        string ftpURI;
        public string url;

        /// <summary>
        /// 连接FTP
        /// </summary>
        /// <param name="ftpServerIp">FTP连接地址</param>
        /// <param name="ftpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="ftpUserId">用户名</param>
        /// <param name="ftpPassword">密码</param>
        public FTPUtil(string ftpServerIp, string ftpRemotePath, string ftpUserId, string ftpPassword) {
            this.ftpServerIP = ftpServerIp;
            this.ftpRemotePath = ftpRemotePath;
            this.ftpUserID = ftpUserId;
            this.ftpPassword = ftpPassword;
            if (string.IsNullOrEmpty(ftpRemotePath)) {
                this.ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
            } else {
                this.ftpURI = "ftp://" + ftpServerIP + "/";
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string filename) {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            this.url = uri;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            Stream strm = reqFTP.GetRequestStream();
            contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0) {
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
            }
            strm.Close();
            fs.Close();
        }

        public void Upload_dz(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + DateTime.Now.ToString("yyyy-MM-dd HHmmssffff")+ fileInf.Extension;
            this.url = uri;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            Stream strm = reqFTP.GetRequestStream();
            contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0)
            {
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
            }
            strm.Close();
            fs.Close();
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string dirName, string filename) {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI+"Img/" + dirName + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            Stream strm = reqFTP.GetRequestStream();
            contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0) {
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
            }
            strm.Close();
            fs.Close();
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public string Download(string filePath, string fileName) {
            FtpWebRequest reqFTP; 
            Stream ftpStream = null;
            FtpWebResponse response=null;
            FileStream outputStream = null;
            string result = "";
            if (filePath.EndsWith("\\")) {
                result = filePath + fileName;
            } else {
                result = filePath + "\\" + fileName;
            }
            try {
                outputStream = new FileStream(result, FileMode.Create);
            } catch (Exception) {
                //if (File.Exists(result)) {
                //    int index = fileName.LastIndexOf('.');
                //    if (-1 == index) {
                //        result = result + DateTime.Now.ToString("yyMMddHHmmssff");
                //    } else {
                //        string lastStr = fileName.Substring(index);//扩展名
                //        string preStr = fileName.Substring(0, index);//文件名
                //        string filename1 = preStr + DateTime.Now.ToString("yyMMddHHmmss") + lastStr;
                //        result = result.Replace(fileName, filename1);
                //    }
                //}
                //outputStream = new FileStream(result, FileMode.Create);
            }
            
            
            try {
                
                //FileStream outputStream = new FileStream(filePath, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0) {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }


            } catch (Exception) {

                throw;
            } finally {
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }

            return result;
        }

        public MemoryStream DownloadToStream_dz(string fileurl)
        {
            
            MemoryStream data_stream = new MemoryStream();
            StreamWriter data_writer = new StreamWriter(data_stream);
            Stream ftpStream = null;
            FtpWebResponse response = null;
            FtpWebRequest reqFTP;
            

            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(fileurl));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    data_stream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ftpStream.Close();  
                response.Close();
            }

            return data_stream;
        }

        public string Download_dz(string fileurl, string outputurl)
        {
            FtpWebRequest reqFTP;
            Stream ftpStream = null;
            FtpWebResponse response = null;
            FileStream outputStream = null;
            string result = "";

            result = outputurl;
            try
            {
                outputStream = new FileStream(result, FileMode.Create);
            }
            catch (Exception)
            {
            }


            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(fileurl));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }

            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName) {
            string uri = ftpURI + fileName;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            string result = String.Empty;
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            long size = response.ContentLength;
            Stream datastream = response.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            result = sr.ReadToEnd();
            sr.Close();
            datastream.Close();
            response.Close();
        }

        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList() {
            //string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest ftp;
            ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
            ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            WebResponse response = ftp.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string line = reader.ReadLine();
            while (line != null) {
                result.Append(line);
                result.Append("\n");
                line = reader.ReadLine();
            }
            if (!string.IsNullOrEmpty(result.ToString())) {
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            return null;
        }

        /// <summary>
        /// 获取ftp服务器上的文件信息
        /// </summary>
        /// <returns>存储了所有文件信息的字符串数组</returns>
        public string[] GetFileList() {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                string line = reader.ReadLine();
                while (line != null) {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');
            } catch (Exception ex) {
                downloadFiles = null;
                return downloadFiles;
            }
        }

        /// <summary>
        /// 获取当前目录下文件列表(仅文件)
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList(string mask) {
            try {
                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                string line = reader.ReadLine();
                while (line != null) {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*") {
                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_) {
                            result.Append(line);
                            result.Append("\n");
                        }
                    } else {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            } catch {
                return null;
            }
        }

        /// <summary>
        /// 获取FTP上指定文件的大小
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string filename) {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
            } catch (Exception ex) {
                throw ex;
            }
            return fileSize;
        }

        /// <summary>
        /// 获取最后修改时间
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public DateTime GetDateTime(string filename) {
            FtpWebRequest reqFTP;

            DateTime time = DateTime.Now;
            try {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                time = response.LastModified; ;

                ftpStream.Close();
                response.Close();
            } catch (Exception ex) {
                throw ex;
            }
            return time;
        }

        /// <summary>
        /// 获取当前目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetDirectoryList() {
            string[] drectory = GetFilesDetailList();
            string m = string.Empty;
            if (drectory != null) {
                foreach (string str in drectory) {
                    if (str.ToUpper().Contains("<DIR>"))
                    {

                        m += str.Substring(str.LastIndexOf(">") + 1).TrimStart().TrimEnd() + "\n";
                    }
                    else
                    {
                        m += str.Trim() + "\n";
                    }
                   
                }

                char[] n = new char[] { '\n' };
                return m.Split(n);
            }
            return null;
        }

        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName) {
            string[] dirList = GetDirectoryList();
            if (dirList != null)
            {
                foreach (string str in dirList)
                {
                    if (str.Trim() == RemoteDirectoryName.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断当前目录下指定的文件是否存在
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        public bool FileExist(string RemoteFileName) {
            string[] fileList = GetFileList("*.*");
            if (fileList != null) {
                foreach (string str in fileList) {
                    if (str.Trim() == RemoteFileName.Trim()) {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName) {
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));
            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            ftpStream.Close();
            response.Close();
        }

        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void ReName(string currentFilename, string newFilename) {
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));
            reqFTP.Method = WebRequestMethods.Ftp.Rename;
            reqFTP.RenameTo = newFilename;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();

            ftpStream.Close();
            response.Close();
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void MovieFile(string currentFilename, string newDirectory) {
            ReName(currentFilename, newDirectory);
        }

        /// <summary>
        /// 切换当前目录
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <param name="IsRoot">true 绝对路径   false 相对路径</param> 
        public void GotoDirectory(string DirectoryName, bool IsRoot) {
            if (IsRoot) {
                ftpRemotePath = DirectoryName;
            } else {
                ftpRemotePath += DirectoryName + "/";
            }
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }
    }
}
