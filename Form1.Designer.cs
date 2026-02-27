namespace FanController;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblStatus = new Label();
        trackBarSpeed = new TrackBar();
        lblSpeedVal = new Label();
        btnApply = new Button();
        btnRefresh = new Button();
        lstSensors = new ListView();
        colName = new ColumnHeader();
        colValue = new ColumnHeader();
        btnAuto = new Button();

        ((System.ComponentModel.ISupportInitialize)trackBarSpeed).BeginInit();
        SuspendLayout();

        // lblTitle
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(20, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(335, 30);
        lblTitle.Text = "Windows Laptop Fan Controller";

        // lstSensors
        lstSensors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstSensors.BackColor = Color.FromArgb(45, 45, 48);
        lstSensors.BorderStyle = BorderStyle.None;
        lstSensors.Columns.AddRange(new ColumnHeader[] { colName, colValue });
        lstSensors.Font = new Font("Segoe UI", 10F);
        lstSensors.ForeColor = Color.White;
        lstSensors.FullRowSelect = true;
        lstSensors.Location = new Point(25, 70);
        lstSensors.Name = "lstSensors";
        lstSensors.Size = new Size(500, 180);
        lstSensors.TabIndex = 2;
        lstSensors.UseCompatibleStateImageBehavior = false;
        lstSensors.View = View.Details;

        // colName
        colName.Text = "Sensor / Component";
        colName.Width = 300;

        // colValue
        colValue.Text = "Value";
        colValue.Width = 180;

        // trackBarSpeed
        trackBarSpeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        trackBarSpeed.Location = new Point(25, 270);
        trackBarSpeed.Maximum = 100;
        trackBarSpeed.Name = "trackBarSpeed";
        trackBarSpeed.Size = new Size(400, 45);
        trackBarSpeed.TabIndex = 3;
        trackBarSpeed.TickFrequency = 10;
        trackBarSpeed.Scroll += TrackBarSpeed_Scroll;

        // lblSpeedVal
        lblSpeedVal.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblSpeedVal.AutoSize = true;
        lblSpeedVal.Font = new Font("Segoe UI", 12F);
        lblSpeedVal.ForeColor = Color.White;
        lblSpeedVal.Location = new Point(440, 270);
        lblSpeedVal.Name = "lblSpeedVal";
        lblSpeedVal.Size = new Size(32, 21);
        lblSpeedVal.Text = "0%";

        // btnApply
        btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnApply.BackColor = Color.FromArgb(0, 122, 204);
        btnApply.FlatAppearance.BorderSize = 0;
        btnApply.FlatStyle = FlatStyle.Flat;
        btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnApply.ForeColor = Color.White;
        btnApply.Location = new Point(25, 330);
        btnApply.Name = "btnApply";
        btnApply.Size = new Size(150, 40);
        btnApply.TabIndex = 4;
        btnApply.Text = "Apply Manual Speed";
        btnApply.UseVisualStyleBackColor = false;
        btnApply.Click += BtnApply_Click;

        // btnAuto
        btnAuto.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAuto.BackColor = Color.FromArgb(45, 45, 48);
        btnAuto.FlatAppearance.BorderSize = 0;
        btnAuto.FlatStyle = FlatStyle.Flat;
        btnAuto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAuto.ForeColor = Color.White;
        btnAuto.Location = new Point(195, 330);
        btnAuto.Name = "btnAuto";
        btnAuto.Size = new Size(100, 40);
        btnAuto.TabIndex = 5;
        btnAuto.Text = "Set Auto";
        btnAuto.UseVisualStyleBackColor = false;
        btnAuto.Click += BtnAuto_Click;

        // btnRefresh
        btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnRefresh.BackColor = Color.FromArgb(45, 45, 48);
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnRefresh.ForeColor = Color.White;
        btnRefresh.Location = new Point(425, 330);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(100, 40);
        btnRefresh.TabIndex = 6;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += BtnRefresh_Click;

        // lblStatus
        lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 9F);
        lblStatus.ForeColor = Color.DarkGray;
        lblStatus.Location = new Point(25, 390);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(59, 15);
        lblStatus.Text = "Ready...";

        // Form1
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(30, 30, 30);
        ClientSize = new Size(550, 430);
        Controls.Add(lblStatus);
        Controls.Add(btnRefresh);
        Controls.Add(btnAuto);
        Controls.Add(btnApply);
        Controls.Add(lblSpeedVal);
        Controls.Add(trackBarSpeed);
        Controls.Add(lstSensors);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Laptop Fan Controller";
        Load += Form1_Load;

        ((System.ComponentModel.ISupportInitialize)trackBarSpeed).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblTitle;
    private ListView lstSensors;
    private ColumnHeader colName;
    private ColumnHeader colValue;
    private TrackBar trackBarSpeed;
    private Label lblSpeedVal;
    private Button btnApply;
    private Button btnAuto;
    private Button btnRefresh;
    private Label lblStatus;
}
