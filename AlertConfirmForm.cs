using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AlertAndConfirm
{
    internal class AlertConfirmForm : Form
    {
        private TextBox textBox;
        private Button button1;
        private Button button2;

        public bool Confirm {  get; private set; }

        public AlertConfirmForm()
        {
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AlertForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = Vars.Alert;

            textBox = new TextBox()
            {
                TextAlign = HorizontalAlignment.Center,
                Multiline = true,
                TabStop = false,
                BackColor = SystemColors.Control,
                
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)))
            };
            
        }

        public bool Alert(string message, int ClientSize_X, int ClientSize_Y)
        {
            Controls.Clear();
            textBox.Text = message;
            textBox.Dock = DockStyle.Fill;
            ClientSize = new Size(ClientSize_X, ClientSize_Y);
            Controls.Add(textBox);
            ShowDialog();
            return false;
        }

        /// <summary>
        /// Возвращает true если была нажата кнопка подверждения.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ClientSize_X"></param>
        /// <param name="ClientSize_Y"></param>
        public bool Confirmation(string message, int ClientSize_X, int ClientSize_Y)
        {
            Controls.Clear();
            textBox.Text = message;
            ClientSize = new Size(ClientSize_X, ClientSize_Y);

            button1 = new Button()
            {
                Text =  Vars.ConfirmWrite,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Size = new Size(120, 30)
            };
            button2 = new Button()
            {
                Text = Vars.Cancel,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Size = new Size(120, 30)
            };
            textBox.Dock = DockStyle.Top;
            textBox.Size = new Size(ClientSize_X, ClientSize_Y - button1.Height - 10);

            button1.Location = new Point(5, textBox.Height + 5);
            button2.Location=new Point(ClientSize_X - 5 - button2.Width, textBox.Height + 5);

            Controls.Add(textBox);
            Controls.Add(button1);
            Controls.Add(button2);

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            ShowDialog();

            return Confirm;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Confirm = true;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Confirm = false;
            Close();
        }
    }
}
