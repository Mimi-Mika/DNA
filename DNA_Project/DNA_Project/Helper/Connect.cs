using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.Helper
{
    class Connect
    {

        /*
if (urlFile != null && urlFile.Text != "")
            {
                logServer.Text += "Envoie du fichier au serveur, patientez...";
                logServer.Text += Environment.NewLine;

                // MAP
                byte[] byteData;
        String[] lines = File.ReadAllLines(urlFile.Text);
        StringBuilder strToSend = new StringBuilder();

        int i = 0;
                while (lines.Count() != i + 1)
                {
                    if (lines.Count() >= 500)
                    {
                        for (int j = i; j<i + 500; j++)
                        {
                            strToSend.Append(lines[j]);
                        }
}
                    else
                    {
                        foreach (string line in lines)
                        {
                            strToSend.Append(line);
                        }

                    }

                    //Converte string to byte array
                    byteData = Encoding.UTF8.GetBytes(strToSend.ToString());
                    //Send byte array
                    // Convert the string data to byte data using ASCII encoding.
                    // Begin sending the data to the remote device.
                    socketConnection.BeginSend(byteData, 0, byteData.Length, 0,
                        new AsyncCallback(SendCallback), socketConnection);
                    sendBegin.WaitOne();

                    byte[] endFile = Encoding.UTF8.GetBytes("<EOF>");
socketConnection.BeginSend(endFile, 0, endFile.Length, 0,
                        new AsyncCallback(EndSendCallback), socketConnection);

                    sendDone.WaitOne();
                    logServer.Text += "Envoie de fichier terminé, Traitement en cours, patientez ...";
                    logServer.Text += Environment.NewLine;

                    // TODO REDUCE
                    //Receive resulte

                    //Reduce

                    // increment i + 500
                    i = i + 500;
                    strToSend.Clear();
                }
                // RECEIVE RESULT & COMPUTE

            }
            else
            {
                logServer.Text += "Fichier introuvable";
                logServer.Text += Environment.NewLine;
            }
    }

    */
    }
}
