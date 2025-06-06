using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BiliardClub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        struct stBookingDetails
        {
            public float Price;
            public stTimeCounter timeCounter;
        }

        struct stTimeCounter
        {
            public byte Hour;
            public byte Minute;
            public byte Second;
        }

        struct stSummary
        {
            public byte TablesAvailableNow;
            public byte SelectedTablesNow;
            public byte TotalCustomersToday;
            public float TotalIncomeToday;
        }

        stBookingDetails BookingDetailsTable1;
        stBookingDetails BookingDetailsTable2;
        stBookingDetails BookingDetailsTable3;
        stBookingDetails BookingDetailsTable4;
        stBookingDetails BookingDetailsTable5;
        stBookingDetails BookingDetailsTable6;
        stBookingDetails BookingDetailsTable7;
        stBookingDetails BookingDetailsTable8;
        stBookingDetails BookingDetailsTable9;

        stTimeCounter TimerTable1;
        stTimeCounter TimerTable2;
        stTimeCounter TimerTable3;
        stTimeCounter TimerTable4;
        stTimeCounter TimerTable5;
        stTimeCounter TimerTable6;
        stTimeCounter TimerTable7;
        stTimeCounter TimerTable8;
        stTimeCounter TimerTable9;

        stSummary Summary;


        private stBookingDetails LoadBilliardDrumDetalis(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox,
            stBookingDetails bookingDetails)
        {
            switch (comboBox.Text)
            {
                case "1 m - 1$":
                    bookingDetails.timeCounter.Minute = 1;
                    bookingDetails.Price = 1;
                    Summary.TotalIncomeToday += 1;
                    UpdateSummary();
                    return bookingDetails;

                case "5 m - 5$":
                    bookingDetails.timeCounter.Minute = 5;
                    bookingDetails.Price = 5;
                    Summary.TotalIncomeToday += 5;
                    UpdateSummary();
                    return bookingDetails;

                case "10 m - 10$":
                    bookingDetails.timeCounter.Minute = 10;
                    bookingDetails.Price = 10;
                    Summary.TotalIncomeToday += 10;
                    UpdateSummary();
                    return bookingDetails;

                case "15 m - 15$":
                    bookingDetails.timeCounter.Minute = 15;
                    bookingDetails.Price = 15;
                    Summary.TotalIncomeToday += 15;
                    UpdateSummary();
                    return bookingDetails;

                case "30 m - 30$":
                    bookingDetails.timeCounter.Minute = 30;
                    bookingDetails.Price = 30;
                    Summary.TotalIncomeToday += 30;
                    UpdateSummary();
                    return bookingDetails;

                case "45 m - 45$":
                    bookingDetails.timeCounter.Minute = 45;
                    bookingDetails.Price = 45;
                    Summary.TotalIncomeToday += 45;
                    UpdateSummary();
                    return bookingDetails;

                default:
                    return bookingDetails;
            }

        }

        private void UpdateLabel(Label lblTimeInTable, Label lblDurationOfTimeTable, Label lblPriceTable,
            Label lblTable, stBookingDetails bookingDetails)
        {
            lblTimeInTable.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDurationOfTimeTable.Text = bookingDetails.timeCounter.Minute.ToString() + " m";
            lblPriceTable.Text = bookingDetails.Price.ToString() + "$";

            lblTable.Text = "Pause";
            lblTable.BackColor = Color.Yellow;
        }

        private void UpdateUserInterfaceAfterClickButtonStart(Label lblTimeInTable, Label lblDurationOfTimeTable, Label lblPriceTable,
           Label lblTable, Timer TimeCounterTable, System.Windows.Forms.Button buttonPause, stBookingDetails bookingDetails)
        {
            UpdateLabel(lblTimeInTable, lblDurationOfTimeTable, lblPriceTable, lblTable, bookingDetails);
            buttonPause.Visible = true;
            TimeCounterTable.Enabled = true;
        }

        private bool Check(byte Minutes, Timer timer, string TableName, stBookingDetails bookingDetails)
        {
            if (bookingDetails.timeCounter.Minute == Minutes)
            {
                timer.Enabled = false;
                MessageBox.Show("The " + TableName + " Finshed", "Finsh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            else
                return false;
        }

        private bool ValidtionTextBoxAndComboBox(System.Windows.Forms.TextBox textBox, System.Windows.Forms.ComboBox comboBox)
        {
            if (String.IsNullOrEmpty(textBox.Text) || String.IsNullOrEmpty(comboBox.Text))
            {
                MessageBox.Show("Wrong Choose!", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            else
                return true;
        }

        private void ShowButtonPause(System.Windows.Forms.Button button, Timer timer, Label lblTable)
        {
            if (Convert.ToByte(button.Tag) == 1)
            {
                timer.Enabled = false;
                lblTable.Text = "Waiting";
                lblTable.BackColor = Color.DodgerBlue;
                button.Tag = 0;
            }

            else
            {
                timer.Enabled = true;
                lblTable.Text = "Pasue";
                lblTable.BackColor = Color.Yellow;
                button.Tag = 1;
            }
        }

        private void UpdateLabelAfterClickStop(Label lblTimeInTable, Label lblDurationOfTimeTable, Label lblPriceTable, Label lblTable,
            Label lblTimeCounter, System.Windows.Forms.TextBox textBox, System.Windows.Forms.Button buttonPause)
        {
            lblTimeInTable.Text = "";
            lblDurationOfTimeTable.Text = "";
            lblPriceTable.Text = "";
            lblTimeCounter.Text = "";

            textBox.Text = "";
            buttonPause.Visible = false;

            lblTable.Text = "Free";
            lblTable.BackColor = Color.Lime;
        }

        private byte UpdateTablesAvailableNow()
        {
            return Convert.ToByte(Summary.TablesAvailableNow - Summary.SelectedTablesNow);
        }

        private void UpdateSummary()
        {
            lblOnlineTables.Text = UpdateTablesAvailableNow().ToString() + " Tables";
            lblSelectedTablesNow.Text = Summary.SelectedTablesNow.ToString() + " Tables";
            lblTotalCustomersToday.Text = Summary.TotalCustomersToday.ToString() + " Tables";
            lblTotalIncomeToday.Text = Summary.TotalIncomeToday.ToString() + "$";
        }

        private string FormatTime(byte Time)
        {
            if (Time < 10)
                return "0" + Time.ToString();

            else
                return Time.ToString();
        }

        private void Start(System.Windows.Forms.Button buttonPause, System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox,
           Label lblTimeInTable, Label lblDurationOfTimeTable, Label lblPriceTable, Timer TimeCounterTable,
           Label lblTable, stBookingDetails bookingDetails)
        {
            if (!ValidtionTextBoxAndComboBox(textBox, comboBox))
            {
                return;
            }

            Summary.SelectedTablesNow++;
            Summary.TotalCustomersToday++;

            UpdateSummary();
            UpdateUserInterfaceAfterClickButtonStart(lblTimeInTable, lblDurationOfTimeTable, lblPriceTable, lblTable, TimeCounterTable, buttonPause, bookingDetails);
        }



        private void Stop(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox, Label lblTimeInTable,
            Label lblDurationOfTimeTable, Label lblPriceTable, Label lblTable, Label lblTimeCounter, System.Windows.Forms.Button buttonPause)
        {
            Summary.SelectedTablesNow--;

            UpdateSummary();
            UpdateLabelAfterClickStop(lblTimeInTable, lblDurationOfTimeTable, lblPriceTable, lblTable, lblTimeCounter, textBox, buttonPause);

            comboBox.Text = "";
        }
















        private void btnStartTable1_Click(object sender, EventArgs e)
        {
            BookingDetailsTable1 = LoadBilliardDrumDetalis(cbChooseTable1, txtPlayerTable1, BookingDetailsTable1);
            Start(btnPauseTable1, cbChooseTable1, txtPlayerTable1, lblTimeInTable1,
                lblDurationOfTimeTable1, lblPriceTable1, TimeCounterTable1, lblTable1, BookingDetailsTable1);
        }

        private void btnStartTable2_Click(object sender, EventArgs e)
        {
            BookingDetailsTable2 = LoadBilliardDrumDetalis(cbChooseTable2, txtPlayerTable2, BookingDetailsTable2);
            Start(btnPauseTable2, cbChooseTable2, txtPlayerTable2, lblTimeInTable2,
                lblDurationOfTimeTable2, lblPriceTable2, TimeCounterTable2, lblTable2, BookingDetailsTable2);
        }

        private void btnStartTable3_Click(object sender, EventArgs e)
        {
            BookingDetailsTable3 = LoadBilliardDrumDetalis(cbChooseTable3, txtPlayerTable3, BookingDetailsTable3);
            Start(btnPauseTable3, cbChooseTable3, txtPlayerTable3, lblTimeInTable3,
                lblDurationOfTimeTable3, lblPriceTable3, TimeCounterTable3, lblTable3, BookingDetailsTable3);
        }

        private void btnStartTable4_Click(object sender, EventArgs e)
        {
            BookingDetailsTable4 = LoadBilliardDrumDetalis(cbChooseTable4, txtPlayerTable4, BookingDetailsTable4);
            Start(btnPauseTable4, cbChooseTable4, txtPlayerTable4, lblTimeInTable4,
                lblDurationOfTimeTable4, lblPriceTable4, TimeCounterTable4, lblTable4, BookingDetailsTable4);
        }

        private void btnStartTable5_Click(object sender, EventArgs e)
        {
            BookingDetailsTable5 = LoadBilliardDrumDetalis(cbChooseTable5, txtPlayerTable5, BookingDetailsTable5);
            Start(btnPauseTable5, cbChooseTable5, txtPlayerTable5, lblTimeInTable5,
                lblDurationOfTimeTable5, lblPriceTable5, TimeCounterTable5, lblTable5, BookingDetailsTable5);
        }

        private void btnStartTable6_Click(object sender, EventArgs e)
        {
            BookingDetailsTable6 = LoadBilliardDrumDetalis(cbChooseTable6, txtPlayerTable6, BookingDetailsTable6);
            Start(btnPauseTable6, cbChooseTable6, txtPlayerTable6, lblTimeInTable6,
                lblDurationOfTimeTable6, lblPriceTable6, TimeCounterTable6, lblTable6, BookingDetailsTable6);
        }

        private void btnStartTable7_Click(object sender, EventArgs e)
        {
            BookingDetailsTable7 = LoadBilliardDrumDetalis(cbChooseTable7, txtPlayerTable7, BookingDetailsTable7);
            Start(btnPauseTable7, cbChooseTable7, txtPlayerTable7, lblTimeInTable7,
                lblDurationOfTimeTable7, lblPriceTable7, TimeCounterTable7, lblTable7, BookingDetailsTable7);
        }

        private void btnStartTable8_Click(object sender, EventArgs e)
        {
            BookingDetailsTable8 = LoadBilliardDrumDetalis(cbChooseTable8, txtPlayerTable8, BookingDetailsTable8);
            Start(btnPauseTable8, cbChooseTable8, txtPlayerTable8, lblTimeInTable8,
                lblDurationOfTimeTable8, lblPriceTable8, TimeCounterTable8, lblTable8, BookingDetailsTable8);
        }

        private void btnStartTable9_Click(object sender, EventArgs e)
        {
            BookingDetailsTable9 = LoadBilliardDrumDetalis(cbChooseTable9, txtPlayerTable9, BookingDetailsTable9);
            Start(btnPauseTable9, cbChooseTable9, txtPlayerTable9, lblTimeInTable9,
                lblDurationOfTimeTable9, lblPriceTable9, TimeCounterTable9, lblTable9, BookingDetailsTable9);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeNow.Text = DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss tt");
        }



        private void TimeCounterTable1_Tick(object sender, EventArgs e)
        {
            TimerTable1.Second++;

            if (TimerTable1.Second == 60)
            {
                TimerTable1.Second = 0;
                ++TimerTable1.Minute;
            }

            if (TimerTable1.Minute == 60)
            {
                TimerTable1.Minute = 0;
                ++TimerTable1.Hour;
            }

            lblTimeCounterTable1.Text = FormatTime(TimerTable1.Hour) + ":" + FormatTime(TimerTable1.Minute) + ":"
            + FormatTime(TimerTable1.Second);

            if (Check(TimerTable1.Minute, TimeCounterTable1, "Table 1 ", BookingDetailsTable1))
            {
                Stop(cbChooseTable1, txtPlayerTable1, lblTimeInTable1, lblDurationOfTimeTable1,
                    lblPriceTable1, lblTable1, lblTimeCounterTable1, btnPauseTable1);

                TimeCounterTable1.Enabled = false;
                TimerTable1 = new stTimeCounter();
                BookingDetailsTable1 = new stBookingDetails();
            }

        }

        private void TimeCounterTable2_Tick(object sender, EventArgs e)
        {

            TimerTable2.Second++;

            if (TimerTable2.Second == 60)
            {
                TimerTable2.Second = 0;
                ++TimerTable2.Minute;
            }

            if (TimerTable2.Minute == 60)
            {
                TimerTable2.Minute = 0;
                ++TimerTable2.Hour;
            }

            lblTimeCounterTable2.Text = FormatTime(TimerTable2.Hour) + ":" + FormatTime(TimerTable2.Minute) + ":"
             + FormatTime(TimerTable2.Second);

            if (Check(TimerTable2.Minute, TimeCounterTable2, "Table 2 ", BookingDetailsTable2))
            {

                Stop(cbChooseTable2, txtPlayerTable2, lblTimeInTable2, lblDurationOfTimeTable2,
                    lblPriceTable2, lblTable2, lblTimeCounterTable2, btnPauseTable2);

                TimeCounterTable2.Enabled = false;
                TimerTable2 = new stTimeCounter();
                BookingDetailsTable2 = new stBookingDetails();
            }

        }

        private void TimeCounterTable3_Tick(object sender, EventArgs e)
        {
            TimerTable3.Second++;

            if (TimerTable3.Second == 60)
            {
                TimerTable3.Second = 0;
                ++TimerTable3.Minute;
            }

            if (TimerTable3.Minute == 60)
            {
                TimerTable3.Minute = 0;
                ++TimerTable3.Hour;
            }

            lblTimeCounterTable3.Text = FormatTime(TimerTable3.Hour) + ":" + FormatTime(TimerTable3.Minute) + ":"
            + FormatTime(TimerTable3.Second);

            if (Check(TimerTable3.Minute, TimeCounterTable3, "Table 3 ", BookingDetailsTable3))
            {
                Stop(cbChooseTable3, txtPlayerTable3, lblTimeInTable3, lblDurationOfTimeTable3,
                    lblPriceTable3, lblTable3, lblTimeCounterTable3, btnPauseTable3);

                TimeCounterTable3.Enabled = false;
                TimerTable3 = new stTimeCounter();
                BookingDetailsTable3 = new stBookingDetails();
            }

        }

        private void TimeCounterTable4_Tick(object sender, EventArgs e)
        {
            TimerTable4.Second++;

            if (TimerTable4.Second == 60)
            {
                TimerTable4.Second = 0;
                ++TimerTable4.Minute;
            }

            if (TimerTable4.Minute == 60)
            {
                TimerTable4.Minute = 0;
                ++TimerTable4.Hour;
            }

            lblTimeCounterTable4.Text = FormatTime(TimerTable4.Hour) + ":" + FormatTime(TimerTable4.Minute) + ":"
             + FormatTime(TimerTable4.Second);

            if (Check(TimerTable4.Minute, TimeCounterTable4, "Table 4 ", BookingDetailsTable4))
            {
                Stop(cbChooseTable4, txtPlayerTable4, lblTimeInTable4, lblDurationOfTimeTable4,
                    lblPriceTable4, lblTable4, lblTimeCounterTable4, btnPauseTable4);

                TimeCounterTable4.Enabled = false;
                TimerTable4 = new stTimeCounter();
                BookingDetailsTable4 = new stBookingDetails();
            }

        }

        private void TimeCounterTable5_Tick(object sender, EventArgs e)
        {
            TimerTable5.Second++;

            if (TimerTable5.Second == 60)
            {
                TimerTable5.Second = 0;
                ++TimerTable5.Minute;
            }

            if (TimerTable5.Minute == 60)
            {
                TimerTable5.Minute = 0;
                ++TimerTable5.Hour;
            }

            lblTimeCounterTable5.Text = FormatTime(TimerTable5.Hour) + ":" + FormatTime(TimerTable5.Minute) + ":"
             + FormatTime(TimerTable5.Second);

            if (Check(TimerTable5.Minute, TimeCounterTable5, "Table 5 ", BookingDetailsTable5))
            {

                Stop(cbChooseTable5, txtPlayerTable5, lblTimeInTable5, lblDurationOfTimeTable5,
                    lblPriceTable5, lblTable5, lblTimeCounterTable5, btnPauseTable5);

                TimeCounterTable5.Enabled = false;
                TimerTable5 = new stTimeCounter();
                BookingDetailsTable5 = new stBookingDetails();
            }

        }

        private void TimeCounterTable6_Tick(object sender, EventArgs e)
        {
            TimerTable6.Second++;

            if (TimerTable6.Second == 60)
            {
                TimerTable6.Second = 0;
                ++TimerTable6.Minute;
            }

            if (TimerTable6.Minute == 60)
            {
                TimerTable6.Minute = 0;
                ++TimerTable6.Hour;
            }

            lblTimeCounterTable6.Text = FormatTime(TimerTable6.Hour) + ":" + FormatTime(TimerTable6.Minute) + ":"
             + FormatTime(TimerTable6.Second);

            if (Check(TimerTable6.Minute, TimeCounterTable6, "Table 6 ", BookingDetailsTable6))
            {
                Stop(cbChooseTable6, txtPlayerTable6, lblTimeInTable6, lblDurationOfTimeTable6,
                    lblPriceTable6, lblTable6, lblTimeCounterTable6, btnPauseTable6);

                TimeCounterTable6.Enabled = false;
                TimerTable6 = new stTimeCounter();
                BookingDetailsTable6 = new stBookingDetails();
            }

        }

        private void TimeCounterTable7_Tick(object sender, EventArgs e)
        {
            TimerTable7.Second++;

            if (TimerTable7.Second == 60)
            {
                TimerTable7.Second = 0;
                ++TimerTable7.Minute;
            }

            if (TimerTable7.Minute == 60)
            {
                TimerTable7.Minute = 0;
                ++TimerTable7.Hour;
            }

            lblTimeCounterTable7.Text = FormatTime(TimerTable7.Hour) + ":" + FormatTime(TimerTable7.Minute) + ":"
             + FormatTime(TimerTable7.Second);

            if (Check(TimerTable7.Minute, TimeCounterTable7, "Table 7 ", BookingDetailsTable7))
            {
                Stop(cbChooseTable7, txtPlayerTable7, lblTimeInTable7, lblDurationOfTimeTable7,
                    lblPriceTable7, lblTable7, lblTimeCounterTable7, btnPauseTable7);

                TimeCounterTable7.Enabled = false;
                TimerTable7 = new stTimeCounter();
                BookingDetailsTable7 = new stBookingDetails();
            }

        }

        private void TimeCounterTable8_Tick(object sender, EventArgs e)
        {
            TimerTable8.Second++;

            if (TimerTable8.Second == 60)
            {
                TimerTable8.Second = 0;
                ++TimerTable8.Minute;
            }

            if (TimerTable8.Minute == 60)
            {
                TimerTable8.Minute = 0;
                ++TimerTable8.Hour;
            }

            lblTimeCounterTable8.Text = FormatTime(TimerTable8.Hour) + ":" + FormatTime(TimerTable8.Minute) + ":"
              + FormatTime(TimerTable8.Second);

            if (Check(TimerTable8.Minute, TimeCounterTable8, "Table 8 ", BookingDetailsTable8))
            {
                Stop(cbChooseTable8, txtPlayerTable8, lblTimeInTable8, lblDurationOfTimeTable8,
                    lblPriceTable8, lblTable8, lblTimeCounterTable8, btnPauseTable8);

                TimeCounterTable8.Enabled = false;
                TimerTable8 = new stTimeCounter();
                BookingDetailsTable8 = new stBookingDetails();
            }

        }

        private void TimeCounterTable9_Tick(object sender, EventArgs e)
        {
            TimerTable9.Second++;

            if (TimerTable9.Second == 60)
            {
                TimerTable9.Second = 0;
                ++TimerTable9.Minute;
            }

            if (TimerTable9.Minute == 60)
            {
                TimerTable9.Minute = 0;
                ++TimerTable9.Hour;
            }

            lblTimeCounterTable9.Text = FormatTime(TimerTable9.Hour) + ":" + FormatTime(TimerTable9.Minute) + ":"
               + FormatTime(TimerTable9.Second);

            if (Check(TimerTable9.Minute, TimeCounterTable9, "Table 9 ", BookingDetailsTable9))
            {
                Stop(cbChooseTable9, txtPlayerTable9, lblTimeInTable9, lblDurationOfTimeTable9,
                    lblPriceTable9, lblTable9, lblTimeCounterTable9, btnPauseTable9);

                TimeCounterTable9.Enabled = false;
                TimerTable9 = new stTimeCounter();
                BookingDetailsTable9 = new stBookingDetails();
            }

        }



        private void btnStopTable1_Click_1(object sender, EventArgs e)
        {

            TimeCounterTable1.Enabled = false;
            TimerTable1 = new stTimeCounter();
            BookingDetailsTable1 = new stBookingDetails();

            Stop(cbChooseTable1, txtPlayerTable1, lblTimeInTable1, lblDurationOfTimeTable1,
                     lblPriceTable1, lblTable1, lblTimeCounterTable1, btnPauseTable1);
        }

        private void btnStopTable2_Click(object sender, EventArgs e)
        {
            TimeCounterTable2.Enabled = false;
            TimerTable2 = new stTimeCounter();
            BookingDetailsTable2 = new stBookingDetails();

            Stop(cbChooseTable2, txtPlayerTable2, lblTimeInTable2, lblDurationOfTimeTable2,
                    lblPriceTable2, lblTable2, lblTimeCounterTable2, btnPauseTable2);
        }

        private void btnStopTable3_Click(object sender, EventArgs e)
        {
            TimeCounterTable3.Enabled = false;
            TimerTable3 = new stTimeCounter();
            BookingDetailsTable3 = new stBookingDetails();

            Stop(cbChooseTable3, txtPlayerTable3, lblTimeInTable3, lblDurationOfTimeTable3,
                    lblPriceTable3, lblTable3, lblTimeCounterTable3, btnPauseTable3);
        }

        private void btnStopTable4_Click(object sender, EventArgs e)
        {
            TimeCounterTable4.Enabled = false;
            TimerTable4 = new stTimeCounter();
            BookingDetailsTable4 = new stBookingDetails();

            Stop(cbChooseTable4, txtPlayerTable4, lblTimeInTable4, lblDurationOfTimeTable4,
                    lblPriceTable4, lblTable4, lblTimeCounterTable4, btnPauseTable4);
        }

        private void btnStopTable5_Click(object sender, EventArgs e)
        {
            TimeCounterTable5.Enabled = false;
            TimerTable5 = new stTimeCounter();
            BookingDetailsTable5 = new stBookingDetails();

            Stop(cbChooseTable5, txtPlayerTable5, lblTimeInTable5, lblDurationOfTimeTable5,
                    lblPriceTable5, lblTable5, lblTimeCounterTable5, btnPauseTable5);
        }

        private void btnStopTable6_Click(object sender, EventArgs e)
        {
            TimeCounterTable6.Enabled = false;
            TimerTable6 = new stTimeCounter();
            BookingDetailsTable6 = new stBookingDetails();

            Stop(cbChooseTable6, txtPlayerTable6, lblTimeInTable6, lblDurationOfTimeTable6,
                    lblPriceTable6, lblTable6, lblTimeCounterTable6, btnPauseTable6);
        }

        private void btnStopTable7_Click(object sender, EventArgs e)
        {
            TimeCounterTable7.Enabled = false;
            TimerTable7 = new stTimeCounter();
            BookingDetailsTable7 = new stBookingDetails();

            Stop(cbChooseTable7, txtPlayerTable7, lblTimeInTable7, lblDurationOfTimeTable7,
                               lblPriceTable7, lblTable7, lblTimeCounterTable7, btnPauseTable7);
        }

        private void btnStopTable8_Click(object sender, EventArgs e)
        {
            TimeCounterTable8.Enabled = false;
            TimerTable8 = new stTimeCounter();
            BookingDetailsTable8 = new stBookingDetails();

            Stop(cbChooseTable8, txtPlayerTable8, lblTimeInTable8, lblDurationOfTimeTable8,
                    lblPriceTable8, lblTable8, lblTimeCounterTable8, btnPauseTable8);
        }

        private void btnStopTable9_Click(object sender, EventArgs e)
        {
            TimeCounterTable9.Enabled = false;
            TimerTable9 = new stTimeCounter();
            BookingDetailsTable9 = new stBookingDetails();

            Stop(cbChooseTable9, txtPlayerTable9, lblTimeInTable9, lblDurationOfTimeTable9,
                    lblPriceTable9, lblTable9, lblTimeCounterTable9, btnPauseTable9);
        }



        private void btnPauseTable1_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable1, TimeCounterTable1, lblTable1);
        }

        private void btnPauseTable2_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable2, TimeCounterTable2, lblTable2);
        }

        private void btnPauseTable3_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable3, TimeCounterTable3, lblTable3);
        }

        private void btnPauseTable4_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable4, TimeCounterTable4, lblTable4);
        }

        private void btnPauseTable5_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable5, TimeCounterTable5, lblTable5);
        }

        private void btnPauseTable6_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable6, TimeCounterTable6, lblTable6);
        }

        private void btnPauseTable7_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable7, TimeCounterTable7, lblTable7);
        }

        private void btnPauseTable8_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable8, TimeCounterTable8, lblTable8);
        }

        private void btnPauseTable9_Click(object sender, EventArgs e)
        {
            ShowButtonPause(btnPauseTable9, TimeCounterTable9, lblTable9);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Summary.TablesAvailableNow = 9;
        }
    }
}
