namespace Kniffel
{
    public partial class Kniffel : Form
    {
        public Kniffel()
        {
            InitializeComponent();
            dice = new int[6];
        }

        Random rnd = new Random();

        int UebrigeWuerfe = 3;
        int Halten1, Halten2, Halten3, Halten4, Halten5;
        int[] dice;
        public void Reset() // Reset-Funktion welches alle Werte auf die Ursprünglichen zurücksetzt
        {
            // Ändere die Spielernamen-Eingabe (Spieler 1) zurück zu "Spieler 1"
            txtSpieler1.Text = "Spieler 1";
            // Ändere die Spielernamen-Eingabe (Spieler 2) zurück zu "Spieler 2"
            txtSpieler2.Text = "Spieler 2";
            // Aktueller Spieler auf 1 zurücksetzen
            AktuellerSpieler = 1;
            // Aktuellen Spieler auf Benutzeroberfläche zurücksetzen (Spieler 1)
            lblAktuellerSpieler.Text = "Aktueller Spieler: " + txtSpieler1.Text;
            // Übrige Würfe zurücksetzen
            UebrigeWuerfe = 3;
            // Übrige Würfe auf Benutzeroberfläche zurücksetzen
            lblUebrigeWuerfel.Text = UebrigeWuerfe.ToString();

        }

        private void btnWählenDreierpaschS1_Click(object sender, EventArgs e)
        {
            // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
            int[] counts = new int[7];
            int summe = 0;
            bool hatDreierpasch = false;

            // Wir gehen durch dice[1] bis dice[5]
            for (int i = 1; i <= 5; i++)
            {
                int wert = dice[i];
                if (wert >= 1 && wert <= 6)
                {
                    counts[wert]++;
                    summe += wert;
                }
            }

            // Prüfen, ob eine Zahl 3x oder öfter vorkommt
            for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
            {
                if (counts[augenzahl] >= 3)
                {
                    hatDreierpasch = true;
                    break;
                }
            }

            if (hatDreierpasch)
            {
                lblDreierpaschS1.Text = summe.ToString();
                btnWählenDreierpaschS1.Enabled = false;
                btnZug_Click(sender, e);
            }
            else
            {
                if (MessageBox.Show("Kein Dreierpasch vorhanden. Feld streichen?", "Kniffel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lblDreierpaschS1.Text = "0";
                    btnWählenDreierpaschS1.Enabled = false;
                    btnZug_Click(sender, e);
                }
            }
        }

        private void btnNeuesSpiel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnZug_Click(object sender, EventArgs e)
        {
            chkHalten1.Checked = false;
            chkHalten2.Checked = false;
            chkHalten3.Checked = false;
            chkHalten4.Checked = false;
            chkHalten5.Checked = false;

            chkHalten1.Visible = false;
            chkHalten2.Visible = false;
            chkHalten3.Visible = false;
            chkHalten4.Visible = false;
            chkHalten5.Visible = false;

            switch (AktuellerSpieler)
            {
                case 1: // Falls Aktueller Spieler = 1
                    AktuellerSpieler = 2; // Gebe Spieler 2 die Spieler-Initiative
                    // Ändere den Aktuellen Spieler zu Spieler 2 auf Benutzeroberfläche
                    lblAktuellerSpieler.Text = txtSpieler2.Text;
                    break;
                case 2: // Falls Aktueller Spieler = 2
                    AktuellerSpieler = 1; // Gebe Spieler 1 die Spieler-Initiative
                    // Ändere den Aktuellen Spieler zu Spieler 2 auf Benutzeroberfläche
                    lblAktuellerSpieler.Text = txtSpieler1.Text;
                    break;
                default: // Default
                    return;
            }
            // Übrige Würfe zurücksetzen
            UebrigeWuerfe = 3;
            // Übrige Würfe auf Benutzeroberfläche zurücksetzen
            lblUebrigeWuerfel.Text = UebrigeWuerfe.ToString();
        }

        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            WuerfelnLogik();
        }

