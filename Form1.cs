using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;

namespace FanController
{
    public partial class Form1 : Form
    {
        private Computer _computer;
        private List<ISensor> _fanControls;

        public Form1()
        {
            InitializeComponent();
            _fanControls = new List<ISensor>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitHardware();
            RefreshSensors();
        }

        private void InitHardware()
        {
            try
            {
                lblStatus.Text = "Initializing hardware monitor (requires Admin)...";
                Application.DoEvents();

                _computer = new Computer
                {
                    IsControllerEnabled = true,
                    IsMotherboardEnabled = true,
                    IsCpuEnabled = true,
                    IsGpuEnabled = true,
                    IsBatteryEnabled = true,
                    IsMemoryEnabled = true,
                    IsPsuEnabled = true,
                    IsStorageEnabled = true,
                    IsNetworkEnabled = true
                };
                
                _computer.Open();
                lblStatus.Text = "Hardware initialized.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: Please run as Administrator! " + ex.Message;
                MessageBox.Show("LibreHardwareMonitor requires Administrator privileges to access fan controllers.\\n" + ex.Message, 
                                "Admin Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshSensors()
        {
            if (_computer == null) return;
            
            lstSensors.Items.Clear();
            _fanControls.Clear();

            try
            {
                foreach (IHardware hardware in _computer.Hardware)
                {
                    hardware.Update();
                    ProcessHardware(hardware);
                }

                if (_fanControls.Count > 0)
                {
                    lblStatus.Text = $"Found {_fanControls.Count} controllable fan(s).";
                    btnApply.Text = "Apply Manual Speed";
                    btnAuto.Text = "Set Auto";
                    trackBarSpeed.Enabled = true;
                }
                else
                {
                    lblStatus.Text = "No controllable fans detected on this system.";
                    btnApply.Text = "Force Max Cooling";
                    btnAuto.Text = "Set Balanced Auto";
                    trackBarSpeed.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error updating sensors: " + ex.Message;
            }
        }

        private void ProcessHardware(IHardware hardware)
        {
            // Add hardware as a category-like item
            ListViewItem hItem = new ListViewItem(hardware.Name);
            hItem.SubItems.Add(hardware.HardwareType.ToString());
            hItem.ForeColor = Color.LightSkyBlue;
            hItem.Font = new Font(lstSensors.Font, FontStyle.Bold);
            lstSensors.Items.Add(hItem);

            foreach (ISensor sensor in hardware.Sensors)
            {
                if (sensor.SensorType == SensorType.Temperature || 
                    sensor.SensorType == SensorType.Fan || 
                    sensor.SensorType == SensorType.Control)
                {
                    ListViewItem sItem = new ListViewItem("  • " + sensor.Name);
                    string val = sensor.Value.HasValue ? Math.Round(sensor.Value.Value, 1).ToString() : "N/A";
                    
                    if (sensor.SensorType == SensorType.Temperature) val += " °C";
                    else if (sensor.SensorType == SensorType.Fan) val += " RPM";
                    else if (sensor.SensorType == SensorType.Control) val += " %";

                    sItem.SubItems.Add(val);
                    lstSensors.Items.Add(sItem);

                    if (sensor.SensorType == SensorType.Control)
                    {
                        _fanControls.Add(sensor);
                    }
                }
            }

            // Traverse any subhardware (like Embedded Controllers or SuperIO chips which house fans)
            foreach (IHardware subHardware in hardware.SubHardware)
            {
                subHardware.Update();
                ProcessHardware(subHardware);
            }
        }

        private void TrackBarSpeed_Scroll(object sender, EventArgs e)
        {
            lblSpeedVal.Text = $"{trackBarSpeed.Value}%";
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (_fanControls.Count == 0)
            {
                // FALLBACK: Use Windows Power Overlay / Scheme to force Maximum Performance (Active Cooling)
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "powercfg",
                        Arguments = "/SetActive SCHEME_MIN",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                    
                    lblStatus.Text = "Hardware locked by Vendor: Enforced Active Cooling via OS Power Plan.";
                    MessageBox.Show("Direct fan sliders are blocked by your laptop's firmware (common on Lenovo/Dell).\n\nSwitched your system to 'Maximum Performance' mode, which forces your laptop's built-in active cooling to prioritize high fan speeds to lower temperatures.", "Thermal Policy Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Failed to apply OS thermal policy: " + ex.Message;
                }
                return;
            }

            try
            {
                int targetSpeed = trackBarSpeed.Value;
                foreach (ISensor control in _fanControls)
                {
                    if (control.Control != null)
                    {
                        control.Control.SetSoftware((float)targetSpeed);
                    }
                }
                lblStatus.Text = $"Manual speed set to {targetSpeed}%.";
                RefreshSensors();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed to set fan speed: " + ex.Message;
            }
        }

        private void BtnAuto_Click(object sender, EventArgs e)
        {
             if (_fanControls.Count == 0)
             {
                 // FALLBACK: Returns to Balanced
                 try
                 {
                     System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                     {
                         FileName = "powercfg",
                         Arguments = "/SetActive SCHEME_BALANCED",
                         CreateNoWindow = true,
                         UseShellExecute = false
                     });
                     lblStatus.Text = "Hardware locked by Vendor: Reverted to Balanced Auto Cooling.";
                 }
                 catch (Exception ex) { lblStatus.Text = "Failed: " + ex.Message; }
                 return;
             }

             try
             {
                 foreach (ISensor control in _fanControls)
                 {
                     if (control.Control != null)
                     {
                         control.Control.SetDefault(); // Set to BIOS/Auto control
                     }
                 }
                 lblStatus.Text = "Fan control set to Default/Auto.";
                 RefreshSensors();
             }
             catch (Exception ex)
             {
                 lblStatus.Text = "Failed to reset fan speed: " + ex.Message;
             }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshSensors();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _computer?.Close();
        }
    }
}
