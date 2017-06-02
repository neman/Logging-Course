using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using ForExceptionSample;

namespace Enjoying.Logging.WindowsApp
{
    public partial class LoggingExampleForm : Form
    {
        public LoggingExampleForm()
        {
            InitializeComponent();
        }

        private void btnLogError_Click(object sender, EventArgs e)
        {
            var logger = ApplicationLogging.CreateLogger<LoggingExampleForm>();
            logger.LogError(new EventId(), new StackOverflowException("Exception with inner exception", new ArgumentNullException("Argument missing")), "Error from windows applicaiton");
                        
        }

        private void btnExample1_Click(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            string newButtonName = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("/course"))
                {
                    newButtonName = args[i + 1];
                }
            }
            btnExample1.Text = newButtonName;

            var punctuation = GetPunctuation();

        }

        private string GetPunctuation()
        {
            return GetExclamationPoint();
        }

        private string GetExclamationPoint()
        {
            return "!";
        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            throw new Exception("Exception with inner exception", new StackOverflowException("Inner second", new ArgumentNullException("not good")));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ReadKeysDemo().ReadKeys();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new NullExample().Create().GetFirst().GetSecond().GetThird();
        }
    }
}
