namespace Kniffel
{
    public partial class Kniffel : Form
    {
        public Kniffel()
        {
            InitializeComponent();
        }

        Random rnd = new Random();

        int UebrigeWuerfe = 3;
        int Halten1, Halten2, Halten3, Halten4, Halten5;

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
            // Generiere eine Zahl zwischen 1 und 6 für jeden Würfel, der nicht gehalten wird
            if (!chkHalten1.Checked) Halten1 = rnd.Next(1, 7);
            if (!chkHalten2.Checked) Halten2 = rnd.Next(1, 7);
            if (!chkHalten3.Checked) Halten3 = rnd.Next(1, 7);
            if (!chkHalten4.Checked) Halten4 = rnd.Next(1, 7);
            if (!chkHalten5.Checked) Halten5 = rnd.Next(1, 7);

            // Ändere dynamisch die Bilder der Würfel nach Zahl
            pctWuerfel1.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten1}");
            pctWuerfel2.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten2}");
            pctWuerfel3.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten3}");
            pctWuerfel4.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten4}");
            pctWuerfel5.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten5}");
        }

        private void tmrAnimationStopper_Tick(object sender, EventArgs e)
        {
            tmrAnimation.Stop();
            tmrAnimationStopper.Stop();

            // Generiere eine Zahl zwischen 1 und 6 für jeden Würfel, der nicht gehalten wird
            if (!chkHalten1.Checked) Halten1 = rnd.Next(1, 7);
            if (!chkHalten2.Checked) Halten2 = rnd.Next(1, 7);
            if (!chkHalten3.Checked) Halten3 = rnd.Next(1, 7);
            if (!chkHalten4.Checked) Halten4 = rnd.Next(1, 7);
            if (!chkHalten5.Checked) Halten5 = rnd.Next(1, 7);

            // Ändere dynamisch die Bilder der Würfel nach Zahl
            pctWuerfel1.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten1}");
            pctWuerfel2.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten2}");
            pctWuerfel3.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten3}");
            pctWuerfel4.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten4}");
            pctWuerfel5.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{Halten5}");

            // Falls jede Halten-Checkbox abgehakt wurde
            if
                (
                chkHalten1.Checked
                && chkHalten2.Checked
                && chkHalten3.Checked
                && chkHalten4.Checked
                && chkHalten5.Checked
                )
            {
                // Zeige MessageBox, die sagt dass ein Wurf mit diesen Bedingungen nicht möglich sei
                MessageBox.Show("Es kann nicht gewürfelt werden, wenn alle Würfel gehalten werden. Falls du den Zug weitergeben möchtest, klicke Zug");
            }
            // Fals nicht jede Halten-Checkbox abgehakt wurde
            else
            {
                // Subtrahiere "Übrige Würfe" um 1
                UebrigeWuerfe--;
                // Ändert die Anzeige der übrigen Würfen zu dem Integer-Wert der übrigen Würfen
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