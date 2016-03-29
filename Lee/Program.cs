using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Diagnostics;

/*
 * "Lee" Joke Program
 * 
 * Written by thegreatrazz
 * Based on mikey's lolo.vbs
 * 
 * Description:
 *     Plays troll song, opens a Notepad window, types text and queries a system restart
 *     
 * ~~ Please use this responsibly ~~
 * 
 */

namespace Lee
{
    class Program
    {

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">Program arguments</param>
        static void Main(string[] args)
        {
            #region intro

            // Introduce yourself to the debugger
            Console.WriteLine("Hello debugger!");
            Console.WriteLine("'Lee' Joke program. Rewritten by thegreatrazz, based on mikey's lolo.vbs");
            Console.WriteLine();
            Console.WriteLine("You can always revoke the shutdown after query by typing 'shutdown -a' in a run box.");

            // Parameters
            // Timing - in milliseconds
            int waitsong      = 1000; // Wait after song finished
            int waitnotepad   = 1000; // Wait after Notepad launches
            int keypresstime  = 100;  // Time between keypresses

            // Shutdown variables
            string shutdownmode = "r"; // Shutdown mode
            int shutdowntime    = 120; // Time before shutdown - in seconds
            int sdscaretime     = 15;  // Time for shutdown scare

            #endregion

            #region init

            // Play the Troll song
            SoundPlayer trololo = new SoundPlayer(Lee.Properties.Resources.song); // Get Resource
            trololo.Play();         // Play it
            Thread.Sleep(waitsong); // Wait

            #endregion

            #region message1

            // Launch Notepad
            Process.Start("notepad.exe"); 
            Thread.Sleep(waitnotepad); // And wait

            // Type out first message
            string prestring = "Shutting down in: ";        // Preload a variable
            for (int i = 0; i < prestring.Length; i++)      // For each character
            {
                SendKeys.SendWait(prestring[i].ToString()); // Send to present app (hopefully Notepad)
                Thread.Sleep(keypresstime);                 // Wait
            }

            #endregion

            #region scareCount
            
            // Do the scare
            for (int i = sdscaretime; i > 0; i--)             // For every second
            {
                SendKeys.SendWait(i.ToString());              // Send count to notepad
                Thread.Sleep(1000);                           // Wait a second
                for (int a = 0; a < i.ToString().Length; a++) // For each digit
                {
                    SendKeys.SendWait("{BACKSPACE}");         // Go back once
                }
            }

            #endregion

            #region message2
            
            // Type out second message
            string poststring =
                "You have " + shutdowntime.ToString() +
                " seconds to close all your open apps and files"; // Preload variable
            for (int i = 0; i < poststring.Length; i++)           // For each character
            {
                SendKeys.SendWait(poststring[i].ToString());      // Send to present app (hopefully Notepad)
                Thread.Sleep(keypresstime);                       // Wait
            }
            
            #endregion

            #region shutdown

            // Query shutdown
            var sdattr = "/" + shutdownmode + " /t " + shutdowntime.ToString() + " /f"; // Generate attributes
            System.Diagnostics.Process.Start("shutdown", sdattr);                       // Pass the command

            // Wait forever (program will be force-closed at shutdown)
            Thread.Sleep(Timeout.Infinite);

            #endregion

        }
    }
}
