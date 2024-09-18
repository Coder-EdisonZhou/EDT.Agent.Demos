namespace EDT.WorkOrderAgent.Portal;

partial class AgentForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblTitle = new Label();
        tbxPrompt = new TextBox();
        tbxResponse = new TextBox();
        lblPrompt = new Label();
        lblResponse = new Label();
        cbxUseFunctionCalling = new CheckBox();
        btnSendPrompt = new Button();
        cbxUseFunctionPlanner = new CheckBox();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.Location = new Point(46, 40);
        lblTitle.Margin = new Padding(5, 0, 5, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(201, 48);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "AI Chatbot";
        // 
        // tbxPrompt
        // 
        tbxPrompt.BackColor = SystemColors.Window;
        tbxPrompt.Location = new Point(53, 178);
        tbxPrompt.Margin = new Padding(5);
        tbxPrompt.Multiline = true;
        tbxPrompt.Name = "tbxPrompt";
        tbxPrompt.Size = new Size(504, 468);
        tbxPrompt.TabIndex = 1;
        // 
        // tbxResponse
        // 
        tbxResponse.Location = new Point(665, 178);
        tbxResponse.Margin = new Padding(5);
        tbxResponse.Multiline = true;
        tbxResponse.Name = "tbxResponse";
        tbxResponse.Size = new Size(504, 468);
        tbxResponse.TabIndex = 2;
        // 
        // lblPrompt
        // 
        lblPrompt.AutoSize = true;
        lblPrompt.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblPrompt.Location = new Point(53, 133);
        lblPrompt.Margin = new Padding(5, 0, 5, 0);
        lblPrompt.Name = "lblPrompt";
        lblPrompt.Size = new Size(177, 40);
        lblPrompt.TabIndex = 3;
        lblPrompt.Text = "Your Prompt:";
        // 
        // lblResponse
        // 
        lblResponse.AutoSize = true;
        lblResponse.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblResponse.Location = new Point(665, 133);
        lblResponse.Margin = new Padding(5, 0, 5, 0);
        lblResponse.Name = "lblResponse";
        lblResponse.Size = new Size(172, 40);
        lblResponse.TabIndex = 4;
        lblResponse.Text = "AI Response:";
        // 
        // cbxUseFunctionCalling
        // 
        cbxUseFunctionCalling.AutoSize = true;
        cbxUseFunctionCalling.Location = new Point(665, 61);
        cbxUseFunctionCalling.Margin = new Padding(5);
        cbxUseFunctionCalling.Name = "cbxUseFunctionCalling";
        cbxUseFunctionCalling.Size = new Size(212, 28);
        cbxUseFunctionCalling.TabIndex = 5;
        cbxUseFunctionCalling.Text = "Use Function Calling";
        cbxUseFunctionCalling.UseVisualStyleBackColor = true;
        cbxUseFunctionCalling.CheckedChanged += cbxUseFunctionCalling_CheckedChanged;
        // 
        // btnSendPrompt
        // 
        btnSendPrompt.Location = new Point(415, 678);
        btnSendPrompt.Margin = new Padding(5);
        btnSendPrompt.Name = "btnSendPrompt";
        btnSendPrompt.Size = new Size(145, 56);
        btnSendPrompt.TabIndex = 6;
        btnSendPrompt.Text = "Send";
        btnSendPrompt.UseVisualStyleBackColor = true;
        btnSendPrompt.Click += btnSendPrompt_Click;
        // 
        // cbxUseFunctionPlanner
        // 
        cbxUseFunctionPlanner.AutoSize = true;
        cbxUseFunctionPlanner.Location = new Point(916, 61);
        cbxUseFunctionPlanner.Margin = new Padding(5);
        cbxUseFunctionPlanner.Name = "cbxUseFunctionPlanner";
        cbxUseFunctionPlanner.Size = new Size(217, 28);
        cbxUseFunctionPlanner.TabIndex = 7;
        cbxUseFunctionPlanner.Text = "Use Function Planner";
        cbxUseFunctionPlanner.UseVisualStyleBackColor = true;
        // 
        // AgentForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.Control;
        ClientSize = new Size(1230, 771);
        Controls.Add(cbxUseFunctionPlanner);
        Controls.Add(btnSendPrompt);
        Controls.Add(cbxUseFunctionCalling);
        Controls.Add(lblResponse);
        Controls.Add(lblPrompt);
        Controls.Add(tbxResponse);
        Controls.Add(tbxPrompt);
        Controls.Add(lblTitle);
        Margin = new Padding(5);
        MaximizeBox = false;
        Name = "AgentForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "AI Chat";
        Load += AgentForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private TextBox tbxPrompt;
    private TextBox tbxResponse;
    private Label lblPrompt;
    private Label lblResponse;
    private CheckBox cbxUseFunctionCalling;
    private Button btnSendPrompt;
    private CheckBox cbxUseFunctionPlanner;
}
