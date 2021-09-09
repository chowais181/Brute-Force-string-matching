using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
//using System;
namespace algoassignment3
{

    public partial class Form1 : Form
    {
        public string[] lines;
       // public string[] outputdisplay = new string[10];
        public int i = 0, j = 0, k = 0; //i is file line no., j is line char no.
        public Form1()
        {
            InitializeComponent();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void loadfile(string filename)
        {
            try
            {
                var list = new List<string>();
                var fileStream = new FileStream(@filename, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                lines = list.ToArray();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //loadfile("Research#1.txt"); //to load a single file on startup

        }
        private string extractword(string line, int index)
        {

            string Sword = "";
            while (true)
            {
                if (index >= line.Length)
                    break;
                if (line[index] == ' ')
                {
                    //Sword += line[index];
                    break;
                }
                else
                {
                    Sword += line[index];
                    index++;
                }
            }
            return Sword;
        }
        
        private string extractwordnew(string line, int index)
        {
            string Sword = "";

            while (true)
            {
                if (index >= line.Length)
                    break;
                if (line[index] == ' ')
                {
                    //Sword += line[index];
                    break;
                }
                else
                {
                    Sword += line[index];
                    index++;
                }
            }
            return Sword;
        }

        char sr;
        string wordinfocus;
        int l = 1; //to maintain number of files searched
        bool wordfound = false, samefile = true, firsttime = true;
        string lowerc1, lowerc2;
        long totaltime = 0, totaltimeall = 0;
        private void button2_Click(object sender, EventArgs e) //cancel btn
        {
            textBox1.Text = "";
            i = j = k = 0;
            wordfound = false;
            wordinfocus = "";
            l = 1;
            firsttime = true;
            samefile = false;
        
            try
            {
                for (int s = 0; s < lines.Length; s++)
                {
                    lines[s] = " ";
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Error");
            }
            textBox1.Enabled = true;
            //MessageBox.Show("Cancelled", "");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
        }

        private bool searchinside(string gw, string sw)
        {
            int a = 0, count = 0 ;
            if (sw.Length < gw.Length)
            {
                while (a < gw.Length)
                {
                    count = 0;   
                    if (sw[0] == gw[a])
                    { 
                        for (int b = 0; b < sw.Length; b++)
                        {
                            if ((a+b) >= gw.Length) //here
                                return false;
                            if (gw[a + b] == sw[b])
                            {
                                count++;
                            }
                        }
                        if (count == sw.Length)
                        {
                            return true;
                        }
                    }

                    a++;
                }
                return false;
            }
            else
               return false;
        }

      

        string foundindex = "";
        private void skipword(string fw)
        {
            if ((j + (fw.Length + 1)) <= lines[i].Length)
            {
                j += (fw.Length + 1); //skipping the word
                                             // MessageBox.Show("Word not found (different length)");  //for debugging
                
                foundindex = "";
            }
            else
            {

                i++;
                j = 0;
              
                foundindex = "";
            }
        }



        // tosearch: the word we searched for
        // wholefile: if you want to search whole file at once without single word found prompts each time
        // allfiles: if you want to search all the files at once or be stuck at file number 1 only
        // fullword: if you want to search inside the word (terrible name)


        private void fullwordsearch1(string tosearch, bool wholefile, bool allfiles, bool fullword)
        {
            var watch = new System.Diagnostics.Stopwatch();
            totaltimeall = 0;
            foundindex = "";
            do
            {
                if (!samefile || firsttime)
                {
                    string fname = "Research#";
                    fname += l + ".txt";
                    if (l <= 10) //to not go over the limit of 10 files to check
                        loadfile(fname);
                    else
                        l = 1;
                }
                try
                {
                    
                    watch.Start();
                    firsttime = false;
                    // MessageBox.Show(Convert.ToString(lines.Length)); //for debugging
                    while (i != lines.Length && i <= lines.Length) //traverse lines
                    {
                        while (j != lines[i].Length && j <= lines[i].Length) //traverse inside lines
                        {
                            sr = lines[i][j];
                            if (sr == tosearch[0]) //if the first character of words match
                            {
                                wordinfocus = extractwordnew(lines[i], j); //wordinfocus has the word which is under consideration
                                                                           // lowerc1 = wordinfocus.ToLower();

                                //   MessageBox.Show("word in focus: " + wordinfocus); //Debugging purposes
                                if (tosearch.Length == wordinfocus.Length) //if the length isnt same then skip the word
                                {
                                    foundindex += Convert.ToString(i + 1) + " " + Convert.ToString(j + 1); //the index where the word is found
                                    
                                    if (wordinfocus == tosearch) //word found
                                    {
                                        if (!wholefile) //if search overall checkbox isnt checked
                                        {
                                           // watch.Stop();
                                            MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + wordinfocus + "\nfile number: " + l);
                                           // watch.Start();
                                            wordfound = true;
                                            k++; //increase searched word count of the file
                                            samefile = true;
                                            skipword(wordinfocus);
                                            return;
                                        }
                                        else //if overall search checkbox isnt checked
                                        {
                                            //wordfound was one of the worst boolean name i couldve used
                                            wordfound = false;
                                            k++;
                                            skipword(wordinfocus);
                                        }
                                    }
                                   

                                }
                                else //the length of the word wasnt same so skip the word
                                {
                                    if (fullword)
                                    {
                                        wordinfocus = extractwordnew(lines[i], j);
                                        if (searchinside(wordinfocus, tosearch)) //if the word exist inside the word
                                        {
                                            foundindex += i + " " + j;
                                            if (!wholefile) //if search overall checkbox isnt 
                                            {
                                                MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + wordinfocus + "\nfile number: " + l);
                                                wordfound = true;
                                                k++; //increase searched word count of the file
                                                samefile = true;
                                                skipword(wordinfocus);
                                                return;
                                            }
                                            else
                                            {
                                                skipword(wordinfocus);
                                                wordfound = false;
                                                k++;
                                            }
                                        }


                                    }
                                    else
                                    {
                                        //do nothing
                                    }

                                    skipword(wordinfocus);
                                }
                            }
                            else if (fullword)
                            {
                                wordinfocus = extractwordnew(lines[i], j);
                                if (searchinside(wordinfocus, tosearch)) //if the word exist inside the word
                                {
                                    foundindex += i + " " + j;
                                    if (!wholefile) //if search overall checkbox isnt 
                                    {
                                        MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + wordinfocus + "\nfile number: " + l);
                                        wordfound = true;
                                        k++; //increase searched word count of the file
                                        samefile = true;
                                        skipword(wordinfocus);
                                        return;
                                    }
                                    else
                                    {
                                        skipword(wordinfocus);
                                        wordfound = false;
                                        k++;
                                    }
                                }
                            }
                            wordinfocus = extractword(lines[i], j);
                            skipword(wordinfocus);
                        }
                        //MessageBox.Show("line " + i + " ended!"); //for debugging
                        j = 0;
                        i++;
                    }
                    watch.Stop();
                    totaltime += watch.ElapsedMilliseconds;
                }
                catch (Exception b)
                {
                    watch.Stop();
                    MessageBox.Show(b.Message + "\nfile number: " + l + " \nRow: " + i + " \nColoumn: " + j, "Error");
                    watch.Start();
                }
                watch.Stop();
                totaltime += watch.ElapsedMilliseconds;
                totaltimeall += totaltime;
                MessageBox.Show("Text File " + l + " Ended!\n" + k + " '" + tosearch + "' words found.  \n\nTotal time taken to search the file: " + totaltime + " milliseconds.", "Results"); //one file ended, reset all variables

                //MessageBox.Show("Text File " + l + " Ended!\n" + k + " '" + tosearch + "' words found.", "Results"); //one file ended, reset all variables
             
                foundindex = "";
                wordfound = false;
                samefile = false;
                if (allfiles)
                {
                    i = j = k = 0;
                    l++;
                    watch.Reset();
                    totaltime = 0;
                }
                else
                {
                    i = j = k = 0;
                    //samefile = true;
                }
                if (l > 10)
                {
                    //watch.Stop();
                    // totaltime += watch.ElapsedMilliseconds;
                    totaltimeall += totaltime;
                    MessageBox.Show("Total time taken to search all the files is: " + totaltimeall + " ms", "Time");
                }
            }
            //one text file ends here. here we can start searching the other file (the next one in order)
            while (allfiles && l <= 10);
        }

        private void fullwordsearch2(string tosearch, bool wholefile, bool allfiles, bool fullword, bool uplowcheck) //for upperlower cases
        {
            var watch = new System.Diagnostics.Stopwatch();
            totaltimeall = 0;
            
            string charup = "";
            foundindex ="";
            if (!watch.IsRunning)
                watch.Start();
            do
            {
                if (!samefile || firsttime)
                {
                    string fname = "Research#";
                    fname += l + ".txt";
                    if (l <= 10) //to not go over the limit of 10 files to check
                        loadfile(fname);
                    else
                        l = 1;
                }
                try
                {
                    if (allfiles)
                    {
                        totaltimeall += totaltime;
                        //totaltime = 0;

                    }
                    watch.Start();
                    firsttime = false;
                    // MessageBox.Show(Convert.ToString(lines.Length)); //for debugging
                    while (i != lines.Length && i <= lines.Length) //traverse lines
                    {
                       
                        while (j != lines[i].Length && j <= lines[i].Length) //traverse inside lines
                        {
                            charup = "";
                            sr = lines[i][j];
                            charup += sr;
                            charup = charup.ToLower();

                            if (charup[0] == tosearch.ToLower()[0]) //if the first character of words match
                            {
                                wordinfocus = extractwordnew(lines[i], j); //wordinfocus has the word which is under consideration
                                lowerc1 = wordinfocus.ToLower();
                                lowerc2 = tosearch.ToLower();
                                //   MessageBox.Show("word in focus: " + wordinfocus); //Debugging purposes
                                if (lowerc2.Length == lowerc1.Length) //if the length isnt same then skip the word
                                {
                                    foundindex += Convert.ToString(i + 1) + " " + Convert.ToString(j + 1); //the index where the word is found

                                    if (lowerc1 == lowerc2) //word found
                                    {
                                        if (!wholefile) //if search overall checkbox isnt checked
                                        {

                                            MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + wordinfocus + "\nfile number: " + l);
                                            wordfound = true;
                                            k++; //increase searched word count of the file
                                            samefile = true;
                                            skipword(wordinfocus);
                                            return;
                                        }
                                        else //if overall search checkbox isnt checked
                                        {
                                            //wordfound was one of the worst boolean name i couldve used
                                            wordfound = false;
                                            k++;
                                            skipword(wordinfocus);
                                        }
                                    }
                                }
                                else //the length of the word wasnt same so skip the word
                                {
                                    if (fullword)
                                    {
                                        wordinfocus = extractwordnew(lines[i], j);
                                        lowerc1 = wordinfocus.ToLower();
                                        lowerc2 = tosearch.ToLower();
                                        if (searchinside(lowerc1, lowerc2)) //if the word exist inside the word
                                        {
                                            foundindex += i + " " + j;
                                            if (!wholefile) //if search overall checkbox isnt 
                                            {
                                                MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + wordinfocus + "\nfile number: " + l);
                                                wordfound = true;
                                                k++; //increase searched word count of the file
                                                samefile = true;
                                                skipword(wordinfocus);
                                                return;
                                            }
                                            else
                                            {
                                                skipword(wordinfocus);
                                                wordfound = false;
                                                k++;
                                            }
                                        }


                                    }
                                    else
                                    {
                                        //do nothing
                                    }

                                    skipword(wordinfocus);
                                }
                            }
                            else if (fullword)
                            {
                                wordinfocus = extractwordnew(lines[i], j);
                                lowerc1 = wordinfocus.ToLower();
                                lowerc2 = tosearch.ToLower();
                                if (searchinside(lowerc1, lowerc2)) //if the word exist inside the word
                                {
                                    foundindex += i + " " + j;
                                    if (!wholefile) //if search overall checkbox isnt 
                                    {
                                        MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + wordinfocus + "\nfile number: " + l);
                                        wordfound = true;
                                        k++; //increase searched word count of the file
                                        samefile = true;
                                        skipword(wordinfocus);
                                        return;
                                    }
                                    else
                                    {
                                        skipword(wordinfocus);
                                        wordfound = false;
                                        k++;
                                    }
                                }
                            }
                            wordinfocus = extractword(lines[i], j);
                            skipword(wordinfocus);
                        }
                        //MessageBox.Show("line " + i + " ended!"); //for debugging
                        j = 0;
                        i++;
                       // watch.Stop();
                        //totaltime += watch.ElapsedMilliseconds;
                    }
                    watch.Stop();
                    totaltime += watch.ElapsedMilliseconds;
                }
                catch (Exception b)
                {
                    watch.Stop();
                    MessageBox.Show(b.Message + "\nfile number: " + l + " \nRow: " + i + " \nColoumn: " + j, "Error");
                    watch.Start();
                }
                watch.Stop();
                totaltime += watch.ElapsedMilliseconds;
                totaltimeall += totaltime;
                MessageBox.Show("Text File " + l + " Ended!\n" + k + " '" + tosearch + "' words found.  \n\nTotal time taken to search the file: " + totaltime + " milliseconds.", "Results"); //one file ended, reset all variables
                