        private void WuerfelnLogik()
        {
            if (!chkHalten1.Checked) { Halten1 = rnd.Next(1, 7); dice[1] = Halten1; }
            if (!chkHalten2.Checked) { Halten2 = rnd.Next(1, 7); dice[2] = Halten2; }
            if (!chkHalten3.Checked) { Halten3 = rnd.Next(1, 7); dice[3] = Halten3; }
            if (!chkHalten4.Checked) { Halten4 = rnd.Next(1, 7); dice[4] = Halten4; }
            if (!chkHalten5.Checked) { Halten5 = rnd.Next(1, 7); dice[5] = Halten5; }

            pctWuerfel1.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[1]}");
            pctWuerfel2.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[2]}");
            pctWuerfel3.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[3]}");
            pctWuerfel4.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[4]}");
            pctWuerfel5.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[5]}");
        }

        private void tmrAnimationStopper_Tick(object sender, EventArgs e)
        {
            tmrAnimation.Stop();
            tmrAnimationStopper.Stop();

            WuerfelnLogik(); // Ein letztes Mal die finalen Werte setzen

            if (chkHalten1.Checked && chkHalten2.Checked && chkHalten3.Checked && chkHalten4.Checked && chkHalten5.Checked)
            {
                MessageBox.Show("Es kann nicht gewürfelt werden, wenn alle Würfel gehalten werden.");
            }
            else
            {
                UebrigeWuerfe--;
                lblUebrigeWuerfel.Text = UebrigeWuerfe.ToString();
            }
        }

        private void btnWürfeln_Click(object sender, EventArgs e)
        {
            // Wenn es übrige Würfe gibt
            if (UebrigeWuerfe != 0)
            {
                if (UebrigeWuerfe > 4)
                {
                    chkHalten1.Checked = false;
                    chkHalten2.Checked = false;
                    chkHalten3.Checked = false;
                    chkHalten4.Checked = false;
                    chkHalten5.Checked = false;

                    chkHalten1.Visible = false;
                    chkHalten2.Visible = false;
                    chkHalten3.Visible = false;
                    chkHalten4.Visible = false;
                    chkHalten5.Visible = false;
                }
                else
                {
                    chkHalten1.Visible = true;
                    chkHalten2.Visible = true;
                    chkHalten3.Visible = true;
                    chkHalten4.Visible = true;
                    chkHalten5.Visible = true;
                }

                tmrAnimation.Start();
                tmrAnimationStopper.Start();

            }
            // Falls es keine übrige Würfe mehr gibt
            else
            {
                // Informiere durch einer MessageBox, dass keine Würfe mehr übrig sind
                MessageBox.Show("Es sind keine Würfe mehr übrig!");
            }
        }

        // Aktueller Spieler / Spieler-Initiative: 1 = Spieler 1; 2 = Spieler 2
        int AktuellerSpieler = 1;

        private void txtSpieler1_TextChanged(object sender, EventArgs e)
        {
            // Ändere Spielernamen (Spieler 1) auf der Punkteanzeige zu dem Inhalt der Spielernameneingabe (Spieler 1)
            lblSpieler1Name.Text = txtSpieler1.Text;

            // Falls Aktueller Spieler, Spieler 1 ist
            if (AktuellerSpieler == 1)
            {
                // Spielernamen Aktualisieren der "Aktueller Spieler" Anzeige
                lblAktuellerSpieler.Text = txtSpieler1.Text;
            }
        }

        private void txtSpieler2_TextChanged(object sender, EventArgs e)
        {
            // Ändere Spielernamen (Spieler 1) auf der Punkteanzeige zu dem Inhalt der Spielernameneingabe (Spieler 1)
            lblSpieler2Name.Text = txtSpieler2.Text;

            // Falls Aktueller Spieler, Spieler 1 ist
            if (AktuellerSpieler == 2)
            {
                // Spielernamen Aktualisieren der "Aktueller Spieler" Anzeige
                lblAktuellerSpieler.Text = txtSpieler2.Text;
            }
        }


    }
}