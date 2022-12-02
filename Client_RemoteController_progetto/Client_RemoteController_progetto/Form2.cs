﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_RemoteController_progetto
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        byte[] bytes = new byte[1024];

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_Status.Text = "CONNESSIONE APERTA";
            //lbl_Status.ForeColor = System.Drawing.Color.Green;
        }

        private void inviaSegnale(String message)
        {
            byte[] bytes = new byte[1024];
            try
            {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(remoteEP);
                    lbl_Status.Text = "CONNESSIONE APERTA";
                    lbl_Status.ForeColor = System.Drawing.Color.Green;

                    Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());

                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    int bytesSent = socket.Send(msg);

                    int bytesRec = socket.Receive(bytes);
                    string rispostaServer = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    if (rispostaServer.Equals("ok"))
                    {
                        label3.Text = "Tutto ok";
                        label3.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        label3.Text = "ERRORE";
                        label3.ForeColor = System.Drawing.Color.Red;
                    }

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    lbl_Status.Text = "Segnale inviato";
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception a)
                {
                    Console.WriteLine("Unexpected exception : {0}", a.ToString());
                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a.ToString());
                lbl_Status.Text = "ERRORE DURANTE LA CONNESSIONE";
                lbl_Status.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("GENERALE"))
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.ShowDialog();
            }
            else if (comboBox1.SelectedItem.Equals("CAMERA"))
            {
                this.Hide();
                Form3 f3 = new Form3();
                f3.ShowDialog();
            }
            else if (comboBox1.SelectedItem.Equals("LAVANDERIA"))
            {
                this.Hide();
                Form4 f4 = new Form4();
                f4.ShowDialog();
            }
            else if (comboBox1.SelectedItem.Equals("CUCINA"))
            {
                this.Hide();
                Form5 f5 = new Form5();
                f5.ShowDialog();
            }
            else if (comboBox1.SelectedItem.Equals("GARAGE"))
            {
                this.Hide();
                Form6 f6 = new Form6();
                f6.ShowDialog();
            }
        }

        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleButton1.Checked)
            {
                this.inviaSegnale("LUCE_SALA_ON");
            }
            else
            {
                this.inviaSegnale("LUCE_SALA_OFF");
            }
        }

        private void rjToggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleButton2.Checked)
            {
                this.inviaSegnale("TV_ON");
            }
            else
            {
                this.inviaSegnale("TV_OFF");
            }
        }
    }
}
