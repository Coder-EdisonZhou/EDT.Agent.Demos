namespace EDT.QuickRAG.Portal;

partial class ChatForm
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
        btnEmbedding = new Button();
        btnGetRagResponse = new Button();
        tbxIndex = new TextBox();
        lblIndex = new Label();
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
        // btnEmbedding
        // 
        btnEmbedding.Location = new Point(1009, 49);
        btnEmbedding.Margin = new Padding(5);
        btnEmbedding.Name = "btnEmbedding";
        btnEmbedding.Size = new Size(145, 46);
        btnEmbedding.TabIndex = 7;
        btnEmbedding.Text = "Embedding";
        btnEmbedding.UseVisualStyleBackColor = true;
        btnEmbedding.Click += btnEmbedding_Click;
        // 
        // btnGetRagResponse
        // 
        btnGetRagResponse.Location = new Point(412, 686);
        btnGetRagResponse.Margin = new Padding(5);
        btnGetRagResponse.Name = "btnGetRagResponse";
        btnGetRagResponse.Size = new Size(145, 46);
        btnGetRagResponse.TabIndex = 8;
        btnGetRagResponse.Text = "Send";
        btnGetRagResponse.UseVisualStyleBackColor = true;
        btnGetRagResponse.Click += btnGetRagResponse_Click;
        // 
        // tbxIndex
        // 
        tbxIndex.BackColor = SystemColors.Window;
        tbxIndex.Location = new Point(852, 57);
        tbxIndex.Margin = new Padding(5);
        tbxIndex.Name = "tbxIndex";
        tbxIndex.Size = new Size(147, 30);
        tbxIndex.TabIndex = 9;
        tbxIndex.Text = "DEMO";
        // 
        // lblIndex
        // 
        lblIndex.AutoSize = true;
        lblIndex.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblIndex.Location = new Point(753, 49);
        lblIndex.Margin = new Padding(5, 0, 5, 0);
        lblIndex.Name = "lblIndex";
        lblIndex.Size = new Size(89, 40);
        lblIndex.TabIndex = 10;
        lblIndex.Text = "Index:";
        // 
        // ChatForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.Control;
        ClientSize = new Size(1230, 771);
        Controls.Add(lblIndex);
        Controls.Add(tbxIndex);
        Controls.Add(btnGetRagResponse);
        Controls.Add(btnEmbedding);
        Controls.Add(lblResponse);
        Controls.Add(lblPrompt);
        Controls.Add(tbxResponse);
        Controls.Add(tbxPrompt);
        Controls.Add(lblTitle);
        Margin = new Padding(5);
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
    private Button btnEmbedding;
    private Button btnGetRagResponse;
    private TextBox tbxIndex;
    private Label lblIndex;
}
