using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YBCG_FD_Grab
{
    public partial class Main_Form : Form
    {
        private ChromiumWebBrowser chromeBrowser;
        private JObject __jo;
        private bool __isLogin = false;
        private bool __isClose;
        private bool m_aeroEnabled;
        private int __send = 0;
        private int __total_player = 0;
        private string __brand_code = "YBCG";
        private string __brand_color = "#EE2224";
        private string __app = "FD Grab";
        private string __app_type = "1";
        private string __player_last_bill_no = "";
        private string _playerlist_cn = "";
        private string _playerlist_cn_pending = "";
        private string __last_username = "";
        private string __last_username_pending = "";
        private string __url = "";
        private string __auth = "";
        private string __end_time = "";
        private string __start_time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        List<string> __deposit_payment_type = new List<string>();
        Form __mainFormHandler;

        // Drag Header to Move
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        // ----- Drag Header to Move

        // Form Shadow
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        // ----- Form Shadow

        public Main_Form()
        {
            InitializeComponent();

            timer_landing.Start();
        }

        // Drag to Move
        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

            //Properties.Settings.Default.______last_bill_no = "";
            //Properties.Settings.Default.Save();
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_loader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_brand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_retry_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----- Drag to Move

        // Click Close
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the program?", "YBCG FD Grab", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                __isClose = true;
                Environment.Exit(0);
            }
        }

        // Click Minimize
        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Form Closing
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!__isClose)
            {
                DialogResult dr = MessageBox.Show("Exit the program?", "YBCG FD Grab", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            Environment.Exit(0);
        }

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            InitializeChromium();

            try
            {
                label1.Text = Properties.Settings.Default.______pending_bill_no;
            }
            catch (Exception err)
            {
                SendITSupport("There's a problem to the server, please re-open the application.");
                SendMyBot(err.ToString());

                __isClose = false;
                Environment.Exit(0);
            }
        }

        // CefSharp Initialize
        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser("https://bo.yongbao66.com/login");
            panel_cefsharp.Controls.Add(chromeBrowser);
            chromeBrowser.AddressChanged += ChromiumBrowserAddressChanged;
        }

        static int LineNumber([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }

        // CefSharp Address Changed
        private void ChromiumBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            __url = e.Address.ToString();
            if (e.Address.ToString().Equals("https://bo.yongbao66.com/login"))
            {
                if (!String.IsNullOrEmpty(__end_time))
                {
                    //SendITSupport("BO Back to Normal. Firing up!");
                    SendMyBot("BO Back to Normal. Firing up!");
                    __end_time = "";
                }

                Invoke(new Action(async () =>
                {
                    __isLogin = true;
                    timer_pending.Start();
                    panel_cefsharp.Visible = false;
                    label_brand.Visible = true;
                    pictureBox_loader.Visible = true;
                    label_player_last_bill_no.Visible = true;
                    await ___LoginAuthAsync();
                    await ___PlayerLastBillNoAsync();
                    await ___GetDepositPaymentTypeAsync();
                    await ___GetPlayerListsRequest();
                }));
            }
            
            if (!__isLogin && (__url.Contains("maintenance") || __url.Contains("503")))
            {
                Invoke(new Action(() =>
                {
                    __end_time = DateTime.Now.AddMinutes(5).AddSeconds(2).ToString("dd/MM/yyyy HH:mm:ss");
                    timer_retry.Start();
                    panel_cefsharp.Visible = false;
                    label_brand.Visible = true;
                    label_brand.Text = "Yong Bao [Under Maintenance]";
                    pictureBox_loader.Visible = true;
                }));
                
                if (!Properties.Settings.Default.______is_send)
                {
                    Properties.Settings.Default.______is_send = true;
                    Properties.Settings.Default.Save();
                    //SendITSupport("BO Under Maintenance.");
                    SendMyBot("BO Under Maintenance.");
                }
            }
            else if (__isLogin && !__url.Contains("maintenance") && !__url.Contains("503"))
            {
                if (Properties.Settings.Default.______is_send)
                {
                    Invoke(new Action(() =>
                    {
                        label_brand.Text = "Yong Bao";
                        timer_size.Stop();
                        __mainFormHandler = Application.OpenForms[0];
                        __mainFormHandler.Size = new Size(466, 168);
                    }));
                }

                Properties.Settings.Default.______is_send = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                //SendITSupport("There's a problem to the server, please re-open the application.");
                SendMyBot("Under Maintenance.");

                __isClose = false;
                Environment.Exit(0);
            }
        }
        
        private void timer_retry_Tick(object sender, EventArgs e)
        {            
            label_retry.Visible = true;
            
            DateTime end = DateTime.ParseExact(__end_time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime start = DateTime.ParseExact(__start_time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            TimeSpan difference = end - start;
            int hrs = difference.Hours;
            int mins = difference.Minutes;
            int secs = difference.Seconds;

            TimeSpan spinTime = new TimeSpan(hrs, mins, secs);

            TimeSpan delta = DateTime.Now - start;
            TimeSpan timeRemaining = spinTime - delta;

            if (timeRemaining.Minutes != 0)
            {
                if (timeRemaining.Seconds == 0)
                {
                    label_retry.Text = "Retry in " + timeRemaining.Minutes + ":" + timeRemaining.Seconds + "0";
                }
                else
                {
                    if (timeRemaining.Seconds.ToString().Length == 1)
                    {
                        label_retry.Text = "Retry in " + timeRemaining.Minutes + ":0" + timeRemaining.Seconds;
                    }
                    else
                    {
                        label_retry.Text = "Retry in " + timeRemaining.Minutes + ":" + timeRemaining.Seconds;
                    }
                }
            }
            else
            {
                if (label_retry.Text == "Retry in 1")
                {
                    label_retry.Visible = false;
                    label_retry.Text = "-";
                    timer_retry.Stop();
                    chromeBrowser.Load("https://bo.yongbao66.com/login");
                }
                else
                {
                    label_retry.Text = "Retry in " + timeRemaining.Seconds.ToString();
                }
            }
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        void ___CloseMessageBox()
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "JavaScript Alert - https://bo.yongbao66.com");

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void timer_landing_Tick(object sender, EventArgs e)
        {
            panel_landing.Visible = false;
            timer_landing.Stop();
            timer_size.Start();
        }

        private void timer_close_message_box_Tick(object sender, EventArgs e)
        {
            ___CloseMessageBox();
        }

        private async Task ___PlayerLastBillNoAsync()
        {
            // 04/22/19
            Properties.Settings.Default.______last_bill_no = "";

            try
            {
                if (Properties.Settings.Default.______last_bill_no == "")
                {
                    await ___GetLastBillNoAsync();
                }

                label_player_last_bill_no.Text = "Last Bill No.: " + Properties.Settings.Default.______last_bill_no;
            }
            catch (Exception err)
            {
                __send++;
                if (__send == 5)
                {
                    SendITSupport("There's a problem to the server, please re-open the application.");
                    SendMyBot(err.ToString());

                    __isClose = false;
                    Environment.Exit(0);
                }
                else
                {
                    ___WaitNSeconds(10);
                    await ___PlayerLastBillNoAsync();
                }
            }
        }

        private async Task ___GetLastBillNoAsync()
        {
            try
            {
                string password = __brand_code.ToString() + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["token"] = token
                    };

                    byte[] result = await wb.UploadValuesTaskAsync("http://192.168.10.252:8080/API/lastFDRecord", "POST", data);
                    string responsebody = Encoding.UTF8.GetString(result);
                    var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                    JObject jo = JObject.Parse(deserializeObject.ToString());
                    JToken lbn = jo.SelectToken("$.msg");
                    
                    Properties.Settings.Default.______last_bill_no = lbn.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___GetLastBillNo2Async();
                    }
                }
            }
        }

        private async Task ___GetLastBillNo2Async()
        {
            try
            {
                string password = __brand_code + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["token"] = token
                    };

                    var result = await wb.UploadValuesTaskAsync("http://zeus.ssitex.com:8080/API/lastFDRecord", "POST", data);
                    string responsebody = Encoding.UTF8.GetString(result);
                    var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                    JObject jo = JObject.Parse(deserializeObject.ToString());
                    JToken lbn = jo.SelectToken("$.msg");

                    Properties.Settings.Default.______last_bill_no = lbn.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___GetLastBillNoAsync();
                    }
                }
            }
        }

        private void ___SavePlayerLastBillNo(string bill_no)
        {
            Properties.Settings.Default.______last_bill_no = bill_no.Trim();
            Properties.Settings.Default.Save();
        }

        // ----- Functions
        private async Task ___GetPlayerListsRequest()
        {
            try
            {
                string __start_time_replace = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00");
                DateTime __start_time = DateTime.ParseExact(__start_time_replace, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                int __start_time_epoch = (int)(__start_time.AddHours(16) - new DateTime(1970, 1, 1)).TotalSeconds;

                string __end_time_replace = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                DateTime __end_time = DateTime.ParseExact(__end_time_replace, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                int __end_time_epoch = (int)(__end_time.AddHours(16) - new DateTime(1970, 1, 1)).TotalSeconds;

                WebClient wc = new WebClient(); string value = wc.Headers["Authorization"];
                wc.Encoding = Encoding.UTF8;
                wc.UseDefaultCredentials = false;
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                wc.Headers.Add("Referer", "https://bo.yongbao66.com/deposit");
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                wc.Headers[HttpRequestHeader.Authorization] = __auth;
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("Content-Type", "application/json");
                byte[] result = await wc.DownloadDataTaskAsync("https://boapi.yongbao66.com/yongbao-ims/api/v1/deposits/search?endtime=" + __end_time_epoch + "999&language=1&limit=100000&offset=0&sort=DESC&sortcolumn=deposittime&starttime=" + __start_time_epoch + "000&statusall=true");
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                __jo = JObject.Parse(deserializeObject.ToString());
                JToken count = __jo.SelectToken("$.data");
                __total_player = count.Count();
                await ___PlayerListAsync();
                __send = 0;
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___GetPlayerListsRequest();
                    }
                }
            }
        }

        private async Task ___PlayerListAsync()
        {
            List<string> player_info = new List<string>();

            for (int i = 0; i < __total_player; i++)
            {
                JToken bill_no = __jo.SelectToken("$.data[" + i + "].depositid").ToString();
                if (bill_no.ToString().Trim() != Properties.Settings.Default.______last_bill_no)
                {
                    JToken status = __jo.SelectToken("$.data[" + i + "].status").ToString();
                    if (status.ToString() != "1" || status.ToString() != "2")
                    {
                        if (i == 0)
                        {
                            __player_last_bill_no = bill_no.ToString().Trim();
                        }
                        
                        JToken username = __jo.SelectToken("$.data[" + i + "].playerid").ToString();
                        string _playerlist_cn = await ___PlayerListContactNumberAsync(username.ToString());
                        JToken name = __jo.SelectToken("$.data[" + i + "].firstname").ToString();
                        JToken date_deposit = __jo.SelectToken("$.data[" + i + "].audittime").ToString();
                        DateTime date_deposit_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble("1553642608803") / 1000d)).ToLocalTime();
                        if (!String.IsNullOrEmpty(date_deposit.ToString()))
                        {
                            date_deposit_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(date_deposit.ToString()) / 1000d)).ToLocalTime();
                            date_deposit = date_deposit_replace.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            date_deposit = "";
                        }
                        JToken process_datetime = __jo.SelectToken("$.data[" + i + "].deposittime").ToString();
                        DateTime process_datetime_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(process_datetime) / 1000d)).ToLocalTime();
                        process_datetime = process_datetime_replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken vip = __jo.SelectToken("$.data[" + i + "].groupname").ToString();
                        JToken method = __jo.SelectToken("$.data[" + i + "].depositpaymenttype").ToString();
                        for (int ii = 0; ii < __deposit_payment_type.Count; ii++)
                        {
                            string[] _deposit_payment_type_replace = __deposit_payment_type[ii].Replace("_", " ").Split('|');
                            if (_deposit_payment_type_replace[1] == method.ToString())
                            {
                                method = _deposit_payment_type_replace[0];
                            }
                        }
                        if (method.ToString() == "")
                        {
                            SendMyBot("New Deposit Payment Type Detected.");

                            await ___GetDepositPaymentTypeAsync();
                            
                            for (int ii = 0; ii < __deposit_payment_type.Count; ii++)
                            {
                                string[] _deposit_payment_type_replace = __deposit_payment_type[ii].Replace("_", " ").Split('|');
                                if (_deposit_payment_type_replace[1] == method.ToString())
                                {
                                    method = _deposit_payment_type_replace[0];
                                }
                            }
                        }
                        JToken gateway = __jo.SelectToken("$.data[" + i + "].thirdpartypaymentstaticname").ToString();
                        JToken amount = __jo.SelectToken("$.data[" + i + "].receiveddepositamt").ToString().Replace(",", "");
                        JToken pg_bill_no = __jo.SelectToken("$.data[" + i + "].remarks").ToString();
                        if (status.ToString() == "3")
                        {
                            status = "1";
                        }
                        else
                        {
                            amount = "0";
                            status = "0";
                        }

                        player_info.Add(username + "*|*" + name + "*|*" + process_datetime + "*|*" + vip + "*|*" + amount + "*|*" + gateway + "*|*" + status + "*|*" + bill_no + "*|*" + _playerlist_cn + "*|*" + date_deposit + "*|*" + method + "*|*" + pg_bill_no);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            __player_last_bill_no = bill_no.ToString().Trim();
                        }

                        JToken username = __jo.SelectToken("$.data[" + i + "].playerid").ToString();
                        string _playerlist_cn = await ___PlayerListContactNumberAsync(username.ToString());
                        JToken name = __jo.SelectToken("$.data[" + i + "].firstname").ToString();
                        JToken date_deposit = __jo.SelectToken("$.data[" + i + "].audittime").ToString();
                        DateTime date_deposit_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble("1553642608803") / 1000d)).ToLocalTime();
                        if (!String.IsNullOrEmpty(date_deposit.ToString()))
                        {
                            date_deposit_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(date_deposit.ToString()) / 1000d)).ToLocalTime();
                            date_deposit = date_deposit_replace.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            date_deposit = "";
                        }
                        JToken process_datetime = __jo.SelectToken("$.data[" + i + "].deposittime").ToString();
                        DateTime process_datetime_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(process_datetime) / 1000d)).ToLocalTime();
                        process_datetime = process_datetime_replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken vip = __jo.SelectToken("$.data[" + i + "].groupname").ToString();
                        JToken method = __jo.SelectToken("$.data[" + i + "].depositpaymenttype").ToString();
                        for (int ii = 0; ii < __deposit_payment_type.Count; ii++)
                        {
                            string[] _deposit_payment_type_replace = __deposit_payment_type[ii].Replace("_", " ").Split('|');
                            if (_deposit_payment_type_replace[1] == method.ToString())
                            {
                                method = _deposit_payment_type_replace[0];
                            }
                        }
                        if (method.ToString() == "")
                        {
                            SendMyBot("New Deposit Payment Type Detected.");

                            await ___GetDepositPaymentTypeAsync();

                            for (int ii = 0; ii < __deposit_payment_type.Count; ii++)
                            {
                                string[] _deposit_payment_type_replace = __deposit_payment_type[ii].Replace("_", " ").Split('|');
                                if (_deposit_payment_type_replace[1] == method.ToString())
                                {
                                    method = _deposit_payment_type_replace[0];
                                }
                            }
                        }
                        JToken gateway = __jo.SelectToken("$.data[" + i + "].thirdpartypaymentstaticname").ToString();
                        string amount = "0";
                        JToken pg_bill_no = __jo.SelectToken("$.data[" + i + "].remarks").ToString();
                        status = "2";

                        player_info.Add(username + "*|*" + name + "*|*" + process_datetime + "*|*" + vip + "*|*" + amount + "*|*" + gateway + "*|*" + status + "*|*" + bill_no + "*|*" + _playerlist_cn + "*|*" + date_deposit + "*|*" + method + "*|*" + pg_bill_no);

                        bool isContains = false;
                        char[] split = "*|*".ToCharArray();
                        string[] values = Properties.Settings.Default.______pending_bill_no.Split(split);
                        foreach (var value in values)
                        {
                            if (value != "")
                            {
                                if (bill_no.ToString() == value)
                                {
                                    isContains = true;
                                    break;
                                }
                                else
                                {
                                    isContains = false;
                                }
                            }
                        }

                        if (!isContains)
                        {
                            Properties.Settings.Default.______pending_bill_no += bill_no + "*|*";
                            label1.Text = Properties.Settings.Default.______pending_bill_no;
                            Properties.Settings.Default.Save();
                        }
                        else if (Properties.Settings.Default.______pending_bill_no == "")
                        {
                            Properties.Settings.Default.______pending_bill_no += bill_no + "*|*";
                            label1.Text = Properties.Settings.Default.______pending_bill_no;
                            Properties.Settings.Default.Save();
                        }
                    }
                }
                else
                {
                    // send to api
                    if (player_info.Count != 0)
                    {
                        player_info.Reverse();
                        string player_info_get = String.Join(",", player_info);
                        char[] split = ",".ToCharArray();
                        string[] values = player_info_get.Split(split);
                        foreach (string value in values)
                        {
                            Application.DoEvents();
                            string[] values_inner = value.Split(new string[] { "*|*" }, StringSplitOptions.None);
                            int count = 0;
                            string _username = "";
                            string _name = "";
                            string _date_deposit = "";
                            string _vip = "";
                            string _amount = "";
                            string _gateway = "";
                            string _status = "";
                            string _bill_no = "";
                            string _contact_no = "";
                            string _process_datetime = "";
                            string _method = "";
                            string _pg_bill_no = "";

                            foreach (string value_inner in values_inner)
                            {
                                count++;

                                // Username
                                if (count == 1)
                                {
                                    _username = value_inner;
                                }
                                // Name
                                else if (count == 2)
                                {
                                    _name = value_inner;
                                }
                                // Deposit Date
                                else if (count == 3)
                                {
                                    _date_deposit = value_inner;
                                }
                                // VIP
                                else if (count == 4)
                                {
                                    _vip = value_inner;
                                }
                                // Amount
                                else if (count == 5)
                                {
                                    _amount = value_inner;
                                }
                                // Gateway
                                else if (count == 6)
                                {
                                    _gateway = value_inner;
                                }
                                // Status
                                else if (count == 7)
                                {
                                    _status = value_inner;
                                }
                                // Bill No
                                else if (count == 8)
                                {
                                    _bill_no = value_inner;
                                }
                                // Contact No
                                else if (count == 9)
                                {
                                    _contact_no = value_inner;
                                }
                                // Process Time
                                else if (count == 10)
                                {
                                    _process_datetime = value_inner;
                                }
                                // Method
                                else if (count == 11)
                                {
                                    _method = value_inner;
                                }
                                // PG Bill No
                                else if (count == 12)
                                {
                                    _pg_bill_no = value_inner;
                                }
                            }

                            // ----- Insert Data
                            using (StreamWriter file = new StreamWriter(Path.GetTempPath() + @"\fdgrab_ybcg.txt", true, Encoding.UTF8))
                            {
                                file.WriteLine(_username + "*|*" + _name + "*|*" + _contact_no + "*|*" + _date_deposit + "*|*" + _vip + "*|*" + _amount + "*|*" + _gateway + "*|*" + _status + "*|*" + _bill_no + "*|*" + _process_datetime + "*|*" + _method + "*|*" + _pg_bill_no);
                                file.Close();
                            }
                            
                            // 04/22/19
                            SendMyBot(_username + " --- " + _name + " --- " + _date_deposit + " --- " + _vip + " --- " + _amount + " --- " + _gateway + " --- " + _status + " --- " + _bill_no + " --- " + _contact_no + " --- " + _process_datetime + " --- " + _method + " --- " + _pg_bill_no);

                            if (__last_username == _username)
                            {
                                Thread.Sleep(Properties.Settings.Default.______thread_mill);
                                await ___InsertDataAsync(_username, _name, _date_deposit, _vip, _amount, _gateway, _status, _bill_no, _contact_no, _process_datetime, _method, _pg_bill_no);
                            }
                            else
                            {
                                await ___InsertDataAsync(_username, _name, _date_deposit, _vip, _amount, _gateway, _status, _bill_no, _contact_no, _process_datetime, _method, _pg_bill_no);
                            }
                            __last_username = _username;

                            __send = 0;
                        }
                    }

                    if (!String.IsNullOrEmpty(__player_last_bill_no.Trim()))
                    {
                        ___SavePlayerLastBillNo(__player_last_bill_no);

                        Invoke(new Action(() =>
                        {
                            label_player_last_bill_no.Text = "Last Bill No.: " + Properties.Settings.Default.______last_bill_no;
                        }));
                    }

                    player_info.Clear();
                    timer.Start();
                    break;
                }
            }
        }

        private async void timer_pending_TickAsync(object sender, EventArgs e)
        {
            if (__isLogin)
            {
                timer_pending.Stop();
                char[] split = "*|*".ToCharArray();
                string[] values = Properties.Settings.Default.______pending_bill_no.Split(split);
                foreach (var value in values)
                {
                    if (value != "")
                    {
                        await ___SearchPendingAsync(value);
                    }
                }
                timer_pending.Start();
            }
        }

        private async Task ___SearchPendingAsync(string bill_no)
        {
            try
            {
                string __start_time_replace = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00");
                DateTime __start_time = DateTime.ParseExact(__start_time_replace, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                int __start_time_epoch = (int)(__start_time.AddHours(16) - new DateTime(1970, 1, 1)).TotalSeconds;

                string __end_time_replace = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                DateTime __end_time = DateTime.ParseExact(__end_time_replace, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                int __end_time_epoch = (int)(__end_time.AddHours(16) - new DateTime(1970, 1, 1)).TotalSeconds;

                WebClient wc = new WebClient(); string value = wc.Headers["Authorization"];
                wc.Encoding = Encoding.UTF8;
                wc.UseDefaultCredentials = false;
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                wc.Headers.Add("Referer", "https://bo.yongbao66.com/deposit");
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                wc.Headers[HttpRequestHeader.Authorization] = __auth;
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("Content-Type", "application/json");

                byte[] result = await wc.DownloadDataTaskAsync("https://boapi.yongbao66.com/yongbao-ims/api/v1/deposits/search?depositid=" + bill_no + "&endtime=" + __end_time + "&language=1&limit=100000&offset=0&sort=DESC&sortcolumn=deposittime&starttime=" + __start_time + "&statusall=true&timefilter=deposittime");
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JToken _jo = JObject.Parse(deserializeObject.ToString());
                JToken status = _jo.SelectToken("$.data[0].status");

                string path = Path.GetTempPath() + @"\fdgrab_ybcg_pending.txt";
                if (status.ToString() != "1" || status.ToString() != "2")
                {
                    Properties.Settings.Default.______pending_bill_no = Properties.Settings.Default.______pending_bill_no.Replace(bill_no + "*|*", "");
                    label1.Text = Properties.Settings.Default.______pending_bill_no;
                    Properties.Settings.Default.Save();
                    
                    JToken username = _jo.SelectToken("$.data[0].playerid").ToString();
                    string _playerlist_cn = await ___PlayerListContactNumberAsync(username.ToString());
                    JToken name = _jo.SelectToken("$.data[0].firstname").ToString();
                    JToken date_deposit = _jo.SelectToken("$.data[0].audittime").ToString();
                    DateTime date_deposit_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble("1553642608803") / 1000d)).ToLocalTime();
                    if (!String.IsNullOrEmpty(date_deposit.ToString()))
                    {
                        date_deposit_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(date_deposit.ToString()) / 1000d)).ToLocalTime();
                        date_deposit = date_deposit_replace.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        date_deposit = "";
                    }
                    JToken process_datetime = _jo.SelectToken("$.data[0].deposittime").ToString();
                    DateTime process_datetime_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(process_datetime) / 1000d)).ToLocalTime();
                    process_datetime = process_datetime_replace.ToString("yyyy-MM-dd HH:mm:ss");
                    JToken vip = _jo.SelectToken("$.data[0].groupname").ToString();
                    JToken method = _jo.SelectToken("$.data[0].depositpaymenttype").ToString();
                    for (int ii = 0; ii < __deposit_payment_type.Count; ii++)
                    {
                        string[] _deposit_payment_type_replace = __deposit_payment_type[ii].Replace("_", " ").Split('|');
                        if (_deposit_payment_type_replace[1] == method.ToString())
                        {
                            method = _deposit_payment_type_replace[0];
                        }
                    }
                    if (method.ToString() == "")
                    {
                        SendMyBot("New Deposit Payment Type Detected.");

                        await ___GetDepositPaymentTypeAsync();

                        for (int ii = 0; ii < __deposit_payment_type.Count; ii++)
                        {
                            string[] _deposit_payment_type_replace = __deposit_payment_type[ii].Replace("_", " ").Split('|');
                            if (_deposit_payment_type_replace[1] == method.ToString())
                            {
                                method = _deposit_payment_type_replace[0];
                            }
                        }
                    }
                    JToken gateway = _jo.SelectToken("$.data[0].thirdpartypaymentstaticname").ToString();
                    JToken amount = _jo.SelectToken("$.data[0].receiveddepositamt").ToString().Replace(",", "");
                    JToken pg_bill_no = _jo.SelectToken("$.data[0].remarks").ToString();
                    if (status.ToString() == "3")
                    {
                        status = "1";
                    }
                    else
                    {
                        amount = "0";
                        status = "0";
                    }

                    // 04/22/19
                    SendMyBot(username.ToString() + " --- " + name.ToString() + " --- " + process_datetime.ToString() + " --- " + vip.ToString() + " --- " + amount.ToString() + " --- " + gateway.ToString() + " --- " + status.ToString() + " --- " + bill_no + " --- " + _playerlist_cn_pending + " --- " + date_deposit.ToString() + " --- " + method.ToString() + " --- " + pg_bill_no.ToString());

                    if (__last_username_pending == username.ToString())
                    {
                        Thread.Sleep(Properties.Settings.Default.______thread_mill);
                        await ___InsertDataAsync(username.ToString(), name.ToString(), process_datetime.ToString(), vip.ToString(), amount.ToString(), gateway.ToString(), status.ToString(), bill_no, _playerlist_cn_pending, date_deposit.ToString(), method.ToString(), pg_bill_no.ToString());
                    }
                    else
                    {
                        await ___InsertDataAsync(username.ToString(), name.ToString(), process_datetime.ToString(), vip.ToString(), amount.ToString(), gateway.ToString(), status.ToString(), bill_no, _playerlist_cn_pending, date_deposit.ToString(), method.ToString(), pg_bill_no.ToString());
                    }
                    __last_username_pending = username.ToString();

                    __send = 0;
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());
                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___SearchPendingAsync(bill_no);
                    }
                }
            }
        }

        private async Task ___InsertDataAsync(string username, string name, string date_deposit, string vip, string amount, string gateway, string status, string bill_no, string contact_no, string process_datetime, string method, string pg_bill_no)
        {
            try
            {
                double amount_replace = Convert.ToDouble(amount);
                string password = __brand_code + username.ToLower() + date_deposit + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["username"] = username,
                        ["name"] = name,
                        ["date_deposit"] = date_deposit,
                        ["contact"] = contact_no,
                        ["vip"] = vip,
                        ["gateway"] = gateway,
                        ["brand_code"] = __brand_code,
                        ["amount"] = amount_replace.ToString("N0"),
                        ["success"] = status,
                        ["action_date"] = process_datetime,
                        ["method"] = method,
                        ["trans_id"] = bill_no,
                        ["pg_trans_id"] = pg_bill_no,
                        ["token"] = token
                    };

                    var response = await wb.UploadValuesTaskAsync("http://192.168.10.252:8080/API/sendFD", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ____InsertData2Async(username, name, date_deposit, vip, amount, gateway, status, bill_no, contact_no, process_datetime, method, pg_bill_no);
                    }
                }
            }
        }

        private async Task ____InsertData2Async(string username, string name, string date_deposit, string vip, string amount, string gateway, string status, string bill_no, string contact_no, string process_datetime, string method, string pg_bill_no)
        {
            try
            {
                double amount_replace = Convert.ToDouble(amount);
                string password = __brand_code + username.ToLower() + date_deposit + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["username"] = username,
                        ["name"] = name,
                        ["date_deposit"] = date_deposit,
                        ["contact"] = contact_no,
                        ["vip"] = vip,
                        ["gateway"] = gateway,
                        ["brand_code"] = __brand_code,
                        ["amount"] = amount_replace.ToString("N0"),
                        ["success"] = status,
                        ["action_date"] = process_datetime,
                        ["method"] = method,
                        ["trans_id"] = bill_no,
                        ["pg_trans_id"] = pg_bill_no,
                        ["token"] = token
                    };

                    var response = await wb.UploadValuesTaskAsync("http://zeus.ssitex.com:8080/API/sendFD", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___InsertDataAsync(username, name, date_deposit, vip, amount, gateway, status, bill_no, contact_no, process_datetime, method, pg_bill_no);
                    }
                }
            }
        }

        private async Task<string> ___PlayerListContactNumberAsync(string username)
        {
            try
            {
                WebClient wc = new WebClient(); string value = wc.Headers["Authorization"];
                wc.Encoding = Encoding.UTF8;
                wc.UseDefaultCredentials = false;
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                wc.Headers.Add("Referer", "https://bo.yongbao66.com/deposit");
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                wc.Headers[HttpRequestHeader.Authorization] = __auth;
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("Content-Type", "application/json");
                byte[] result = await wc.DownloadDataTaskAsync("https://boapi.yongbao66.com/yongbao-ims/api/v1/players/" + username);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JObject jo_deposit = JObject.Parse(deserializeObject.ToString());
                JToken _phone_number = jo_deposit.SelectToken("$.mobile").ToString().Trim().Replace(" ", "");
                if (!String.IsNullOrEmpty(_phone_number.ToString()))
                {
                    if (_phone_number.ToString().Substring(0, 2) == "86")
                    {
                        return _phone_number.ToString().Substring(2);
                    }
                    else
                    {
                        return _phone_number.ToString();
                    }
                }
                else
                {
                    return _phone_number.ToString();
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___PlayerListContactNumberAsync(username);
                    }
                }
            }

            return null;
        }

        private async void timer_TickAsync(object sender, EventArgs e)
        {
            timer.Stop();
            await ___GetPlayerListsRequest();
        }

        private void SendMyBot(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "772918363:AAHn2ufmP3ocLEilQ1V-IHcqYMcSuFJHx5g";
                string chatId = "@allandrake";
                string text = "-----" + __brand_code + " " + __app + "-----%0A%0AIP:%20" + Properties.Settings.Default.______server_ip + "%0ALocation:%20" + Properties.Settings.Default.______server_location + "%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
            }
            catch (Exception err)
            {
                if (err.ToString().ToLower().Contains("hexadecimal"))
                {
                    string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                    string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                    string apiToken = "772918363:AAHn2ufmP3ocLEilQ1V-IHcqYMcSuFJHx5g";
                    string chatId = "@allandrake";
                    string text = "-----" + __brand_code + " " + __app + "-----%0A%0AIP:%20192.168.10.60%0ALocation:%20192.168.10.60%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                    urlString = String.Format(urlString, apiToken, chatId, text);
                    WebRequest request = WebRequest.Create(urlString);
                    Stream rs = request.GetResponse().GetResponseStream();
                    StreamReader reader = new StreamReader(rs);
                    string line = "";
                    StringBuilder sb = new StringBuilder();
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                            sb.Append(line);
                    }

                    __isClose = false;
                    Environment.Exit(0);
                }
                else
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        SendMyBot(message);
                    }
                }
            }
        }

        private void SendITSupport(string message)
        {
            if (Properties.Settings.Default.______is_send_telegram)
            {
                try
                {
                    string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                    string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                    string apiToken = "612187347:AAE9doWWcStpWrDrfpOod89qGSxCJ5JwQO4";
                    string chatId = "@it_support_ssi";
                    string text = "-----" + __brand_code + " " + __app + "-----%0A%0AIP:%20" + Properties.Settings.Default.______server_ip + "%0ALocation:%20" + Properties.Settings.Default.______server_location + "%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                    urlString = String.Format(urlString, apiToken, chatId, text);
                    WebRequest request = WebRequest.Create(urlString);
                    Stream rs = request.GetResponse().GetResponseStream();
                    StreamReader reader = new StreamReader(rs);
                    string line = "";
                    StringBuilder sb = new StringBuilder();
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            sb.Append(line);
                        }
                    }
                }
                catch (Exception err)
                {
                    if (err.ToString().ToLower().Contains("hexadecimal"))
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                        string apiToken = "612187347:AAE9doWWcStpWrDrfpOod89qGSxCJ5JwQO4";
                        string chatId = "@it_support_ssi";
                        string text = "-----" + __brand_code + " " + __app + "-----%0A%0AIP:%20192.168.10.60%0ALocation:%20192.168.10.60%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                        urlString = String.Format(urlString, apiToken, chatId, text);
                        WebRequest request = WebRequest.Create(urlString);
                        Stream rs = request.GetResponse().GetResponseStream();
                        StreamReader reader = new StreamReader(rs);
                        string line = "";
                        StringBuilder sb = new StringBuilder();
                        while (line != null)
                        {
                            line = reader.ReadLine();
                            if (line != null)
                            {
                                sb.Append(line);
                            }
                        }

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        __send++;
                        if (__send == 5)
                        {
                            SendITSupport("There's a problem to the server, please re-open the application.");
                            SendMyBot(err.ToString());

                            __isClose = false;
                            Environment.Exit(0);
                        }
                        else
                        {
                            ___WaitNSeconds(10);
                            SendITSupport(message);
                        }
                    }
                }
            }
            else { }
        }

        private void label_player_last_bill_no_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label_player_last_bill_no_MouseClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(label_player_last_bill_no.Text.Replace("Last Bill No.: ", "").Trim());
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Visible = true;
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Visible = false;
        }

        private void timer_flush_memory_Tick(object sender, EventArgs e)
        {
            ___FlushMemory();
        }

        public static void ___FlushMemory()
        {
            Process prs = Process.GetCurrentProcess();
            try
            {
                prs.MinWorkingSet = (IntPtr)(300000);
            }
            catch (Exception err)
            {
                // leave blank
            }
        }

        private void timer_detect_running_Tick(object sender, EventArgs e)
        {
            ___DetectRunning();
        }

        private void ___DetectRunning()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://192.168.10.252:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        ___DetectRunning2();
                    }
                }
            }
        }

        private void ___DetectRunning2()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssitex.com:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        ___DetectRunning();
                    }
                }
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label1.Visible = false;

            if (Properties.Settings.Default.______is_send_telegram)
            {
                Properties.Settings.Default.______is_send_telegram = false;
                Properties.Settings.Default.Save();
                MessageBox.Show("Telegram Notification is Disabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Properties.Settings.Default.______is_send_telegram = true;
                Properties.Settings.Default.Save();
                MessageBox.Show("Telegram Notification is Enabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ___WaitNSeconds(int sec)
        {
            if (sec < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(sec);
            while (DateTime.Now < _desired)
            {
                Application.DoEvents();
            }
        }

        private async Task ___GetDepositPaymentTypeAsync()
        {
            try
            {
                WebClient wc = new WebClient(); string value = wc.Headers["Authorization"];
                wc.Encoding = Encoding.UTF8;
                wc.UseDefaultCredentials = false;
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                wc.Headers.Add("Referer", "https://bo.yongbao66.com/deposit");
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                wc.Headers[HttpRequestHeader.Authorization] = __auth;
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("Content-Type", "application/json");
                byte[] result = await wc.DownloadDataTaskAsync("https://boapi.yongbao66.com/yongbao-ims/api/v1/deposits/options");
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserializeObject.ToString());
                JToken count = _jo.SelectToken("$.depositpaymenttype");

                __deposit_payment_type.Clear();

                for (int i = 0; i < count.Count(); i++)
                {
                    JToken _id = _jo.SelectToken("$.depositpaymenttype[" + i + "].id").ToString();
                    if (_id.ToString().ToLower() != "all")
                    {
                        JToken _value = _jo.SelectToken("$.depositpaymenttype[" + i + "].value").ToString();
                        __deposit_payment_type.Add(_id + "|" + _value);
                    }
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __send++;
                    if (__send == 5)
                    {
                        SendITSupport("There's a problem to the server, please re-open the application.");
                        SendMyBot(err.ToString());

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___WaitNSeconds(10);
                        await ___GetDepositPaymentTypeAsync();
                    }
                }
            }
        }

        private async Task ___LoginAuthAsync()
        {
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            wc.Headers["X-Requested-With"] = "XMLHttpRequest";
            var data = new NameValueCollection
            {
                ["userid"] = "ybrain",
                ["password"] = "29f2a78c34a61d8205c12183197f0d64f9a05671"
            };
            string result = await wc.UploadStringTaskAsync("https://boapi.yongbao66.com/yongbao-ims/api/v1/login", "{\"userid\":\"ybrain\",\"password\":\"29f2a78c34a61d8205c12183197f0d64f9a05671\"}");
            var deserializeObject = JsonConvert.DeserializeObject(result);
            JObject _jo = JObject.Parse(deserializeObject.ToString());
            __auth = _jo.SelectToken("$.token").ToString();
        }

        private void timer_size_Tick(object sender, EventArgs e)
        {
            __mainFormHandler = Application.OpenForms[0];
            __mainFormHandler.Size = new Size(466, 168);
        }
    }
}