                foundindex = "";
                wordfound = false;
                samefile = false;
                if (allfiles)
                {
                    i = j = k = 0;
                    l++;
                    watch.Reset();
                    totaltime = 0;
                }
                else
                {
                    i = j = k = 0;
                    //samefile = true;
                }
                if (l > 10)
                {
                    //watch.Stop();
                    // totaltime += watch.ElapsedMilliseconds;
                    totaltimeall += totaltime;
                    MessageBox.Show("Total time taken to search all the files is: " + totaltimeall + " ms", "Time");
                }
            }
            //one text file ends here. here we can start searching the other file (the next one in order)
            while (allfiles && l <= 10);
        }

        
        string foundword = "";
        private void upperlowerscene(string tosearch, bool wholefile, bool allfiles)
        {
           // var watch = new System.Diagnostics.Stopwatch();
            Stopwatch watch = new Stopwatch();
            totaltime = 0;
            totaltimeall = 0;
           
            string charup = "";
            string foundindex = "";
            do
            {
               
                if (!samefile || firsttime)
                {
                    string fname = "Research#";
                    fname += l + ".txt";
                    if (l <= 10) //to not go over the limit of 10 files to check
                        loadfile(fname);
                    else
                    {
                        l = 1;
                        //totaltimeall = 0;
                    }
                }
                try
                {
                    if (allfiles)
                    {
                       totaltimeall += totaltime;
                        //totaltime = 0;
                       
                    }
                    if (!watch.IsRunning) // checks if it is not running
                        watch.Start();
                    firsttime = false;
                    // MessageBox.Show(Convert.ToString(lines.Length)); //for debugging
                    // Start the counter from where it stopped
                    while (i != lines.Length && i <= lines.Length) //traverse lines
                    {
                        while (j != lines[i].Length && j <= lines[i].Length) //traverse inside lines
                        {
                            charup = "";
                            sr = lines[i][j];
                            charup += sr;
                            charup = charup.ToLower();
                            lowerc2 = tosearch.ToLower();
                            //both first letters are lowercase now
                            if (charup[0] == lowerc2[0]) 
                            {
                                wordinfocus = extractword(lines[i], j); //wordinfocus has the word which is under consideration
                                lowerc1 = wordinfocus.ToLower();

                                //   MessageBox.Show("word in focus: " + wordinfocus); //Debugging purposes
                                if (lowerc2.Length == lowerc1.Length) //if the length isnt same then skip the word
                                {
                                    foundindex += Convert.ToString(i + 1) + " " + Convert.ToString(j + 1); //the index where the word is found
                                    for (int a = 0; a < tosearch.Length; a++)
                                    {
                                        foundword += lines[i][j]; //storing the searched word
                                        j++;
                                    }
                                    if (foundword.ToLower() == lowerc2) //word found
                                    {
                                        if (!wholefile) //if search overall checkbox isnt checked
                                        {
                                            //watch.Stop();
                                            //totaltime += watch.ElapsedMilliseconds;
                                            MessageBox.Show("Word Found! \nIndexes: " + foundindex + " \nWord: " + foundword + "\nfile number: " + l);
                                           // watch.Start();
                                            wordfound = true;
                                            k++; //increase searched word count of the file
                                            samefile = true; //to remain on the same file
                                                             // totaltime += watch.ElapsedMilliseconds;
                                                             //return;
                                            return;
                                        }
                                        else //if overall search checkbox isnt checked
                                        {
                                            
                                            //wordfound was one of the worst boolean name i couldve used
                                            wordfound = false;
                                            k++;
                                        }
                                        foundword = "";
                                        foundindex = "";
                                    }
                                    else //if word didnt match e.g for and foe
                                    {
                                        // MessageBox.Show("Word not found (didnt match)!");

                                        foundword = "";
                                        foundindex = "";
                                    }
                                }
                                else //the length of the word wasnt same so skip the word
                                {
                                    if ((j + wordinfocus.Length) <= lines[i].Length)
                                    {
                                        j += wordinfocus.Length; //skipping the word
                                                                 // MessageBox.Show("Word not found (different length)");  //for debugging
                                        foundword = "";
                                        foundindex = "";
                                    }
                                    else
                                    {
                                        i++;
                                        foundword = "";
                                        foundindex = "";
                                    }
                                }
                            }

                            j++;
                            
                        }
                        //MessageBox.Show("line " + i + " ended!"); //for debugging
                        j = 0;
                        i++;
                        //watch.Stop();
                        //totaltime += watch.ElapsedMilliseconds;
                    }
                    watch.Stop();
                    totaltime += watch.ElapsedMilliseconds;
                }
                catch (Exception b)
                {
                    watch.Stop();
                    MessageBox.Show(b.Message);
                    watch.Start();
                }
                watch.Stop();
                totaltime += watch.ElapsedMilliseconds;
                totaltimeall += totaltime;
                //outputdisplay[l] = "Text File " + l + "\n" + k + " '" + tosearch + "' words found.";
                MessageBox.Show("Text File " + l + " Ended!\n" + k + " '" + tosearch + "' words found. \n\nTotal time taken to search the file: " + totaltime + " milliseconds." , "Results"); //one file ended, reset all variables
                //watch.Start();
                foundword = "";
                foundindex = "";
                wordfound = false;
                samefile = false;
                if (allfiles)
                {
                    i = j = k = 0;
                    l++;
                    watch.Reset();
                    totaltime = 0;
                }
                else
                {
                    //watch.Stop();
                    //MessageBox
                    i = j = k = 0;
                    //samefile = true;
                }
                if (l > 10)
                {
                    watch.Stop();
                    totaltime += watch.ElapsedMilliseconds;
                    totaltimeall += totaltime;
                   MessageBox.Show("Total time taken to search all the files is: " + totaltimeall + " ms", "Time");
                }
            }
            //one text file ends here. here we can start searching the other file (the next one in order)
            while (allfiles && l <= 10);
           
        }


        

        private void button1_Click(object sender, EventArgs e) //find btn
        {
            var watch = new System.Diagnostics.Stopwatch();
           
            if (textBox1.Text != "")
            {
                textBox1.Enabled = false;
                if (checkBox1.Checked && checkBox2.Checked) //complete word and case sensitive
                {
                    if (checkBox3.Checked)
                    {
                        if (checkBox4.Checked)
                        {
                            watch.Start();
                            fullwordsearch1(textBox1.Text, true, true, false);
                            //wordsearchperline(textBox1.Text, true, true); //all options
                            watch.Stop();
                            // MessageBox.Show("time taken = " + watch.ElapsedMilliseconds);
                        }
                        else
                        {
                            watch.Start();
                            fullwordsearch1(textBox1.Text, true, false, false);
                            watch.Stop();
                            //  MessageBox.Show("time taken = " + watch.ElapsedMilliseconds);
                        }
                    }
                    else
                    {
                        if (checkBox4.Checked)
                            fullwordsearch1(textBox1.Text, false, true, false);
                        else
                            fullwordsearch1(textBox1.Text, false, false, false);
                    }
                }
                else if (checkBox1.Checked && !checkBox2.Checked) //full word but not case match
                {
                    if (checkBox3.Checked)
                    {
                        if (checkBox4.Checked)
                        {
                            watch.Start();
                            upperlowerscene(textBox1.Text, true, true); //all options
                            watch.Stop();
                            //MessageBox.Show("time taken = " + watch.ElapsedMilliseconds);
                        }
                        else
                            upperlowerscene(textBox1.Text, true, false);
                    }
                    else
                    {
                        if (checkBox4.Checked)
                            upperlowerscene(textBox1.Text, false, true);
                        else
                            upperlowerscene(textBox1.Text, false, false);
                    }
                }
                else if (!checkBox1.Checked && checkBox2.Checked) //not full word and macth case sensitive
                {
                    if (checkBox3.Checked)
                    {
                        if (checkBox4.Checked)
                            fullwordsearch1(textBox1.Text, true, true, true); //all options
                        else
                            fullwordsearch1(textBox1.Text, true, false, true);
                    }
                    else
                    {
                        if (checkBox4.Checked)
                            fullwordsearch1(textBox1.Text, false, true, true);
                        else
                            fullwordsearch1(textBox1.Text, false, false, true);
                    }
                }
                else if (!checkBox1.Checked && !checkBox2.Checked) //not full word and not case sensitive
                {
                    if (checkBox3.Checked)
                    {
                        if (checkBox4.Checked)
                            fullwordsearch2(textBox1.Text, true, true, true, true); //all options
                        else
                            fullwordsearch2(textBox1.Text, true, false, true, true);
                    }
                    else
                    {
                        if (checkBox4.Checked)
                            fullwordsearch2(textBox1.Text, false, true, true, true);
                        else
                            fullwordsearch2(textBox1.Text, false, false, true, true);
                    }
                }

                else
                    MessageBox.Show("Current settings not currently implemented!");
            }
            else
                MessageBox.Show("Search field can't be empty! \nCheck input.");


            
        } 
    }
}
