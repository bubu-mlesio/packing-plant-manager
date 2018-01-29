using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FtpLib;
using System.Net.Sockets;
using System.Net;

namespace packing_plant_manager
{
    public partial class Form1 : Form
    {
        Thread startOperations;
        int packingStationNumber;
        string pass;
        internal static string Form2_Message;
        bool unlock = true;
        int PORT_NO = 2201;
        string SERVER_IP = "127.0.0.1";
        static Socket clientSocket; //put here
        bool autoupdate = false;

        public Form1()
        {
            //load libary (dll) from .exe (main app)
            AppDomain.CurrentDomain.AssemblyResolve += (Object sender, ResolveEventArgs args) =>
            {
                String thisExe = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                System.Reflection.AssemblyName embeddedAssembly = new System.Reflection.AssemblyName(args.Name);
                String resourceName = thisExe + "." + embeddedAssembly.Name + ".dll";

                using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return System.Reflection.Assembly.Load(assemblyData);
                }
            };
            InitializeComponent();
            //check is exist file, when is old = remove
            if (checkModified("C:\\tmp\\log.txt"))
            {
                File.Delete("C:\\tmp\\log.txt");
            }
            loadData();
            pass = passwordGenerator();
            if(port_number.Text != "")
            {
                if(serwerIP.Text != "")
                {
                    connection.RunWorkerAsync();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //check box
            if (server.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (login.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (packingStation.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (printer.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (serwerIP.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (port_number.Text == "")
            {
                MessageBox.Show("Dane do serwera nie mogą być puste", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //run new thread
                packingStationNumber = Convert.ToInt32(packingStation.SelectedItem);
                ThreadStart ts = new ThreadStart(ftpOperations);
                startOperations = new Thread(ts);
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                startOperations.Start();
                loggingBox.Items.Add("Pakowalnio wybieram Cię!");
                saveToFile();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                //stop thread
                startOperations.Abort();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception)
            {
                loggingBox.Refresh();
            }
            loggingBox.Items.Add("Przerwanie pobierania etykiet");
            saveToFile();

        }
        //main operations
        //download .pdf and print
        private void ftpOperations()
        {
            Int64 fileSize = 0;
            bool fileProblem = false;
            using (FtpConnection ftp = new FtpConnection(server.Text, login.Text, password.Text))
            {
                try
                {
                    ftp.Open();
                    loggingBox.Invoke(new Action(delegate ()
                            {
                                loggingBox.Items.Add("Nawiązuję połączenie...");
                            }));
                    saveToFile();
                    //connect to ftp and set remote and local directory
                    ftp.Login();
                    ftp.SetCurrentDirectory("//" + packingStationNumber);
                    ftp.SetLocalDirectory("C:\\tmp");
                }
                catch (ThreadAbortException) { }
                catch (Exception e)
                {
                    loggingBox.Invoke(new Action(delegate ()
                    {
                        loggingBox.Items.Add("Błąd połączenia " + e);
                    }));
                    saveToFile();
                    btnStart.Invoke(new Action(delegate ()
                    {
                        btnStart.Enabled = true;
                    }));
                    btnStop.Invoke(new Action(delegate ()
                    {
                        btnStop.Enabled = false;
                    }));
                    startOperations.Abort();
                }
                while (true)
                {
                    loggingBox.Invoke(new Action(delegate ()
                    {
                        if (loggingBox.Items.Count > 2000)
                        {
                            loggingBox.Items.Clear();
                        }
                    }));
                    try
                    {
                        //search file on ftp
                        foreach (var file in ftp.GetFiles())
                        {
                            loggingBox.Invoke(new Action(delegate ()
                            {
                                loggingBox.Items.Add("Pobieram plik " + file.Name);
                            }));
                            saveToFile();
                            foreach (var pdfFile in Directory.GetFiles("C:\\tmp"))
                            {
                                if (pdfFile == "C:\\tmp\\" + file.Name)
                                {
                                    loggingBox.Invoke(new Action(delegate ()
                                    {
                                        loggingBox.Items.Add("Znalazłem dubla: " + file.Name);
                                    }));
                                    saveToFile();
                                    fileSize = new FileInfo("C:\\tmp\\" + file.Name).Length;
                                    fileProblem = true;
                                }
                            }
                            if (!fileProblem)
                            {
                                ftp.GetFile(file.Name, false);
                            }

                            else if (fileSize > 40000)
                            {
                                MessageBox.Show("Twoja etykieta została pobrana już wcześniej i prawdopodobnie została wysłana. Jej nazwa to " + file.Name, "WARRNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                loggingBox.Invoke(new Action(delegate ()
                                {
                                    loggingBox.Items.Add("Etykieta już jest na dysku: " + file.Name);
                                }));
                                saveToFile();
                                fileProblem = true;

                            }
                            else if (fileSize < 40000)
                            {
                                File.Delete("C:\\tmp\\" + file.Name);
                                ftp.GetFile(file.Name, false);
                                loggingBox.Invoke(new Action(delegate ()
                                {
                                    loggingBox.Items.Add("Etykieta w tmp ma zbyt mały rozmiar: " + file.Name + " i została znowu pobrana");
                                }));
                                saveToFile();
                                fileProblem = false;
                            }
                            ftp.RemoveFile(file.Name);
                            if (!fileProblem)
                            {
                                //run program to print .pdf
                                if (sumatra_checkbox.Checked == false)
                                {
                                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                    FindProgram pdfProgramName = new FindProgram();
                                    startInfo.FileName = (pdfProgramName.findPDFprogram("Adobe") + "\\Reader 11.0\\Reader\\AcroRd32.exe");
                                    startInfo.Arguments = "/s /o /t C:\\tmp\\" + file.Name + " " + printer.Text;
                                    process.StartInfo = startInfo;
                                    loggingBox.Invoke(new Action(delegate ()
                                    {
                                        loggingBox.Items.Add("Otwieram AR i wywołuję wydruk...");
                                    }));
                                    saveToFile();
                                    process.Start();
                                    Thread.Sleep(4000);
                                    process.Close();
                                }
                                else
                                {
                                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                    FindProgram pdfProgramName = new FindProgram();
                                    startInfo.FileName = (pdfProgramName.findPDFprogram("SumatraPDF") + "\\SumatraPDF.exe");
                                    startInfo.Arguments = "-silent C:\\tmp\\" + file.Name + " -print-settings fit -print-to " + printer.Text + " -exit-when-done";
                                    process.StartInfo = startInfo;
                                    loggingBox.Invoke(new Action(delegate ()
                                    {
                                        loggingBox.Items.Add("Otwieram SumatraPDF i wywołuję wydruk...");
                                    }));
                                    saveToFile();
                                    process.Start();
                                    Thread.Sleep(2000);
                                    process.Close();
                                }
                            }
                            fileProblem = false;
                        }
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception e)
                    {
                        loggingBox.Invoke(new Action(delegate ()
                        {
                            loggingBox.Items.Add("Błąd przetwarzania plików " + e);
                        }));
                        saveToFile();
                        loggingBox.Invoke(new Action(delegate ()
                        {
                            loggingBox.Items.Add("Ponowne nawiązanie połączenia");
                        }));
                        ftp.Close();
                        ftp.Open();
                        ftp.Login();
                        ftp.SetCurrentDirectory("/" + packingStationNumber);
                        ftp.SetLocalDirectory("C:\\tmp");
                        continue;
                    }
                    Thread.Sleep(750);
                    loggingBox.Invoke(new Action(delegate ()
                    {
                        loggingBox.Items.Add("[...]");
                    }));

                    loggingBox.Invoke(new Action(delegate ()
                    {
                        loggingBox.TopIndex = loggingBox.Items.Count - 1;
                    }));
                }
            }
        }
        //save log
        private void saveToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\tmp\\log.txt", true);
                sw.WriteLine("[" + DateTime.Now + "]" + "\t" + loggingBox.Items[loggingBox.Items.Count - 1]);
                sw.Close();
            }
            catch (ThreadAbortException) { }
            catch (Exception e)
            {
                loggingBox.Invoke(new Action(delegate ()
                {
                    loggingBox.Items.Add("Błąd zapisu" + e);
                }));
            }
        }
        //when form closing, cancel thread 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Czy na pewno chcesz zamknąć program?", "WARRNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                
                e.Cancel = true;
            }
            else
            {
                if(startOperations != null)
                {
                    if ((startOperations.ThreadState & ThreadState.Running) == ThreadState.Running)
                    {
                        startOperations.Abort();
                        connection.CancelAsync();
                    }
                }
            }
        }
        //load data from file
        private void loadData()
        {
            if (File.Exists("C:\\Windows\\config_packing_manager.sys"))
            {
                string[] linijki = File.ReadAllLines("C:\\Windows\\config_packing_manager.sys", Encoding.UTF8);
                for (int i = 0; i < linijki.Length; i++)
                {
                    if (linijki[i] == "")
                    {
                        continue;
                    }
                    string[] aktualnaLinijka = linijki[i].Split('=');
                    if (aktualnaLinijka[0] == "[Server]")
                    {
                        server.Text = Base64Decode(aktualnaLinijka[1]);
                    }
                    if (aktualnaLinijka[0] == "[Login]")
                    {
                        login.Text = Base64Decode(aktualnaLinijka[1]);
                    }
                    if (aktualnaLinijka[0] == "[Password]")
                    {
                        password.Text = Base64Decode(aktualnaLinijka[1]);
                    }
                    if (aktualnaLinijka[0] == "[PackingNumber]")
                    {
                        packingStation.Text = Base64Decode(aktualnaLinijka[1]);
                    }
                    if (aktualnaLinijka[0] == "[ThermalPrinter]")
                    {
                        printer.Text = Base64Decode(aktualnaLinijka[1]);
                        //printer.Text = aktualnaLinijka[1];
                    }
                    if (aktualnaLinijka[0] == "[SerwerIP]")
                    {
                        serwerIP.Text = Base64Decode(aktualnaLinijka[1]);
                    }
                    if (aktualnaLinijka[0] == "[Port]")
                    {
                        port_number.Text = Base64Decode(aktualnaLinijka[1]);
                    }
                    if (aktualnaLinijka[0] == "[SumatraPDF]")
                    {
                        if (aktualnaLinijka[1] == "False")
                            sumatra_checkbox.Checked = false;
                        else
                            sumatra_checkbox.Checked = true;
                    }
                }
                if (server.Text == "")
                {
                    btnUnlock.Text = "Zablokuj";
                    unlock = true;
                }
                else if (login.Text == "")
                {
                    btnUnlock.Text = "Zablokuj";
                    unlock = true;
                }
                else if (password.Text == "")
                {
                    btnUnlock.Text = "Zablokuj";
                    unlock = true;
                }
                else if(printer.Text == "")
                {
                    btnUnlock.Text = "Zablokuj";
                    unlock = true;
                }
                else if (port_number.Text == "")
                {
                    btnUnlock.Text = "Zablokuj";
                    unlock = true;
                }
                else if (serwerIP.Text == "")
                {
                    btnUnlock.Text = "Zablokuj";
                    unlock = true;
                }
                else
                {
                    server.Enabled = false;
                    login.Enabled = false;
                    password.Enabled = false;
                    packingStation.Enabled = false;
                    printer.Enabled = false;
                    serwerIP.Enabled = false;
                    port_number.Enabled = false;
                    saveLoginData.Enabled = false;
                    unlock = false;
                    btnUnlock.Text = "Odblokuj";
                }
            }
        }
        //save login data for ftp
        private void saveLoginData_Click(object sender, EventArgs e)
        {
            if (packingStation.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (printer.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (server.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (login.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (port_number.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (serwerIP.Text == "")
            {
                MessageBox.Show("Pola do zapisu nie mogą być puste!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (autoupdate == false)
                {
                    DialogResult dialog = MessageBox.Show("Jesteś pewny że chcesz dokonać zapisu?", "WARRNING!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        if (File.Exists("C:\\Windows\\config_packing_manager.sys"))
                        {
                            File.Delete("C:\\Windows\\config_packing_manager.sys");
                        }
                        string[] dane = { "[Server]=" + Base64Encode(server.Text), "[Login]=" + Base64Encode(login.Text), "[Password]=" + Base64Encode(password.Text), "[PackingNumber]=" + Base64Encode(packingStation.SelectedItem.ToString()), "[ThermalPrinter]=" + Base64Encode(printer.Text), "[SumatraPDF]=" + sumatra_checkbox.Checked.ToString(), "[SerwerIP]=" + Base64Encode(serwerIP.Text), "[Port]=" + Base64Encode(port_number.Text) };
                        System.IO.File.WriteAllLines("C:\\Windows\\config_packing_manager.sys", dane);
                    }
                }
                else
                {
                    if (File.Exists("C:\\Windows\\config_packing_manager.sys"))
                    {
                        File.Delete("C:\\Windows\\config_packing_manager.sys");
                    }
                    string[] dane = { "[Server]=" + Base64Encode(server.Text), "[Login]=" + Base64Encode(login.Text), "[Password]=" + Base64Encode(password.Text), "[PackingNumber]=" + Base64Encode(packingStation.SelectedItem.ToString()), "[ThermalPrinter]=" + Base64Encode(printer.Text), "[SumatraPDF]=" + sumatra_checkbox.Checked.ToString(), "[SerwerIP]=" + Base64Encode(serwerIP.Text), "[Port]=" + Base64Encode(port_number.Text) };
                    System.IO.File.WriteAllLines("C:\\Windows\\config_packing_manager.sys", dane);
                    autoupdate = false;
                }
            }
    }
        //check date modified log.txt
        private bool checkModified(string filename)
    {
        DateTime lastModified = File.GetLastWriteTime("C:\\tmp\\log.txt");
        if (lastModified.Date < DateTime.Now.Date)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
        //Unblock textbox
        private void btnUnlock_Click(object sender, EventArgs e)
    {
            if (unlock == false)
        {
            using (Form2 f2 = new Form2())
            {
                if (f2.ShowDialog() == DialogResult.OK)
                {
                        if (Form2_Message == pass)
                        {
                            server.Enabled = true;
                            login.Enabled = true;
                            password.Enabled = true;
                            packingStation.Enabled = true;
                            printer.Enabled = true;
                            saveLoginData.Enabled = true;
                            serwerIP.Enabled = true;
                            port_number.Enabled = true;
                            unlock = true;
                            btnUnlock.Text = "Zablokuj";
                        }
                        else
                        {
                            MessageBox.Show("Podano błędne hasło", "ERROR 404", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            loggingBox.Invoke(new Action(delegate ()
                            {
                                loggingBox.Items.Add("Błędne hasło!");
                            }));
                            saveToFile();
                        }
                    }
            }
        }
        else
        {
            server.Enabled = false;
            login.Enabled = false;
            password.Enabled = false;
            packingStation.Enabled = false;
            printer.Enabled = false;
            saveLoginData.Enabled = false;
                serwerIP.Enabled = false;
                port_number.Enabled = false;
            unlock = false;
            btnUnlock.Text = "Odblokuj";
        }
    }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            string dummyData = base64EncodedData.Trim().Replace(" ", "+");
            if (dummyData.Length % 4 > 0)
                dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
            byte[] byteArray = Convert.FromBase64String(dummyData);
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }
        //generate password for station
        private string passwordGenerator()
        {
            string namePC = Environment.MachineName;
            DateTime dateTime = DateTime.UtcNow.Date;

            return namePC + "+" + dateTime.ToString("dd.MM.yyyy");
        }
        private void packingStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            packingStation.Text = "";
        }
        private void packingStation_TextChanged(object sender, EventArgs e)
        {
            packingStation.Text = "";
        }
        //TODO auto update ftp data from server
        private void connection_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                PORT_NO = Int32.Parse(port_number.Text);
                if (PORT_NO < 65536)
                {
                    if (PORT_NO < 0)
                    {
                        PORT_NO = 2201;
                        loggingBox.Items.Add("Problem z portem, używam 2201");
                    }
                }
                else
                {
                    PORT_NO = 2201;
                    loggingBox.Items.Add("Problem z portem, używam 2201");
                }

            }
            catch
            {
                PORT_NO = 2201;
                loggingBox.Items.Add("Problem z portem, używam 2201");
            }
            try
            {
                SERVER_IP = serwerIP.Text;
                IPAddress.Parse(SERVER_IP);
            }
            catch
            {
                SERVER_IP = "127.0.0.1";
            }
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            loopConnect(3, 3); //for failure handling
            byte[] bytes = Encoding.ASCII.GetBytes("hello");
            clientSocket.Send(bytes);
            loggingBox.Items.Add("Wysyłam zapytanie...");
        }
        void loopConnect(int noOfRetry, int attemptPeriodInSeconds)
        {
            int attempts = 0;
            while (!clientSocket.Connected && attempts < noOfRetry)
            {
                try
                {
                    ++attempts;
                    loggingBox.Items.Add("Próbuję się łączyć");
                    IAsyncResult result = clientSocket.BeginConnect(IPAddress.Parse(SERVER_IP), PORT_NO, endConnectCallback, null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(10));
                    //result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(attemptPeriodInSeconds));
                    //System.Threading.Thread.Sleep(attemptPeriodInSeconds * 1000);
                    System.Threading.Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    loggingBox.Items.Add("Error: " + e.ToString());
                    btnStart.Enabled = true;
                    saveToFile();
                }
            }
            if (!clientSocket.Connected)
            {
                loggingBox.Items.Add("Connection attempt is unsuccessful!");
                saveToFile();
                btnStart.Enabled = true;
                this.connection.CancelAsync();
                return;
            }
        }
        private const int BUFFER_SIZE = 4096;
        private static byte[] buffer = new byte[BUFFER_SIZE]; //buffer size is limited to BUFFER_SIZE per message
        private void endConnectCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                if (clientSocket.Connected)
                {
                    loggingBox.Invoke(new Action(delegate ()
                    {
                        loggingBox.Items.Add("Jestem połączony, przesyłam dane");
                    }));
                    clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), clientSocket);
                }
                else
                {
                    loggingBox.Invoke(new Action(delegate ()
                    {
                        loggingBox.Items.Add("Nie można odebrać danych");
                    }));
                    btnStart.Invoke(new Action(delegate ()
                    {
                        btnStart.Enabled = true;
                    }));

                }
            }
            catch (Exception e)
            {
                loggingBox.Invoke(new Action(delegate ()
                {
                    loggingBox.Items.Add("Połączenie nie zostało nawiązane " + e.ToString());
                }));
                saveToFile();
                btnStart.Invoke(new Action(delegate ()
                {
                    btnStart.Enabled = true;
                }));

            }
        }
        const int MAX_RECEIVE_ATTEMPT = 10;
        static int receiveAttempt = 0;
        private void receiveCallback(IAsyncResult result)
        {
            System.Net.Sockets.Socket socket = null;
            try
            {
                socket = (System.Net.Sockets.Socket)result.AsyncState;
                if (socket.Connected)
                {
                    int received = socket.EndReceive(result);
                    if (received > 0)
                    {
                        receiveAttempt = 0;
                        byte[] data = new byte[received];
                        Buffer.BlockCopy(buffer, 0, data, 0, data.Length); //copy the data from your buffer
                        if (Encoding.UTF8.GetString(data) == "OK")//DO SOMETHING ON THE DATA IN byte[] data!! Yihaa!!
                        {
                            loggingBox.Invoke(new Action(delegate ()
                            {
                                loggingBox.Items.Add("Wysyłam żądanie");
                            }));
                            string msg = "Requier";
                            socket.Send(Encoding.ASCII.GetBytes(msg)); //Note that you actually send data in byte[]
                        }
                        else if(Encoding.UTF8.GetString(data) == "IsEmpty")
                        {
                            loggingBox.Invoke(new Action(delegate ()
                            {
                                loggingBox.Items.Add("Twoje dane są aktualne!");
                            }));
                            btnStart.Invoke(new Action(delegate ()
                            {
                                btnStart.Enabled = true;
                            }));
                        }
                        else//DO SOMETHING ON THE DATA IN byte[] data!! Yihaa!!
                        {
                            /*loggingBox.Invoke(new Action(delegate ()
                            {
                                loggingBox.Items.Add("server: " + Encoding.UTF8.GetString(data));
                            }));*/
                            string tmp = Encoding.UTF8.GetString(data);
                            string[] lines = tmp.Split('=');
                            if (lines[0] == "[Server]")
                            {
                                server.Invoke(new Action(delegate () {
                                    server.Text = lines[1];
                                }));
                            } else if (lines[0] == "[Login]")
                            {
                                login.Invoke(new Action(delegate () {
                                    login.Text = lines[1];
                                }));
                                } else if (lines[0] == "[Password]")
                            {
                                password.Invoke(new Action(delegate () { 
                                password.Text = lines[1];
                                }));
                                    } else if (lines[0] == "[SumatraPDF]")
                            {
                                if (lines[1] == "True") {
                                    sumatra_checkbox.Invoke(new Action(delegate () { 
                                    sumatra_checkbox.Checked = true;
                                }));
                            }
                            else {
                                sumatra_checkbox.Invoke(new Action(delegate () { 
                                    sumatra_checkbox.Checked = false;
                            })); }
                            }
                            else if (lines[0] == "[Save]")
                            {
                                if (lines[1] == "True")
                                {
                                    saveLoginData.Invoke(new Action(delegate ()
                                    {
                                        autoupdate = true;
                                        saveLoginData_Click(new object(), new EventArgs());
                                    }));
                                }
                                loggingBox.Invoke(new Action(delegate ()
                                {
                                    loggingBox.Items.Add("Aktualizacja zakończona pomyślnie.");
                                }));
                                btnStart.Invoke(new Action(delegate ()
                                {
                                    btnStart.Enabled = true;
                                }));
                                this.connection.CancelAsync();
                            }
                            //string msg = "OK";
                            //socket.Send(Encoding.ASCII.GetBytes(msg)); //Note that you actually send data in byte[]
                        }//DO ANYTHING THAT YOU WANT WITH data, IT IS THE RECEIVED PACKET!
                         //Notice that your data is not string! It is actually byte[]
                         //For now I will just print it out
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
                    }
                    else if (receiveAttempt < MAX_RECEIVE_ATTEMPT)
                    { //not exceeding the max attempt, try again
                        ++receiveAttempt;
                        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
                    }
                    else
                    { //completely fails!
                        loggingBox.Invoke(new Action(delegate ()
                        {
                            loggingBox.Items.Add("receiveCallback is failed!");
                        }));
                        btnStart.Invoke(new Action(delegate ()
                        {
                            btnStart.Enabled = true;
                        }));
                        receiveAttempt = 0;
                        clientSocket.Close();
                    }
                }
            }
            catch (Exception e)
            { // this exception will happen when "this" is be disposed...
                loggingBox.Invoke(new Action(delegate ()
                {
                    loggingBox.Items.Add("receiveCallback is failed! " + e.ToString());
                    loggingBox.Items.Add(e.ToString());
                }));
                saveToFile();
                btnStart.Invoke(new Action(delegate()
                {
                    btnStart.Enabled = true;
                }));
            }
        }
    }
}
        
