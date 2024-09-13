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
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.Location = new Point(29, 25);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(136, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "AI Chatbot";
        // 
        // tbxPrompt
        // 
        tbxPrompt.BackColor = SystemColors.Window;
        tbxPrompt.Location = new Point(34, 111);
        tbxPrompt.Multiline = true;
        tbxPrompt.Name = "tbxPrompt";
        tbxPrompt.Size = new Size(322, 294);
        tbxPrompt.TabIndex = 1;
        // 
        // tbxResponse
        // 
        tbxResponse.Location = new Point(423, 111);
        tbxResponse.Multiline = true;
        tbxResponse.Name = "tbxResponse";
        tbxResponse.Size = new Size(322, 294);
        tbxResponse.TabIndex = 2;
        // 
        // lblPrompt
        // 
        lblPrompt.AutoSize = true;
        lblPrompt.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblPrompt.Location = new Point(34, 83);
        lblPrompt.Name = "lblPrompt";
        lblPrompt.Size = new Size(117, 25);
        lblPrompt.TabIndex = 3;
        lblPrompt.Text = "Your Prompt:";
        // 
        // lblResponse
        // 
        lblResponse.AutoSize = true;
        lblResponse.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblResponse.Location = new Point(423, 83);
        lblResponse.Name = "lblResponse";
        lblResponse.Size = new Size(114, 25);
        lblResponse.TabIndex = 4;
        lblResponse.Text = "AI Response:";
        // 
        // cbxUseFunctionCalling
        // 
        cbxUseFunctionCalling.AutoSize = true;
        cbxUseFunctionCalling.Location = new Point(423, 38);
        cbxUseFunctionCalling.Name = "cbxUseFunctionCalling";
        cbxUseFunctionCalling.Size = new Size(135, 19);
        cbxUseFunctionCalling.TabIndex = 5;
        cbxUseFunctionCalling.Text = "Use Function Calling";
        cbxUseFunctionCalling.UseVisualStyleBackColor = true;
        cbxUseFunctionCalling.CheckedChanged += cbxUseFunctionCalling_CheckedChanged;
        // 
        // btnSendPrompt
        // 
        btnSendPrompt.Location = new Point(264, 424);
        btnSendPrompt.Name = "btnSendPrompt";
        btnSendPrompt.Size = new Size(92, 35);
        btnSendPrompt.TabIndex = 6;
        btnSendPrompt.Text = "Send";
        btnSendPrompt.UseVisualStyleBackColor = true;
        btnSendPrompt.Click += btnSendPrompt_Click;
        // 
        // ChatForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.Control;
        ClientSize = new Size(783, 482);
        Controls.Add(btnSendPrompt);
        Controls.Add(cbxUseFunctionCalling);
        Controls.Add(lblResponse);
        Controls.Add(lblPrompt);
        Controls.Add(tbxResponse);
        Controls.Add(tbxPrompt);
        Controls.Add(lblTitle);
        MaximizeBox = false;
        Name = "ChatForm";
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
}
