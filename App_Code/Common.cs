using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;


    public static class Common
    {
        #region "Enums"     

        public enum UserRole
        {
            Admin = 1,
            User = 2
        }

        #endregion

        #region "Properties"

        public static Exception ServerLastException { get; set; }

        /// <summary>
        /// Get Application path 
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["ApplicationPath"]);
            }
        }

        public static string CentralSystemUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["CentralSystemUrl"]);
            }
        }

        public static string GetAppPath
        {
            get
            {
                if (HttpContext.Current.Request.ApplicationPath == "/")
                {
                    if (HttpContext.Current.Request.Url.Port != null && HttpContext.Current.Request.Url.Port > 0)
                    {
                        return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    }
                    else
                    {
                        return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
                    }
                }
                else
                    if (HttpContext.Current.Request.Url.Port != null && HttpContext.Current.Request.Url.Port > 0)
                {
                    return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                }
            }
        }

        public static string EmailFormatDir { get { return HttpContext.Current.Server.MapPath("/Templates"); } }


        /// <summary>
        /// Get Error Page
        /// </summary>
        public static string ErrorPage
        {
            get
            {
                return ConfigurationManager.AppSettings["ErrorPage"].ToString();
            }
        }
        #endregion

        #region "Methods"

        public static int GetInteger(string value)
        {
            int data = 0;
            if (int.TryParse(value, out data))
            {
                return data;
            }
            else
            {
                return 0;
            }
        }

        public static int RemoveString(string value)
        {
            int data = 0;
            if (value.Length > 0)
            {
                value = value.Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "");
                if (int.TryParse(value, out data))
                {
                    return data;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public static string RemoveSpecialChars(string value)
        {
            if (value.Length > 0)
            {
                value = value.Replace(",", "").Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "").Replace("_", "");
                return value;
            }
            else
            {
                return "";
            }
        }

        public static string SqlClean(string value)
        {
            if (value == "")
            {
                return value;
            }
            else
            {
                value = value.Replace("'", "''");
                return value;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string GetDataBaseFriendlyString(string content)
        {
            string textString = string.Empty;
            if ((Convert.ToString(content).Trim().Length != 0))
            {
                textString = content.Replace("'", "''");
            }
            return textString;
        }
        public static string GetImageVirtualPath()
        {
            return ApplicationPath + "/Images/";
        }

        // To check dataset is valid
        public static bool CheckDataSet(DataSet ds, int tableNumber)
        {
            if (ds != null)
            {
                if (ds.Tables.Count > tableNumber)
                {
                    if (ds.Tables[tableNumber] != null)
                    {
                        if (ds.Tables[tableNumber].Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the specified file 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileTemplate(string FileName, string path)
        {
            // string dirPath = ApplicationPath + "/Templates";

            string dirPath = path;

            DirectoryInfo dir = new DirectoryInfo(dirPath);

            if (dir != null)
            {
                FileInfo[] htmFiles = dir.GetFiles("*.htm");

                string body = string.Empty;

                foreach (FileInfo item in htmFiles)
                {
                    if (item.Name.Equals(FileName))
                    {
                        StreamReader sr = item.OpenText();

                        body = sr.ReadToEnd();
                        sr.Dispose();
                        break;
                    }
                }
                return body;
            }
            else
            {
                return string.Empty;
            }
        }

        #region function cryptography
        public static string Encrypt(string texttobeencrypted)
        {
            RijndaelManaged rajindel = new RijndaelManaged();
            string password = "DC123";
            byte[] plaintext = System.Text.Encoding.Unicode.GetBytes(texttobeencrypted);
            byte[] salt = Encoding.ASCII.GetBytes(password.Length.ToString());
            PasswordDeriveBytes secreatekey = new PasswordDeriveBytes(password, salt);
            ICryptoTransform encryptor = rajindel.CreateEncryptor(secreatekey.GetBytes(32), secreatekey.GetBytes(16));
            MemoryStream memorystream = new MemoryStream();
            CryptoStream cryptostream = new CryptoStream(memorystream, encryptor, CryptoStreamMode.Write);
            cryptostream.Write(plaintext, 0, plaintext.Length);
            cryptostream.FlushFinalBlock();
            byte[] CipherBytes = memorystream.ToArray();
            memorystream.Close();
            cryptostream.Clear();
            string encryptdata = Convert.ToBase64String(CipherBytes);
            return encryptdata;
        }
        #endregion

        #region decrypt data
        public static string Decrypt(string texttobedecrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = "DC123";
            string DecryptedData;
            try
            {
                texttobedecrypted = texttobedecrypted.Replace(" ", "+");
                byte[] EncryptedData = Convert.FromBase64String(texttobedecrypted);
                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = texttobedecrypted;
            }
            return DecryptedData;
        }
        #endregion

        /// <summary>
        /// Encrypt password text
        /// </summary>
        /// <param name="stringToEncrypt">string to encrypt</param>
        /// <returns>encrypted string</returns>
        public static string EncryptPassword(string stringToEncrypt)
        {

            TripleDESCryptoServiceProvider des;
            MD5CryptoServiceProvider hashmd5;
            byte[] bytPwdhash, bytBuff;

            string encrypted = "";
            //create a string to encrypt
            string original = stringToEncrypt;

            //generate an MD5 hash from the password. a hash is a one way encryption meaning once you generate the hash, you cant derive the password back from it.
            hashmd5 = new MD5CryptoServiceProvider();
            bytPwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes("EncryptDecrypt"));
            hashmd5 = null;

            //implement DES3 encryption
            des = new TripleDESCryptoServiceProvider();

            //the key is the secret password hash.
            des.Key = bytPwdhash;

            des.Mode = CipherMode.ECB; //CBC, CFB

            bytBuff = ASCIIEncoding.ASCII.GetBytes(original);

            encrypted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(bytBuff, 0, bytBuff.Length));

            //cleanup
            des = null;

            return encrypted;


        }

        /// <summary>
        /// Decrypt password text
        /// </summary>
        /// <param name="stringToDecrypt">string to decrypt</param>
        /// <returns>decrypted string</returns>
        public static string DecryptPassword(string stringToDecrypt)
        {
            //stringToDecrypt.Replace("%3d", "=");

            TripleDESCryptoServiceProvider des;
            MD5CryptoServiceProvider hashmd5;
            byte[] bytPwdhash, bytBuff;
            string decrypted = "";

            stringToDecrypt = stringToDecrypt.Replace(" ", "+");
            //generate an MD5 hash from the password. 
            //a hash is a one way encryption meaning once you generate
            //the hash, you cant derive the password back from it.
            hashmd5 = new MD5CryptoServiceProvider();
            bytPwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes("EncryptDecrypt"));
            hashmd5 = null;

            //implement DES3 encryption
            des = new TripleDESCryptoServiceProvider();

            //the key is the secret password hash.
            des.Key = bytPwdhash;

            des.Mode = CipherMode.ECB; //CBC, CFB

            //----- decrypt an encrypted string ------------

            bytBuff = Convert.FromBase64String(stringToDecrypt);

            //decrypt DES 3 encrypted byte buffer and return ASCII string
            decrypted = ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(bytBuff, 0, bytBuff.Length));

            //cleanup
            des = null;

            return decrypted;
        }

        #endregion


        /// <summary>
        /// Sends an mail message asynchronously.
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="toAddress">Recepient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="ccAddress">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>

        public static void SendMailMessage(string toAddress, string subject, string body, string[] attachments = null)
        {
            string fromEmail = "";
            string server = "";
            int emailPort = 0;
            string userName = "";
            string password = "";
            Boolean sslEnabled = false;

            fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            server = ConfigurationManager.AppSettings["host"].ToString();
            emailPort = Convert.ToInt32(ConfigurationManager.AppSettings["hostPort"]);
            userName = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            password = ConfigurationManager.AppSettings["FromPassword"].ToString();
            sslEnabled = true;

            MailAddress from = new MailAddress(fromEmail, "Subject");
            MailAddress to = new MailAddress(toAddress, "");
            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();

                if (attachments != null && attachments.Length > 0)
                {
                    foreach (string item in attachments)
                    {
                        mm.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }
                }

                smtp.Host = server;
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = emailPort;
                smtp.Send(mm);
            }
        }


        /// <summary>
        /// Determines if GZip is supported
        /// </summary>
        /// <returns></returns>
        public static bool IsGZipSupported()
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(AcceptEncoding) &&
                 AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
                return true;
            return false;
        }

        /// <summary>
        /// Sets up the current page or handler to use GZip through a Response.Filter
        /// IMPORTANT:  
        /// You have to call this method before any output is generated!
        /// </summary>
        public static void GZipEncodePage()
        {
            HttpResponse Response = HttpContext.Current.Response;

            if (IsGZipSupported())
            {
                string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

                if (AcceptEncoding.Contains("deflate"))
                {
                    Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                                               System.IO.Compression.CompressionMode.Compress);
                    Response.AppendHeader("Content-Encoding", "deflate");
                }
                else
                {
                    Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                                              System.IO.Compression.CompressionMode.Compress);
                    Response.AppendHeader("Content-Encoding", "gzip");
                }
            }

            // Allow proxy servers to cache encoded and unencoded versions separately
            Response.AppendHeader("Vary", "Content-Encoding");
        }

        /// <summary>
        /// This will deseralize xml to proper object of a given type
        /// </summary>
        /// <param name="input">String, Input XML</param>
        /// <param name="toType">Type of Object</param>
        /// <returns>Object of Given Type</returns>
        public static object Deserialize(string input, Type toType)
        {
            XmlSerializer ser = new XmlSerializer(toType);

            using (StringReader sr = new StringReader(input))
                return ser.Deserialize(sr);
        }

        /// <summary>
        /// This will deseralize object of a given type to xml
        /// </summary>
        /// <param name="input">Object of a Type</param>
        /// <param name="toType">Type</param>
        /// <returns>XML</returns>
        public static string Serialize(object input, Type toType)
        {
            XmlSerializer serializer = new XmlSerializer(toType);

            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, input);
            return stringwriter.ToString();
        }
        public static Dictionary<string, object> GetParams(XmlDocument doc)
        {
            Dictionary<string, object> paramCollection = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            if (doc.DocumentElement.HasChildNodes)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name.ToLower().Contains("arrayof"))
                    {

                    }
                    else
                    {
                        if (node.HasChildNodes)
                        {
                            if (node.ChildNodes.Count == 1)
                            {
                                paramCollection.Add(node.Name.ToLower(), node.ChildNodes[0].Value);
                            }
                            else
                            {
                                //it is a array
                                if (node.ChildNodes[0].Name.ToLower() == "string")
                                {
                                    string[] arr = new string[node.ChildNodes.Count];
                                    for (int i = 0; i < node.ChildNodes.Count; i++)
                                    {
                                        if (node.ChildNodes[i].HasChildNodes)
                                        {
                                            arr[i] = node.ChildNodes[i].LastChild.Value;
                                        }
                                    }

                                    paramCollection.Add(node.Name.ToLower(), arr);
                                }
                                else if (node.ChildNodes[0].Name.ToLower() == "int")
                                {
                                    int[] arr = new int[node.ChildNodes.Count];
                                    for (int i = 0; i < node.ChildNodes.Count; i++)
                                    {
                                        if (node.ChildNodes[i].HasChildNodes)
                                        {
                                            arr[i] = Convert.ToInt32(node.ChildNodes[i].LastChild.Value);
                                        }
                                    }
                                    paramCollection.Add(node.Name.ToLower(), arr);
                                }
                                else if (node.ChildNodes[0].Name.ToLower() == "object")
                                {
                                    object[] arr = new object[node.ChildNodes.Count];
                                    for (int i = 0; i < node.ChildNodes.Count; i++)
                                    {
                                        if (node.ChildNodes[i].HasChildNodes)
                                        {
                                            arr[i] = node.ChildNodes[i].LastChild.Value;
                                        }
                                    }
                                    paramCollection.Add(node.Name.ToLower(), arr);
                                }
                                else if (node.ChildNodes[0].Name.ToLower() == "bool")
                                {
                                    bool[] arr = new bool[node.ChildNodes.Count];
                                    for (int i = 0; i < node.ChildNodes.Count; i++)
                                    {
                                        if (node.ChildNodes[i].HasChildNodes)
                                        {
                                            arr[i] = Convert.ToBoolean(node.ChildNodes[i].LastChild.Value);
                                        }
                                    }
                                    paramCollection.Add(node.Name.ToLower(), arr);
                                }
                                else if (node.ChildNodes[0].Name.ToLower() == "datetime")
                                {
                                    DateTime[] arr = new DateTime[node.ChildNodes.Count];
                                    for (int i = 0; i < node.ChildNodes.Count; i++)
                                    {
                                        if (node.ChildNodes[i].HasChildNodes)
                                        {
                                            arr[i] = Convert.ToDateTime(node.ChildNodes[i].LastChild.Value);
                                        }
                                    }
                                    paramCollection.Add(node.Name.ToLower(), arr);
                                }
                            }
                        }
                        else
                        {
                            paramCollection.Add(node.Name.ToLower(), "");
                        }
                    }
                }
            }

            return paramCollection;
        }

        public static string GetLowerParam(string param)
        {
            return param.ToLower();
        }

        public static void BindDropDown<T>(System.Web.UI.WebControls.DropDownList ddl, List<T> data, string dataTextField, string dataValueField, string defaultText, string defaultValue = "0")
        {
            ddl.DataSource = data;
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataBind();
            if (defaultText.Length > 0)
            {
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(defaultText, defaultValue));
            }
        }
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    object value = dr[column.ColumnName];
                    if (value == DBNull.Value)
                    {
                        value = null;
                    }
                    if (pro.Name == column.ColumnName)
                    {
                        pro.SetValue(obj, value, null);
                        break;
                    }
                }
            }
            return obj;
        }
    }