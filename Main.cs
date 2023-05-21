using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using System.Diagnostics;

namespace CS_SME_MScript
{
    public partial class Main : Form
    {
        private MouseSimulator _mouse = new MouseSimulator();
        private KeyboardSimulator _keyboard = new KeyboardSimulator();
        private IntPtr _handle = IntPtr.Zero;
        private InputSimulator _input = new InputSimulator();
        private bool _working = false;

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // key
        //[DllImport("user32.dll")]
        //public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, IntPtr dwExtraInfo);

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            HotKeyManager.RegisterHotKey(Keys.F1, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F2, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F3, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F4, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F5, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F6, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F7, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F8, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.F9, KeyModifiers.None);
            HotKeyManager.RegisterHotKey(Keys.C, KeyModifiers.None);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKey_Pressed);
            CheckGameWindow();
            this.TopMost = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("你確定要關閉本程式嗎?", "Close?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Bye Bye", "Exit");
            foreach (var process in Process.GetProcessesByName("CS SME MScript"))
            {
                process.Kill();
            }
        }

        private void HotKey_Pressed(object sender, HotKeyEventArgs e)
        {
            if (e.Modifiers == KeyModifiers.None)
            {
                if (e.Key == Keys.F1)
                {
                    radioButton1.Checked = !radioButton1.Checked;
                }

                if (e.Key == Keys.F2)
                {
                    radioButton2.Checked = !radioButton2.Checked;
                }

                if (e.Key == Keys.F3)
                {
                    radioButton3.Checked = !radioButton3.Checked;
                }

                if (e.Key == Keys.F4)
                {
                    radioButton4.Checked = !radioButton4.Checked;
                }

                if (e.Key == Keys.F5)
                {
                    radioButton5.Checked = !radioButton5.Checked;
                }

                if (e.Key == Keys.F6)
                {
                    radioButton6.Checked = !radioButton6.Checked;
                }

                if (e.Key == Keys.F7)
                {
                    radioButton7.Checked = !radioButton7.Checked;
                }

                if (e.Key == Keys.F8)
                {
                    radioButton8.Checked = !radioButton8.Checked;
                }

                if (e.Key == Keys.F9)
                {
                    radioButton9.Checked = !radioButton9.Checked;
                }

                if (e.Key == Keys.C)
                {
                    SetForegroundWindow(_handle);
                    _mouse.LeftButtonClick();
                    Thread.Sleep(10);
                    _mouse.RightButtonClick();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // 普通連擊
            if (radioButton1.Checked == true)
            {
                StartProgram();
            }

            if (radioButton1.Checked == false)
            {
                StopProgram();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //擊退連擊
            if (radioButton2.Checked == true)
            {
                StartProgram2();
            }

            if (radioButton2.Checked == false)
            {
                StopProgram2();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //閃燈
            if (radioButton3.Checked == true)
            {
                StartProgram3();
            }

            if (radioButton3.Checked == false)
            {
                StopProgram3();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //跳躍 (大跳)
            if (radioButton4.Checked == true)
            {
                StartProgram4();
            }

            if (radioButton4.Checked == false)
            {
                StopProgram4();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            //極道滅殺A模式連擊
            if (radioButton5.Checked == true)
            {
                StartProgram5();
            }

            if (radioButton5.Checked == false)
            {
                StopProgram5();
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            //極道滅殺B模式連擊
            if (radioButton6.Checked == true)
            {
                StartProgram6();
            }

            if (radioButton6.Checked == false)
            {
                StopProgram6();
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            //跳躍 (小跳)
            if (radioButton7.Checked == true)
            {
                StartProgram7();
            }

            else
            {
                StopProgram7();
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            //AFK
            if (radioButton8.Checked == true)
            {
                StartProgram8();
            }

            else
            {
                StopProgram8();
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            //Python Desperado
            if (radioButton9.Checked == true)
            {
                StartProgram9();
            }

            else
            {
                StopProgram9();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _mouse.LeftButtonClick();
            Thread.Sleep(10);
            _mouse.RightButtonClick();
        }

        private void StartProgram()
        {
            timer1.Start();
            _working = true;
            label1.Text = "狀態 : 已運行";
            label1.ForeColor = System.Drawing.Color.Tomato;
            radioButton1.ForeColor = System.Drawing.Color.Tomato;
            label5.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram()
        {
            timer1.Stop();
            _working = false;
            label1.Text = "狀態 : 已停止";
            label1.ForeColor = System.Drawing.Color.Black;
            radioButton1.ForeColor = System.Drawing.Color.Black;
            label5.ForeColor = System.Drawing.Color.Black;
        }

        //擊退連擊
        private void StartProgram2()
        {
            timer2.Start();
            _working = true;
            label1.Text = "狀態 : 已運行";
            label1.ForeColor = System.Drawing.Color.Tomato;
            radioButton2.ForeColor = System.Drawing.Color.Tomato;
            label6.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram2()
        {
            timer2.Stop();
            _working = false;
            label1.Text = "狀態 : 已停止";
            label1.ForeColor = System.Drawing.Color.Black;
            radioButton2.ForeColor = System.Drawing.Color.Black;
            label6.ForeColor = System.Drawing.Color.Black;
            _mouse.RightButtonClick();
        }

        //閃燈
        private void StartProgram3()
        {
            timer3.Start();
            _working = true;
            label4.Text = "狀態 : 已運行";
            label4.ForeColor = System.Drawing.Color.Tomato;
            radioButton3.ForeColor = System.Drawing.Color.Tomato;
            label7.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram3()
        {
            timer3.Stop();
            _working = false;
            label4.Text = "狀態 : 已停止";
            label4.ForeColor = System.Drawing.Color.Black;
            radioButton3.ForeColor = System.Drawing.Color.Black;
            label7.ForeColor = System.Drawing.Color.Black;
        }

        //跳躍 (大跳)
        private void StartProgram4()
        {
            timer4.Start();
            _working = true;
            label8.Text = "狀態 : 已運行";
            label8.ForeColor = System.Drawing.Color.Tomato;
            radioButton4.ForeColor = System.Drawing.Color.Tomato;
            label9.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram4()
        {
            timer4.Stop();
            _working = false;
            label8.Text = "狀態 : 已停止";
            label8.ForeColor = System.Drawing.Color.Black;
            radioButton4.ForeColor = System.Drawing.Color.Black;
            label9.ForeColor = System.Drawing.Color.Black;
        }

        //極道滅殺A模式連擊
        private void StartProgram5()
        {
            _keyboard.KeyPress(DirectInputKeyCode.DIK_3);
            timer5.Start();
            _working = true;
            label1.Text = "狀態 : 已運行";
            label1.ForeColor = System.Drawing.Color.Tomato;
            radioButton5.ForeColor = System.Drawing.Color.Tomato;
            label15.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram5()
        {
            timer5.Stop();
            _mouse.LeftButtonClick();
            _working = false;
            label1.Text = "狀態 : 已停止";
            label1.ForeColor = System.Drawing.Color.Black;
            radioButton5.ForeColor = System.Drawing.Color.Black;
            label15.ForeColor = System.Drawing.Color.Black;
        }

        //極道滅殺B模式
        private void StartProgram6()
        {
            timer6.Start();
            _working = true;
            label1.Text = "狀態 : 已運行";
            label1.ForeColor = System.Drawing.Color.Tomato;
            radioButton6.ForeColor = System.Drawing.Color.Tomato;
            label14.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram6()
        {
            timer6.Stop();
            _working = false;
            label1.Text = "狀態 : 已停止";
            label1.ForeColor = System.Drawing.Color.Black;
            radioButton6.ForeColor = System.Drawing.Color.Black;
            label14.ForeColor = System.Drawing.Color.Black;
            _mouse.RightButtonClick();
        }

        //跳躍 (小跳)
        private void StartProgram7()
        {
            timer7.Start();
            _working = true;
            label8.Text = "狀態 : 已運行";
            label8.ForeColor = System.Drawing.Color.Tomato;
            radioButton7.ForeColor = System.Drawing.Color.Tomato;
            label16.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram7()
        {
            timer7.Stop();
            _working = false;
            label8.Text = "狀態 : 已停止";
            label8.ForeColor = System.Drawing.Color.Black;
            radioButton7.ForeColor = System.Drawing.Color.Black;
            label16.ForeColor = System.Drawing.Color.Black;
        }

        //AFK
        private void StartProgram8()
        {
            timer8.Start();
            _working = true;
            label19.Text = "狀態 : 已運行";
            label19.ForeColor = System.Drawing.Color.Tomato;
            radioButton8.ForeColor = System.Drawing.Color.Tomato;
            label18.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram8()
        {
            timer8.Stop();
            _working = false;
            label19.Text = "狀態 : 已停止";
            label19.ForeColor = System.Drawing.Color.Black;
            radioButton8.ForeColor = System.Drawing.Color.Black;
            label18.ForeColor = System.Drawing.Color.Black;
        }

        //Python Desperado
        private void StartProgram9()
        {
            _keyboard.KeyPress(DirectInputKeyCode.DIK_2);
            timer9.Start();
            _working = true;
            label4.Text = "狀態 : 已運行";
            label4.ForeColor = System.Drawing.Color.Tomato;
            radioButton9.ForeColor = System.Drawing.Color.Tomato;
            label28.ForeColor = System.Drawing.Color.Tomato;
        }

        private void StopProgram9()
        {
            timer9.Stop();
            _working = false;
            label4.Text = "狀態 : 已停止";
            label4.ForeColor = System.Drawing.Color.Black;
            radioButton9.ForeColor = System.Drawing.Color.Black;
            label28.ForeColor = System.Drawing.Color.Black;
        }

        private bool CheckGameWindow()
        {
            _handle = FindWindow("Valve001", null);

            if (_handle == IntPtr.Zero)
            {
                MessageBox.Show("遊戲找不到", "NotFound");
                return false;
            }
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_Q);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_3);
            Thread.Sleep(255);
            //label2.Visible = false;
            //label2.Text += i.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_3);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_Q);
            _mouse.RightButtonDown();
            Thread.Sleep(570);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_F);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_SPACE);
            Thread.Sleep(110);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_3);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_Q);
            _mouse.LeftButtonDown();
            Thread.Sleep(810);
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_Q);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_3);
            _mouse.RightButtonDown();
            Thread.Sleep(420);
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            _keyboard.KeyPress(DirectInputKeyCode.DIK_LCONTROL);
            Thread.Sleep(141);
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(_handle);
            timer8.Enabled = true;
            label21.Text = "Full Screen X/Y : " + Cursor.Position.ToString();
            var rct = new RECT();
            GetWindowRect(_handle, ref rct);

            var pos = Cursor.Position;
            pos.X -= rct.Left;
            pos.Y -= rct.Top;
            label20.Text = "PrOGram X/Y : " + pos.ToString();
            Cursor.Position = new Point(rct.Left + 11, rct.Top + 40);
            _input.Mouse.LeftButtonClick();
            Thread.Sleep(800);
        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            _mouse.LeftButtonDown();
            Thread.Sleep(800);
            _mouse.LeftButtonClick();
            _mouse.RightButtonDown();
            Thread.Sleep(800);
            _mouse.RightButtonClick();
        }

        private void AFK_CheckedChanged(object sender, EventArgs e)
        {
            if (AFK.Checked == true)
            {
                this.TopMost = false;
            }

            else
            {
                this.TopMost = true;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently there is no website XD", "XD", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently there is no forum XD", "XD", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
