namespace Kniffel
{
    public partial class Kniffel : Form
    {
        public Kniffel()
        {
            InitializeComponent();
            dice = new int[6];
        }

        // ----------VARIABLEN----------

        bool formRegelnOpened = false; // Variabel für den Status, ob das Regeln-Formular geöffnet oder geschlossen ist. false = geschlossen, true = geöffnet
        bool gewürfelt = false; // Status-Boolean Variabel, ob gewürfelt wurde oder nicht. (Wichtig, damit ein Spieler nach dem Spielerwechsel nicht das Gleiche,
                                // wählen kann, was der vorherige Spieler gewählt hat)
        bool gewählt = false; // Status-Boolean Variabel, ob der Spieler bereits eine Kategorie gewählt hat oder nicht.
        int UebrigeWuerfe = 3; // Variabel für die übrigen Würfe die jeder Spieler hat.
                               // Die Restlichen Variabeln die fürs Halten der Würfel, der Summe für den Oberen und Unteren Block gebraucht werden.
        int Halten1, Halten2, Halten3, Halten4, Halten5, obererblocks1 = 0, obererblocks2 = 0, untererblocks1 = 0, untererblocks2 = 0, gesamts1 = 0, gesamts2 = 0;
        // Array für den Dice
        int[] dice;
        // Objekt Definition (Random)
        Random rnd = new Random();

        // ----------NEUES SPIEL----------
        private void btnNeuesSpiel_Click(object sender, EventArgs e)
        {
            Application.Restart(); // Schließt die konkurrente Applikationsinstanz und öffnet eine neue. (Hard - Reload)
        }

        // ----------LOGIK----------

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

        // ----------SPIEL LOGIK----------


