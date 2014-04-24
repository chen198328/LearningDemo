using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
namespace ThreadProject
{

    public partial class Form1 : Form
    {
        //保存文件名
        public List<string> FileNames = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|Endnote(*.ciw)|*.ciw";
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                OpenFileDialog openFileDialog = (OpenFileDialog)sender;
                FileNames = openFileDialog.FileNames.ToList<string>();
                StringBuilder filenames = new StringBuilder();
                foreach (var filename in openFileDialog.FileNames)
                {
                    filenames.AppendFormat("{0};", filename);
                }
                rtbLogList.AppendText(DateTime.Now.ToShortTimeString() + "总共打开:" + openFileDialog.FileNames.Length + "个文件\r\n");
                txtFileNames.Text = filenames.ToString().Trim(';');
                filenames.Clear();
            }
            else
            {
                FileNames.Clear();
                txtFileNames.Text = string.Empty;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
           // System.Threading.Tasks.Parallel.Invoke(Handles);
            Task task = new Task(() => {
                Handles();
            });
            task.Start();
        }
        private void Handles() {
            int TaskCount = 4;

            //文件名分配
            List<string>[] filenameList = new List<string>[TaskCount];
            for (int i = 0; i < filenameList.Length; i++)
            {
                filenameList[i] = new List<string>();
            }
            for (int i = 0; i < FileNames.Count; i++)
            {
                switch (i % TaskCount)
                {
                    case 0:
                        filenameList[0].Add(FileNames[i]);
                        break;
                    case 1:
                        filenameList[1].Add(FileNames[i]);
                        break;
                    case 2:
                        filenameList[2].Add(FileNames[i]);
                        break;
                    case 3:
                        filenameList[3].Add(FileNames[i]);
                        break;
                    default:
                        break;

                }
            }
            Task<int>[] tasks = new Task<int>[TaskCount];
            tasks[0] = Task.Factory.StartNew<int>(() =>
            {
                return ImportFiles(filenameList[0]);
            });
            tasks[1] = Task.Factory.StartNew<int>(() =>
            {
                return ImportFiles(filenameList[1]);
            });
            tasks[2] = Task.Factory.StartNew<int>(() =>
            {
                return ImportFiles(filenameList[2]);
            });
            tasks[3] = Task.Factory.StartNew<int>(() =>
            {
                return ImportFiles(filenameList[3]);
            });
            //for (int i = 0; i < TaskCount; i++)
            //{
            //    tasks[i] = new Task<int>(() =>
            //    {
            //        return ImportFiles(filenameList[i]);
            //    });
            //}

            try
            {
                Task.WaitAll(tasks, 10000);
                int totalCount = 0;
                for (int i = 0; i < tasks.Length; i++)
                {
                    totalCount += tasks[i].Result;
                }
                rtbLogList.AppendText(DateTime.Now + "总共导入数据" + totalCount + "条");
                MessageBox.Show("wancheng");
            }
            catch (AggregateException ex)
            {
                rtbLogList.AppendText("超时");
            }
        }
        object obj = new object();
        /// <summary>
        /// 导入单个文件到数据库
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>返回导入数据库的记录数</returns>
        private int ImportFile(string filename)
        {
           // Thread.Sleep(1000);

            using (StreamReader reader = new StreamReader(filename, Encoding.Default)) {
                reader.ReadLine();
                reader.Close();
            }
            
            lock (obj)
            {
                rtbLogList.AppendText(DateTime.Now.ToShortTimeString() + filename + "\r\n");
            }
            return 500;
        }

        private int ImportFiles(List<string> filenames)
        {
            int filecount = 0;
            filenames.ForEach(p =>
            {
                ImportFile(p);
                filecount++;
            });
            return filecount;
        }
        delegate void AddTextHandler(string message);
        private void AddText(string message)
        {
            if (rtbLogList.InvokeRequired)
            {
                AddTextHandler handler = new AddTextHandler(AddText);
                rtbLogList.Invoke(handler, new object[] { message });
            }
            else
            {
                rtbLogList.AppendText(DateTime.Now.ToShortTimeString() + message + "\r\n");
            }
        }
        private void AddMessage(string message)
        {
            AddText(message);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

    }
}