        private void WuerfelnLogik() // Die Würfel Logik für das Würfeln. Checkt ob ein Würfel gehalten ist oder nicht damit dann der nächste Wurf funktioniert.
        {
            // Falls Würfel 1 nicht gehalten ist, generiere eine Zufallszahl zwischen 1 und 6 und speichere sie in Halten1 und dice[1]
            if (!chkHalten1.Checked) { Halten1 = rnd.Next(1, 7); dice[1] = Halten1; }
            // Falls Würfel 2 nicht gehalten ist, generiere eine Zufallszahl zwischen 1 und 6 und speichere sie in Halten2 und dice[2]
            if (!chkHalten2.Checked) { Halten2 = rnd.Next(1, 7); dice[2] = Halten2; }
            // Falls Würfel 3 nicht gehalten ist, generiere eine Zufallszahl zwischen 1 und 6 und speichere sie in Halten3 und dice[3]
            if (!chkHalten3.Checked) { Halten3 = rnd.Next(1, 7); dice[3] = Halten3; }
            // Falls Würfel 4 nicht gehalten ist, generiere eine Zufallszahl zwischen 1 und 6 und speichere sie in Halten4 und dice[4]
            if (!chkHalten4.Checked) { Halten4 = rnd.Next(1, 7); dice[4] = Halten4; }
            // Falls Würfel 5 nicht gehalten ist, generiere eine Zufallszahl zwischen 1 und 6 und speichere sie in Halten5 und dice[5]
            if (!chkHalten5.Checked) { Halten5 = rnd.Next(1, 7); dice[5] = Halten5; }

            // Setze das Bild von Würfel 1 auf das entsprechende Würfelbild basierend auf dem Wert in dice[1]
            pctWuerfel1.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[1]}");
            // Setze das Bild von Würfel 2 auf das entsprechende Würfelbild basierend auf dem Wert in dice[2]
            pctWuerfel2.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[2]}");
            // Setze das Bild von Würfel 3 auf das entsprechende Würfelbild basierend auf dem Wert in dice[3]
            pctWuerfel3.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[3]}");
            // Setze das Bild von Würfel 4 auf das entsprechende Würfelbild basierend auf dem Wert in dice[4]
            pctWuerfel4.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[4]}");
            // Setze das Bild von Würfel 5 auf das entsprechende Würfelbild basierend auf dem Wert in dice[5]
            pctWuerfel5.Image = (Image)Properties.Resources.ResourceManager.GetObject($"dice{dice[5]}");
        }

        // Zug Methode damit der Spieler den Zug für den anderen Spieler startet.
        private void btnZug_Click(object sender, EventArgs e)
        {
            // Falls der Spieler bereits eine Kategorie gewählt hat
            if (gewählt)
            {
                // Setzt die Checkbox zum Halten beim Zug auf false damit der nächste Spieler anfangen kann. (1-5)
                chkHalten1.Checked = false;
                chkHalten2.Checked = false;
                chkHalten3.Checked = false;
                chkHalten4.Checked = false;
                chkHalten5.Checked = false;

                // Setzt die Checkbox Sichtbarkeit auf false damit der Spieler erst nachdem ersten Wurf halten kann.
                chkHalten1.Visible = false;
                chkHalten2.Visible = false;
                chkHalten3.Visible = false;
                chkHalten4.Visible = false;
                chkHalten5.Visible = false;

                // Setze die Status-Variabeln zurück für den nächsten Spieler
                gewählt = false;
                gewürfelt = false;

                // Switch damit lblAktuellerSpieler immer den akuraten Spieler anzeigt der gerade am Zug ist.
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
            else
            {
                // Falls der Spieler noch nichts gewählt hat, zeige eine Fehlermeldung an
                MessageBox.Show
                    (
                    "Du kannst den Zug erst weitergeben, nachdem du etwas gewählt oder gestrichen hast!",
                    "Vorgang nicht möglich!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );
            }
        }

        // Klick Methode damit durchs klicken des Buttons gewürfelt wird und die Animation startet und der Animationsstopper der über einen Timer funktioniert.
        private void btnWürfeln_Click(object sender, EventArgs e)
        {
            // Setze den Status auf true, dass gewürfelt wurde
            gewürfelt = true;
            // Wenn es übrige Würfe gibt
            if (UebrigeWuerfe != 0)
            {
                // Falls mehr als 4 übrige Würfe vorhanden sind (sollte nicht vorkommen, aber Sicherheitsprüfung)
                if (UebrigeWuerfe > 4)
                {
                    // Setze alle Halten-Checkboxen auf unchecked
                    chkHalten1.Checked = false;
                    chkHalten2.Checked = false;
                    chkHalten3.Checked = false;
                    chkHalten4.Checked = false;
                    chkHalten5.Checked = false;

                    // Verstecke alle Halten-Checkboxen
                    chkHalten1.Visible = false;
                    chkHalten2.Visible = false;
                    chkHalten3.Visible = false;
                    chkHalten4.Visible = false;
                    chkHalten5.Visible = false;
                }
                else
                {
                    // Zeige alle Halten-Checkboxen an, damit der Spieler Würfel halten kann
                    chkHalten1.Visible = true;
                    chkHalten2.Visible = true;
                    chkHalten3.Visible = true;
                    chkHalten4.Visible = true;
                    chkHalten5.Visible = true;
                }

                // Starte den Animations-Timer für die Würfelanimation
                tmrAnimation.Start();
                // Starte den Timer der die Animation nach einer gewissen Zeit stoppt
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


        // ----------ANIMATION----------


        // Würfel Animation damit beim Wurf eine Würfel animation abgespielt wird. 
        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            // Rufe die Würfeln-Logik auf um die Würfelbilder zu aktualisieren (erzeugt Animation-Effekt)
            WuerfelnLogik();
        }

        // Timer um die Animation vom würfeln zu stoppen
        private void tmrAnimationStopper_Tick(object sender, EventArgs e)
        {
            // Stoppe den Animations-Timer
            tmrAnimation.Stop();
            // Stoppe den Animations-Stopper-Timer
            tmrAnimationStopper.Stop();

            // Ein letztes Mal die finalen Werte setzen
            WuerfelnLogik();

            // if statement um zu checken ob alle 5 Würfeln gehalten sind, if true dann wird eine Messagebox angezeigt damit der Spieler nicht Würfeln kann.
            if (chkHalten1.Checked && chkHalten2.Checked && chkHalten3.Checked && chkHalten4.Checked && chkHalten5.Checked)
            {
                MessageBox.Show("Es kann nicht gewürfelt werden, wenn alle Würfel gehalten werden.");
            }
            else
            {
                // Reduziere die Anzahl der übrigen Würfe um 1
                UebrigeWuerfe--;
                // Aktualisiere die Anzeige der übrigen Würfe auf der Benutzeroberfläche
                lblUebrigeWuerfel.Text = UebrigeWuerfe.ToString();
            }
        }


        // ----------WÜRFEL LOGIK----------

        //Würfel Logik für die Einser (Spieler 1)
        private void btnWählenEinserS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt == true && gewählt == false)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 1) // falls Würfel 1-5(je nach Iterationsnummer) = 1 ist:
                        {
                            summe++; // Addiere 1 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenEinserS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenEinserS1
                        lblEinserS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 1
                        obererblocks1 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS1.Text = obererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Einser vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Einser vorhanden. Feld streichen?", "Einser", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblEinserS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenEinserS1.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks1 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS1.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts1 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS1.Text = gesamts1.ToString();

                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Einser (Spieler 2)
        private void btnWählenEinserS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 2 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 1) // falls Würfel 1-5(je nach Iterationsnummer) = 1 ist:
                        {
                            summe++; // Addiere 1 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenEinserS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenEinserS2
                        lblEinserS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 2
                        obererblocks2 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS2.Text = obererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls keine Einser vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Einser vorhanden. Feld streichen?", "Einser", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblEinserS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenEinserS2.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks2 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS2.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts2 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Zweier (Spieler 1)
        private void btnWählenZweierS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 2) // falls Würfel 1-5(je nach Iterationsnummer) = 2 ist:
                        {
                            summe += 2; // Addiere 2 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenZweierS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenZweierS1
                        lblZweierS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 1
                        obererblocks1 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS1.Text = obererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Zweier vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Zweier vorhanden. Feld streichen?", "Zweier", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblZweierS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenZweierS1.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks1 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS1.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts1 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Zweier (Spieler 2)
        private void btnWählenZweierS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 2) // falls Würfel 1-5(je nach Iterationsnummer) = 2 ist:
                        {
                            summe += 2; // Addiere 2 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenZweierS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenZweierS2
                        lblZweierS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 2
                        obererblocks2 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS2.Text = obererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls keine Zweier vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Zweier vorhanden. Feld streichen?", "Zweier", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblZweierS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenZweierS2.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks2 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS2.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts2 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Dreier (Spieler 1)
        private void btnWählenDreierS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 3) // falls Würfel 1-5(je nach Iterationsnummer) = 3 ist:
                        {
                            summe += 3; // Addiere 3 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenDreierS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenDreierS1
                        lblDreierS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 1
                        obererblocks1 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS1.Text = obererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Dreier vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Dreier vorhanden. Feld streichen?", "Dreier", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblDreierS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenDreierS1.Enabled = false;

                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks1 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS1.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts1 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Dreier (Spieler 2)
        private void btnWählenDreierS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 3) // falls Würfel 1-5(je nach Iterationsnummer) = 3 ist:
                        {
                            summe += 3; // Addiere 3 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenDreierS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenDreierS2
                        lblDreierS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 2
                        obererblocks2 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS2.Text = obererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls keine Dreier vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Dreier vorhanden. Feld streichen?", "Dreier", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblDreierS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenDreierS2.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks2 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS2.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts2 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Vierer (Spieler 1)
        private void btnWählenViererS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 4) // falls Würfel 1-5(je nach Iterationsnummer) = 4 ist:
                        {
                            summe += 4; // Addiere 4 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenViererS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenViererS1
                        lblViererS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 1
                        obererblocks1 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS1.Text = obererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Vierer vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Vierer vorhanden. Feld streichen?", "Vierer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblViererS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenViererS1.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks1 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS1.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts1 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Vierer (Spieler 2)
        private void btnWählenViererS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 2 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 4) // falls Würfel 1-5(je nach Iterationsnummer) = 4 ist:
                        {
                            summe += 4; // Addiere 4 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenViererS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenViererS2
                        lblViererS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 2
                        obererblocks2 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS2.Text = obererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls keine Vierer vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Vierer vorhanden. Feld streichen?", "Vierer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblViererS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenViererS2.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks2 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS2.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts2 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Fünfer (Spieler 1)
        private void btnWählenFünferS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 5) // falls Würfel 1-5(je nach Iterationsnummer) = 5 ist:
                        {
                            summe += 5; // Addiere 5 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenFünferS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenFünferS1
                        lblFuenferS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 1
                        obererblocks1 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS1.Text = obererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Fünfer vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Fünfer vorhanden. Feld streichen?", "Fünfer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblFuenferS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenFünferS1.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks1 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS1.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts1 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        // Würfel Logik für die Fünfer (Spieler 2)
        private void btnWählenFünferS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 2 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 5) // falls Würfel 1-5(je nach Iterationsnummer) = 5 ist:
                        {
                            summe += 5; // Addiere 5 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenFünferS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenFünferS1
                        lblFuenferS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 2
                        obererblocks2 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS2.Text = obererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche (HINWEIS: Hier ist ein Bug - es sollte gesamts2 sein)
                        lblGesamtS2.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Fünfer vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Fünfer vorhanden. Feld streichen?", "Fünfer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblFuenferS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenFünferS2.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks2 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS2.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts2 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Sechser (Spieler 1)
        private void btnWählenSechserS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 2 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 6) // falls Würfel 1-5(je nach Iterationsnummer) = 6 ist:
                        {
                            summe += 6; // Addiere 6 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenSechserS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenSechserS1
                        lblSechserS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 1
                        obererblocks1 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS1.Text = obererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Sechser vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Sechser vorhanden. Feld streichen?", "Sechser", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblSechserS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenSechserS1.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks1 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS1.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts1 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Sechser (Spieler 2)
        private void btnWählenSechserS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 2 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        if (dice[i] == 6) // falls Würfel 1-5(je nach Iterationsnummer) = 6 ist:
                        {
                            summe += 6; // Addiere 6 auf dem Summenwert
                        }
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenSechserS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenSechserS2
                        lblSechserS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum oberen Block von Spieler 2
                        obererblocks2 += summe;
                        // Aktualisiere die Anzeige des oberen Blocks auf der Benutzeroberfläche
                        lblObererBlockS2.Text = obererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls keine Sechser vorhanden sind, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Sechser vorhanden. Feld streichen?", "Sechser", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblSechserS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenSechserS2.Enabled = false;
                        }
                    }

                    // Prüfe ob der obere Block mindestens 63 Punkte hat für den Bonus
                    if (obererblocks2 >= 63)
                    {
                        // Setze den Bonus auf 35
                        lblBonusS2.Text = "35";
                        // Addiere den Bonus zum Gesamtpunktestand
                        gesamts2 += 35;
                        // Aktualisiere die Gesamtpunkteanzeige
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Dreierpasch (Spieler 1)
        private void btnWählenDreierpaschS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 1 ist
                if (AktuellerSpieler == 1)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Dreierpasch vorhanden ist
                    bool hatDreierpasch = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 3x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 3x vorkommt
                        if (counts[augenzahl] >= 3)
                        {
                            // Setze hatDreierpasch auf true
                            hatDreierpasch = true;
                            // Verlasse die Schleife da ein Dreierpasch gefunden wurde
                            break;
                        }
                    }

                    // Falls ein Dreierpasch vorhanden ist
                    if (hatDreierpasch)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf die Summe aller Würfel
                        lblDreierpaschS1.Text = summe.ToString();
                        // Deaktiviere den Button
                        btnWählenDreierpaschS1.Enabled = false;
                        // Addiere die Summe zum unteren Block von Spieler 1
                        untererblocks1 += summe;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls kein Dreierpasch vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keinen Dreierpasch vorhanden. Feld streichen?", "Dreierpasch", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblDreierpaschS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenDreierpaschS1.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Dreierpasch (Spieler 2)
        private void btnWählenDreierpaschS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 2 ist
                if (AktuellerSpieler == 2)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Dreierpasch vorhanden ist
                    bool hatDreierpasch = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 3x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 3x vorkommt
                        if (counts[augenzahl] >= 3)
                        {
                            // Setze hatDreierpasch auf true
                            hatDreierpasch = true;
                            // Verlasse die Schleife da ein Dreierpasch gefunden wurde
                            break;
                        }
                    }

                    // Falls ein Dreierpasch vorhanden ist
                    if (hatDreierpasch)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf die Summe aller Würfel
                        lblDreierpaschS2.Text = summe.ToString();
                        // Deaktiviere den Button
                        btnWählenDreierpaschS2.Enabled = false;
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls kein Dreierpasch vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keinen Dreierpasch vorhanden. Feld streichen?", "Dreierpasch", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblDreierpaschS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenDreierpaschS2.Enabled = false;
                            // Addiere die Summe zum unteren Block von Spieler 2 (HINWEIS: Bei Streichen sollte 0 addiert werden, nicht summe)
                            untererblocks2 += summe;
                            // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                            lblUntererBlockS2.Text = untererblocks2.ToString();
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Viererpasch (Spieler 1)
        private void btnWählenViererpaschS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 1 ist
                if (AktuellerSpieler == 1)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Viererpasch vorhanden ist
                    bool hatViererpasch = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 3x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 4x vorkommt
                        if (counts[augenzahl] >= 4)
                        {
                            // Setze hatViererpasch auf true
                            hatViererpasch = true;
                            // Verlasse die Schleife da ein Viererpasch gefunden wurde
                            break;
                        }
                    }

                    // Falls ein Viererpasch vorhanden ist
                    if (hatViererpasch)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf die Summe aller Würfel
                        lblViererpaschS1.Text = summe.ToString();
                        // Deaktiviere den Button
                        btnWählenViererpaschS1.Enabled = false;
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls kein Viererpasch vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keinen Viererpasch vorhanden. Feld streichen?", "Viererpasch", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblViererpaschS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenViererpaschS1.Enabled = false;
                            // Addiere die Summe zum unteren Block von Spieler 1 (HINWEIS: Bei Streichen sollte 0 addiert werden, nicht summe)
                            untererblocks1 += summe;
                            // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                            lblUntererBlockS1.Text = untererblocks1.ToString();
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Viererpasch (Spieler 2)
        private void btnWählenViererpaschS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 2 ist
                if (AktuellerSpieler == 2)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Viererpasch vorhanden ist
                    bool hatViererpasch = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 3x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 4x vorkommt
                        if (counts[augenzahl] >= 4)
                        {
                            // Setze hatViererpasch auf true
                            hatViererpasch = true;
                            // Verlasse die Schleife da ein Viererpasch gefunden wurde
                            break;
                        }
                    }

                    // Falls ein Viererpasch vorhanden ist
                    if (hatViererpasch)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf die Summe aller Würfel
                        lblViererpaschS2.Text = summe.ToString();
                        // Deaktiviere den Button
                        btnWählenViererpaschS2.Enabled = false;
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls kein Viererpasch vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keinen Viererpasch vorhanden. Feld streichen?", "Viererpasch", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblViererpaschS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenViererpaschS2.Enabled = false;
                            // Addiere die Summe zum unteren Block von Spieler 2 (HINWEIS: Bei Streichen sollte 0 addiert werden, nicht summe)
                            untererblocks2 += summe;
                            // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                            lblUntererBlockS2.Text = untererblocks2.ToString();
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        // Event-Handler für das Laden des Formulars (leer, keine zusätzliche Logik benötigt)
        private void Kniffel_Load(object sender, EventArgs e)
        {

        }

        // Klick-Event für den Regeln-Button um das Regeln-Formular zu öffnen
        private void btnRegeln_Click(object sender, EventArgs e)
        {
            Form formRegeln = new Form2(); // formRegeln Definieren
            formRegeln.FormClosed += Form2_FormClosed; // Event-Listener Initialisieren für Funktion Form2_FormClosed()

            if (!formRegelnOpened) // Falls Variabel formRegelnOpened false ist
            {
                formRegeln.Show(); // Öffne Regel-Formular 
                formRegelnOpened = true; // Ändere Variabel formRegelnOpened zu true
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e) // Listener-Event (Wenn Regel-Formular geschlossen wird)
        {
            formRegelnOpened = false; // Ändere Variabel formRegelnOpened zu false
        }

        //Würfel Logik für die FullHouse (Spieler 1)
        private void btnWählenFullHouseS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 1 ist
                if (AktuellerSpieler == 1)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Drilling (3 gleiche) vorhanden ist
                    bool hatFullHouse3er = false;
                    // Boolean-Variabel um zu speichern ob ein Paar (2 gleiche) vorhanden ist
                    bool hatFullHouse2er = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 3x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 3x vorkommt
                        if (counts[augenzahl] >= 3)
                        {
                            // Setze hatFullHouse3er auf true (Drilling gefunden)
                            hatFullHouse3er = true;
                            // Debug-Ausgabe in der Konsole
                            System.Console.WriteLine("1");
                        }
                        // Prüfen, ob eine Zahl 2x vorkommt
                        else if (counts[augenzahl] == 2)
                        {
                            // Setze hatFullHouse2er auf true (Paar gefunden)
                            hatFullHouse2er = true;
                            // Debug-Ausgabe in der Konsole
                            System.Console.WriteLine("2");
                        }
                    }

                    // Falls sowohl ein Paar als auch ein Drilling vorhanden ist (= Full House)
                    if (hatFullHouse2er == true && hatFullHouse3er == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 25 (fester Punktwert für Full House)
                        lblFullHouseS1.Text = "25";
                        // Deaktiviere den Button
                        btnWählenFullHouseS1.Enabled = false;
                        // Addiere 25 Punkte zum Gesamtpunktestand von Spieler 1
                        gesamts1 += 25;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                        // Addiere 25 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 25;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                    }
                    else
                    {
                        // Falls kein Full House vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keinen Full House vorhanden. Feld streichen?", "Full House", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblFullHouseS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenFullHouseS1.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die FullHouse (Spieler 2)
        private void btnWählenFullHouseS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 2 ist
                if (AktuellerSpieler == 2)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Drilling (3 gleiche) vorhanden ist
                    bool hatFullHouse3er = false;
                    // Boolean-Variabel um zu speichern ob ein Paar (2 gleiche) vorhanden ist
                    bool hatFullHouse2er = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 3x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 3x vorkommt
                        if (counts[augenzahl] >= 3)
                        {
                            // Setze hatFullHouse3er auf true (Drilling gefunden)
                            hatFullHouse3er = true;
                            // Debug-Ausgabe in der Konsole
                            System.Console.WriteLine("1");
                        }
                        // Prüfen, ob eine Zahl 2x vorkommt
                        else if (counts[augenzahl] == 2)
                        {
                            // Setze hatFullHouse2er auf true (Paar gefunden)
                            hatFullHouse2er = true;
                            // Debug-Ausgabe in der Konsole
                            System.Console.WriteLine("2");
                        }
                    }

                    // Falls sowohl ein Paar als auch ein Drilling vorhanden ist (= Full House)
                    if (hatFullHouse2er == true && hatFullHouse3er == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 25 (fester Punktwert für Full House)
                        lblFullHouseS2.Text = "25";
                        // Deaktiviere den Button
                        btnWählenFullHouseS2.Enabled = false;
                        // Addiere 25 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 25;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 25 Punkte zum Gesamtpunktestand von Spieler 2
                        gesamts2 += 25;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls kein Full House vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keinen Full House vorhanden. Feld streichen?", "Full House", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblFullHouseS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenFullHouseS2.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Kleine Straße (Spieler 1)
        private void btnWählenKleineStrasseS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 1 ist
                if (AktuellerSpieler == 1)
                {
                    // Boolean-Variabeln um zu speichern welche Augenzahlen vorhanden sind
                    bool eins = false;
                    bool zwei = false;
                    bool drei = false;
                    bool vier = false;
                    bool fuenf = false;
                    bool sechs = false;
                    // Durchlaufe alle Würfel um zu prüfen welche Augenzahlen vorhanden sind
                    for (int i = 0; i < 6; i++)
                    {
                        // Checkt ob dice[i] eins ist wenn ja wird die Variable auf true gesetzt
                        if (dice[i] == 1)
                        {
                            eins = true;
                        }
                        // Checkt ob dice[i] zwei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 2)
                        {
                            zwei = true;
                        }
                        // Checkt ob dice[i] drei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 3)
                        {
                            drei = true;
                        }
                        // Checkt ob dice[i] vier ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 4)
                        {
                            vier = true;
                        }
                        // Checkt ob dice[i] fünf ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 5)
                        {
                            fuenf = true;
                        }
                        // Checkt ob dice[i] sechs ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 6)
                        {
                            sechs = true;
                        }
                    }

                    // Prüfe ob die Kleine Straße 1-2-3-4 vorhanden ist
                    if (eins == true && zwei == true && drei == true && vier == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 30 (fester Punktwert für Kleine Straße)
                        lblKleineStrasseS1.Text = "30";
                        // Deaktiviere den Button
                        btnWählenKleineStrasseS1.Enabled = false;
                        // Addiere 30 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 30;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere 30 Punkte zum Gesamtpunktestand von Spieler 1
                        gesamts1 += 30;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    // Prüfe ob die Kleine Straße 2-3-4-5 vorhanden ist
                    else if (zwei == true && drei == true && vier == true && fuenf == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 30 (fester Punktwert für Kleine Straße)
                        lblKleineStrasseS1.Text = "30";
                        // Deaktiviere den Button
                        btnWählenKleineStrasseS1.Enabled = false;
                        // Addiere 30 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 30;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere 30 Punkte zum Gesamtpunktestand von Spieler 1
                        gesamts1 += 30;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    // Prüfe ob die Kleine Straße 3-4-5-6 vorhanden ist
                    else if (drei == true && vier == true && fuenf == true && sechs == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 30 (fester Punktwert für Kleine Straße)
                        lblKleineStrasseS1.Text = "30";
                        // Deaktiviere den Button
                        btnWählenKleineStrasseS1.Enabled = false;
                        // Addiere 30 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 30;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere 30 Punkte zum Gesamtpunktestand von Spieler 1
                        gesamts1 += 30;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Kleine Straße vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Kleine Strasse vorhanden. Feld streichen?", "Kleine Strasse", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblKleineStrasseS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenKleineStrasseS1.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Kleine Straße (Spieler 2)
        private void btnWählenKleineStrasseS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 2 ist
                if (AktuellerSpieler == 2)
                {
                    // Boolean-Variabeln um zu speichern welche Augenzahlen vorhanden sind
                    bool eins = false;
                    bool zwei = false;
                    bool drei = false;
                    bool vier = false;
                    bool fuenf = false;
                    bool sechs = false;
                    // Durchlaufe alle Würfel um zu prüfen welche Augenzahlen vorhanden sind
                    for (int i = 0; i < 6; i++)
                    {
                        // Checkt ob dice[i] eins ist wenn ja wird die Variable auf true gesetzt
                        if (dice[i] == 1)
                        {
                            eins = true;
                        }
                        // Checkt ob dice[i] zwei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 2)
                        {
                            zwei = true;
                        }
                        // Checkt ob dice[i] drei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 3)
                        {
                            drei = true;
                        }
                        // Checkt ob dice[i] vier ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 4)
                        {
                            vier = true;
                        }
                        // Checkt ob dice[i] fünf ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 5)
                        {
                            fuenf = true;
                        }
                        // Checkt ob dice[i] sechs ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 6)
                        {
                            sechs = true;
                        }
                    }
                    // Prüfe ob die Kleine Straße 1-2-3-4 vorhanden ist
                    if (eins == true && zwei == true && drei == true && vier == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 30 (fester Punktwert für Kleine Straße)
                        lblKleineStrasseS2.Text = "30";
                        // Deaktiviere den Button
                        btnWählenKleineStrasseS2.Enabled = false;
                        // Addiere 30 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 30;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 30 Punkte zum Gesamtpunktestand von Spieler 2
                        gesamts2 += 30;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    // Prüfe ob die Kleine Straße 2-3-4-5 vorhanden ist
                    else if (zwei == true && drei == true && vier == true && fuenf == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 30 (fester Punktwert für Kleine Straße)
                        lblKleineStrasseS2.Text = "30";
                        // Deaktiviere den Button
                        btnWählenKleineStrasseS2.Enabled = false;
                        // Addiere 30 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 30;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 30 Punkte zum Gesamtpunktestand von Spieler 1 (HINWEIS: Hier ist ein Bug - es sollte gesamts2 sein)
                        gesamts1 += 30;
                        // Aktualisiere die Gesamtpunkteanzeige von Spieler 1 (HINWEIS: Hier ist ein Bug - es sollte lblGesamtS2 und gesamts2 sein)
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    // Prüfe ob die Kleine Straße 3-4-5-6 vorhanden ist
                    else if (drei == true && vier == true && fuenf == true && sechs == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 30 (fester Punktwert für Kleine Straße)
                        lblKleineStrasseS2.Text = "30";
                        // Deaktiviere den Button
                        btnWählenKleineStrasseS2.Enabled = false;
                        // Addiere 30 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 30;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 30 Punkte zum Gesamtpunktestand von Spieler 1 (HINWEIS: Hier ist ein Bug - es sollte gesamts2 sein)
                        gesamts1 += 30;
                        // Aktualisiere die Gesamtpunkteanzeige von Spieler 1 (HINWEIS: Hier ist ein Bug - es sollte lblGesamtS2 und gesamts2 sein)
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls keine Kleine Straße vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Kleine Strasse vorhanden. Feld streichen?", "Kleine Strasse", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblKleineStrasseS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenKleineStrasseS2.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Größe Straße (Spieler 1)
        private void btnWählenGrosseStrasseS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Checkt welcher Spieler gerade dran ist, wenn Spieler 1 ist dann geht es weiter
                if (AktuellerSpieler == 1)
                {
                    // Bools um zu checken ob die Grosse Strasse erkannt ist oder nicht, wird am anfang auf false gesetzt.
                    bool eins = false;
                    bool zwei = false;
                    bool drei = false;
                    bool vier = false;
                    bool fuenf = false;
                    bool sechs = false;
                    // for loop der jeden würfel einmal checkt ob es eine 1-6 ist
                    for (int i = 0; i < 6; i++)
                    {
                        // Checkt ob dice[i] eins ist wenn ja wird die Variable auf true gesetzt
                        if (dice[i] == 1)
                        {
                            eins = true;
                        }
                        // Checkt ob dice[i] zwei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 2)
                        {
                            zwei = true;
                        }
                        // Checkt ob dice[i] drei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 3)
                        {
                            drei = true;
                        }
                        // Checkt ob dice[i] vier ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 4)
                        {
                            vier = true;
                        }
                        // Checkt ob dice[i] fuenf ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 5)
                        {
                            fuenf = true;
                        }
                        // Checkt ob dice[i] sechs ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 6)
                        {
                            sechs = true;
                        }
                    }
                    // Prüfe ob die Große Straße 1-2-3-4-5 vorhanden ist
                    if (eins == true && zwei == true && drei == true && vier == true && fuenf == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 40 (fester Punktwert für Große Straße)
                        lblGrosseStrasseS1.Text = "40";
                        // Deaktiviere den Button
                        btnWählenGrosseStrasseS1.Enabled = false;
                        // Addiere 40 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 40;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere 40 Punkte zum Gesamtpunktestand von Spieler 1
                        gesamts1 += 40;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    // Prüfe ob die Große Straße 2-3-4-5-6 vorhanden ist
                    else if (zwei == true && drei == true && vier == true && fuenf == true && sechs == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 40 (fester Punktwert für Große Straße)
                        lblGrosseStrasseS1.Text = "40";
                        // Deaktiviere den Button
                        btnWählenGrosseStrasseS1.Enabled = false;
                        // Addiere 40 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 40;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                    }
                    else
                    {
                        // Falls keine Große Straße vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Große Strasse vorhanden. Feld streichen?", "Grosse Strasse", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblGrosseStrasseS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenGrosseStrasseS1.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Größe Straße (Spieler 2)
        private void btnWählenGroßeStraßeS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Checkt welcher Spieler gerade dran ist, wenn Spieler 1 ist dann geht es weiter
                if (AktuellerSpieler == 2)
                {
                    // Bools um zu checken ob die Grosse Strasse erkannt ist oder nicht, wird am anfang auf false gesetzt.
                    bool eins = false;
                    bool zwei = false;
                    bool drei = false;
                    bool vier = false;
                    bool fuenf = false;
                    bool sechs = false;
                    // for loop der jeden würfel einmal checkt ob es eine 1-6 ist
                    for (int i = 0; i < 6; i++)
                    {
                        // Checkt ob dice[i] eins ist wenn ja wird die Variable auf true gesetzt
                        if (dice[i] == 1)
                        {
                            eins = true;
                        }
                        // Checkt ob dice[i] zwei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 2)
                        {
                            zwei = true;
                        }
                        // Checkt ob dice[i] drei ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 3)
                        {
                            drei = true;
                        }
                        // Checkt ob dice[i] vier ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 4)
                        {
                            vier = true;
                        }
                        // Checkt ob dice[i] fuenf ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 5)
                        {
                            fuenf = true;
                        }
                        // Checkt ob dice[i] sechs ist wenn ja wird die Variable auf true gesetzt
                        else if (dice[i] == 6)
                        {
                            sechs = true;
                        }
                    }
                    // Prüfe ob die Große Straße 1-2-3-4-5 vorhanden ist
                    if (eins == true && zwei == true && drei == true && vier == true && fuenf == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 40 (fester Punktwert für Große Straße)
                        lblGrosseStrasseS2.Text = "40";
                        // Deaktiviere den Button
                        btnWählenGroßeStraßeS2.Enabled = false;
                        // Addiere 40 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 40;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 40 Punkte zum Gesamtpunktestand von Spieler 2
                        gesamts2 += 40;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    // Prüfe ob die Große Straße 2-3-4-5-6 vorhanden ist
                    else if (zwei == true && drei == true && vier == true && fuenf == true && sechs == true)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 40 (fester Punktwert für Große Straße)
                        lblGrosseStrasseS2.Text = "40";
                        // Deaktiviere den Button
                        btnWählenGroßeStraßeS2.Enabled = false;
                        // Addiere 40 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 40;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 40 Punkte zum Gesamtpunktestand von Spieler 2
                        gesamts2 += 40;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls keine Große Straße vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Keine Große Strasse vorhanden. Feld streichen?", "Große Strasse", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblGrosseStrasseS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenGroßeStraßeS2.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Kniffel (Spieler 1)
        private void btnWählenKniffelS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 1 ist
                if (AktuellerSpieler == 1)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Kniffel (5 gleiche) vorhanden ist
                    bool hatKniffel = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 5x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 5x vorkommt (alle 5 Würfel zeigen die gleiche Zahl)
                        if (counts[augenzahl] >= 5)
                        {
                            // Setze hatKniffel auf true
                            hatKniffel = true;
                            // Verlasse die Schleife da ein Kniffel gefunden wurde
                            break;
                        }
                    }

                    // Falls ein Kniffel vorhanden ist
                    if (hatKniffel)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 50 (fester Punktwert für Kniffel)
                        lblKniffelS1.Text = "50";
                        // Deaktiviere den Button
                        btnWählenKniffelS1.Enabled = false;
                        // Addiere 50 Punkte zum unteren Block von Spieler 1
                        untererblocks1 += 50;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere 50 Punkte zum Gesamtpunktestand von Spieler 1
                        gesamts1 += 50;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                    else
                    {
                        // Falls kein Kniffel vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Kein Kniffel möglich. Feld streichen?", "Kniffel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblKniffelS1.Text = "0";
                            // Deaktiviere den Button
                            btnWählenKniffelS1.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Kniffel (Spieler 2)
        private void btnWählenKniffelS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                // Falls der Aktuelle Spieler = 2 ist
                if (AktuellerSpieler == 2)
                {
                    // Array zum Zählen, wie oft jede Augenzahl (1-6) vorkommt
                    int[] counts = new int[7];
                    // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0
                    int summe = 0;
                    // Boolean-Variabel um zu speichern ob ein Kniffel (5 gleiche) vorhanden ist
                    bool hatKniffel = false;

                    // Wir gehen durch dice[1] bis dice[5]
                    for (int i = 1; i <= 5; i++)
                    {
                        // Speichere den Würfelwert in einer lokalen Variable
                        int wert = dice[i];
                        // Falls der Wert zwischen 1 und 6 liegt (gültige Augenzahl)
                        if (wert >= 1 && wert <= 6)
                        {
                            // Erhöhe den Zähler für diese Augenzahl um 1
                            counts[wert]++;
                            // Addiere den Wert zur Summe
                            summe += wert;
                        }
                    }

                    // Prüfen, ob eine Zahl 5x oder öfter vorkommt
                    for (int augenzahl = 1; augenzahl <= 6; augenzahl++)
                    {
                        // Falls diese Augenzahl mindestens 5x vorkommt (alle 5 Würfel zeigen die gleiche Zahl)
                        if (counts[augenzahl] >= 5)
                        {
                            // Setze hatKniffel auf true
                            hatKniffel = true;
                            // Verlasse die Schleife da ein Kniffel gefunden wurde
                            break;
                        }
                    }

                    // Falls ein Kniffel vorhanden ist
                    if (hatKniffel)
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        // Setze den Punktestandtext auf 50 (fester Punktwert für Kniffel)
                        lblKniffelS2.Text = "50";
                        // Deaktiviere den Button
                        btnWählenKniffelS2.Enabled = false;
                        // Addiere 50 Punkte zum unteren Block von Spieler 2
                        untererblocks2 += 50;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere 50 Punkte zum Gesamtpunktestand von Spieler 2
                        gesamts2 += 50;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                    else
                    {
                        // Falls kein Kniffel vorhanden ist, frage ob das Feld gestrichen werden soll
                        if (MessageBox.Show("Kein Kniffel möglich. Feld streichen?", "Kniffel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Setze den Status auf true, dass eine Kategorie gewählt wurde
                            gewählt = true;
                            // Setze den Punktestandtext auf 0
                            lblKniffelS2.Text = "0";
                            // Deaktiviere den Button
                            btnWählenKniffelS2.Enabled = false;
                        }
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Chance (Spieler 1)
        private void btnWählenChanceS1_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 1) // Falls der Aktuelle Spieler = 1 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        summe += dice[i]; // Addiere Augenzahl zu der Chance
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenChanceS1.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenEinserS1
                        lblChanceS1.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum unteren Block von Spieler 1
                        untererblocks1 += summe;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS1.Text = untererblocks1.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 1
                        gesamts1 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS1.Text = gesamts1.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

        //Würfel Logik für die Chance (Spieler 2)
        private void btnWählenChanceS2_Click(object sender, EventArgs e)
        {
            // Prüfe ob gewürfelt wurde und noch nichts gewählt wurde
            if (gewürfelt && !gewählt)
            {
                if (AktuellerSpieler == 2) // Falls der Aktuelle Spieler = 2 ist:
                {
                    int summe = 0; // Initialisiere Integer-Variabel "summe" für den Summenwert auf 0

                    for (int i = 0; i < 6; i++) // Setzt Lokale Variabel "i" gleich 0; falls Variabel "i" kleiner ist, als 6; addiere 1 auf Iterationsvariabel "i":
                    {
                        summe += dice[i]; // Addiere Augenzahl zu der Chance
                    }

                    if (summe != 0) // Falls Summenwert nicht 0 ist:
                    {
                        // Setze den Status auf true, dass eine Kategorie gewählt wurde
                        gewählt = true;
                        btnWählenChanceS2.Enabled = false; // Deaktiviert die Knopffunktion von btnWählenEinserS1
                        lblChanceS2.Text = summe.ToString(); // Ändere den Punktestandtext zu der Summe
                        // Addiere die Summe zum unteren Block von Spieler 2
                        untererblocks2 += summe;
                        // Aktualisiere die Anzeige des unteren Blocks auf der Benutzeroberfläche
                        lblUntererBlockS2.Text = untererblocks2.ToString();
                        // Addiere die Summe zum Gesamtpunktestand von Spieler 2
                        gesamts2 += summe;
                        // Aktualisiere die Gesamtpunkteanzeige auf der Benutzeroberfläche
                        lblGesamtS2.Text = gesamts2.ToString();
                    }
                }
            }
            else if (!gewürfelt)
            {
                // Falls noch nicht gewürfelt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast noch nicht gewürfelt. Das Wählen ist nicht möglich!",
                    "Vorgang nicht möglich!");
            }
            else
            {
                // Falls bereits gewählt wurde, zeige Fehlermeldung
                MessageBox.Show("Du hast schon gewählt. Klicke Zug um den Spieler zu wechseln.",
                    "Vorgang nicht möglich!");
            }
        }

    }
}